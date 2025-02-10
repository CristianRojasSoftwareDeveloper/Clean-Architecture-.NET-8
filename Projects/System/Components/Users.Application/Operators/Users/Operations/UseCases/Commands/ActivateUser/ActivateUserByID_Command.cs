using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.ActivateUser;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.ActivateUser {

    /// <summary>
    /// Comando para activar un usuario en el sistema.
    /// </summary>
    [RequiredPermissions(SystemPermissions.ActivateUserByID, SystemPermissions.UpdateEntity)]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class ActivateUserByID_Command (int userID) : IActivateUserByID_Command {

        public int UserID { get; } = userID;

    }

}