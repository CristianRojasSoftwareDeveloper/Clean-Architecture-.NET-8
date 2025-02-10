using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de roles utilizando Entity Framework.
    /// Extiende las capacidades genéricas de un repositorio de Entity Framework con métodos específicos de gestión de roles.
    /// </summary>
    /// <remarks>
    /// Este repositorio proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
    /// sobre entidades de rol en el sistema de autorización.
    /// </remarks>
    public class Role_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<Role>(dbContext), IRoleRepository {

        /// <summary>
        /// Crea un nuevo rol en el sistema.
        /// </summary>
        /// <param name="newRole">Objeto de rol a crear en la base de datos.</param>
        /// <returns>El rol recién creado con su identificador asignado.</returns>
        public Task<Role> AddRole (Role newRole) =>
            AddEntity(newRole);

        /// <summary>
        /// Recupera la lista completa de roles del sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de roles almacenados en el sistema.</returns>
        public Task<List<Role>> GetRoles (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca un rol específico por su identificador único.
        /// </summary>
        /// <param name="roleID">Identificador numérico del rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>El rol encontrado o null si no existe.</returns>
        public Task<Role?> GetRoleByID (int roleID, bool enableTracking = false) =>
            GetEntityByID(roleID, enableTracking);

        /// <summary>
        /// Actualiza la información de un rol existente.
        /// </summary>
        /// <param name="roleUpdate">Objeto con las actualizaciones parciales del rol.</param>
        /// <returns>El rol actualizado con los cambios aplicados.</returns>
        public Task<Role> UpdateRole (Partial<Role> roleUpdate) =>
            UpdateEntity(roleUpdate);

        /// <summary>
        /// Elimina un rol del sistema por su identificador.
        /// </summary>
        /// <param name="roleID">Identificador numérico del rol a eliminar.</param>
        /// <returns>El rol que ha sido eliminado.</returns>
        public Task<Role> DeleteRoleByID (int roleID) =>
            DeleteEntityByID(roleID);

    }

}