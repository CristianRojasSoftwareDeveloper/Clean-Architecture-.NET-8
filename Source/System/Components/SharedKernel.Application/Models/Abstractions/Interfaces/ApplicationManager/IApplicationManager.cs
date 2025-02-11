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

using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager {

    /// <summary>
    /// Interfaz que define el administrador de la aplicación, proporcionando acceso a los servicios y operadores principales.
    /// </summary>
    public interface IApplicationManager {

        /// <summary>
        /// Servicio de autenticación que maneja la autenticación y autorización de usuarios en el sistema.
        /// </summary>
        IAuthService AuthService { get; }

        /// <summary>
        /// Servicio de persistencia que proporciona una interfaz para interactuar con el almacenamiento de datos.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Operador de usuarios que orquesta los servicios de infraestructura para realizar operaciones relacionadas con usuarios, como crear, leer, actualizar y eliminar usuarios.
        /// </summary>
        IUserOperationHandlerFactory UserOperationHandlerFactory { get; }

        /// <summary>
        /// Operador de roles de usuario que orquesta los servicios de infraestructura para realizar operaciones relacionadas con roles, como crear, leer, actualizar y eliminar roles de usuario.
        /// </summary>
        IRoleOperationHandlerFactory RoleOperationHandlerFactory { get; }

        /// <summary>
        /// Operador de permisos de roles de usuario que orquesta los servicios de infraestructura para realizar operaciones relacionadas con permisos, como crear, leer, actualizar y eliminar permisos de roles de usuario.
        /// </summary>
        IPermissionOperationHandlerFactory PermissionOperationHandlerFactory { get; }

        /// <summary>
        /// Operador de registros del sistema que orquesta los servicios de infraestructura para realizar operaciones relacionadas con registros, como crear, leer, actualizar y eliminar registros del sistema.
        /// </summary>
        ISystemLogOperationHandlerFactory SystemLogOperationHandlerFactory { get; }

        /// <summary>
        /// Ejecuta una operación de manera asíncrona.
        /// </summary>
        /// <typeparam name="OperationType">El tipo de la operación a ejecutar.</typeparam>
        /// <typeparam name="ResponseType">El tipo de la respuesta esperada de la operación.</typeparam>
        /// <param name="operation">La operación que se desea ejecutar.</param>
        /// <param name="accessToken">El token de acceso utilizado para validar permisos y claims.</param>
        /// <returns>Una tarea que representa la respuesta de la operación ejecutada.</returns>
        Task<Response<ResponseType>> ExecuteOperation<OperationType, ResponseType> (OperationType operation, string? accessToken = null) where OperationType : IOperation;

    }

}