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

using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.AddRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles {

    /// <summary>
    /// Interfaz que define operaciones para la gestión de roles de usuarios en el sistema.
    /// </summary>
    public interface IRoleOperationHandlerFactory : IOperationHandlerFactory {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para obtener un rol por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{Role}"/>.</returns>
        IGetEntityByID_QueryHandler<Role> Create_GetRoleByID_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener todos los roles.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{Role}"/>.</returns>
        IGetEntities_QueryHandler<Role> Create_GetRoles_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener los roles asociados a un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetRolesByUserID_QueryHandler"/>.</returns>
        IGetRolesByUserID_QueryHandler Create_GetRolesByUserID_QueryHandler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para agregar un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAddRole_CommandHandler"/>.</returns>
        IAddRole_CommandHandler Create_AddRole_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar un rol existente.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IUpdateRole_CommandHandler"/>.</returns>
        IUpdateRole_CommandHandler Create_UpdateRole_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un rol por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{Role}"/>.</returns>
        IDeleteEntityByID_CommandHandler<Role> Create_DeleteRoleByID_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para agregar un permiso a un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAddPermissionToRole_CommandHandler"/>.</returns>
        IAddPermissionToRole_CommandHandler Create_AddPermissionToRole_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un permiso de un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IRemovePermissionFromRole_CommandHandler"/>.</returns>
        IRemovePermissionFromRole_CommandHandler Create_RemovePermissionFromRole_CommandHandler (IUnitOfWork unitOfWork);

        #endregion

    }

}