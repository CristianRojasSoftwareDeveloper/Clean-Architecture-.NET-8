using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.ActivateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByUsername;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users {

    /// <summary>
    /// Interfaz que define operaciones para la gestión de usuarios en el sistema.
    /// Permite realizar autenticación, consultas y modificaciones sobre usuarios y sus roles.
    /// </summary>
    public interface IUserOperationHandlerFactory : IOperationHandlerFactory {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para autenticar un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAuthenticateUser_QueryHandler"/>.</returns>
        IAuthenticateUser_QueryHandler Create_AuthenticateUser_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener un usuario por su identificador.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{User}"/>.</returns>
        IGetEntityByID_QueryHandler<User> Create_GetUserByID_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener una lista de usuarios.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{User}"/>.</returns>
        IGetEntities_QueryHandler<User> Create_GetUsers_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetUserByUsername_QueryHandler"/>.</returns>
        IGetUserByUsername_QueryHandler Create_GetUserByUsername_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener un usuario mediante un token de autenticación.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetUserByToken_QueryHandler"/>.</returns>
        IGetUserByToken_QueryHandler Create_GetUserByToken_QueryHandler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para registrar un nuevo usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IRegisterUser_CommandHandler"/>.</returns>
        IRegisterUser_CommandHandler Create_RegisterUser_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar un usuario existente.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IUpdateUser_CommandHandler"/>.</returns>
        IUpdateUser_CommandHandler Create_UpdateUser_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para activar un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IActivateUserByID_CommandHandler"/>.</returns>
        IActivateUserByID_CommandHandler Create_ActivateUserByID_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para desactivar un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IDeactivateUserByID_CommandHandler"/>.</returns>
        IDeactivateUserByID_CommandHandler Create_DeactivateUserByID_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un usuario por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{User}"/>.</returns>
        IDeleteEntityByID_CommandHandler<User> Create_DeleteUserByID_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para asignar un rol a un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAddRoleToUser_CommandHandler"/>.</returns>
        IAddRoleToUser_CommandHandler Create_AddRoleToUser_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un rol de un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IRemoveRoleFromUser_CommandHandler"/>.</returns>
        IRemoveRoleFromUser_CommandHandler Create_RemoveRoleFromUser_CommandHandler (IUnitOfWork unitOfWork);

        #endregion

    }

}