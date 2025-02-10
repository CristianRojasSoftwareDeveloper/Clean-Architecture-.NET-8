using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Reflection;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class Permission_EntityTypeConfiguration : IEntityTypeConfiguration<Permission> {

        /// <summary>
        /// Configura la entidad `Entity` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="permissionModelBuilder">Generador de modelo de permisos de usuario.</param>
        public void Configure (EntityTypeBuilder<Permission> permissionModelBuilder) {

            // Mapea la entidad a la tabla 'permissions'
            permissionModelBuilder.ToTable("permissions");

            // Configura el identificador único de la entidad
            permissionModelBuilder.Property(permission => permission.ID).HasColumnName("id");
            // Configura la clave primaria de la entidad
            permissionModelBuilder.HasKey(permission => permission.ID);

            // Configura el nombre del permiso
            permissionModelBuilder.Property(permission => permission.Name).HasColumnName("name").IsRequired().HasMaxLength(30);

            // Configura la descripción del permiso
            permissionModelBuilder.Property(permission => permission.Description).HasColumnName("description").HasMaxLength(80);

            // Configura la propiedad única del nombre
            permissionModelBuilder.HasIndex(permission => permission.Name).IsUnique();

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(permissionModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="permissionModelBuilder">Generador de modelo de permisos de usuario.</param>
        private static void InitializeData (EntityTypeBuilder<Permission> permissionModelBuilder) {

            var permissions = Enum.GetValues<SystemPermissions>()
                .Where(p => p != SystemPermissions.None)
                .Select(permission => {
                    var metadata = typeof(SystemPermissions).GetField(permission.ToString())!.GetCustomAttribute<PermissionAttribute>() ??
                        throw new InvalidOperationException($"El permiso {permission} no tiene definidos los metadatos requeridos.");
                    return new Permission {
                        ID = (int) permission,
                        Name = permission.ToString(),
                        Description = metadata.Description
                    };
                });

            permissionModelBuilder.HasData(permissions);

        }

    }

}