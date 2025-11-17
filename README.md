# RealEstateApp: Plataforma de Gestión Inmobiliaria

[![Licencia MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.txt)
[![Tecnología Principal](https://img.shields.io/badge/Tech-ASP.NET%20Core%20MVC-purple.svg)](https://dotnet.microsoft.com/apps/aspnet/mvc)
[![Arquitectura](https://img.shields.io/badge/Architecture-Onion%20CQRS-orange.svg)](https://en.wikipedia.org/wiki/Onion_architecture)

## 🏠 Descripción del Proyecto

**RealEstateApp** es una aplicación web robusta y completa diseñada para la gestión integral de propiedades inmobiliarias. Desarrollada con **ASP.NET Core MVC** y **Entity Framework Core**, la plataforma ofrece funcionalidades diferenciadas para clientes, agentes inmobiliarios y administradores, además de exponer una **API RESTful** segura para la integración con sistemas externos.

El proyecto sigue rigurosamente los principios de **Arquitectura Limpia (Onion Architecture)**, complementada con los patrones **CQRS (Command Query Responsibility Segregation)** y **Mediator**, lo que garantiza una alta mantenibilidad, escalabilidad y separación de preocupaciones.

## ✨ Características Principales

La aplicación está segmentada para atender las necesidades de diferentes perfiles de usuario:

| Perfil de Usuario | Funcionalidades Clave |
| :--- | :--- |
| **Clientes** | Búsqueda y visualización de propiedades, gestión de lista de favoritos. |
| **Agentes Inmobiliarios** | Gestión completa de su cartera de propiedades (creación, edición, eliminación), actualización de perfil y listado de propiedades asignadas. |
| **Administradores** | Gestión de usuarios (agentes, desarrolladores), administración de catálogos (tipos de propiedades, tipos de ventas, mejoras) y acceso a estadísticas del sistema. |
| **API RESTful** | Interfaz de programación de aplicaciones segura (mediante **JWT**) para la manipulación de propiedades y catálogos, facilitando la integración con otras plataformas. |

## 🛠️ Tecnologías y Arquitectura

Este proyecto se basa en un conjunto de tecnologías modernas y patrones de diseño avanzados:

### Stack Tecnológico

*   **Backend:** ASP.NET Core MVC (versión 6.0 o superior)
*   **ORM:** Entity Framework Core
*   **Base de Datos:** SQL Server (compatible con cualquier otra base de datos soportada por EF Core)
*   **Autenticación/Autorización:** ASP.NET Core Identity y JWT (para la API)
*   **Mapeo de Objetos:** AutoMapper
*   **UI/Estilos:** Bootstrap

### Patrones de Diseño

El proyecto implementa una **Arquitectura Limpia (Onion Architecture)** para desacoplar la lógica de negocio de la infraestructura y la interfaz de usuario. Además, utiliza:

*   **CQRS (Command Query Responsibility Segregation):** Separación de las operaciones de lectura (Queries) y escritura (Commands) para optimizar el rendimiento y la escalabilidad.
*   **Mediator:** Implementado a través de la librería MediatR para gestionar la comunicación entre las capas de la aplicación de forma desacoplada.

## Estructura del proyecto

Este proyecto sigue la arquitectura Onion Architecture y está organizado en múltiples proyectos y capas. A continuación, se detallan los principales proyectos y sus dependencias:

### RealEstateApp.Core.Application

Este proyecto contiene las interfaces, servicios, viewmodels, maps, dtos, helpers, enums y lógica de negocio principal de la aplicación.

Dependencias:
- RealEstateApp.Core.Domain
- Microsoft.EntityFrameworkCore
- AutoMapper
- MediatR
- Microsoft.AspNetCore.Http
- Microsoft.AspNetCore.Http.Abstractions
- Microsoft.AspNetCore.Http.Extensions
- Microsoft.Extensions.Options.ConfigurationExtensions
- Newtonsoft.Json
- System.Text.Encodings.Web

### RealEstateApp.Core.Domain
Este proyecto contiene las entidades y settins de la app.

Dependencias:

### RealEstateApp.Infrastructure.Idenity

Este proyecto contiene el manejador de usuario

Dependencias:
- RealEstateApp.Core.Application
- RealEstateApp.Infrastructure.Shared
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Options.ConfigurationExtensions

### RealEstateApp.Infrastructure.Persistence

Este proyecto contiene la configuracion del contexto de la base de datos y migrations y repositorios

- RealEstateApp.Core.Application
- RealEstateApp.Core.Domain
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.DependencyInjection.Abstractions
- Microsoft.Extensions.Options.ConfigurationExtensions

### RealEstateApp.Infrastructure.Shared

Este proyecto contiene el manejo de mails

- RealEstateApp.Core.Application
- RealEstateApp.Core.Domain
- MailKit
- MimeKit
- Microsoft.Extensions.Options.ConfigurationExtensions

### RealEstateApp.Presentation.WebApp

Este proyecto contiene la aplicación web ASP.NET Core MVC.

Dependencias:
- RealEstateApp.Core.Application
- RealEstateApp.Core.Domain
- RealEstateApp.Infrastructure.Idenity
- RealEstateApp.Infrastructure.Persistence
- RealEstateApp.Infrastructure.Shared
- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Web.CodeGeneration.Design

### RealEstateApp.Presentation.WebApi

Este proyecto contiene la API RESTful de la aplicación.

Dependencias:
- RealEstateApp.Core
- RealEstateApp.Infrastructure
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle.AspNetCore

## Requisitos

- Visual Studio 2022 (o posterior)
- .NET Core 6.0 (o posterior)
- SQL Server (o cualquier otra base de datos compatible con EF Core)

## Configuración

1. Clona este repositorio en tu máquina local.
2. Abre la solución en Visual Studio.
3. Configura la cadena de conexión a la base de datos en el archivo `appsettings.json`.
4. Configura el webApp como proyecto por default
5. Ejecuta las migraciones de Entity Framework Core para crear la base de datos: `Update-Database` (desde la Consola del Administrador de Paquetes de NuGet).
6. Ejecuta la aplicación.

## Contribución

Si deseas contribuir a este proyecto, puedes seguir los siguientes pasos:

1. Haz un fork de este repositorio.
2. Crea una rama para tu nueva funcionalidad: `git checkout -b feature/nueva-funcionalidad`.
3. Realiza tus cambios y haz commit: `git commit -m 'Agregar nueva funcionalidad'`.
4. Sube tus cambios a tu repositorio fork: `git push origin feature/nueva-funcionalidad`.
5. Crea una nueva Pull Request en este repositorio.

## Licencia

Este proyecto está licenciado bajo la [Licencia MIT](LICENSE).
