using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Reflection;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class Role_EntityTypeConfiguration : IEntityTypeConfiguration<Role> {

        /// <summary>
        /// Configura la entidad `Entity` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="roleModelBuilder">Generador de modelo de roles de usuario.</param>
        public void Configure (EntityTypeBuilder<Role> roleModelBuilder) {

            // Mapea la entidad a la tabla 'roles'
            roleModelBuilder.ToTable("roles");

            // Configura el identificador único de la entidad
            roleModelBuilder.Property(role => role.ID).HasColumnName("id");
            // Configura la clave primaria de la entidad
            roleModelBuilder.HasKey(role => role.ID);

            // Configura el nombre del rol
            roleModelBuilder.Property(role => role.Name).HasColumnName("name").IsRequired().HasMaxLength(30);

            // Configura la descripción del rol
            roleModelBuilder.Property(role => role.Description).HasColumnName("description").HasMaxLength(80);

            // Configura la propiedad única del nombre
            roleModelBuilder.HasIndex(role => role.Name).IsUnique();

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(roleModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="roleModelBuilder">Generador de modelo de roles de usuario.</param>
        private static void InitializeData (EntityTypeBuilder<Role> roleModelBuilder) {

            static RoleAttribute GetRoleMetadata (DefaultRoles role) =>
                typeof(DefaultRoles).GetField(role.ToString())!.GetCustomAttribute<RoleAttribute>()
                    ?? throw new InvalidOperationException($"El rol {role} no tiene definidos los metadatos requeridos mediante el atributo «RoleMetadata».");

            var roles = Enum.GetValues<DefaultRoles>().Select(role => {
                var metadata = GetRoleMetadata(role);
                return new Role {
                    ID = (int) role,
                    Name = role.ToString(), // Se usa el nombre del valor de la enumeración.
                    Description = metadata.Description // Se usa la descripción del rol definida en la metadata de la enumeración.
                };
            });

            roleModelBuilder.HasData(roles);

        }

    }

}