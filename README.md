# GestionUsuarios-Backend


## Descripción
Esta es una API RESTful para gestionar usaurios. La API permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) personas, y manejar la autenticación de usuarios para acceder a las funcionalidades del sistema.

## Tecnologías Utilizadas
- *.NET Core 8*: Framework utilizado para el desarrollo de la API.
- *Entity Framework Core*: ORM utilizado para interactuar con la base de datos SQL Server.
- *SQL Server*: Base de datos relacional utilizada para almacenar los datos.
- *SQL Server* -Store Procedures
- *JWT*: JSON Web Tokens para la autenticación y autorización de los usuarios.


## Requisitos

- *.NET Core 8*: Se requiere tener instalado el SDK de .NET 8 para poder ejecutar la API localmente.
- *SQL Server*: Se debe tener una base de datos de SQL Server configurada para almacenar los datos.

## Configuración

1. *Configuarcion Base de Datos:*
   Crea una base de datos llamada DbGestionusuarios utilizando SQL Server.
   Ejecuta el script *DbGestionUsuarios.sql* ubicado en el directorio raíz del proyecto. *GestionUsuarios.Api*
   Esto creará las tablas necesarias y un usuario administrador por defecto.

2. *Restaurar dependencias:*
   Dentro del directorio del proyecto:
   bash
   dotnet restore
   

3. *Configurar la base de datos:*
   Asegúrate de configurar la cadena de conexión a tu base de datos SQL Server en el archivo appsettings.json.

4. *Migraciones de la base de datos:*
   Si es necesario, puedes aplicar las migraciones para crear las tablas de la base de datos:
   bash
   dotnet ef database update
   

5. *Ejecutar la API:*
   Para iniciar el servidor localmente:
   bash
   dotnet run
   

La API estará disponible en https://localhost:44395 de manera predeterminada.

## Autenticación

Para interactuar con los endpoints de la API que requieren autenticación, deberás incluir el token JWT en las cabeceras de las solicitudes. Ejemplo:

bash
Authorization: Bearer jwt_token_aqui


## Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

---

## Contacto

Si tienes preguntas o sugerencias, no dudes en ponerte en contacto.

*Email*: leo15_ita@hotmail.com 
