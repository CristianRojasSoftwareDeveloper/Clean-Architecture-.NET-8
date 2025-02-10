using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica de «DbContext» para pruebas en memoria.
    /// Proporciona una base de datos en memoria para pruebas unitarias y desarrollo, sin necesidad de disponer de una base de datos real.
    /// </summary>
    /// <param name="databaseName">
    /// Nombre que identifica la instancia de la base de datos en memoria. Si no se especifica, se utiliza «"InMemoryDb"» por defecto.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se lanza cuando «databaseName» es nulo, vacío o contiene únicamente espacios en blanco.
    /// </exception>
    public class InMemory_DbContext (string databaseName = "InMemoryDb") : ApplicationDbContext(CreateConfiguration(databaseName)) {

        /// <summary>
        /// Crea la configuración necesaria para la base de datos en memoria.
        /// </summary>
        /// <param name="databaseName">
        /// Nombre que identifica la instancia de la base de datos en memoria.
        /// </param>
        /// <returns>
        /// Opciones de configuración específicas para la base de datos en memoria.
        /// </returns>
        private static DbContextOptions<InMemory_DbContext> CreateConfiguration (string databaseName) {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName), "El nombre de la base de datos en memoria no puede estar vacío.");
            return new DbContextOptionsBuilder<InMemory_DbContext>().UseInMemoryDatabase(databaseName).Options;
        }

    }

}