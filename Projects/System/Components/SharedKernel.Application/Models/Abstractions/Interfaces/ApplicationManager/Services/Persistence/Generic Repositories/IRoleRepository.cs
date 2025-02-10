using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IRoleRepository : IQueryableRepository<Role> {

        /// <summary>
        /// Agrega un nuevo rol al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newRole">El rol a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol agregado como resultado.</returns>
        Task<Role> AddRole (Role newRole);

        /// <summary>
        /// Obtiene todos los roles del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de roles como resultado.</returns>
        Task<List<Role>> GetRoles (bool enableTracking = false);

        /// <summary>
        /// Obtiene un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleID">El ID del rol.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol encontrado o null si no existe como resultado.</returns>
        Task<Role?> GetRoleByID (int roleID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un rol existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleUpdate">Actualización del rol de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol actualizado o null si no se encontró como resultado.</returns>
        Task<Role> UpdateRole (Partial<Role> roleUpdate);

        /// <summary>
        /// Elimina un rol por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleID">El ID del rol a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<Role> DeleteRoleByID (int roleID);

    }

}