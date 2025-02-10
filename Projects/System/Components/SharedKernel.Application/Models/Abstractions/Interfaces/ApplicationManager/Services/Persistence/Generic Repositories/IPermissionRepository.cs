using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IPermissionRepository : IQueryableRepository<Permission> {

        /// <summary>
        /// Agrega un nuevo permiso de usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newPermission">El permiso de usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario agregado como resultado.</returns>
        Task<Permission> AddPermission (Permission newPermission);

        /// <summary>
        /// Obtiene todos los permiso de usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de permiso de usuarios como resultado.</returns>
        Task<List<Permission>> GetPermissions (bool enableTracking = false);

        /// <summary>
        /// Obtiene un permiso de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario encontrado o null si no existe como resultado.</returns>
        Task<Permission?> GetPermissionByID (int permissionID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un permiso de usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionUpdate">Actualización del permiso de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el permiso de usuario actualizado o null si no se encontró como resultado.</returns>
        Task<Permission> UpdatePermission (Partial<Permission> permissionUpdate);

        /// <summary>
        /// Elimina un permiso de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="permissionID">El ID del permiso de usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<Permission> DeletePermissionByID (int permissionID);

    }

}