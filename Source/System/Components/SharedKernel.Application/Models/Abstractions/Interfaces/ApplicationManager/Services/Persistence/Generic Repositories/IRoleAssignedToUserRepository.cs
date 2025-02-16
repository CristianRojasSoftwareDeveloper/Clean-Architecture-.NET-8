
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

using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IRoleAssignedToUserRepository : IQueryableRepository<RoleAssignedToUser> {

        /// <summary>
        /// Agrega un nuevo rol de usuario al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newRoleAssignedToUser">El rol de usuario a agregar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario agregado como resultado.</returns>
        Task<RoleAssignedToUser> AddRoleAssignedToUser (RoleAssignedToUser newRoleAssignedToUser);

        /// <summary>
        /// Obtiene todos los rol de usuarios del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>La tarea que representa la operación asincrónica, con una colección de rol de usuarios como resultado.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUsers (bool enableTracking = false);

        /// <summary>
        /// Obtiene un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario encontrado o null si no existe como resultado.</returns>
        Task<RoleAssignedToUser?> GetRoleAssignedToUserByID (int roleAssignedToUserID, bool enableTracking = false);

        /// <summary>
        /// Obtiene todos los roles de usuario asociados a un usuario según su ID.
        /// </summary>
        /// <returns>Una colección de rol de usuarios.</returns>
        Task<List<RoleAssignedToUser>> GetRolesAssignedToUserByUserID (int userID, bool enableTracking = false);

        Task<RoleAssignedToUser?> GetRoleAssignedToUserByForeignKeys (int userID, int roleID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un rol de usuario existente por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserUpdate">Actualización del rol de usuario asociado.</param>
        /// <returns>La tarea que representa la operación asincrónica, con el rol de usuario actualizado o null si no se encontró como resultado.</returns>
        Task<RoleAssignedToUser> UpdateRoleAssignedToUser (Partial<RoleAssignedToUser> roleAssignedToUserUpdate);

        /// <summary>
        /// Elimina un rol de usuario por su ID de forma asíncrona.
        /// </summary>
        /// <param name="roleAssignedToUserID">El ID del rol de usuario a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<RoleAssignedToUser> DeleteRoleAssignedToUserByID (int ID_roleAssignedToUser);

    }

}