using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error 401 Unauthorized.
    /// </summary>
    public class UnauthorizedError : ApplicationError {

        /// <summary>
        /// Constructor de la clase UnauthorizedError.
        /// </summary>
        /// <param name="message">Mensaje de error detallado.</param>
        private UnauthorizedError (string message) : base(HttpStatusCode.Unauthorized, message) { }

        /// <summary>
        /// Método estático para crear una instancia de UnauthorizedError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>Una nueva instancia de UnauthorizedError.</returns>
        public static UnauthorizedError Create (string message = "Acceso no autorizado") => new(message);

    }

}