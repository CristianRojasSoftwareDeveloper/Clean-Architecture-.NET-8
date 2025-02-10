using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions {

    /// <summary>
    /// Interfaz que define operaciones para la gestión de permisos en el sistema.
    /// </summary>
    public interface IPermissionOperationHandlerFactory : IOperationHandlerFactory {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para obtener un permiso por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{Permission}"/>.</returns>
        IGetEntityByID_QueryHandler<Permission> Create_GetPermissionByID_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener todos los permisos.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{Permission}"/>.</returns>
        IGetEntities_QueryHandler<Permission> Create_GetPermissions_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener los permisos asociados a un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetPermissionsByRoleID_QueryHandler"/>.</returns>
        IGetPermissionsByRoleID_QueryHandler Create_GetPermissionsByRoleID_QueryHandler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para agregar un permiso.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAddPermission_CommandHandler"/>.</returns>
        IAddPermission_CommandHandler Create_AddPermission_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar un permiso existente.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IUpdatePermission_CommandHandler"/>.</returns>
        IUpdatePermission_CommandHandler Create_UpdatePermission_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un permiso por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{Permission}"/>.</returns>
        IDeleteEntityByID_CommandHandler<Permission> Create_DeletePermissionByID_CommandHandler (IUnitOfWork unitOfWork);

        #endregion

    }

}