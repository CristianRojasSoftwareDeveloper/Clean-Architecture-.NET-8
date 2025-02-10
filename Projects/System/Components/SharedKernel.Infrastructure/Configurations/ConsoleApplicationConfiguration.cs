using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Gestor de configuración para aplicaciones de consola con garantías de inmutabilidad y seguridad.
    /// Implementa un patrón Singleton con control estricto de inicialización.
    /// </summary>
    public sealed class ConsoleApplicationConfiguration {

        // Indicador de estado de inicialización
        // Garantiza que solo se pueda inicializar una vez
        private static bool _isInitialized = false;

        // Instancia única de configuración 
        // Almacena la configuración con un tipo nullable para control preciso
        private static ApplicationConfiguration? _instance;

        // Objeto de bloqueo para sincronización thread-safe
        // Previene condiciones de carrera durante la inicialización
        private static readonly object _lock = new();

        // Constructor privado para prevenir instanciación externa
        // Refuerza el patrón Singleton
        private ConsoleApplicationConfiguration () { }

        /// <summary>
        /// Proporciona acceso a la instancia de configuración.
        /// Lanza una excepción si no se ha inicializado previamente.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si se intenta acceder a la configuración sin inicialización previa.
        /// </exception>
        public static ApplicationConfiguration Instance {
            get {
                // Verificación de inicialización antes de acceder
                if (!_isInitialized)
                    throw new InvalidOperationException("La configuración debe inicializarse previamente mediante el método Initialize().");
                // Aserción de no nulidad tras verificación de inicialización
                return _instance!;
            }
        }

        /// <summary>
        /// Inicializa la configuración de manera segura y controlada.
        /// Solo puede ejecutarse una única vez en el ciclo de vida de la aplicación.
        /// </summary>
        /// <param name="configuration">
        /// Fuente de configuración opcional. 
        /// Si es nula, se utilizarán valores predeterminados.
        /// </param>
        /// <returns>La instancia de configuración inicializada.</returns>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si se intenta inicializar más de una vez.
        /// </exception>
        public static ApplicationConfiguration Initialize (IConfiguration? configuration) {
            // Bloqueo para garantizar inicialización thread-safe
            lock (_lock) {
                // Previene múltiples inicializaciones
                if (_isInitialized)
                    throw new InvalidOperationException("La configuración ya ha sido inicializada. No se permiten reconfiguraciones.");
                // Construcción de opciones de configuración.
                var options = new ApplicationConfigurationOptions();
                // Enlace de configuración externa si está disponible.
                // Solo sobrescribe propiedades definidas, manteniendo valores por defecto.
                configuration?.Bind(options);
                // Creación de la instancia única de configuración.
                _instance = new ApplicationConfiguration(Options.Create(options));
                // Marcado de inicialización completa.
                _isInitialized = true;
                // Retorna la instancia de la configuración de la aplicación.
                return _instance;
            }
        }

    }

}