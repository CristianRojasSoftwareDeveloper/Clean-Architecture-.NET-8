using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un error ocurrido durante la ejecución de una operación en la base de datos.
    /// Este error es marcado con el código 500 InternalServerError.
    /// </summary>
    public class DatabaseError : ApplicationError {

        /// <summary>
        /// Constructor de la clase DatabaseError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        private DatabaseError (string message, Exception? innerException = null) : base(HttpStatusCode.InternalServerError, message, innerException) { }

        /// <summary>
        /// Método estático para crear una instancia de DatabaseError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        /// <returns>Una nueva instancia de DatabaseError.</returns>
        public static DatabaseError Create (string message, Exception? innerException = null) => new(message, innerException);

    }

}