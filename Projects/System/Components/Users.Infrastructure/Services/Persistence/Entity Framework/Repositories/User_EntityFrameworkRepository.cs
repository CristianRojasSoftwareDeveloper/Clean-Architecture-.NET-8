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