using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogs;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogs {

    /// <summary>
    /// Consulta para obtener todos los logs del sistema.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetSystemLogs, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class GetSystemLogs_Query : IGetSystemLogs_Query {

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los registros de sistema.
        /// </summary>
        public GetSystemLogs_Query (bool enableTracking = false) => EnableTracking = enableTracking;

    }

}