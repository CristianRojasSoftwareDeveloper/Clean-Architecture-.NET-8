namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Clase que contiene las cadenas de conexión a bases de datos.
    /// En este ejemplo se define la conexión a PostgreSQL.
    /// </summary>
    public class ConnectionStrings {

        /// <summary>
        /// Cadena de conexión para PostgreSQL.
        /// Valor por defecto:
        /// «Host=localhost;Port=5432;Database=web_system_db;Username=SuperAdmin;Password=Root2025;Include Error Detail=true;».
        /// </summary>
        public string PostgreSQL { get; set; } = "Host=localhost;Port=5432;Database=web_system_db;Username=SuperAdmin;Password=Root2025;Include Error Detail=true;";

    }

}