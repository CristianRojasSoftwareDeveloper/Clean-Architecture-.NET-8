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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.AddRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.DeleteRoleByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Queries.GetRoleByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Queries.GetRoles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Roles.Operations.CRUD.Commands.AddRole;
using Users.Application.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using Users.Application.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole;
using Users.Application.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole;
using Users.Application.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID;

namespace Users.Application.Operators.Roles {

    /// <summary>
    /// Implementación de la interfaz <see cref="IRoleOperationHandlerFactory"/>.
    /// Fábrica de manejadores de operaciones para roles de usuario del sistema.
    /// </summary>
    public class RoleOperationHandlerFactory : GenericOperationHandlerFactory<Role>, IRoleOperationHandlerFactory {

        // Si fuera necesario inyectar otros servicios, se pueden agregar al constructor.
        //public RoleOperationHandlerFactory () : base() { }

        #region Queries (Consultas)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetRoleByID_Query))]
        public IGetEntityByID_QueryHandler<Role> Create_GetRoleByID_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetRoles_Query))]
        public IGetEntities_QueryHandler<Role> Create_GetRoles_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntities_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetRolesByUserID_Query))]
        public IGetRolesByUserID_QueryHandler Create_GetRolesByUserID_QueryHandler (IUnitOfWork unitOfWork) => new GetRolesByUserID_QueryHandler(unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddRole_Command))]
        public IAddRole_CommandHandler Create_AddRole_CommandHandler (IUnitOfWork unitOfWork) => new AddRole_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IUpdateRole_Command))]
        public IUpdateRole_CommandHandler Create_UpdateRole_CommandHandler (IUnitOfWork unitOfWork) => new UpdateRole_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeleteRoleByID_Command))]
        public IDeleteEntityByID_CommandHandler<Role> Create_DeleteRoleByID_CommandHandler (IUnitOfWork unitOfWork) => Create_DeleteEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddPermissionToRole_Command))]
        public IAddPermissionToRole_CommandHandler Create_AddPermissionToRole_CommandHandler (IUnitOfWork unitOfWork) => new AddPermissionToRole_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IRemovePermissionFromRole_Command))]
        public IRemovePermissionFromRole_CommandHandler Create_RemovePermissionFromRole_CommandHandler (IUnitOfWork unitOfWork) => new RemovePermissionFromRole_CommandHandler(unitOfWork);

        #endregion

    }

}