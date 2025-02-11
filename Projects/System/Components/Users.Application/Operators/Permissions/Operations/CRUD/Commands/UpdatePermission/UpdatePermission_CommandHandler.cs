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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission {

    /// <summary>
    /// Manejador para el comando de actualización de permiso.
    /// </summary>
    public class UpdatePermission_CommandHandler : IUpdatePermission_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermission_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        ///// <summary>
        ///// Maneja el comando de actualización de un permiso de forma asíncrona.
        ///// </summary>
        ///// <param name="command">El comando que contiene la actualización del permiso.</param>
        ///// <returns>Una tarea que representa la operación asíncrona y contiene el permiso actualizado.</returns>
        public async Task<Permission> Handle (IUpdatePermission_Command command) {

            // Verifica si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si la instancia Partial<Permission> es nula
            if (command.EntityUpdate == null)
                throw BadRequestError.Create("La actualización del permiso de usuario no puede ser nula.");

            var permissionUpdate = command.EntityUpdate;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Valida el identificador del permiso.
            if (!permissionUpdate.ID.HasValue || (int) permissionUpdate.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(Permission.ID), "El identificador del permiso de usuario no es válido."));

            // Si está presente, valida el nombre del permiso de usuario.
            var nameProperty = nameof(Permission.Name);
            if (permissionUpdate.Properties.TryGetValue(nameProperty, out var nameValue)) {
                var name = nameValue as string;
                if (string.IsNullOrWhiteSpace(name))
                    validationErrors.Add(ValidationError.Create(nameProperty, "El nombre del permiso de usuario no puede estar vacío."));
                else if (await _unitOfWork.PermissionRepository.FirstOrDefault(permission => permission.Name!.Equals(name)) != null)
                    validationErrors.Add(ValidationError.Create(nameProperty, $"El nombre del permiso de usuario «{name}» ya existe."));
            }

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecuta la actualización del permiso de usuario de forma asíncrona.
            return await _unitOfWork.PermissionRepository.UpdatePermission(permissionUpdate);

        }

    }

}