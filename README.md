**Requisitos Previos**

Base de Datos:

Asegúrate de tener cargada la base de datos BdPrueba.sql en tu instancia de SQL Server. Esta base de datos contiene la estructura necesaria para el funcionamiento del programa.

Versión de .NET:

Es necesario tener instalada la versión 8.0 de la Plataforma .NET en tu máquina.

Instalación de Librerías:

Verifica que las siguientes librerías estén correctamente instaladas. Para hacerlo, abre una terminal en la carpeta raíz del proyecto (en VSCode, usa Ctrl + ñ) y ejecuta los comandos:

    dotnet add package Microsoft.EntityFrameworkCore

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer

    dotnet add package Microsoft.EntityFrameworkCore.Tools

**Pasos para Ejecutar la Aplicación**
Compilación del Proyecto:

Compila la aplicación ejecutando:

    dotnet build

Ejecución del Servidor:

Inicia el servidor de .NET ejecutando:

    dotnet run

Acceso a la Aplicación, Una vez iniciada, deberías ver el siguiente mensaje en la terminal:

    info: Microsoft.Hosting.Lifetime[14]
        Now listening on: http://localhost:5237
    info: Microsoft.Hosting.Lifetime[0]
        Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
        Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
        Content root path: C:\Users\luism\OneDrive\Escritorio\serv\PaginaServ

Abre un navegador web y accede a: http://localhost:5237.

**Login Inicial:**

Usa las siguientes credenciales para iniciar sesión:
    
Correo: 
    
    correo123@gmail.com
    
Contraseña: 
    
    123456

Detener el Servidor:

Para detener la ejecución del servidor, regresa a la terminal y presiona Ctrl + C.

Modificaciones al Código:

Si realizas cambios en el código, asegúrate de:

Detener el servidor (Ctrl + C).

Volver a compilar (dotnet build).
Ejecutar de nuevo (dotnet run).

**Posibles Problemas**

Consulta de Ingredientes:

Si la funcionalidad para consultar ingredientes no funciona, verifica que:

1. Existan registros en las tablas ingredientes y platillos.

2. Las relaciones estén correctamente configuradas en la tabla ingredientesPlatillos.

**Estructura del Proyecto**

NombreDelProyecto

    ├── Controllers                // Lógica de la aplicación
    │   └── HomeController.cs      // Ejemplo de controlador
    │
    ├── Models                     // Modelos de datos (Getters y Setters)
    │   └── LoginViewModel.cs      // Ejemplo de modelo
    │
    ├── Views                      // Vistas (HTML + Razor)
    │   ├── Home                   // Vistas para el controlador Home
    │   │   └── Index.cshtml
    │   ├── Shared                 // Vistas compartidas
    │   │   └── _Layout.cshtml
    │   └── _ViewImports.cshtml
    │   └── _ViewStart.cshtml
    │
    ├── wwwroot                    // Archivos estáticos
    │   ├── css                    // Hojas de estilos
    │   │   └── site.css
    │   ├── js                     // JavaScript
    │   │   └── site.js
    │   └── lib                    // Bibliotecas externas (Bootstrap, jQuery, etc.)
    │       ├── bootstrap
    │       └── jquery
    │
    ├── appsettings.json           // Configuración (e.g., cadena de conexión)
    ├── Program.cs                 // Punto de entrada de la aplicación
    └── Startup.cs                 // Configuración de servicios

**Configuración de Imágenes en las Vistas**
Para mostrar imágenes en las vistas, utiliza la siguiente estructura:
    
    <img src="@Url.Content("~/images/Products/plato.png")" alt="Plato de comida">

Asegúrate de guardar las imágenes en la carpeta wwwroot/images.