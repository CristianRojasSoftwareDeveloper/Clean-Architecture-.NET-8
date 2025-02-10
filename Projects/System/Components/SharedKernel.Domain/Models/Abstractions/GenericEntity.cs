using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Domain.Models.Abstractions {

    /// <summary>
    /// Representa una implementación abstracta de una entidad genérica con un identificador único.
    /// </summary>
    public abstract class GenericEntity (int? identifier = null) : IGenericEntity {

        /// <inheritdoc />
        public int? ID { get; set; } = identifier;

    }

}