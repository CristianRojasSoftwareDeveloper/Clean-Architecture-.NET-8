using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Clase base para todos los errores de la aplicación.
    /// Incluye un código de error y un mensaje descriptivo.
    /// </summary>
    public class ApplicationError : Exception {

        /// <summary>
        /// Código de error HTTP asociado a este error.
        /// </summary>
        public HttpStatusCode ErrorCode { get; }

        /// <summary>
        /// Constructor protegido de la clase ApplicationError.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        protected ApplicationError (HttpStatusCode errorCode, string message, Exception? innerException = null) : base(message, innerException) =>
            ErrorCode = errorCode;

        /// <summary>
        /// Crea un ApplicationError genérico para errores internos del servidor.
        /// </summary>
        /// <param name="innerException">Excepción interna [opcional].</param>
        /// <returns>Una nueva instancia de ApplicationError con un código de error 500.</returns>
        public static ApplicationError Create (Exception? innerException = null) => new(HttpStatusCode.InternalServerError, "Ha ocurrido un error interno en la aplicación", innerException);

        /// <summary>
        /// Crea una instancia de ApplicationError con un código y mensaje específicos.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción interna opcional.</param>
        /// <returns>Una nueva instancia de ApplicationError.</returns>
        public static ApplicationError Create (HttpStatusCode errorCode, string message, Exception? innerException = null) => new(errorCode, message, innerException);

        /// <summary>
        /// Lanza una excepción basada en este ApplicationError.
        /// </summary>
        /// <returns>Una excepción que representa este error.</returns>
        public ApplicationError Throw () => throw this;

    }

}