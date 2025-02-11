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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.DeleteSystemLogByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;

namespace SystemLogs.Application.Operators.SystemLogs {

    /// <summary>
    /// Implementación de la interfaz <see cref="ISystemLogOperationHandlerFactory"/>.
    /// Fábrica de manejadores de operaciones para registros del sistema.
    /// </summary>
    public class SystemLogOperationHandlerFactory : GenericOperationHandlerFactory<SystemLog>, ISystemLogOperationHandlerFactory {

        // Si fuera necesario inyectar otros servicios, se pueden agregar al constructor.
        //public SystemLogOperationHandlerFactory () : base() { }

        #region Queries (Consultas)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogByID_Query))]
        public IGetEntityByID_QueryHandler<SystemLog> Create_GetSystemLogByID_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogs_Query))]
        public IGetEntities_QueryHandler<SystemLog> Create_GetSystemLogs_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntities_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogsByUserID_Query))]
        public IGetSystemLogsByUserID_QueryHandler Create_GetSystemLogsByUserID_QueryHandler (IUnitOfWork unitOfWork) => new GetSystemLogsByUserID_QueryHandler(unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddSystemLog_Command))]
        public IAddSystemLog_CommandHandler Create_AddSystemLog_CommandHandler (IUnitOfWork unitOfWork) => new AddSystemLog_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IUpdateSystemLog_Command))]
        public IUpdateSystemLog_CommandHandler Create_UpdateSystemLog_CommandHandler (IUnitOfWork unitOfWork) => new UpdateSystemLog_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeleteSystemLogByID_Command))]
        public IDeleteEntityByID_CommandHandler<SystemLog> Create_DeleteSystemLogByID_CommandHandler (IUnitOfWork unitOfWork) => Create_DeleteEntityByID_Handler(unitOfWork);

        #endregion

    }

}