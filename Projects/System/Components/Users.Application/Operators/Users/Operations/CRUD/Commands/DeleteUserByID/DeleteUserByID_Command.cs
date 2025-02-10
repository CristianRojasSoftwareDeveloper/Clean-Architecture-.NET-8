using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.DeleteUserByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.DeleteUserByID {

    /// <summary>
    /// Comando para eliminar un usuario por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.DeleteUserByID, SystemPermissions.DeleteEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class DeleteUserByID_Command : IDeleteUserByID_Command {

        /// <summary>
        /// Obtiene el ID del usuario a eliminar.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario a eliminar.</param>
        public DeleteUserByID_Command (int userID) => ID = userID;

    }

}