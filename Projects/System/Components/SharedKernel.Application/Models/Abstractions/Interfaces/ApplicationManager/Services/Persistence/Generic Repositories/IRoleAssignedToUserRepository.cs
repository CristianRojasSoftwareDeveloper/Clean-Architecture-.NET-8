using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IRoleAssignedToUserRepository : IQueryableRepository<RoleAssignedToUser> {

        /// <summary>
        /// Agrega un nuevo rol de usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newRoleAssignedToUser">El rol de usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario agregado como resultado.</returns>
        Task<RoleAssignedToUser> AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser);

        /// <summary>
        /// Obtiene todos los rol de usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de rol de usuarios como resultado.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUsers (bool enableTracking = false);

        /// <summary>
        /// Obtiene un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario encontrado o null si no existe como resultado.</returns>
        Task<RoleAssignedToUser?> GetRoleAssignedToUserByID (int roleAssignedToUserID, bool enableTracking = false);

        /// <summary>
        /// Obtiene todos los roles de usuario asociados a un usuario según su ID.
        /// </summary>
        /// <returns>Una colección de rol de usuarios.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserID (int userID, bool enableTracking = false);

        Task<RoleAssignedToUser?> GetRoleAssignedToUserByForeignKeys (int userID, int roleID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un rol de usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserUpdate">Actualización del rol de usuario asociado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario actualizado o null si no se encontró como resultado.</returns>
        Task<RoleAssignedToUser> UpdateRoleAssignedToUser (Partial<RoleAssignedToUser> roleAssignedToUserUpdate);

        /// <summary>
        /// Elimina un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<RoleAssignedToUser> DeleteRoleAssignedToUserByID (int ID_roleAssignedToUser);

    }

}