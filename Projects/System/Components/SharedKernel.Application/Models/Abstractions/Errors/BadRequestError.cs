using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error 400 Bad Operation.
    /// </summary>
    public class BadRequestError : ApplicationError {

        /// <summary>
        /// Constructor de la clase BadRequestError.
        /// </summary>
        /// <param name="message">Mensaje de error detallado.</param>
        private BadRequestError (string message) : base(HttpStatusCode.BadRequest, message) { }

        /// <summary>
        /// Método estático para crear una instancia de BadRequestError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <returns>Una nueva instancia de BadRequestError.</returns>
        public static BadRequestError Create (string message) => new(message);

    }

}