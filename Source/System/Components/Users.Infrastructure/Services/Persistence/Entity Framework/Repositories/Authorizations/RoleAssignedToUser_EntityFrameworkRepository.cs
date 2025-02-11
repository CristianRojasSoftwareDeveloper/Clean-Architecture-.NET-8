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
    /// Repositorio especializado para operaciones de persistencia de asignaciones de roles a usuarios utilizando Entity Framework.
    /// Proporciona una abstracción para gestionar las relaciones entre usuarios y roles en el sistema.
    /// </summary>
    /// <remarks>
    /// Este repositorio extiende las capacidades genéricas de un repositorio de Entity Framework, 
    /// ofreciendo métodos específicos para la gestión de asignaciones de roles a usuarios.
    /// Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) con funcionalidades 
    /// adicionales de consulta y recuperación de relaciones usuario-rol.
    /// </remarks>
    public class RoleAssignedToUser_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<RoleAssignedToUser>(dbContext), IRoleAssignedToUserRepository {

        /// <summary>
        /// Agrega una nueva asignación de rol a un usuario.
        /// </summary>
        /// <param name="newRoleAssignedToUser">Objeto de asignación de rol a crear en la base de datos.</param>
        /// <returns>La asignación de rol recién creada con su identificador asignado.</returns>
        public Task<RoleAssignedToUser> AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser) =>
            AddEntity(newRoleAssignedToUser);

        /// <summary>
        /// Recupera la lista completa de asignaciones de roles a usuarios.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de roles a usuarios almacenados en el sistema.</returns>
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUsers (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca una asignación de rol a usuario específica por su identificador único.
        /// </summary>
        /// <param name="roleAssignedToUserID">Identificador numérico de la asignación de rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de rol encontrada o null si no existe.</returns>
        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByID (int roleAssignedToUserID, bool enableTracking = false) =>
            GetEntityByID(roleAssignedToUserID, enableTracking);

        /// <summary>
        /// Recupera todas las asignaciones de roles para un usuario específico, incluyendo detalles de roles y permisos.
        /// </summary>
        /// <param name="userID">Identificador del usuario.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de asignaciones de roles para el usuario especificado.</returns>
        public Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserID (int userID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(roleAssignedToUser => roleAssignedToUser.UserID == userID).
            Include(roleAssignedToUser => roleAssignedToUser.Role).
            ThenInclude(role => role.PermissionAssignedToRoles).
            ThenInclude(permissionAssignedToRole => permissionAssignedToRole.Permission).
            ToListAsync();

        /// <summary>
        /// Busca una asignación de rol específica para un usuario y un rol determinados.
        /// </summary>
        /// <param name="userID">Identificador del usuario.</param>
        /// <param name="roleID">Identificador del rol.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>La asignación de rol encontrada o null si no existe.</returns>
        public Task<RoleAssignedToUser?> GetRoleAssignedToUserByForeignKeys (int userID, int roleID, bool enableTracking = false) =>
            FirstOrDefault(roleAssignedToUser => roleAssignedToUser.UserID == userID && roleAssignedToUser.RoleID == roleID, enableTracking);

        /// <summary>
        /// Actualiza la información de una asignación de rol a usuario existente.
        /// </summary>
        /// <param name="roleAssignedToUserUpdate">Objeto con las actualizaciones parciales de la asignación de rol.</param>
        /// <returns>La asignación de rol actualizada con los cambios aplicados.</returns>
        public Task<RoleAssignedToUser> UpdateRoleAssignedToUser (Partial<RoleAssignedToUser> roleAssignedToUserUpdate) =>
            UpdateEntity(roleAssignedToUserUpdate);

        /// <summary>
        /// Elimina una asignación de rol a usuario del sistema por su identificador.
        /// </summary>
        /// <param name="roleAssignedToUserID">Identificador numérico de la asignación de rol a eliminar.</param>
        /// <returns>La asignación de rol que ha sido eliminada.</returns>
        public Task<RoleAssignedToUser> DeleteRoleAssignedToUserByID (int roleAssignedToUserID) =>
            DeleteEntityByID(roleAssignedToUserID);

    }

}