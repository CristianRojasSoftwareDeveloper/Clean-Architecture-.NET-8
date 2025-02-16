
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

using SharedKernel.Domain.Models.Abstractions.Attributes;

namespace SharedKernel.Domain.Models.Abstractions.Enumerations {

    /// <summary>
    /// Define los permisos de acceso disponibles en el sistema utilizando una estructura de bits.
    /// Esta enumeración permite gestionar los distintos niveles de acceso que pueden ser asignados a los usuarios.
    /// </summary>
    /// <remarks>
    /// Los permisos están organizados en las siguientes categorías:
    /// <list type="bullet">
    /// <item><description>Permisos Genéricos (permisos 1-5)</description></item>
    /// <item><description>Gestión de Usuarios (permisos 6-15)</description></item>
    /// <item><description>Gestión de Roles (permisos 16-23)</description></item>
    /// <item><description>Gestión de Permisos de Acceso (permisos 24-29)</description></item>
    /// <item><description>Gestión de Registros del Sistema (permisos 30-35)</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Ejemplos de uso:
    /// <code>
    ///
    /// // Agregar un nuevo permiso a la lista
    /// permisos.Add(SystemPermissions.UpdateUser);
    ///
    /// // Crear una lista de permisos
    /// var permisos = [
    ///     SystemPermissions.GetUsers,
    ///     SystemPermissions.RegisterUser,
    ///     SystemPermissions.AddRole
    /// ];
    ///
    /// // Agregar múltiples permisos a la lista usando AddRange
    /// permisos.AddRange([
    ///     SystemPermissions.UpdateUser,
    ///     SystemPermissions.DeleteUserByID
    /// ]);
    ///
    /// // Verificar si un permiso específico está en la lista
    /// if (permisos.Contains(SystemPermissions.GetUsers)) {
    ///     // El usuario tiene permiso para listar usuarios
    /// }
    ///
    /// // Filtrar permisos que comienzan con 'Get'
    /// var permisosGet = permisos.Where(p => p.ToString().StartsWith("Get")).ToList();
    ///
    /// // Verificar si el usuario tiene todos los permisos necesarios para registrar un usuario y agregar un rol
    /// var requiredPermissions = new[] { SystemPermissions.RegisterUser, SystemPermissions.AddRole };
    /// if (requiredPermissions.All(rp => permisos.Contains(rp))) {
    ///     // El usuario tiene todos los permisos necesarios
    /// }
    ///
    /// // Contar cuántos permisos tiene el usuario
    /// int count = permisos.Count;
    /// Console.WriteLine($"El usuario tiene {count} permisos.");
    ///
    /// // Mostrar todos los permisos disponibles
    /// foreach (var permiso in permisos) {
    ///     Console.WriteLine(permiso);
    /// }
    /// </code>
    /// </example>
    public enum SystemPermissions {

        /// <summary>
        /// Representa ningún permiso.
        /// </summary>
        [Permission("Ningún permiso")]
        None,

        #region Operaciones Genéricas

        #region Queries
        /// <summary>
        /// Permite obtener una lista paginada de entidades con filtrado y ordenamiento.
        /// </summary>
        [Permission("Permiso para obtener entidades")]
        GetEntities,

        /// <summary>
        /// Permite obtener una entidad específica por su ID.
        /// </summary>
        [Permission("Permiso para obtener entidades por ID")]
        GetEntityByID,

        #endregion

        #region Commands
        /// <summary>
        /// Permite crear una nueva entidad en el sistema.
        /// </summary>
        [Permission("Permiso para agregar entidades")]
        AddEntity,

        /// <summary>
        /// Permite actualizar una entidad existente.
        /// </summary>
        [Permission("Permiso para actualizar entidades")]
        UpdateEntity,

        /// <summary>
        /// Permite eliminar una entidad por su ID.
        /// </summary>
        [Permission("Permiso para eliminar entidades")]
        DeleteEntityByID,
        #endregion

        #endregion

        #region Gestión de Usuarios

        #region Queries
        /// <summary>
        /// Permite obtener la lista de usuarios del sistema.
        /// </summary>
        [Permission("Permiso para listar usuarios")]
        GetUsers,

        /// <summary>
        /// Permite obtener un usuario por su ID.
        /// </summary>
        [Permission("Permiso para obtener usuarios por ID")]
        GetUserByID,

        /// <summary>
        /// Permite obtener un usuario por su nombre de usuario.
        /// </summary>
        [Permission("Permiso para obtener usuarios por nombre de usuario")]
        GetUserByUsername,

        /// <summary>
        /// Permite obtener el usuario actual por su token JWT.
        /// </summary>
        [Permission("Permiso para obtener usuarios por token JWT")]
        GetUserByToken,

        /// <summary>
        /// Permite autenticar un usuario con sus credenciales.
        /// </summary>
        [Permission("Permiso para autenticar usuarios")]
        AuthenticateUser,
        #endregion

        #region Commands
        /// <summary>
        /// Permite registrar un nuevo usuario en el sistema.
        /// </summary>
        [Permission("Permiso para registrar usuarios")]
        RegisterUser,

