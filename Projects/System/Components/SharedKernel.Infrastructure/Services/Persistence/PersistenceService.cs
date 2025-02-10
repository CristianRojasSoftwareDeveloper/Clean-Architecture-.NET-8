// Importa los espacios de nombres necesarios para el servicio de persistencia
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Collections.Frozen;

namespace SharedKernel.Infrastructure.Services.Persistence {

    /// <summary>
    /// Encapsula la colección de repositorios necesarios para la gestión de la persistencia.
    /// </summary>
    /// <remarks>
    /// Proporciona un contenedor inmutable para todos los repositorios del sistema,
    /// facilitando la inyección de dependencias y el mantenimiento del código.
    /// </remarks>
    public record RepositoryCollection (
        IUserRepository UserRepository,                                             // Gestiona las operaciones de usuarios.
        IRoleRepository RoleRepository,                                             // Gestiona las operaciones de roles.
        IPermissionRepository PermissionRepository,                                 // Gestiona las operaciones de permisos.
        IRoleAssignedToUserRepository RoleAssignedToUserRepository,                 // Gestiona las asignaciones de roles a usuarios.
        IPermissionAssignedToRoleRepository PermissionAssignedToRoleRepository,     // Gestiona las asignaciones de permisos a roles.
        ISystemLogRepository SystemLogRepository                                    // Gestiona los registros del sistema.
    );

    /// <summary>
    /// Implementa el servicio de persistencia utilizando reflexión para automatizar 
    /// el mapeo entre entidades y sus repositorios correspondientes.
    /// </summary>
    /// <remarks>
    /// Proporciona una capa de abstracción sobre los repositorios individuales,
    /// facilitando el acceso centralizado a la persistencia de datos.
    /// </remarks>
    public class PersistenceService : IPersistenceService {

        // Propiedades de solo lectura para acceder a los repositorios específicos.
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IPermissionRepository PermissionRepository { get; }
        public IRoleAssignedToUserRepository RoleAssignedToUserRepository { get; }
        public IPermissionAssignedToRoleRepository PermissionAssignedToRoleRepository { get; }
        public ISystemLogRepository SystemLogRepository { get; }

        // Almacena el mapeo entre tipos de entidad y sus repositorios.
        private readonly FrozenDictionary<Type, object> _repositories;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de persistencia.
        /// </summary>
        /// <param name="repositories">Colección de repositorios a utilizar.</param>
        /// <exception cref="ArgumentNullException">Se lanza si repositories es null.</exception>
        public PersistenceService (RepositoryCollection repositories) {
            // Verifica que la colección de repositorios no sea null
            ArgumentNullException.ThrowIfNull(repositories);
            // Asigna los repositorios a sus propiedades correspondientes, luego crea el diccionario de mapeo entre tipos y repositorios.
            _repositories = new Dictionary<Type, object> {
                [typeof(User)] = UserRepository = repositories.UserRepository,
                [typeof(Role)] = RoleRepository = repositories.RoleRepository,
                [typeof(Permission)] = PermissionRepository = repositories.PermissionRepository,
                [typeof(RoleAssignedToUser)] = RoleAssignedToUserRepository = repositories.RoleAssignedToUserRepository,
                [typeof(PermissionAssignedToRole)] = PermissionAssignedToRoleRepository = repositories.PermissionAssignedToRoleRepository,
                [typeof(SystemLog)] = SystemLogRepository = repositories.SystemLogRepository
            }.ToFrozenDictionary();
        }

        /// <summary>
        /// Obtiene el repositorio genérico para un tipo de entidad específico.
        /// </summary>
        /// <typeparam name="TEntity">Tipo de entidad para la cual se requiere el repositorio.</typeparam>
        /// <returns>Repositorio genérico para el tipo de entidad especificado.</returns>
        /// <exception cref="KeyNotFoundException">Se lanza si no se encuentra un repositorio para el tipo de entidad.</exception>
        public IGenericRepository<TEntity> GetGenericRepository<TEntity> () where TEntity : IGenericEntity =>
            _repositories.TryGetValue(typeof(TEntity), out var repository) && repository is IGenericRepository<TEntity> genericRepository
                ? genericRepository : throw new KeyNotFoundException($"No se encuentra un repositorio para la entidad «{typeof(TEntity).Name}».");

    }

}