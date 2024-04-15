# RealEstateApp

RealEstateApp es una aplicación web completa para el manejo de propiedades inmobiliarias, desarrollada utilizando ASP.NET Core MVC 
y Entity Framework Core. La aplicación cuenta con funcionalidades para clientes, agentes inmobiliarios y administradores, así como 
una API RESTful para integración con otros sistemas.

## Características principales

- **Clientes:** Los clientes pueden buscar y ver detalles de propiedades, marcar propiedades como favoritas y acceder a su lista de favoritos.
- **Agentes inmobiliarios:** Los agentes pueden gestionar sus propiedades (crear, editar y eliminar), actualizar su perfil y ver un listado de sus propiedades.
- **Administradores:** Los administradores pueden gestionar agentes, desarrolladores, tipos de propiedades, tipos de ventas y mejoras. También pueden ver estadísticas del sistema.
- **API RESTful:** La aplicación incluye una API RESTful segura con JWT para el manejo de propiedades, agentes, tipos de propiedades, tipos de ventas y mejoras.

## Tecnologías utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (o cualquier otra base de datos compatible con EF Core)
- Bootstrap
- AutoMapper
- Identity para la autenticación y autorización
- JWT para la autenticación en la API
- Patrones de diseño: Onion Architecture, CQRS, Mediator

## Requisitos

- Visual Studio 2022 (o posterior)
- .NET Core 6.0 (o posterior)
- SQL Server (o cualquier otra base de datos compatible con EF Core)

## Configuración

1. Clona este repositorio en tu máquina local.
2. Abre la solución en Visual Studio.
3. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`.
4. Ejecuta las migraciones de Entity Framework Core para crear la base de datos: `Update-Database` (desde la Consola del Administrador de Paquetes de NuGet).
5. Ejecuta la aplicación.

## Contribución

Si deseas contribuir a este proyecto, puedes seguir los siguientes pasos:

1. Haz un fork de este repositorio.
2. Crea una rama para tu nueva funcionalidad: `git checkout -b feature/nueva-funcionalidad`.
3. Realiza tus cambios y haz commit: `git commit -m 'Agregar nueva funcionalidad'`.
4. Sube tus cambios a tu repositorio fork: `git push origin feature/nueva-funcionalidad`.
5. Crea una nueva Pull Request en este repositorio.

## Licencia

Este proyecto está licenciado bajo la [Licencia MIT](LICENSE).
