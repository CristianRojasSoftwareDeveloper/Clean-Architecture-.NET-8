using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error 404 Not Found.
    /// </summary>
    public class NotFoundError : ApplicationError {

        /// <summary>
        /// El identificador del recurso no encontrado, si está disponible.
        /// </summary>
        public int? ID { get; }

        /// <summary>
        /// El nombre del tipo de recurso o entidad que no se encontró.
        /// </summary>
        public string ResourceType { get; }

        /// <summary>
        /// Constructor privado de la clase NotFoundError.
        /// </summary>
        /// <param name="resourceType">El nombre del tipo de entidad o recurso.</param>
        /// <param name="message">El mensaje de Error detallado.</param>
        /// <param name="id">El identificador del recurso no encontrado, si está disponible.</param>
        private NotFoundError (string resourceType, string message, int? id = null) : base(HttpStatusCode.NotFound, message) {
            ResourceType = resourceType;
            ID = id;
        }

        /// <summary>
        /// Método estático para crear una instancia de NotFoundError.
        /// </summary>
        /// <param name="resourceType">El nombre del tipo de entidad o recurso.</param>
        /// <param name="id">El identificador del recurso no encontrado, si está disponible.</param>
        /// <returns>Una nueva instancia de NotFoundError con o sin identificador.</returns>
        public static NotFoundError Create (string resourceType, int? id = null) {
            string message = !id.HasValue ?
                $"El recurso solicitado de tipo '{resourceType}' no ha sido encontrado." :
                $"El recurso solicitado de tipo '{resourceType}' con identificador [{id}] no ha sido encontrado.";
            return new NotFoundError(resourceType, message, id);
        }

    }

}