using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.DeletePermissionByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries.GetPermissionByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries.GetPermissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using Users.Application.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using Users.Application.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using Users.Application.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID;

namespace Users.Application.Operators.Permissions {

    /// <summary>
    /// Implementación de la interfaz <see cref="IPermissionOperationHandlerFactory"/>.
    /// Fábrica de manejadores de operaciones para permisos del sistema.
    /// </summary>
    public class PermissionOperationHandlerFactory : GenericOperationHandlerFactory<Permission>, IPermissionOperationHandlerFactory {

        // Si fuera necesario inyectar otros servicios, se pueden agregar al constructor.
        //public PermissionOperationHandlerFactory () : base() { }

        #region Queries (Consultas)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetPermissionByID_Query))]
        public IGetEntityByID_QueryHandler<Permission> Create_GetPermissionByID_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetPermissions_Query))]
        public IGetEntities_QueryHandler<Permission> Create_GetPermissions_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntities_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetPermissionsByRoleID_Query))]
        public IGetPermissionsByRoleID_QueryHandler Create_GetPermissionsByRoleID_QueryHandler (IUnitOfWork unitOfWork) => new GetPermissionsByRoleID_QueryHandler(unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddPermission_Command))]
        public IAddPermission_CommandHandler Create_AddPermission_CommandHandler (IUnitOfWork unitOfWork) => new AddPermission_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IUpdatePermission_Command))]
        public IUpdatePermission_CommandHandler Create_UpdatePermission_CommandHandler (IUnitOfWork unitOfWork) => new UpdatePermission_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeletePermissionByID_Command))]
        public IDeleteEntityByID_CommandHandler<Permission> Create_DeletePermissionByID_CommandHandler (IUnitOfWork unitOfWork) => Create_DeleteEntityByID_Handler(unitOfWork);

        #endregion

    }

}