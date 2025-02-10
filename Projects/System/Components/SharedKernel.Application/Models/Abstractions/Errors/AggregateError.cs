using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un Error agregado que contiene una lista de errores.
    /// </summary>
    public class AggregateError : ApplicationError {

        /// <summary>
        /// Lista de errores agregados.
        /// </summary>
        public List<ApplicationError> Errors { get; }

        /// <summary>
        /// Constructor de la clase AggregateError.
        /// </summary>
        /// <param name="errors">Lista de errores agregados.</param>
        private AggregateError (List<ApplicationError> errors) : base(HttpStatusCode.BadRequest, $"Se han detectado {errors.Count} errores en tiempo de ejecución.") =>
            Errors = errors;

        /// <summary>
        /// Método estático para crear una instancia de AggregateError.
        /// </summary>
        /// <param name="errors">Lista de errores agregados.</param>
        /// <returns>Una nueva instancia de AggregateError.</returns>
        public static AggregateError Create (List<ApplicationError> errors) => new(errors);

    }

}