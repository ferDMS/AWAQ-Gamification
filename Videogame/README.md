# Instrucciones de juego

## Setup API

Para poder hostear la API en su máquina seguir paso a paso las instrucciones `README.md` dentro de `~/API`.

## Setup Juego

Para que se pueda conectar al API localmente en su máquina:

1. Abrir la solución de Visual Studio ubicada en `Juego-Biomonitor/Juego-Biomonitor.sln` con la cual se puede navegar a través de los scripts del juego.

2. Ir al archivo `APIManager.cs` en `Assets/Scripts/APIScripts/`

3. Reemplazar en la línea 13 el valor de `baseUrl` para que coincida con la IP donde se está hosteando la API. La API se puede hostear en su computadora de manera aislada ó en la IP de su red local. Dentro de la URL encontrará el valor `10.22.238.66`. Este valor se debe reemplazar a la IP donde se está hosteando la API. (e.g.: `localhost` ó IP local).

4. Hacer el mismo paso 3. pero ahora en la línea 16 dentro de `LoginManager.cs`, que está ubicado en `Assets/Scripts/APIScripts/`.

5. Confirmar que compile el juego y regresar a Unity

## Instrucciones de juego

1. Para empezar el juego, se debe de abrir la primera escena, la cual es `PantallaInicio`, y dar play!

2. En la pantalla inicial, dar continuar / empezar para avanzar a la ventana de login.

3. En la pantalla de login se pueden ingresar alguna cualquiera de las siguientes credenciales de juego completamente vacías de ejemplo:

```
USUARIO            CONTRASEÑA
angel152@awaq.org    152
miguel103@awaq.org    103
eimy57@awaq.org    57
martin172@awaq.org    172
alaia83@awaq.org    83
abril31@awaq.org    31
annie54@awaq.org    54
dulce20@awaq.org    20
luna25@awaq.org    25
lorenzo179@awaq.org    179
ethan110@awaq.org    110
milagros55@awaq.org    55
alaia73@awaq.org    73
jacob131@awaq.org    131
paulina98@awaq.org    98
emanuel126@awaq.org    126
angel181@awaq.org    181
evangeline33@awaq.org    33
sofia44@awaq.org    44
felipe134@awaq.org    134
```
