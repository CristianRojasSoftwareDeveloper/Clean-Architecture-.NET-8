using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities {

    /// <summary>
    /// Consulta para obtener todos los registros de una determinada entidad.
    /// </summary>
    [RequiredPermissions(SystemPermissions.GetEntities)]
    public class GetEntities_Query : IGetEntities_Query {

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todas las entidades.
        /// </summary>
        public GetEntities_Query (bool enableTracking = false) => EnableTracking = enableTracking;

    }

}