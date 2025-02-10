using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;

// Este namespace organiza las configuraciones de Entity Framework dentro de la capa de infraestructura
// siguiendo los principios de Clean Architecture para mantener las configuraciones de persistencia separadas
namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    /// <summary>
    /// Define la configuración de Entity Framework para la entidad SystemLog.
    /// Esta clase establece el mapeo entre la entidad de dominio y su representación en la base de datos,
    /// incluyendo nombres de tablas y columnas, restricciones, relaciones e índices.
    /// La configuración soporta la eliminación completa de los logs cuando se elimina un usuario,
    /// alineándose con el caso de uso DeleteUser para situaciones excepcionales.
    /// </summary>
    public class SystemLog_EntityTypeConfiguration : IEntityTypeConfiguration<SystemLog> {

        /// <summary>
        /// Método principal de configuración que establece el mapeo de la entidad SystemLog en la base de datos.
        /// Organiza la configuración en grupos lógicos para mejorar la mantenibilidad y legibilidad del código.
        /// </summary>
        /// <param name="systemLogModelBuilder">Builder utilizado para configurar la entidad SystemLog.</param>
        public void Configure (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {
            // Organizamos la configuración en grupos lógicos para mejor separación de responsabilidades
            // y facilitar el mantenimiento del código
            ConfigureTableAndPrimaryKey(systemLogModelBuilder);  // Configura la tabla y clave primaria
            ConfigureProperties(systemLogModelBuilder);          // Configura las propiedades básicas
            ConfigureRelationships(systemLogModelBuilder);       // Configura las relaciones con otras entidades
            ConfigureIndexes(systemLogModelBuilder);            // Configura los índices para optimización
        }

        /// <summary>
        /// Configura el nombre de la tabla en la base de datos y la clave primaria de la entidad.
        /// </summary>
        /// <remarks>
        /// El nombre de la tabla sigue la convención snake_case para estándares de nomenclatura en base de datos.
        /// La clave primaria se mapea a una columna llamada 'id' siguiendo la misma convención.
        /// </remarks>
        private static void ConfigureTableAndPrimaryKey (EntityTypeBuilder<SystemLog> builder) {
            // Definimos el nombre de la tabla usando la convención snake_case para consistencia con la base de datos
            builder.ToTable("system_logs");

            // Configuramos la propiedad ID como clave primaria de la tabla
            builder.HasKey(systemLog => systemLog.ID);

            // Mapeamos la propiedad ID a la columna 'id' en la base de datos
            builder.Property(systemLog => systemLog.ID).HasColumnName("id");
        }

        /// <summary>
        /// Configura las propiedades básicas de la entidad SystemLog.
        /// Define nombres de columnas, tipos de datos, longitudes máximas y restricciones de nulidad.
        /// Cada propiedad se documenta con su propósito específico y restricciones.
        /// </summary>
        private static void ConfigureProperties (EntityTypeBuilder<SystemLog> builder) {
            // Configuramos LogLevel como una columna enum requerida con tipo PostgreSQL personalizado
            builder.Property(systemLog => systemLog.LogLevel)
                .HasColumnName("log_level")                    // Nombre de la columna en snake_case
                .HasColumnType("log_level")                    // Tipo PostgreSQL personalizado para el enum
                .IsRequired()                                  // La columna no puede ser null
                .HasComment("Nivel de severidad del log (información, advertencia, error, etc.)");

            // Configuramos Source con una restricción de longitud máxima para optimizar el rendimiento
            builder.Property(systemLog => systemLog.Source)
                .HasColumnName("source")                      // Nombre de la columna en snake_case
                .HasMaxLength(80)                             // Limitamos la longitud para prevenir uso excesivo de almacenamiento
                .IsRequired()                                 // La columna no puede ser null
                .HasComment("Componente o origen del sistema que generó el log");

            // Configuramos Message con una longitud máxima generosa para logging detallado
            builder.Property(systemLog => systemLog.Message)
                .HasColumnName("message")                     // Nombre de la columna en snake_case
                .HasMaxLength(1000)                          // Permitimos mensajes largos para stacktraces y errores detallados
                .IsRequired()                                // La columna no puede ser null
                .HasComment("Descripción detallada del evento registrado");

            // Configuramos la marca de tiempo de creación
            builder.Property(systemLog => systemLog.CreatedAt)
                .HasColumnName("created_at")                  // Nombre de la columna en snake_case
                .IsRequired()                                 // La columna no puede ser null
                .HasComment("Fecha y hora de creación del registro");

            // Configuramos la marca de tiempo de última actualización
            builder.Property(systemLog => systemLog.UpdatedAt)
                .HasColumnName("updated_at")                  // Nombre de la columna en snake_case
                .IsRequired()                                 // La columna no puede ser null
                .HasComment("Fecha y hora de última actualización del registro");
        }

        /// <summary>
        /// Configura las relaciones entre SystemLog y otras entidades.
        /// Específicamente define la relación con la entidad User, incluyendo
        /// el comportamiento de eliminación en cascada para soportar el caso de uso DeleteUser.
        /// </summary>
        /// <remarks>
        /// El comportamiento de eliminación en cascada se establece intencionalmente para soportar
        /// el caso de uso DeleteUser, mientras que DeactivateUserByID preservará los logs. Esto permite
        /// la eliminación completa de usuarios en casos excepcionales mientras se mantiene la
        /// integridad de datos durante escenarios normales de desactivación.
        /// </remarks>
        private static void ConfigureRelationships (EntityTypeBuilder<SystemLog> builder) {
            // Configuramos la clave foránea opcional hacia User
            builder.Property(systemLog => systemLog.UserID)
                .HasColumnName("user_id")                     // Nombre de la columna en snake_case
                .IsRequired(false)                            // Permitimos logs sin usuario asociado
                .HasComment("ID del usuario asociado al log (opcional)");

            // Configuramos la relación con la entidad User
            builder
                .HasOne<User>()                              // Relación uno a muchos con User
                .WithMany()                                  // Sin propiedad de navegación en el lado de User
                .HasForeignKey(systemLog => systemLog.UserID) // Definimos la clave foránea
                .OnDelete(DeleteBehavior.Cascade)            // Eliminación en cascada para DeleteUser
                .HasConstraintName("fk_system_logs_user_id"); // Nombramos explícitamente la restricción
        }

        /// <summary>
        /// Configura los índices de la base de datos para optimizar patrones comunes de consulta.
        /// Los índices se crean en columnas frecuentemente consultadas para mejorar
        /// el rendimiento de las operaciones comunes.
        /// </summary>
        private static void ConfigureIndexes (EntityTypeBuilder<SystemLog> builder) {
            // Índice para búsquedas eficientes por ID de usuario
            builder.HasIndex(systemLog => systemLog.UserID)
                .HasDatabaseName("idx_system_logs_user_id");  // Nombre explícito del índice

            // Índice para consultas y ordenamiento eficiente por fecha
            builder.HasIndex(systemLog => systemLog.CreatedAt)
                .HasDatabaseName("idx_system_logs_created_at"); // Nombre explícito del índice
        }

        /// <summary>
        /// Método reservado para futura implementación de inicialización de datos semilla.
        /// Podría utilizarse para poblar logs iniciales del sistema o datos de prueba.
        /// </summary>
        private static void InitializeData (EntityTypeBuilder<SystemLog> systemLogModelBuilder) {
            // Reservado para futura implementación de datos semilla
        }

    }

}