using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity {

    /// <summary>
    /// Comando para actualizar una entidad existente.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a actualizar.</typeparam>
    /// <remarks>
    /// Inicializa una nueva instancia del comando con la entidad especificada.
    /// </remarks>
    /// <param name="entity">La entidad a actualizar.</param>
    [RequiredPermissions(SystemPermissions.UpdateEntity)]
    public class UpdateEntity_Command<EntityType> (Partial<EntityType> entityUpdate) : IUpdateEntity_Command<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene la actualización de la entidad.
        /// </summary>
        public Partial<EntityType> EntityUpdate { get; } = entityUpdate ?? throw new ArgumentNullException(nameof(entityUpdate));

    }

}