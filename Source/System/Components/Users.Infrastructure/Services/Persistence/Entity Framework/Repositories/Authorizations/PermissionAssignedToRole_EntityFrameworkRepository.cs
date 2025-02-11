#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
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

using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de asignaciones de permisos a roles utilizando Entity Framework.
    /// Extiende las capacidades genéricas de un repositorio de Entity Framework con métodos específicos de gestión de asignaciones de permisos a roles.
    /// </summary>
    /// <remarks>
    /// Este repositorio proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
    /// sobre entidades de asignación de permisos a roles, permitiendo una gestión detallada de las relaciones 
    /// entre permisos y roles en el sistema de autorización.
    /// </remarks>
    public class PermissionAssignedToRole_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<PermissionAssignedToRole>(dbContext), IPermissionAssignedToRoleRepository {

        /// <summary>
        /// Agrega una nueva asignación de permiso a un rol en el sistema.
        /// </summary>
        /// <param name="newPermissionAssignedToRole">Objeto de asignación de permiso a crear en la base de datos.</param>
        /// <returns>La asignación de permiso recién creada con su identificador asignado.</returns>
        public Task<PermissionAssignedToRole> AddPermissionAssignedToRole (PermissionAssignedToRole newPermissionAssignedToRole) =>
            AddEntity(newPermissionAssignedToRole);

        /// <summary>
        /// Recupera la lista completa de asignaciones de permisos a roles del sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de permisos a roles almacenados en el sistema.</returns>
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRoles (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca una asignación de permiso a rol específica por su identificador único.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">Identificador numérico de la asignación de permiso.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de permiso encontrada o null si no existe.</returns>
        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByID (int permissionAssignedToRoleID, bool enableTracking = false) =>
            GetEntityByID(permissionAssignedToRoleID, enableTracking);

        /// <summary>
        /// Recupera todas las asignaciones de permisos para un rol específico, incluyendo detalles de permisos.
        /// </summary>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de permisos para el rol especificado.</returns>
        public Task<List<PermissionAssignedToRole>> GetPermissionAssignedToRolesByRoleID (int roleID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID).
            Include(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        /// <summary>
        /// Busca una asignación de permiso específica para un rol y un permiso determinados.
        /// </summary>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="permissionID">Identificador del permiso.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de permiso encontrada o null si no existe.</returns>
        public Task<PermissionAssignedToRole?> GetPermissionAssignedToRoleByForeignKeys (int roleID, int permissionID, bool enableTracking = false) =>
            FirstOrDefault(permissionAssignedToRole => permissionAssignedToRole.RoleID == roleID && permissionAssignedToRole.PermissionID == permissionID, enableTracking);

        /// <summary>
        /// Actualiza la información de una asignación de permiso a rol existente.
        /// </summary>
        /// <param name="permissionAssignedToRoleUpdate">Objeto con las actualizaciones parciales de la asignación de permiso.</param>
        /// <returns>La asignación de permiso actualizada con los cambios aplicados.</returns>
        public Task<PermissionAssignedToRole> UpdatePermissionAssignedToRole (Partial<PermissionAssignedToRole> permissionAssignedToRoleUpdate) =>
            UpdateEntity(permissionAssignedToRoleUpdate);

        /// <summary>
        /// Elimina una asignación de permiso a rol del sistema por su identificador.
        /// </summary>
        /// <param name="permissionAssignedToRoleID">Identificador numérico de la asignación de permiso a eliminar.</param>
        /// <returns>La asignación de permiso que ha sido eliminada.</returns>
        public Task<PermissionAssignedToRole> DeletePermissionAssignedToRoleByID (int permissionAssignedToRoleID) =>
            DeleteEntityByID(permissionAssignedToRoleID);

    }

}