using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Utils.Extensions;
using System.Linq.Expressions;
using System.Text;

namespace SharedKernel.Domain.Models.Entities.SystemLogs {

    /// <summary>
    /// Representa un log del sistema.
    /// </summary>
    public class SystemLog : GenericEntity, ITrackeable {

        /// <summary>
        /// ID del usuario asociado al evento registrado (opcional).
        /// Puede ser null si el evento no está asociado a ningún usuario específico.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// Nivel de severidad del log.
        /// </summary>
        public LogLevel? LogLevel { get; set; }

        /// <summary>
        /// Fuente del evento registrado.
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Mensaje detallado del evento registrado.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Fecha y hora en que el registro fue creado en el sistema.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora en que el registro fue actualizado por última vez en el sistema.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Devuelve una instancia parcial de la entidad «SystemLog».
        /// Esta instancia contiene únicamente las propiedades seleccionadas
        /// que pueden ser utilizadas en operaciones específicas.
        /// </summary>
        /// <returns>
        /// Un objeto de tipo «Partial<SystemLog>» con las siguientes propiedades seleccionadas:
        /// «UserID», «Message», «Source» y «LogLevel».
        /// </returns>
        public Partial<SystemLog> AsPartial (params Expression<Func<SystemLog, object?>>[] propertyExpressions) => new(this, propertyExpressions.Length > 0 ? propertyExpressions : [
            systemLog => systemLog.UserID,
            systemLog => systemLog.LogLevel,
            systemLog => systemLog.Source,
            systemLog => systemLog.Message
        ]);

        /// <summary>
        /// Construye y muestra la información del log del sistema en un único mensaje compuesto,
        /// incluyendo detalles del evento, su nivel de severidad y el usuario asociado, si lo hay.
        /// </summary>
        public void PrintSystemLog () {
            try {
                // Utilizamos StringBuilder para construir el mensaje compuesto.
                var logMessage = new StringBuilder();

                // Encabezado que indica el inicio de la información del log del sistema.
                logMessage.AppendLine("\n«=== Registro del Sistema ===»");

                // Propiedades principales del log.
                logMessage.AppendLine($"ID: {ID}");
                logMessage.AppendLine($"Log Level: {LogLevel?.ToString().FormatStringValue()}");

                if (UserID != null)
                    // Si hay un UserID pero no está cargada la entidad completa.
                    logMessage.AppendLine($"UserID: {UserID}");
                else
                    // Si no hay información de usuario asociada.
                    logMessage.AppendLine("UserID: No hay ningún usuario asociado");

                logMessage.AppendLine($"Source: {Source.FormatStringValue()}");
                logMessage.AppendLine($"Message: {Message.FormatStringValue()}");

                // Fechas de creación y última actualización.
                logMessage.AppendLine($"Created At: {CreatedAt.FormatDateTime()}");
                logMessage.AppendLine($"Updated At: {UpdatedAt.FormatDateTime()}");

                // Encabezado que indica el fin de la información del log del sistema.
                logMessage.AppendLine("«/=== Registro del Sistema ===/»");

                // Imprimimos el mensaje compuesto de una sola vez.
                Console.WriteLine(logMessage.Replace("\n", "\n\t").ToString());
            } catch (Exception ex) {
                // Manejo de errores durante la construcción o impresión del mensaje compuesto.
                Console.WriteLine($"Ha ocurrido un error al imprimir la información del log del sistema: {ex.Message}");
            }
        }

    }

}