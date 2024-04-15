# For web scraping of names
import requests
import re
from bs4 import BeautifulSoup
from unidecode import unidecode

# For random generation of dates
import random
import time

# To perform mathematical operations and create dataframes
import pandas as pd
import numpy as np
from sklearn.preprocessing import minmax_scale

# To connect to the database and obtain information to generate progress
import json
import mysql.connector


# Function to extract information from wikipedia Colombian cities website
def getColombianCities():
    # Get HTML document
    url = "https://en.wikipedia.org/wiki/List_of_cities_in_Colombia_by_population"
    response = requests.get(url)
    # Parse HTML and get the table
    soup = BeautifulSoup(response.text, 'html.parser')
    table_section = soup.find('span', string='List', attrs={'class': 'mw-headline'}).find_parent('div')
    table = table_section.find('table')
    # Extract information from the table
    cities = []
    for row in table.find_all('tr')[1:]:
        cells = row.find_all('td')
        city = cells[1].text
        population_str = cells[3].text.strip()
        population = int(''.join(population_str.split(',')))
        cities.append((city, population))
    print("Successfully retrieved cities")
    return cities


# Function to extract information from blog post about colombian names
def getColombianNames():
    # Get HTML document
    url = "https://espanol.babycenter.com/colombia"
    response = requests.get(url)
    # Parse HTML and get lists for boy and girl names
    soup = BeautifulSoup(response.text, 'html.parser')
    # Parse girl names
    names_list = soup.find('ol', attrs={'class':None})
    names = {'feminine' : [], 'masculine' : []}
    for name in names_list.find_all('li', limit=75):
        name_tag = name.find('a')
        names['feminine'].append(name_tag.text)
    # Parse boy names
    names_list = names_list.find_next('ol', attrs={'class':None})
    for name in names_list.find_all('li', limit=75):
        name_tag = name.find('a')
        names['masculine'].append(name_tag.text)
    print("Successfully retrieved names")
    return names


# Function to extract information from blog post about colombian surnames
def getColombianSurnames():
    # Get HTML document
    url = "https://medicoplus.com/ciencia/apellidos-mas-comunes-colombia"
    response = requests.get(url)
    # Parse HTML and get structure holding surnames
    soup = BeautifulSoup(response.text, 'html.parser')
    # Find heading of list, then the list items and parse them
    surnames = []
    heading = soup.find('h2')
    for surname in heading.find_all_next('h3', limit=100):
        # Use regular expressions to extract the surname from (surname.text)
        surname_extract = re.search(r'\d+\.\s*(.+)\s*', surname.text).group(1)
        surnames.append(surname_extract)
    print("Successfully retrieved surnames")
    return surnames


# Function to generate n random cities from cities list
# The odds are defined by the population of the city
def generateRandomCities(cities_list, n):
    # Get populations from all cities and normalize to probability distribution
    populations = np.array([city[1] for city in cities_list])
    total_population = populations.sum()
    p = populations / total_population
    # Get city names from all cities
    cities = [city[0] for city in cities_list]
    # Select n cities by random, using populations as weight
    selection = np.random.choice(cities, n, p=p)
    print("Successfully generated cities")
    return selection


# Function to generate n random names from names list
def generateRandomNames(names_list, n):
    # Generate n random names
    names = []
    for _ in range(n):
        names.append(random.choice(names_list))
    print("Successfully generated names")
    return names


# Function to generate n random surnames from names list
def generateRandomSurnames(surnames_list, n):
    # Generate n random surnames
    surnames = []
    for _ in range(n):
        surnames.append(random.choice(surnames_list))
    print("Successfully generated surnames")
    return surnames


# Function to generate n random emails from names
def generateRandomEmails(names, domain):
    # Generate n random emails
    emails = []
    for i in range(len(names)):
        # Grab only first name
        first_name = names[i].split(' ')[0]
        # Change names to lowercase and normalize accents
        name = unidecode(first_name.lower())
        emails.append(f"{name}{i}{domain}")
    print("Successfully generated emails")
    return emails


# Function to generate n random birthdates from range of dates
# The dates must be provided in the YYYY-MM-DD format
def generateRandomDOB(start, end, n):
    # Define range dates in date data type
    time_format = r'%Y-%m-%d'
    stime = time.mktime(time.strptime(start, time_format))
    etime = time.mktime(time.strptime(end, time_format))
    # Generate n random dates of birth
    dob = []
    for _ in range(n):
        # Generate a random proportion from 0 to 1
        prop = random.random()
        # Get random date, casting dates as UNIX time for calculations
        rand_time = stime + prop * (etime - stime)
        # Convert UNIX time to date type and append
        dob.append(time.strftime(time_format, time.localtime(rand_time)))
    print("Successfully generated dates of birth")
    return dob


