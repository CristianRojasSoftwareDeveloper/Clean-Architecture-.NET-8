using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries.GetUserByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.CRUD.Queries.GetUserByID {

    /// <summary>
    /// Consulta para obtener un usuario por su ID.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia de la consulta para obtener un usuario por su ID.
    /// </remarks>
    /// <param name="userID">El ID del usuario a obtener.</param>
    [RequiredPermissions([SystemPermissions.GetUserByID, SystemPermissions.GetEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class GetUserByID_Query (int userID, bool enableTracking = false) : IGetUserByID_Query {

        /// <summary>
        /// Obtiene o establece el ID del usuario.
        /// </summary>
        public int ID { get; } = userID;

        public bool EnableTracking { get; } = enableTracking;

    }

}