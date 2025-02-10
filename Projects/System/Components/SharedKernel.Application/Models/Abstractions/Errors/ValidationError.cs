using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error de validación en la aplicación.
    /// Incluye el nombre de la propiedad que causó el Error de validación y un mensaje de Error detallado.
    /// </summary>
    public class ValidationError : ApplicationError {

        /// <summary>
        /// Nombre de la propiedad que causó el Error de validación.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Mensaje de Error detallado.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Constructor de la clase ValidationError.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que causó el Error de validación.</param>
        /// <param name="errorMessage">Mensaje de Error detallado.</param>
        private ValidationError (string propertyName, string errorMessage) : base(HttpStatusCode.BadRequest, $"[{nameof(ValidationError)} » {propertyName}]: {errorMessage}") {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Método estático para crear una instancia de ValidationError.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que causó el Error de validación.</param>
        /// <param name="errorMessage">Mensaje de Error detallado.</param>
        /// <returns>Una nueva instancia de ValidationError.</returns>
        public static ValidationError Create (string propertyName, string errorMessage) => new(propertyName, errorMessage);

    }

}