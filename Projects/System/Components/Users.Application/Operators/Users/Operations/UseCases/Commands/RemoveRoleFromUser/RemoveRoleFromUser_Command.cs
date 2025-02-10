using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser {

    /// <summary>
    /// Comando para eliminar un rol de un usuario.
    /// </summary>
    [RequiredPermissions([SystemPermissions.RemoveRoleFromUser])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class RemoveRoleFromUser_Command : IRemoveRoleFromUser_Command {

        /// <summary>
        /// Obtiene el ID del usuario del que se eliminará el rol.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Obtiene el ID del rol que se eliminará del usuario.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar un rol de un usuario.
        /// </summary>
        /// <param name="userID">El ID del usuario del que se eliminará el rol.</param>
        /// <param name="roleID">El ID del rol que se eliminará del usuario.</param>
        public RemoveRoleFromUser_Command (int userID, int roleID) {
            UserID = userID;
            RoleID = roleID;
        }

    }

}