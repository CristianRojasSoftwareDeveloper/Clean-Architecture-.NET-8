using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.DeleteUserByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries.GetUserByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries.GetUsers;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.ActivateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByUsername;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.Users;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using Users.Application.Operators.Users.Operations.UseCases.Commands.ActivateUser;
using Users.Application.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;
using Users.Application.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID;
using Users.Application.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser;
using Users.Application.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByUsername;

namespace Users.Application.Operators.Users {

    /// <summary>
    /// Implementación de la interfaz <see cref="IUserOperationHandlerFactory"/>.
    /// Fábrica de manejadores de operaciones para usuarios del sistema.
    /// </summary>
    /// <remarks>
    /// El constructor inicializa la fábrica de manejadores de operaciones de usuario con el servicio de autenticación.
    /// </remarks>
    public class UserOperationHandlerFactory (IAuthService authService) : GenericOperationHandlerFactory<User>, IUserOperationHandlerFactory {

        private readonly IAuthService _authService = authService;

        #region Queries (Consultas)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAuthenticateUser_Query))]
        public IAuthenticateUser_QueryHandler Create_AuthenticateUser_QueryHandler (IUnitOfWork unitOfWork) => new AuthenticateUser_QueryHandler(_authService, unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetUserByID_Query))]
        public IGetEntityByID_QueryHandler<User> Create_GetUserByID_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetUsers_Query))]
        public IGetEntities_QueryHandler<User> Create_GetUsers_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntities_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetUserByUsername_Query))]
        public IGetUserByUsername_QueryHandler Create_GetUserByUsername_QueryHandler (IUnitOfWork unitOfWork) => new GetUserByUsername_QueryHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetUserByToken_Query))]
        public IGetUserByToken_QueryHandler Create_GetUserByToken_QueryHandler (IUnitOfWork unitOfWork) => new GetUserByToken_QueryHandler(_authService, unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IRegisterUser_Command))]
        public IRegisterUser_CommandHandler Create_RegisterUser_CommandHandler (IUnitOfWork unitOfWork) =>
            // Crea una instancia del manejador para la operación de asignación de roles, utilizando la misma unidad de trabajo.
            new RegisterUser_CommandHandler(_authService, unitOfWork, Create_AddRoleToUser_CommandHandler(unitOfWork));

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IUpdateUser_Command))]
        public IUpdateUser_CommandHandler Create_UpdateUser_CommandHandler (IUnitOfWork unitOfWork) => new UpdateUser_CommandHandler(_authService, unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IActivateUserByID_Command))]
        public IActivateUserByID_CommandHandler Create_ActivateUserByID_CommandHandler (IUnitOfWork unitOfWork) => new ActivateUserByID_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeactivateUserByID_Command))]
        public IDeactivateUserByID_CommandHandler Create_DeactivateUserByID_CommandHandler (IUnitOfWork unitOfWork) => new DeactivateUserByID_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeleteUserByID_Command))]
        public IDeleteEntityByID_CommandHandler<User> Create_DeleteUserByID_CommandHandler (IUnitOfWork unitOfWork) => Create_DeleteEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddRoleToUser_Command))]
        public IAddRoleToUser_CommandHandler Create_AddRoleToUser_CommandHandler (IUnitOfWork unitOfWork) => new AddRoleToUser_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IRemoveRoleFromUser_Command))]
        public IRemoveRoleFromUser_CommandHandler Create_RemoveRoleFromUser_CommandHandler (IUnitOfWork unitOfWork) => new RemoveRoleFromUser_CommandHandler(unitOfWork);

        #endregion

    }

}