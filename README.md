# CleanArchitecture_grupo_1A_M_W_Y_J
CleanArchitecture – Implementación del Patrón Repository
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Visual%20Studio-239120?logo=csharp&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue)
Este repositorio implementa el patrón Repository siguiendo principios de Clean Architecture.
> Solución: `CleanArchitecture`  
> Capas: `CleanArchitecture.Application`, `CleanArchitecture.Domain`, `CleanArchitecture.Infrastructure`, `CleanArchitecture.Presentation`
---
Tabla de contenido
1) ¿Qué hace el patrón Repository en este proyecto?
2) ¿Qué archivos creé/modifiqué para implementarlo?
3) ¿Cómo se conecta el Repository con Infraestructura y Aplicación?
4) Dificultades y cómo se resolvieron
Fragmentos de referencia
Uso en Presentation (ejemplo)
Notas finales
---
1) ¿Qué hace el patrón Repository en este proyecto?
El Repository encapsula el acceso a datos de las entidades del dominio (por ejemplo, `Producto` y `Categoria`) para que la capa de aplicación no dependa de Entity Framework Core ni conozca detalles de la base de datos. En resumen:
Expone operaciones CRUD genéricas mediante `IRepositoryBase<T>`.
Permite repositorios concretos como `ProductRepository` para reglas/consultas específicas.
Centraliza el acceso a datos en Infrastructure, manteniendo Application desacoplada y testeable.
Beneficios
Disminuye la duplicación de código de acceso a datos.
Facilita pruebas unitarias (se mockean interfaces `IRepositoryBase<T>`/`IProductRepository`).
Cumple inversión de dependencias: Application depende de interfaces, Infrastructure de implementaciones.
---
2) ¿Qué archivos creé/modifiqué para implementarlo?
> Nombres y rutas según lo observado en la solución y capturas.
Application
`CleanArchitecture.Application/Interface/IRepositoryBase.cs`  
Interface genérica con operaciones CRUD.
`CleanArchitecture.Application/Interface/IProductRepository.cs`  
Interface específica para `Producto`.
`CleanArchitecture.Application/Dto/CategoryDto.cs`  
DTO para `Categoria` (visible en capturas).
`CleanArchitecture.Application/Services/ConfigurationServices.cs`  
Registro de dependencias y configuración (DI/DbContext).
Domain
`CleanArchitecture.Domain/Entities/Producto.cs`  
Entidad de dominio Producto.
`CleanArchitecture.Domain/Entities/Categoria.cs`  
Entidad de dominio Categoria.
Infrastructure
`CleanArchitecture.Infrastructure/Data/CleanArchitectureDbContext.cs`  
DbContext de EF Core.
`CleanArchitecture.Infrastructure/Repository/Base/RepositoryBase.cs`  
Implementación genérica de `IRepositoryBase<T>`.
`CleanArchitecture.Infrastructure/Repository/ProductRepository.cs`  
Implementación concreta de `IProductRepository`.
`CleanArchitecture.Infrastructure/Configurations/...` (si aplica)  
Configuración Fluent API para entidades.
`CleanArchitecture.Infrastructure/DependencyInjection.cs` (opcional)  
Registro de servicios si prefieres centralizarlo en Infrastructure.
Presentation
`CleanArchitecture.Presentation/Controllers/ProductsController.cs` (si aplica)  
Controlador que consume `IProductRepository` o servicios de Application.
---
3) ¿Cómo se conecta el Repository con Infraestructura y Aplicación?
Flujo de dependencias
Application (contratos): define `IRepositoryBase<T>` y `IProductRepository` (no conoce EF Core).
Infrastructure (implementaciones): `RepositoryBase<T>` y `ProductRepository` usan `CleanArchitectureDbContext` para persistencia.
Inyección de dependencias (DI): se registran los mapeos en `ConfigurationServices` (o en `DependencyInjection` de Infrastructure):
```csharp
   services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
   services.AddScoped<IProductRepository, ProductRepository>();

   services.AddDbContext<CleanArchitectureDbContext>(options =>
       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
   ```
Presentation (API/UI): inyecta interfaces; el contenedor resuelve implementaciones de Infrastructure en tiempo de ejecución.
Diagrama rápido:
```
Presentation ──> Application (Interfaces) ──> Infrastructure (Repos + EF Core)
        (consume)             (contratos)             (implementaciones)
```
---
4) Dificultades y cómo se resolvieron
Namespaces y referencias  
Síntoma: tipos no encontrados en `ProductRepository.cs`/`RepositoryBase.cs`.  
Solución: agregar `using CleanArchitecture.Domain.Entities;` y `using CleanArchitecture.Infrastructure.Data;`, y verificar referencias de proyecto (Infrastructure -> Application/Domain; Presentation -> Application/Infrastructure).
Desalineación de firmas genéricas  
Síntoma: implementación no coincide con la interfaz.  
Solución: unificar firmas CRUD (`GetAllAsync`, `GetByIdAsync`, `AddAsync`, `Update`, `DeleteAsync`, `SaveAsync`).
Registro de DI faltante  
Síntoma: `InvalidOperationException` al inyectar `IProductRepository`.  
Solución: registrar `AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))` y `AddScoped<IProductRepository, ProductRepository>()`.
DbContext/cadena de conexión  
Síntoma: fallos en CRUD/migraciones.  
Solución: registrar `CleanArchitectureDbContext` y validar `DefaultConnection` hacia `localhost\\MSSQLLocalDB` (según capturas).
---

