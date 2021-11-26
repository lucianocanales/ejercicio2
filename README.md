# Proyecto de evaluación C&S

## Puesta en marcha

1. Descargue el repositorio
2. Ejecute el archivo que se encuentra en la carpeta bin\publish llamado ejercicio2.exe.
3. Aguarde a que el sistema cargue los datos necesarios para esto debe tener acceso a internet.
4. Escriba la ruta donde quiere que el programa guarde la ruta Ejemplo: D:\Usuarios\Lucho\Desktop\.net\test esta ruta debe existir o dará error y se cerrará la carga.

## Parte de la evaluación

- Crear otro proyecto (usando framework .Net 4.7 o .Net Core 2.2 en adelante) que al ser ejecutado consuma los datos de los siguientes endpoints:
  1. https://api.mercadolibre.com/currencies/
  2. https://api.mercadolibre.com/currency_conversions/search?from=XXX&to=USD
  
- La idea es que se almacene en disco un json con la estructura que devuelve el endpoint “currencies” pero que adicionalmente incluya una nueva property “todolar” con el resultado del segundo endpoint. El endpoint “currency_conversions” toma como parámetro en “from” el id de moneda correspondiente a un país (que devuelve el primer endpoint “currencies”).
- Adicionalmente la misma aplicación tiene que almacenar en disco un archivo csv con cada uno de los resultados obtenidos de “currency_conversions”, es decir debe almacenar sólo los resultados obtenidos de la property “ratio” (Ej: 0.0147275,0.013651,0.727565).
  - Para resolver el problema se creo 2 objetos:
    - Uno con los datos de https://api.mercadolibre.com/currency_conversions/search?from=XXX&to=USD.
    - Otro con los datos https://api.mercadolibre.com/currencies/ conbinado con el objeto antes mencionado.
  - Para la construcción del json solicitado se usó un paquete descargado de nuget llamado Newtonsoft.Json y luego se exportó en un archivo de nombre "currencies.json" en la dirección escrita por el usuario.
  - Para la construcción del archivo CSV se usó un paquete llamado CsvHelper usando los datos del primer objeto nombrado, y exportando se en un archivo llamado "toDolar.csv".
