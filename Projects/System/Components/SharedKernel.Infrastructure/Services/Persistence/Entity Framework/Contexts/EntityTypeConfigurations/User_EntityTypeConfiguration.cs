using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class User_EntityTypeConfiguration : IEntityTypeConfiguration<User> {

        /// <summary>
        /// Configura la entidad `Entity` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="userModelBuilder">Generador de modelo de usuarios.</param>
        public void Configure (EntityTypeBuilder<User> userModelBuilder) {

            // Mapea la entidad a la tabla 'users'
            userModelBuilder.ToTable("users");

            // Configura el identificador único de la entidad
            userModelBuilder
                .Property(user => user.ID)
                .HasColumnName("id");

            // Configura la clave primaria de la entidad
            userModelBuilder
                .HasKey(user => user.ID);

            // Configura las propiedades de la entidad
            userModelBuilder.Property(user => user.Username)
                .HasColumnName("username")
                .HasMaxLength(30)
                .IsRequired(); // El nombre de usuario es obligatorio y tiene un límite de 30 caracteres

            userModelBuilder.Property(user => user.Password)
                .HasColumnName("password")
                .HasMaxLength(64)
                .IsRequired(); // La contraseña es obligatoria, tiene un límite de 64 caracteres y es encriptada por la aplicación.

            userModelBuilder.Property(user => user.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired(); // El nombre es obligatorio y tiene un límite de 50 caracteres

            userModelBuilder.Property(user => user.Email)
                .HasColumnName("email")
                .HasMaxLength(50)
                .IsRequired(); // El correo electrónico es obligatorio y tiene un límite de 50 caracteres

            // Configuración del campo booleano «IsActive»
            userModelBuilder.Property(user => user.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true)
                .IsRequired();

            // La fecha y hora de creación se establece automáticamente al agregar la entidad a la base de datos.
            userModelBuilder.Property(user => user.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            // La fecha y hora de actualización se establece automáticamente al actualizar la entidad en la base de datos.
            userModelBuilder.Property(user => user.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            // Configura los índices únicos
            userModelBuilder
                .HasIndex(user => user.Username)
                .IsUnique(); // Se asegura de que los nombres de usuario sean únicos

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(userModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="userModelBuilder">Generador de modelo de usuarios.</param>
        private static void InitializeData (EntityTypeBuilder<User> userModelBuilder) {
            var dateTimeUtcNow = DateTime.UtcNow;
            // Semillas de datos para la entidad Entity
            userModelBuilder.HasData([
                new User {
                    ID = 1,
                    Username = "CristianSoftwareDeveloper",
                    Email = "cristian.rojas.software.developer@gmail.com",
                    Name = "Cristian Rojas",
                    Password = "$2a$11$DEr.JIMcwp8lhjb4dyOu5Ob.aDZfLOVDHk9otvQjPv1Yi34GY3ZTK",
                    IsActive = true,
                    CreatedAt = dateTimeUtcNow,
                    UpdatedAt = dateTimeUtcNow
                }
            ]);
        }

    }

}