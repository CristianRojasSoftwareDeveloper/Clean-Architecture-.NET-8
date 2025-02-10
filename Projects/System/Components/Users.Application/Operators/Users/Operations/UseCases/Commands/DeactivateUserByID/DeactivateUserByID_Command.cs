using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID {

    /// <summary>
    /// Comando para desactivar un usuario en el sistema.
    /// </summary>
    [RequiredPermissions(SystemPermissions.DeactivateUserByID, SystemPermissions.UpdateEntity)]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class DeactivateUserByID_Command (int userID) : IDeactivateUserByID_Command {

        public int UserID { get; } = userID;

    }

}