using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de asignaciones de roles a usuarios utilizando Entity Framework.
    /// Proporciona una abstracción para gestionar las relaciones entre usuarios y roles en el sistema.
    /// </summary>
    /// <remarks>
    /// Este repositorio extiende las capacidades genéricas de un repositorio de Entity Framework, 
    /// ofreciendo métodos específicos para la gestión de asignaciones de roles a usuarios.
    /// Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) con funcionalidades 
    /// adicionales de consulta y recuperación de relaciones usuario-rol.
    /// </remarks>
    public class RoleAssignedToUser_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<RoleAssignedToUser>(dbContext), IRoleAssignedToUserRepository {

        /// <summary>
        /// Agrega una nueva asignación de rol a un usuario.
        /// </summary>
        /// <param name="newRoleAssignedToUser">Objeto de asignación de rol a crear en la base de datos.</param>
        /// <returns>La asignación de rol recién creada con su identificador asignado.</returns>
        public Task<RoleAssignedToUser> AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser) =>
            AddEntity(newRoleAssignedToUser);

        /// <summary>
        /// Recupera la lista completa de asignaciones de roles a usuarios.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de roles a usuarios almacenados en el sistema.</returns>
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUsers (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca una asignación de rol a usuario específica por su identificador único.
        /// </summary>
        /// <param name="roleAssignedToUserID">Identificador numérico de la asignación de rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de rol encontrada o null si no existe.</returns>
        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByID (int roleAssignedToUserID, bool enableTracking = false) =>
            GetEntityByID(roleAssignedToUserID, enableTracking);

        /// <summary>
        /// Recupera todas las asignaciones de roles para un usuario específico, incluyendo detalles de roles y permisos.
        /// </summary>
        /// <param name="userID">Identificador del usuario.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de roles para el usuario especificado.</returns>
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserID (int userID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(roleAssignedToUser => roleAssignedToUser.UserID == userID).
            Include(roleAssignedToUser => roleAssignedToUser.Role).
            ThenInclude(role => role.PermissionAssignedToRoles).
            ThenInclude(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        /// <summary>
        /// Busca una asignación de rol específica para un usuario y un rol determinados.
        /// </summary>
        /// <param name="userID">Identificador del usuario.</param>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de rol encontrada o null si no existe.</returns>
        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByForeignKeys (int userID, int roleID, bool enableTracking = false) =>
            FirstOrDefault(roleAssignedToUser => roleAssignedToUser.UserID == userID && roleAssignedToUser.RoleID == roleID, enableTracking);

        /// <summary>
        /// Actualiza la información de una asignación de rol a usuario existente.
        /// </summary>
        /// <param name="roleAssignedToUserUpdate">Objeto con las actualizaciones parciales de la asignación de rol.</param>
        /// <returns>La asignación de rol actualizada con los cambios aplicados.</returns>
        public Task<RoleAssignedToUser> UpdateRoleAssignedToUser (Partial<RoleAssignedToUser> roleAssignedToUserUpdate) =>
            UpdateEntity(roleAssignedToUserUpdate);

        /// <summary>
        /// Elimina una asignación de rol a usuario del sistema por su identificador.
        /// </summary>
        /// <param name="roleAssignedToUserID">Identificador numérico de la asignación de rol a eliminar.</param>
        /// <returns>La asignación de rol que ha sido eliminada.</returns>
        public Task<RoleAssignedToUser> DeleteRoleAssignedToUserByID (int roleAssignedToUserID) =>
            DeleteEntityByID(roleAssignedToUserID);

    }

}