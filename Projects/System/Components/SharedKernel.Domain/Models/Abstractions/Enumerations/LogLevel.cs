namespace SharedKernel.Domain.Models.Abstractions.Enumerations {

    /// <summary>
    /// Enumera los niveles de severidad de los logs.
    /// </summary>
    public enum LogLevel {

        /// <summary>
        /// Nivel de log para trazar información detallada, útil para diagnóstico.
        /// </summary>
        Trace,

        /// <summary>
        /// Nivel de log para información de depuración.
        /// </summary>
        Debug,

        /// <summary>
        /// Nivel de log para información general del sistema.
        /// </summary>
        Information,

        /// <summary>
        /// Nivel de log para advertencias que no detienen la ejecución del programa.
        /// </summary>
        Warning,

        /// <summary>
        /// Nivel de log para errores que ocurren durante la ejecución del programa.
        /// </summary>
        Error,

        /// <summary>
        /// Nivel de log para errores críticos que requieren atención inmediata.
        /// </summary>
        Critical

    }

}