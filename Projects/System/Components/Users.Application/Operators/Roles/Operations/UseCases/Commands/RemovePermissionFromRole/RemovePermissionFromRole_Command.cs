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

using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole {

    /// <summary>
    /// Comando para eliminar un permiso de un rol.
    /// </summary>
    [RequiredPermissions([SystemPermissions.RemovePermissionFromRole])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class RemovePermissionFromRole_Command : IRemovePermissionFromRole_Command {

        /// <summary>
        /// Obtiene el ID del rol del que se eliminará el permiso.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Obtiene el ID del permiso que se eliminará del rol.
        /// </summary>
        public int PermissionID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un permiso de un rol.
        /// </summary>
        /// <param name="roleID">El ID del rol del que se eliminará el permiso.</param>
        /// <param name="permissionID">El ID del permiso que se eliminará del rol.</param>
        public RemovePermissionFromRole_Command (int roleID, int permissionID) {
            RoleID = roleID;
            PermissionID = permissionID;
        }

    }

}