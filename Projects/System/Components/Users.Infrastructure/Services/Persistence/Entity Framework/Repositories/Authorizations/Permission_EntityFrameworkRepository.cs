using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de permisos utilizando Entity Framework.
    /// Proporciona una abstracción de alto nivel para interactuar con entidades de permiso en la base de datos.
    /// </summary>
    /// <remarks>
    /// Este repositorio extiende las capacidades genéricas de un repositorio de Entity Framework, 
    /// ofreciendo métodos específicos para la gestión completa de permisos en el sistema de autorización.
    /// Permite realizar operaciones estándar CRUD (Crear, Leer, Actualizar, Eliminar) con optimizaciones 
    /// específicas para entidades de permiso.
    /// </remarks>
    public class Permission_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<Permission>(dbContext), IPermissionRepository {

        /// <summary>
        /// Crea un nuevo permiso en el sistema.
        /// </summary>
        /// <param name="newPermission">Objeto de permiso a crear en la base de datos.</param>
        /// <returns>El permiso recién creado con su identificador asignado.</returns>
        public Task<Permission> AddPermission (Permission newPermission) =>
            AddEntity(newPermission);

        /// <summary>
        /// Recupera la lista completa de permisos del sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de permisos almacenados en el sistema.</returns>
        public Task<List<Permission>> GetPermissions (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca un permiso específico por su identificador único.
        /// </summary>
        /// <param name="permissionID">Identificador numérico del permiso.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>El permiso encontrado o null si no existe.</returns>
        public Task<Permission?> GetPermissionByID (int permissionID, bool enableTracking = false) =>
            GetEntityByID(permissionID, enableTracking);

        /// <summary>
        /// Actualiza la información de un permiso existente.
        /// </summary>
        /// <param name="permissionUpdate">Objeto con las actualizaciones parciales del permiso.</param>
        /// <returns>El permiso actualizado con los cambios aplicados.</returns>
        public Task<Permission> UpdatePermission (Partial<Permission> permissionUpdate) =>
            UpdateEntity(permissionUpdate);

        /// <summary>
        /// Elimina un permiso del sistema por su identificador.
        /// </summary>
        /// <param name="permissionID">Identificador numérico del permiso a eliminar.</param>
        /// <returns>El permiso que ha sido eliminado.</returns>
        public Task<Permission> DeletePermissionByID (int permissionID) =>
            DeleteEntityByID(permissionID);

    }

}