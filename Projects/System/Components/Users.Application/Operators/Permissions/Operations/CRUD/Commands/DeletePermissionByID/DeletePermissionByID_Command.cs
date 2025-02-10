using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.DeletePermissionByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.DeletePermissionByID {

    /// <summary>
    /// Comando para eliminar un permiso por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.DeletePermissionByID, SystemPermissions.DeleteEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IPermissionOperationHandlerFactory))]
    public class DeletePermissionByID_Command : IDeletePermissionByID_Command {

        /// <summary>
        /// Obtiene el ID del permiso que se va a eliminar.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del permiso especificado.
        /// </summary>
        /// <param name="permissionID">El ID del permiso que se va a eliminar.</param>
        public DeletePermissionByID_Command (int permissionID) {
            ID = permissionID;
        }

    }

}