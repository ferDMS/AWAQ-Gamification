import csv
import mysql.connector
import random
import json

# Function to parse CSV and return data
def parse_csv(file_path, num_rows=20):
    data = []
    with open(file_path, 'r', encoding='utf-8') as file:
        csv_reader = csv.reader(file)
        next(csv_reader)  # Skip the header row
        rows = list(csv_reader)
        random.shuffle(rows)  # Shuffle the rows randomly
        for idx, row in enumerate(rows):
            if idx >= num_rows:
                break
            print(row)
            data.append(row)
    return data

# Function to insert data into the database
def insert_data(data, connection):
    cursor = connection.cursor()
    for row in data:
        print(f"Inserted {row[5]} {row[4]}")
        insert_query = """INSERT INTO usuarios (nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word, lastLogin)
                          VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s)"""
        cursor.execute(insert_query, (row[5], row[4], row[6], row[1], row[2], row[3], row[7], row[9], row[8]))
    connection.commit()

try:
    # Database connection credentials
    # credentials.json is a file with private information which should be
    # created based on the credentials_t.json file
    with open('credentials.json') as json_file:
        config = json.load(json_file)['local']

    # Parse CSV file
    file_path = 'people.csv'
    num_people = 20  # Number of people to insert
    data = parse_csv(file_path, num_people)

    # Connect to the MySQL database
    connection = mysql.connector.connect(**config)

    # Insert data into the database
    insert_data(data, connection)

    print("Data inserted successfully!")

except mysql.connector.Error as error:
    print("Error:", error)

finally:
    if 'connection' in locals() and connection.is_connected():
        connection.close()
