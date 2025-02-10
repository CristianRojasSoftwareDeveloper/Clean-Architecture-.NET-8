using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.AddEntity {

    /// <summary>
    /// Comando para agregar una nueva entidad.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a agregar.</typeparam>
    [RequiredPermissions(SystemPermissions.AddEntity)]
    public class AddEntity_Command<EntityType> : IAddEntity_Command<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Obtiene la entidad que se va a agregar.
        /// </summary>
        public EntityType Entity { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para agregar una entidad.
        /// </summary>
        /// <param name="entity">La entidad que se va a agregar.</param>
        public AddEntity_Command (EntityType entity) => Entity = entity ?? throw new ArgumentNullException(nameof(entity));

    }

}