using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID {

    /// <summary>
    /// Consulta para obtener una entidad por su ID.
    /// </summary>
    [RequiredPermissions(SystemPermissions.GetEntityByID)]
    public class GetEntityByID_Query : IGetEntityByID_Query {

        /// <summary>
        /// Obtiene el ID de la entidad a recuperar.
        /// </summary>
        public int ID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a consultar.</param>
        public GetEntityByID_Query (int entityID, bool enableTracking/* = false*/) {
            ID = entityID;
            EnableTracking = enableTracking;
        }

    }

}