# IRS Platform API

## Requisitos Previos

1. **.NET SDK**: Asegúrate de tener instalado el SDK de .NET.
2. **SQL Server**: Debes tener una instancia de SQL Server en tu máquina local.
3. **Base de Datos**: Crea una base de datos llamada `IRSDB` en tu SQL Server local.
4. **Configuración de Usuario y Contraseña**: Asegúrate de tener un usuario y contraseña configurados en tu SQL Server.

## Configuración

1. **Archivo `appsettings.json`**: Crea o edita el archivo `appsettings.json` en la raíz del proyecto con el siguiente contenido. Asegúrate de cambiar los valores de `User Id` y `Password` según tu configuración local.
2.  Dentro de esto, solo cambia el User Id y Password por los que tengas en tu SQL Server, además debe ser dentro del "ConnectionString" de "DefaultConnection".

    ```json
    {
      "TokenSettings": {
        "Secret": "Place here your secret for token generation"
      },
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=BaseDeDatosPruebaTecnica;User Id=sa;Password=123;TrustServerCertificate=True;",
        "ProdConnection": "Server=tcp:eyserver.database.windows.net,1433;Initial Catalog=SampleDatabase;Persist Security Info=False;User ID=test123Q;Password=test123Q;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*"
    }
    ```

## Ejecución Local

1**Tener los requisitos previos configurados**.
2**Ejecutar en IIS Express**: Abre el proyecto en tu IDE (JetBrains Rider o VSCode2022) y selecciona el perfil de IIS Express. Luego, ejecuta la aplicación.

## Notas

- Asegúrate de que los puertos configurados en `launchSettings.json` no estén en uso por otras aplicaciones.
- Si encuentras problemas de conexión con la base de datos, verifica que los valores de `User Id` y `Password` en `appsettings.json` sean correctos y que el usuario tenga los permisos necesarios en SQL Server.