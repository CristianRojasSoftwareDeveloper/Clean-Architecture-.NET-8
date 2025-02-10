using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogByID {

    /// <summary>
    /// Consulta para obtener un log del sistema por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetSystemLogByID, SystemPermissions.GetEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class GetSystemLogByID_Query : IGetSystemLogByID_Query {

        /// <summary>
        /// ID del log del sistema que se intenta obtener.
        /// </summary>
        public int ID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del log del sistema especificado.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema que se desea recuperar.</param>
        public GetSystemLogByID_Query (int systemLogID, bool enableTracking = false) {
            ID = systemLogID;
            EnableTracking = enableTracking;
        }

    }

}