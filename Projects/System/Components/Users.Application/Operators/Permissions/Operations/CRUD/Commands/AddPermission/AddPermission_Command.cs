using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.AddPermission {

    /// <summary>
    /// Comando para agregar un nuevo permiso.
    /// </summary>
    [RequiredPermissions([SystemPermissions.AddPermission, SystemPermissions.AddEntity])]
    [AssociatedOperationHandlerFactory(typeof(IPermissionOperationHandlerFactory))]
    public class AddPermission_Command : IAddPermission_Command {

        /// <summary>
        /// Obtiene el permiso que se va a agregar.
        /// </summary>
        public Permission Entity { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el permiso especificado.
        /// </summary>
        /// <param name="permission">El permiso a agregar.</param>
        public AddPermission_Command (Permission permission) {
            Entity = permission;
        }

    }

}