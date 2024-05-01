# Instrucciones de API

## Setup

Para que se pueda hostear el API localmente en su máquina:

1. Ir al archivo `launchSettings.json` ubicado en `API/ApiQuieroSerBiomonitor/Properties/`

2. Si se quiere hacer un deploy a un iPad del videojuego y queremos que se conecte a esta API, se debe de cambiar la IP sobre la cual se hostea la API.
   
   1. Para hacer eso, se deben de cambiar todos las entradas que digan `localhost` a la IP de tu computadora en tu red local. Para hacer este paso de manera más sencilla se puede hacer `Ctrl+F` (ó `Cmd+F` para Mac) y reemplazar `localhost`

3. Si se quiere hostear la API de manera completamente aislada en su computadora, se puede dejar `localhost` así como está.