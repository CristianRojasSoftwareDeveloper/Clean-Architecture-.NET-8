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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.AddRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.AddRole {

    /// <summary>
    /// Manejador del comando <see cref="AddRole_Command"/> que agrega un nuevo rol.
    /// </summary>
    public class AddRole_CommandHandler : IAddRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador con el repositorio de roles especificado.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando de manera asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la información del rol a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el rol agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el rol son nulos, o si hay errores de validación.</exception>
        public async Task<Role> Handle (IAddRole_Command command) {
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            if (command.Entity == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            var validationErrors = new List<ApplicationError>();

            if (string.IsNullOrWhiteSpace(command.Entity.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), "El nombre del rol no puede ser nulo o vacío"));
            else if (await _unitOfWork.RoleRepository.FirstOrDefault(role => role.Name!.Equals(command.Entity.Name)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), $"El nombre del rol '{command.Entity.Name}' ya existe"));

            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            return await _unitOfWork.RoleRepository.AddRole(command.Entity);
        }

    }

}