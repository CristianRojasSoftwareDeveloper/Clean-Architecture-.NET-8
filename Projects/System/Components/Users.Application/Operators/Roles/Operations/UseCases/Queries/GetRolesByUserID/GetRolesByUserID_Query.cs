using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID {

    /// <summary>
    /// Consulta para obtener los roles de un usuario por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetRolesByUserID, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class GetRolesByUserID_Query : IGetRolesByUserID_Query {

        /// <summary>
        /// Obtiene el ID del usuario para el que se obtendrán los roles.
        /// </summary>
        public int UserID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener los roles de un usuario por su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario para el que se obtendrán los roles.</param>
        public GetRolesByUserID_Query (int userID, bool enableTracking = false) {
            UserID = userID;
            EnableTracking = enableTracking;
        }

    }

}