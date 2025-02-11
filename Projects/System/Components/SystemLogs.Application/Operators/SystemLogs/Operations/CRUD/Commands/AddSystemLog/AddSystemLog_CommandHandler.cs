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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog {

    /// <summary>
    /// Manejador para el comando de agregar un nuevo log de sistema.
    /// </summary>
    public class AddSystemLog_CommandHandler : IAddSystemLog_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de logs de sistema.</param>
        public AddSystemLog_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja de forma asíncrona el comando para agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log de sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el log de sistema agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el log de sistema son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public Task<SystemLog> Handle (IAddSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.Entity == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si las propiedades de Entity contienen valores no vacíos y válidos.
            if (command.Entity.LogLevel == null)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.LogLevel), "El nivel de severidad del registro no puede ser nulo"));
            if (string.IsNullOrWhiteSpace(command.Entity.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Source), "El origen del registro no puede ser nulo o vacío"));
            if (string.IsNullOrWhiteSpace(command.Entity.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Message), "El mensaje del registro no puede ser nulo o vacío"));
            if (command.Entity.UserID != null && command.Entity.UserID.Value <= 0)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.UserID), $"No es posible asociar el registro del sistema con un identificador de usuario negativo [{command.Entity.UserID}]."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el log de sistema de forma asíncrona y devolverlo
            return _unitOfWork.SystemLogRepository.AddSystemLog(command.Entity);
        }

    }

}