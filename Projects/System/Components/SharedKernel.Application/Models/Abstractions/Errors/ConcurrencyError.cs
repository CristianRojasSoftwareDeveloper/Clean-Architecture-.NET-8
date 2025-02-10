using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un error ocurrido por un conflicto producido durante la ejecución concurrente de dos o más operaciones.
    /// Este error es marcado con el código 500 InternalServerError.
    /// </summary>
    public class ConcurrencyError : ApplicationError {

        /// <summary>
        /// Constructor de la clase ConcurrencyError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        private ConcurrencyError (string message, Exception? innerException = null) : base(HttpStatusCode.InternalServerError, message, innerException) { }

        /// <summary>
        /// Método estático para crear una instancia de ConcurrencyError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        /// <returns>Una nueva instancia de ConcurrencyError.</returns>
        public static ConcurrencyError Create (string message, Exception? innerException = null) => new(message, innerException);

    }

}