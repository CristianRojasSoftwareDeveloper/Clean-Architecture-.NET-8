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