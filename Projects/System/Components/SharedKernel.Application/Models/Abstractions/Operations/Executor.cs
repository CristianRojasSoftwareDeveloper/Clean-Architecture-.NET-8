using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Utils.Extensions;
using System.Diagnostics;
using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Operations {

    /// <summary>
    /// Proporciona métodos para ejecutar operaciones sincrónicas y asincrónicas con lógica adicional,
    /// como medición del tiempo de ejecución y manejo de errores.
    /// </summary>
    public static class Executor {

        /// <summary>
        /// Método común para ejecutar operaciones (sincrónicas o asincrónicas).
        /// </summary>
        /// <typeparam name="StaticInputType">Tipo de entrada de la operación.</typeparam>
        /// <typeparam name="StaticResponseType">Tipo de resultado de la operación.</typeparam>
        /// <param name="handler">Función que representa la operación a ejecutar (sincrónica o asincrónica). Si la operación es sincrónica, se debe envolver en un Task utilizando Task.FromResult.</param>
        /// <param name="operationInput">Entrada de la operación.</param>
        /// <param name="detailedLog">Indica si se debe registrar información detallada, como tiempos de ejecución y resultados.</param>
        /// <returns>Respuesta de la operación envuelta en un objeto <see cref="Response{StaticResponseType}"/>.</returns>
        /// <remarks>Este método maneja tanto operaciones sincrónicas como asincrónicas. Si se pasa una operación sincrónica, debe envolverse en un Task utilizando Task.FromResult.</remarks>
        public static async Task<Response<StaticResponseType>> ExecuteOperation<StaticInputType, StaticResponseType> (Func<StaticInputType, Task<StaticResponseType>> handler, StaticInputType operationInput, bool detailedLog) {

            #region Validación de los parámetros de entrada para asegurar que no sean nulos.
            if (handler == null)
                // Si la operación es nula, se lanza una excepción ArgumentNullException con el nombre del parámetro y un mensaje detallado.
                throw new ArgumentNullException(nameof(handler), "La operación no puede ser nula.");
            if (operationInput == null)
                // Si la entrada de la operación es nula, se lanza una excepción ArgumentNullException con el nombre del parámetro y un mensaje detallado.
                throw new ArgumentNullException(nameof(operationInput), "La entrada de la operación no puede ser nula.");
            #endregion

            // Se crea un objeto Stopwatch para medir el tiempo de ejecución de la operación.
            Stopwatch stopwatch = new();

            // Bloque try para ejecutar la operación y manejar errores.
            try {
                // Si se requiere un log detallado, se registra que la ejecución de la operación está por comenzar.
                if (detailedLog) {
                    Console.WriteLine("Iniciando ejecución de la operación...");
                    // Se inicia el cronómetro para medir el tiempo de ejecución.
                    stopwatch.Start();
                }

                // Se ejecuta la operación asincrónica pasando el input correspondiente.
                var operationResponse = await handler(operationInput);

                // Si se requiere un log detallado, se registra que la operación se ejecutó exitosamente.
                if (detailedLog)
                    Console.WriteLine("Operación ejecutada exitosamente.");

                // Si la respuesta de la operación es no nula, se retorna un objeto de respuesta exitoso.
                // Si la respuesta es nula, se retorna una respuesta exitosa con un mensaje indicando que la operación devolvió un resultado vacío.
                return operationResponse != null
                    ? Response<StaticResponseType>.Success(operationResponse)
                    : Response<StaticResponseType>.Success(default(StaticResponseType), statusMessage: "Operación devolvió un resultado vacío.");
            } catch (AggregateError ex) {
                // En caso de que se lance una excepción AggregateError, se maneja el error de manera específica.
                if (ex.Errors != null && ex.Errors.Count != 0) {
                    // Se recorre cada error dentro de AggregateError y se registra en consola.
                    foreach (var error in ex.Errors)
                        Console.WriteLine($"\n{error.ErrorCode}> {error.Message}\n");
                    // Se retorna una respuesta de error utilizando los detalles de la excepción.
                    return Response<StaticResponseType>.Failure(ex);
                }
                // Si el error no se puede manejar específicamente, se lanza nuevamente la excepción original.
                throw;
            } catch (Exception ex) {
                // En caso de un error general, se prepara un mensaje detallado para indicar el fallo en la operación.
                var errorMessage = $"Ha ocurrido un error interno en el servidor durante la ejecución de la operación «{typeof(StaticInputType).Name}»: {ex.Message}";
                // Si se requiere un log detallado, se registra el mensaje de error en consola.
                if (detailedLog)
                    Console.WriteLine(errorMessage);
                // Se retorna una respuesta de error con un código de estado 500 (error interno del servidor) y los detalles del error.
                return Response<StaticResponseType>.Failure(HttpStatusCode.InternalServerError, errorMessage, ex);
            } finally {
                // En el bloque finally, se garantiza que el cronómetro se detenga y se registre el tiempo de ejecución.
                if (detailedLog) {
                    // Se detiene el cronómetro y se registra el tiempo total de ejecución.
                    stopwatch.Stop();
                    Console.WriteLine($"Tiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}");
                }
            }

        }

    }

}