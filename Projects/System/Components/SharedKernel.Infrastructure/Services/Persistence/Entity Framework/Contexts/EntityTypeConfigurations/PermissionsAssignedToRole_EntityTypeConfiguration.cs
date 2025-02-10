using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Abstractions.Attributes;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Reflection;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class PermissionsAssignedToRole_EntityTypeConfiguration : IEntityTypeConfiguration<PermissionAssignedToRole> {

        /// <summary>
        /// Configura la entidad `PermissionAssignedToRole` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="permissionAssignedToRoleModelBuilder">Generador de modelo de permisos de roles de usuario.</param>
        public void Configure (EntityTypeBuilder<PermissionAssignedToRole> permissionAssignedToRoleModelBuilder) {

            // Mapea la entidad a la tabla 'permissions_assigned_to_roles'
            permissionAssignedToRoleModelBuilder.ToTable("permissions_assigned_to_roles");

            // Configura el identificador único de la entidad
            permissionAssignedToRoleModelBuilder.Property(permissionAssignedToRole => permissionAssignedToRole.ID).HasColumnName("id");
            // Configura la clave primaria de la entidad
            permissionAssignedToRoleModelBuilder.HasKey(permissionAssignedToRole => permissionAssignedToRole.ID);

            // Configura el ID del rol asociado al permiso
            permissionAssignedToRoleModelBuilder.Property(permissionAssignedToRole => permissionAssignedToRole.RoleID).HasColumnName("role_id");

            // Configura el ID del permiso asociado al rol
            permissionAssignedToRoleModelBuilder.Property(permissionAssignedToRole => permissionAssignedToRole.PermissionID).HasColumnName("permission_id");

            // Configura la restricción de clave única
            permissionAssignedToRoleModelBuilder.HasIndex(permissionAssignedToRole => new { permissionAssignedToRole.RoleID, permissionAssignedToRole.PermissionID }).IsUnique();

            // Configura la relación con la entidad Entity
            permissionAssignedToRoleModelBuilder.HasOne(permissionAssignedToRole => permissionAssignedToRole.Role)
                  .WithMany(role => role.PermissionAssignedToRoles)
                  .HasForeignKey(permissionAssignedToRole => permissionAssignedToRole.RoleID)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configura el índice para la clave foránea role_id
            // con el objetivo de optimizar la búsqueda de role_permission's asociados a un rol específico.
            permissionAssignedToRoleModelBuilder.HasIndex(roleAssignedToUser => roleAssignedToUser.RoleID).HasDatabaseName("idx_role_permissions_role_id");

            // Configura la relación con la entidad Entity
            permissionAssignedToRoleModelBuilder.HasOne(permissionAssignedToRole => permissionAssignedToRole.Permission)
                  .WithMany(permission => permission.PermissionAssignedToRoles)
                  .HasForeignKey(permissionAssignedToRole => permissionAssignedToRole.PermissionID)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configura el índice para la clave foránea permission_id
            // con el objetivo de optimizar la búsqueda de role_permission's asociados a un permiso específico.
            permissionAssignedToRoleModelBuilder.HasIndex(roleAssignedToUser => roleAssignedToUser.PermissionID).HasDatabaseName("idx_role_permissions_permission_id");

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(permissionAssignedToRoleModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="permissionAssignedToRoleModelBuilder">Generador de modelo de permisos de roles de usuario.</param>
        private static void InitializeData (EntityTypeBuilder<PermissionAssignedToRole> permissionAssignedToRoleModelBuilder) {

            int idCounter = 1;

            var roles = Enum.GetValues<DefaultRoles>().SelectMany(role => {
                var roleMetadata = typeof(DefaultRoles).GetField(role.ToString())!.GetCustomAttribute<RoleAttribute>() ??
                    throw new InvalidOperationException($"El rol {role} no tiene definidos los metadatos requeridos.");
                return roleMetadata.Permissions.Select(permission => {
                    var permissionMetadata = typeof(SystemPermissions).GetField(permission.ToString())!.GetCustomAttribute<PermissionAttribute>() ??
                        throw new InvalidOperationException($"El permiso de acceso {permission} no tiene definidos los metadatos requeridos.");
                    return new PermissionAssignedToRole {
                        ID = idCounter++,
                        RoleID = (int) role,
                        PermissionID = (int) permission
                    };
                });
            });

            permissionAssignedToRoleModelBuilder.HasData(roles);

        }

    }

}