using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence {

    /// <summary>
    /// Interface que define el servicio de persistencia que actúa como una capa de acceso centralizada a los repositorios de la aplicación.
    /// Proporciona instancias de repositorios específicos y un mecanismo genérico para obtener repositorios basados en entidades.
    /// </summary>
    public interface IPersistenceService {

        /// <summary>
        /// Repositorio de usuarios.
        /// </summary>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Repositorio de roles de usuario.
        /// </summary>
        IRoleRepository RoleRepository { get; }

        /// <summary>
        /// Repositorio de permisos de sistema.
        /// </summary>
        IPermissionRepository PermissionRepository { get; }

        /// <summary>
        /// Repositorio de roles asignados a usuarios.
        /// </summary>
        IRoleAssignedToUserRepository RoleAssignedToUserRepository { get; }

        /// <summary>
        /// Repositorio de permisos asignados a roles.
        /// </summary>
        IPermissionAssignedToRoleRepository PermissionAssignedToRoleRepository { get; }

        /// <summary>
        /// Repositorio de registros del sistema.
        /// </summary>
        ISystemLogRepository SystemLogRepository { get; }

        /// <summary>
        /// Obtiene un repositorio genérico basado en el tipo de entidad especificado.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad para la cual se desea obtener el repositorio.</typeparam>
        /// <returns>Instancia del repositorio genérico asociado a la entidad.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Se lanza si no existe un repositorio registrado para el tipo de entidad <typeparamref name="TEntity"/>.
        /// </exception>
        IGenericRepository<TEntity> GetGenericRepository<TEntity> () where TEntity : IGenericEntity;

    }

}