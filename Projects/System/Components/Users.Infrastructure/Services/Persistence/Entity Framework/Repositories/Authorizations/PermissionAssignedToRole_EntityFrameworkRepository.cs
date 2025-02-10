using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de asignaciones de permisos a roles utilizando Entity Framework.
    /// Extiende las capacidades genéricas de un repositorio de Entity Framework con métodos específicos de gestión de asignaciones de permisos a roles.
    /// </summary>
    /// <remarks>
    /// Este repositorio proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
    /// sobre entidades de asignación de permisos a roles, permitiendo una gestión detallada de las relaciones 
    /// entre permisos y roles en el sistema de autorización.
    /// </remarks>
    public class PermissionAssignedToRole_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<PermissionAssignedToRole>(dbContext), IPermissionAssignedToRoleRepository {

        /// <summary>
        /// Agrega una nueva asignación de permiso a un rol en el sistema.
        /// </summary>
        /// <param name="newPermissionAssignedToRole">Objeto de asignación de permiso a crear en la base de datos.</param>
        /// <returns>La asignación de permiso recién creada con su identificador asignado.</returns>
        public Task<PermissionAssignedToRole> AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole) =>
            AddEntity(newPermissionAssignedToRole);

        /// <summary>
        /// Recupera la lista completa de asignaciones de permisos a roles del sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de permisos a roles almacenados en el sistema.</returns>
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRoles (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca una asignación de permiso a rol específica por su identificador único.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">Identificador numérico de la asignación de permiso.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de permiso encontrada o null si no existe.</returns>
        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID, bool enableTracking = false) =>
            GetEntityByID(permissionAssignedToRoleID, enableTracking);

        /// <summary>
        /// Recupera todas las asignaciones de permisos para un rol específico, incluyendo detalles de permisos.
        /// </summary>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de permisos para el rol especificado.</returns>
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleID (int roleID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID).
            Include(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        /// <summary>
        /// Busca una asignación de permiso específica para un rol y un permiso determinados.
        /// </summary>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="permissionID">Identificador del permiso.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de permiso encontrada o null si no existe.</returns>
        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID, bool enableTracking = false) =>
            FirstOrDefault(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID && permissionAssignedToRole.PermissionID == permissionID, enableTracking);

        /// <summary>
        /// Actualiza la información de una asignación de permiso a rol existente.
        /// </summary>
        /// <param name="permissionAssignedToRoleUpdate">Objeto con las actualizaciones parciales de la asignación de permiso.</param>
        /// <returns>La asignación de permiso actualizada con los cambios aplicados.</returns>
        public Task<PermissionAssignedToRole> UpdatePermissionAssignedToRole (Partial<PermissionAssignedToRole> permissionAssignedToRoleUpdate) =>
            UpdateEntity(permissionAssignedToRoleUpdate);

        /// <summary>
        /// Elimina una asignación de permiso a rol del sistema por su identificador.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">Identificador numérico de la asignación de permiso a eliminar.</param>
        /// <returns>La asignación de permiso que ha sido eliminada.</returns>
        public Task<PermissionAssignedToRole> DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID) =>
            DeleteEntityByID(permissionAssignedToRoleID);

    }

}