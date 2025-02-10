using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica de «DbContext» para PostgreSQL.
    /// Configura y gestiona la conexión con una base de datos PostgreSQL, incluyendo el mapeo de enumeraciones y otras configuraciones específicas del proveedor.
    /// </summary>
    /// <param name="connectionString">
    /// Cadena de conexión a la base de datos PostgreSQL.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se lanza cuando «connectionString» es nula, vacía o contiene únicamente espacios en blanco.
    /// </exception>
    public class PostgreSQL_DbContext (string connectionString) : ApplicationDbContext(CreateConfiguration(connectionString)) {

        /// <summary>
        /// Crea la configuración necesaria para la conexión a PostgreSQL.
        /// </summary>
        /// <param name="connectionString">
        /// Cadena de conexión que especifica los parámetros para acceder a la base de datos PostgreSQL.
        /// </param>
        /// <returns>
        /// Opciones de configuración específicas para PostgreSQL, incluyendo el mapeo de enumeraciones.
        /// </returns>
        /// <remarks>
        /// Se configura el mapeo del enum «LogLevel» a la columna «log_level».
        /// </remarks>
        private static DbContextOptions<PostgreSQL_DbContext> CreateConfiguration (string connectionString) {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede estar vacía.");
            return new DbContextOptionsBuilder<PostgreSQL_DbContext>().UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MapEnum<LogLevel>("log_level")
            ).Options;
        }

        /// <summary>
        /// Constructor estático para configurar comportamientos globales específicos de PostgreSQL.
        /// </summary>
        /// <remarks>
        /// Habilita el comportamiento de marca de tiempo heredado para mantener la compatibilidad con versiones anteriores de PostgreSQL en el manejo de timestamps.
        /// </remarks>
        static PostgreSQL_DbContext () => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    }

}