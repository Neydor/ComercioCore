# ComercioCore

## Descripción del Proyecto

ComercioCore es una aplicación diseñada para la gestión de comerciantes y establecimientos. Este proyecto incluye tanto el backend desarrollado en .NET 8 como el frontend desarrollado en Angular.

## Estructura del Proyecto

- **ComercioCore.API**: API REST que sirve como punto de entrada para las solicitudes al backend
- **ComercioCore.Application**: Contiene la lógica de negocio y los casos de uso
- **ComercioCore.Domain**: Define las entidades, interfaces y reglas de negocio
- **ComercioCore.Infrastructure**: Implementaciones concretas de interfaces (repositorios, servicios externos)
- **ComercioCore.Presentation**: Frontend desarrollado en Angular
- **ComercioCore.Test**: Pruebas unitarias e integración del proyecto

## Requisitos Previos

- Docker Desktop (versión 4.0 o superior)
- Visual Studio 2022 con soporte para .NET 8
- Node.js (versión 16 o superior)
- npm (versión 8 o superior)
- SQL Server Management Studio o Azure Data Studio (opcional, para gestión de base de datos)

## Configuración del Backend

1. Asegúrate de tener Docker instalado en tu máquina.
2. Abre el proyecto en Visual Studio 2022.
3. Establece el proyecto `docker-compose` como el proyecto de inicio.
4. Ejecuta el proyecto. Esto iniciará automáticamente todos los servicios necesarios y configurará la base de datos.

## Configuración de la Base de Datos

Una vez que los contenedores estén ejecutándose, es necesario ejecutar el script de base de datos:

1. Accede a la base de datos del contenedor utilizando los siguientes datos de conexión:
   - Servidor: localhost,8002
   - Usuario: SA
   - Contraseña: Password@12345#

2. Ejecuta el archivo SQL que se encuentra en la carpeta raíz del proyecto llamado `BASE DE DATOS.sql`.

## Configuración del Frontend

1. Navega a la carpeta raíz del proyecto `ComercioCore.Presentation`.
2. Ejecuta el siguiente comando para instalar las dependencias necesarias:

```bash npm install

Una vez instaladas las dependencias, puedes iniciar el servidor de desarrollo con:

```bash npm run start

Esto iniciará la aplicación frontend y estará disponible en http://localhost:4200.

## Despliegue
Para preparar el proyecto para producción:

Backend: Utiliza el Dockerfile incluido para construir una imagen de producción
Frontend: Ejecuta npm run build en el directorio de presentación para generar los archivos estáticos

##  Notas Adicionales
Asegúrate de que el backend esté corriendo antes de iniciar el frontend para que la aplicación funcione correctamente.
Puedes acceder a la documentación de la API usando Swagger en http://localhost:[puerto]/swagger cuando el backend esté en ejecución.