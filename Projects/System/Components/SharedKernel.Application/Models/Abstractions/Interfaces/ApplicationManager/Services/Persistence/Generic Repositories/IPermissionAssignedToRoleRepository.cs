using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IPermissionAssignedToRoleRepository : IQueryableRepository<PermissionAssignedToRole> {

        /// <summary>
        /// Agrega un nuevo permiso de rol al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newPermissionAssignedToRole">El permiso de rol a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol agregado como resultado.</returns>
        Task<PermissionAssignedToRole> AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole);

        /// <summary>
        /// Obtiene todos los permiso de roles del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de permiso de roles como resultado.</returns>
        Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRoles (bool enableTracking = false);

        /// <summary>
        /// Obtiene un permiso de rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol encontrado o null si no existe como resultado.</returns>
        Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID, bool enableTracking = false);

        /// <summary>
        /// Obtiene todos los permisos de usuario asociados a un rol según su ID.
        /// </summary>
        /// <returns>Una colección de permisos del rol.</returns>
        Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleID (int roleID, bool enableTracking = false);

        Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un permiso de rol existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionAssignedToRoleUpdate">Actualización del permiso de rol asociado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de rol actualizado o null si no se encontró como resultado.</returns>
        Task<PermissionAssignedToRole> UpdatePermissionAssignedToRole (Partial<PermissionAssignedToRole> permissionAssignedToRoleUpdate);

        /// <summary>
        /// Elimina un permiso de rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">El ID del permiso de rol a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<PermissionAssignedToRole> DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID);

    }

}