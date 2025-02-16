
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