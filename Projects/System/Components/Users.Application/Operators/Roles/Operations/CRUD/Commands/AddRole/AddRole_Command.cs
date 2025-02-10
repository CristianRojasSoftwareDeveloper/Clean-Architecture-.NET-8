using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.AddRole;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.AddRole {

    /// <summary>
    /// Comando para agregar un nuevo rol.
    /// </summary>
    [RequiredPermissions([SystemPermissions.AddRole, SystemPermissions.AddEntity])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class AddRole_Command : IAddRole_Command {

        /// <summary>
        /// Obtiene el rol que se va a agregar.
        /// </summary>
        public Role Entity { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el rol especificado.
        /// </summary>
        /// <param name="role">El rol a agregar.</param>
        public AddRole_Command (Role role) => Entity = role;

    }

}
