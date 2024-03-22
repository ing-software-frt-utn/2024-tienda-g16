# README - TFI Ingeniería de Software Grupo 16

Este repositorio contiene el Trabajo Final Integrador (TFI) del Grupo 16 para la materia Ingeniería de Software.

## Miembros del Grupo

- [Paz Posse, Tobías](https://github.com/toviazs) - 52596
- [Figueroa, Santiago](https://github.com/SantiFigueroa00) - 52679

## Backend

### Características principales

- **Tecnologías utilizadas**: .NET Core, ASP.NET, EF Core con Code First, MediatR para implementar CQRS, FluentValidation para validación de inputs, Mapster para mapeo de comandos y queries, XUnit para pruebas unitarias.
- **Operaciones soportadas**: El backend ofrece todas las operaciones del MVP de venta, incluyendo registro de usuarios, inicio de sesión, creación de nuevas ventas, búsqueda de artículos por código en sucursal, gestión de líneas de venta, consulta de venta actual, métodos de pago (tarjeta y efectivo), gestión de clientes, cancelación y confirmación de ventas.

### Estructura del proyecto
- **Capa de Presentación**: Aloja controladores, requests y responses necesarios para la interacción con el frontend.
- **Capa de Aplicación**: Define los comandos y queries para cada funcionalidad, incluyendo validación opcional, así como los handlers y DTOs correspondientes. Además, define un behavior de validación y uno de startup usando MediatR.
- **Capa de Dominio**: Define los agregados, relaciones entre ellos, errores utilizando ErrorOr, eventos del dominio, excepciones del dominio y contratos de repositorios y gateways.
- **Capa de Infraestructura**: Implementa los repositorios, el generador de tokens, los gateways, los adaptadores y la configuración de la base de datos. También incluye interceptores para la publicación de eventos del dominio.

## Frontend

### Características principales

- **Tecnologías utilizadas**: El frontend está desarrollado utilizando Angular, una plataforma de desarrollo de aplicaciones web de código abierto y basada en TypeScript. Además de HTML, CSS y TypeScript, se emplea la librería RxJS para gestionar operaciones asíncronas de manera eficiente. Los formularios reactivos de Angular son utilizados para la creación y validación de formularios, garantizando una experiencia de usuario fluida.
- **Operaciones soportadas**: El frontend ofrece una interfaz para interactuar con las operaciones del MVP de venta proporcionadas por el backend. Estas incluyen el registro e inicio de sesión de usuarios, creación de nuevas ventas, búsqueda de artículos por código en sucursal, gestión de líneas de venta, consulta de venta actual, métodos de pago (tarjeta y efectivo), gestión de clientes, cancelación y confirmación de ventas.

### Estructura del proyecto
- **Modulos**: El proyecto se organiza en diferentes módulos, cada uno representando una funcionalidad específica. Entre los módulos destacan el de login, ventas, artículos, entre otros. Esta modularización facilita el mantenimiento del código y fomenta la reutilización de componentes.
- **Componentes**: Cada módulo cuenta con una serie de componentes que encapsulan la lógica y la presentación relacionadas con esa funcionalidad. Estos componentes constan de un archivo HTML para la vista y un archivo TypeScript para la lógica. Esta separación permite una mayor legibilidad y escalabilidad del código, además de fomentar las buenas prácticas de desarrollo.
- **Servicios**: Para la comunicación con el backend y el manejo de errores, cada módulo cuenta con un servicio dedicado. Estos servicios se encargan de realizar peticiones HTTP, gestionar la respuesta del servidor y manejar cualquier error que pueda surgir durante el proceso. Esta separación de responsabilidades garantiza un código más limpio y mantenible.
- **Ruteo**: El proyecto implementa un sistema de rutas que dirige la navegación del usuario entre los diferentes componentes y módulos. Además, se utilizan guards de ruta para proteger ciertas rutas y restringir el acceso a usuarios no autenticados. Esto asegura la seguridad de la aplicación y proporciona una experiencia de usuario coherente.