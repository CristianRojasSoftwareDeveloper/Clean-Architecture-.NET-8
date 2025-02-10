using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.DeleteRoleByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.DeleteRoleByID {

    /// <summary>
    /// Representa un comando para eliminar un rol específico por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.DeleteRoleByID, SystemPermissions.DeleteEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class DeleteRoleByID_Command : IDeleteRoleByID_Command {

        /// <summary>
        /// Obtiene el ID del rol que se va a eliminar.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol que se va a eliminar.</param>
        public DeleteRoleByID_Command (int roleID) {
            ID = roleID;
        }

    }

}