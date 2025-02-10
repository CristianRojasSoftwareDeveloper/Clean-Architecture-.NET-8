using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID {

    /// <summary>
    /// Consulta para obtener los logs del sistema asociados a un usuario según su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetSystemLogsByUserID, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class GetSystemLogsByUserID_Query : IGetSystemLogsByUserID_Query {

        /// <summary>
        /// ID del usuario del cual se intentan obtener logs asociados.
        /// </summary>
        public int UserID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del usuario especificado.
        /// </summary>
        /// <param name="userID">ID del usuario del cual se buscarán logs asociados.</param>
        public GetSystemLogsByUserID_Query (int userID, bool enableTracking = false) {
            UserID = userID;
            EnableTracking = enableTracking;
        }

    }

}