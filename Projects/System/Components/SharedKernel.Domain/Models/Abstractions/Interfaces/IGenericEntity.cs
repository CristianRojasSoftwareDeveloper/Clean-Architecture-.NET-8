namespace SharedKernel.Domain.Models.Abstractions.Interfaces {

    /// <summary>
    /// Define la interfaz para una entidad genérica que contiene un identificador único.
    /// </summary>
    public interface IGenericEntity {

        /// <summary>
        /// Obtiene o establece el identificador único de la entidad.
        /// </summary>
        int? ID { get; set; }

    }

}