        /// <summary>
        /// Permite actualizar la información de un usuario.
        /// </summary>
        [Permission("Permiso para actualizar usuarios")]
        UpdateUser,

        /// <summary>
        /// Permite reactivar un usuario que fue previamente desactivado, restaurando su estado activo.
        /// </summary>
        [Permission("Permiso para activar usuarios")]
        ActivateUserByID,

        /// <summary>
        /// Permite desactivar un usuario, restringiendo su acceso sin eliminarlo del sistema.
        /// </summary>
        [Permission("Permiso para desactivar usuarios")]
        DeactivateUserByID,

        /// <summary>
        /// Permite eliminar un usuario por su ID.
        /// </summary>
        [Permission("Permiso para eliminar usuarios")]
        DeleteUserByID,

        /// <summary>
        /// Permite asignar un rol a un usuario.
        /// </summary>
        [Permission("Permiso para asignar roles a usuarios")]
        AddRoleToUser,

        /// <summary>
        /// Permite revocar un rol de un usuario.
        /// </summary>
        [Permission("Permiso para revocar roles de usuarios")]
        RemoveRoleFromUser,
        #endregion

        #endregion

        #region Gestión de Roles

        #region Queries
        /// <summary>
        /// Permite obtener la lista de roles del sistema.
        /// </summary>
        [Permission("Permiso para listar roles")]
        GetRoles,

        /// <summary>
        /// Permite obtener un rol por su ID.
        /// </summary>
        [Permission("Permiso para obtener roles por ID")]
        GetRoleByID,

        /// <summary>
        /// Permite obtener los roles de un usuario.
        /// </summary>
        [Permission("Permiso para obtener roles de usuarios")]
        GetRolesByUserID,
        #endregion

        #region Commands
        /// <summary>
        /// Permite crear un nuevo rol en el sistema.
        /// </summary>
        [Permission("Permiso para agregar roles")]
        AddRole,

        /// <summary>
        /// Permite actualizar un rol existente.
        /// </summary>
        [Permission("Permiso para actualizar roles")]
        UpdateRole,

        /// <summary>
        /// Permite eliminar un rol por su ID.
        /// </summary>
        [Permission("Permiso para eliminar roles")]
        DeleteRoleByID,

        /// <summary>
        /// Permite asignar un permiso a un rol.
        /// </summary>
        [Permission("Permiso para asignar permisos a roles")]
        AddPermissionToRole,

        /// <summary>
        /// Permite revocar un permiso de un rol.
        /// </summary>
        [Permission("Permiso para revocar permisos de roles")]
        RemovePermissionFromRole,
        #endregion

        #endregion

        #region Gestión de Permisos de Acceso

        #region Queries
        /// <summary>
        /// Permite obtener la lista de permisos del sistema.
        /// </summary>
        [Permission("Permiso para listar permisos")]
        GetPermissions,

        /// <summary>
        /// Permite obtener un permiso por su ID.
        /// </summary>
        [Permission("Permiso para obtener permisos por ID")]
        GetPermissionByID,

        /// <summary>
        /// Permite obtener los permisos de un rol.
        /// </summary>
        [Permission("Permiso para obtener permisos de roles")]
        GetPermissionsByRoleID,
        #endregion

        #region Commands
        /// <summary>
        /// Permite crear un nuevo permiso en el sistema.
        /// </summary>
        [Permission("Permiso para agregar permisos")]
        AddPermission,

        /// <summary>
        /// Permite actualizar un permiso existente.
        /// </summary>
        [Permission("Permiso para actualizar permisos")]
        UpdatePermission,

        /// <summary>
        /// Permite eliminar un permiso por su ID.
        /// </summary>
        [Permission("Permiso para eliminar permisos")]
        DeletePermissionByID,
        #endregion

        #endregion

        #region Gestión de Registros del Sistema

        #region Queries
        /// <summary>
        /// Permite obtener los registros de log del sistema.
        /// </summary>
        [Permission("Permiso para obtener registros de log")]
        GetSystemLogs,

        /// <summary>
        /// Permite obtener un registro de log por su ID.
        /// </summary>
        [Permission("Permiso para obtener registros de log por ID")]
        GetSystemLogByID,

        /// <summary>
        /// Permite obtener los registros de log de un usuario.
        /// </summary>
        [Permission("Permiso para obtener registros de log de usuarios")]
        GetSystemLogsByUserID,
        #endregion

        #region Commands
        /// <summary>
        /// Permite agregar un nuevo registro de log.
        /// </summary>
        [Permission("Permiso para agregar registros de log")]
        AddSystemLog,

        /// <summary>
        /// Permite actualizar un registro de log existente.
        /// </summary>
        [Permission("Permiso para actualizar registros de log")]
        UpdateSystemLog,

        /// <summary>
        /// Permite eliminar un registro de log por su ID.
        /// </summary>
        [Permission("Permiso para eliminar registros de log")]
        DeleteSystemLogByID
        #endregion

        #endregion

    }

}