# Function to generate n random last time connected to website (using range of last 7 days)
def generateRandomLastConnected(n):
    # Get time now (end of range), in UNIX
    etime = time.time()
    # Get time 7 days ago (start of range), in UNIX
    # We subtract the number of seconds in 7 days
    stime = etime - 7*24*60*60
    # Generate n random dates of last connection
    last_connected = []
    for _ in range(n):
        # Generate a random proportion from 0 to 1
        prop = random.random()
        # Get random date, casting dates as UNIX time for calculations
        rand_time = stime + prop * (etime - stime)
        # Convert UNIX time to datetime type and append
        last_connected.append(time.strftime('%Y-%m-%d %H:%M:%S', time.localtime(rand_time)))
    print("Successfully generated last connected dates")
    return last_connected


def generatePeople(n):
    domain = "@awaq.org"

    # Declare a new pandas dataframe to save the information
    df = pd.DataFrame()

    # Generate personal information
    df['pais'] = ['Colombia'] * n
    cities_list = getColombianCities()
    df['ciudad'] = generateRandomCities(cities_list, n)
    df['fechaNacimiento'] = generateRandomDOB('1970-01-01', '2000-01-01', n)
    surnames_list = getColombianSurnames()
    df['apellido'] = generateRandomSurnames(surnames_list, n)

    # Generate names
    names_dict = getColombianNames()
    fem_names_list = names_dict['feminine']
    masc_names_list = names_dict['masculine']
    half = n//2+n%2
    df['nombre'] = None
    df['genero'] = None
    df['nombre'].iloc[:half] = generateRandomNames(fem_names_list, half)
    df['genero'].iloc[:half] = 'M'
    df['nombre'].iloc[half:] = generateRandomNames(masc_names_list, half-n%2)
    df['genero'].iloc[half:] = 'H'

    df['correo'] = generateRandomEmails(df['nombre'].to_list(), domain)
    df['lastLogin'] = generateRandomLastConnected(n)

    # Generate passwords as simple digits for each person
    df['pass_word'] = [i for i in range(n)]

    print("\nSuccesfully generated all data")
    return df


# Function to perform a query to the specified MySql database
# The connection details are inside of a JSON file of name `file_name`
# Inside the JSON file, you can include multiple different connections which are labelled
def queryDB(query, file_name="credentials.json", connection="cloud"):
    # Import connection details using the specified connection (cloud or local)
    with open(file_name) as json_file:
        config = json.load(json_file)[connection]
    # Establish connection
    try:
        connection = mysql.connector.connect(**config)
        if connection.is_connected():
            cursor = connection.cursor()
            cursor.execute(query)
            records = cursor.fetchall()
            return records
    # If something failed, display the error
    except mysql.connector.Error as e:
        print("Error while connecting to MySQL", e)
    # Always close the connection before returning anything
    finally:
        # If the connection was opened succesfully, close it
        if 'connection' in locals() and connection.is_connected():
            cursor.close()
            connection.close()


# Function to distribute each of the n people a progress between 0% and 100%.
# Based on a sigmoid + logarithmic function
def distributeXP(n):
    x1 = np.linspace(-35, 15, n)
    x2 = np.linspace(0.0001, 0.25, n)
    f_sigmoid = (1 / (1 + np.exp(-0.5*x1)))
    f_log = -0.04*np.log(1/x2)
    f_x = f_sigmoid + f_log
    # Normalize the resultant function to be between 0 and 1. This is the final progress percentage result for every person prog_perc[i]
    prog_perc = minmax_scale(f_x)
    # Get the total xp for each person based on the progress percentage
    rows = queryDB(connection="local", query=
        """
        SELECT
            xp.xp_desbloqueo + xp.xp_exito
        FROM
            fuentes_xp xp
        WHERE
            xp.xp_desbloqueo = (SELECT MAX(fuentes_xp.xp_desbloqueo) FROM fuentes_xp)
        """
    )
    completion_xp = rows[0]
    prog_xp = np.round(prog_perc * completion_xp)
    return prog_xp


# Function to generate the progress of n number of people
# Completely based off the selected db current information
def generateProgress(n):
    prog_xp = distributeXP(n)
    print(prog_xp)


def generateSessions(n):
    pass