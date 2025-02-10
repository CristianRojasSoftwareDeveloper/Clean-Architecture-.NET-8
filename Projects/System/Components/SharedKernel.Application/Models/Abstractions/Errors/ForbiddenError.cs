using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error 403 Forbidden.
    /// </summary>
    public class ForbiddenError : ApplicationError {

        /// <summary>
        /// Constructor de la clase ForbiddenError.
        /// </summary>
        /// <param name="message">Mensaje de Error detallado.</param>
        private ForbiddenError (string message) : base(HttpStatusCode.Forbidden, message) { }

        /// <summary>
        /// Método estático para crear una instancia de ForbiddenError.
        /// </summary>
        /// <param name="message">Mensaje de Error.</param>
        /// <returns>Una nueva instancia de ForbiddenError.</returns>
        public static ForbiddenError Create (string message) => new(message);

    }

}