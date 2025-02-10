using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    /// <summary>
    /// Define las operaciones CRUD específicas para la entidad Usuario.
    /// </summary>
    public interface IUserRepository : IQueryableRepository<User> {

        /// <summary>
        /// Agrega un nuevo usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newUser">El usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario agregado como resultado.</returns>
        Task<User> AddUser (User newUser);

        /// <summary>
        /// Obtiene todos los usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de usuarios como resultado.</returns>
        Task<List<User>> GetUsers (bool enableTracking = false);

        /// <summary>
        /// Obtiene un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="userID">El ID del usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario encontrado o null si no existe como resultado.</returns>
        Task<User?> GetUserByID (int userID, bool enableTracking = false);

        Task<User?> GetUserByUsername (string username, bool enableTracking = false);

        /// <summary>
        /// Actualiza un usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="userUpdate">Actualización del usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el usuario actualizado o null si no se encontró como resultado.</returns>
        Task<User> UpdateUser (Partial<User> userUpdate);

        /// <summary>
        /// Elimina un usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="userID">El ID del usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<User> DeleteUserByID (int userID);

    }

}