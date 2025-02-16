
#region GPL v3 License Header
/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 * 
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.engineer@gmail.com»
 * 
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 * 
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 * 
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 * 
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion

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