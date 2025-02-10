using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de usuarios utilizando Entity Framework.
    /// Extiende las capacidades genéricas de un repositorio de Entity Framework con métodos específicos de gestión de usuarios.
    /// </summary>
    /// <remarks>
    /// Este repositorio proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
    /// sobre entidades de usuario, con soporte para carga de roles y permisos asociados.
    /// </remarks>
    public class User_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<User>(dbContext), IUserRepository {

        /// <summary>
        /// Agrega un nuevo usuario al sistema.
        /// </summary>
        /// <param name="newUser">Objeto de usuario a crear en la base de datos.</param>
        /// <returns>El usuario recién creado con su identificador asignado.</returns>
        public Task<User> AddUser (User newUser) =>
            AddEntity(newUser);

        /// <summary>
        /// Recupera la lista completa de usuarios del sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de usuarios almacenados en el sistema.</returns>
        public Task<List<User>> GetUsers (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca un usuario específico por su identificador único.
        /// </summary>
        /// <param name="userID">Identificador numérico del usuario.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        public Task<User?> GetUserByID (int userID, bool enableTracking = false) =>
            GetEntityByID(userID, enableTracking);

        /// <summary>
        /// Busca un usuario por su nombre de usuario, incluyendo roles y permisos asociados.
        /// </summary>
        /// <param name="username">Nombre de usuario para la búsqueda.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>
        /// El usuario encontrado con su estructura completa de roles y permisos, 
        /// o null si no se encuentra el usuario.
        /// </returns>
        public Task<User?> GetUserByUsername (string username, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(user => user.Username == username).
            Include(user => user.RolesAssignedToUser).
            ThenInclude(roleAssignedToUser => roleAssignedToUser.Role).
            ThenInclude(role => role.PermissionAssignedToRoles).
            ThenInclude(permissionAssignedToRole => permissionAssignedToRole.Permission).
            SingleOrDefaultAsync();

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="userUpdate">Objeto con las actualizaciones parciales del usuario.</param>
        /// <returns>El usuario actualizado con los cambios aplicados.</returns>
        public Task<User> UpdateUser (Partial<User> userUpdate) =>
            UpdateEntity(userUpdate);

        /// <summary>
        /// Elimina un usuario del sistema por su identificador.
        /// </summary>
        /// <param name="userID">Identificador numérico del usuario a eliminar.</param>
        /// <returns>El usuario que ha sido eliminado.</returns>
        public Task<User> DeleteUserByID (int userID) =>
            DeleteEntityByID(userID);

    }

}