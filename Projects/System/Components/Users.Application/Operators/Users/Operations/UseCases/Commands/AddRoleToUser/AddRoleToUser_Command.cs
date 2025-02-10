using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.AddRoleToUser {

    /// <summary>
    /// Comando para agregar un rol a un usuario.
    /// </summary>
    [RequiredPermissions([SystemPermissions.AddRoleToUser])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class AddRoleToUser_Command : IAddRoleToUser_Command {

        /// <summary>
        /// Obtiene el ID del usuario al que se le agregará el rol.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Obtiene el ID del rol que se agregará al usuario.
        /// </summary>
        public int RoleID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para agregar un rol a un usuario.
        /// </summary>
        /// <param name="userID">El ID del usuario al que se le agregará el rol.</param>
        /// <param name="roleID">El ID del rol que se agregará al usuario.</param>
        public AddRoleToUser_Command (int userID, int roleID) {
            UserID = userID;
            RoleID = roleID;
        }

    }

}