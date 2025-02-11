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

using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole {

    /// <summary>
    /// Manejador para el comando de agregar un permiso a un rol.
    /// </summary>
    public class AddPermissionToRole_CommandHandler : IAddPermissionToRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador del comando para agregar un permiso a un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddPermissionToRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando para agregar un permiso a un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para agregar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el permiso de rol agregado.</returns>
        public async Task<PermissionAssignedToRole> Handle (IAddPermissionToRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));
            else if ((await _unitOfWork.RoleRepository.GetRoleByID(command.RoleID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), $"No se ha encontrado ningún rol de usuario con el identificador {command.RoleID}."));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));
            else if ((await _unitOfWork.PermissionRepository.GetPermissionByID(command.PermissionID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), $"No se ha encontrado ningún permiso de usuario con el identificador {command.PermissionID}."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Crear el nuevo permiso de rol y guardarlo en el repositorio
            var newPermissionAssignedToRole = new PermissionAssignedToRole {
                RoleID = command.RoleID,
                PermissionID = command.PermissionID
            };

            return await _unitOfWork.PermissionAssignedToRoleRepository.AddPermissionAssignedToRole(newPermissionAssignedToRole);

        }

    }

}