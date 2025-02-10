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