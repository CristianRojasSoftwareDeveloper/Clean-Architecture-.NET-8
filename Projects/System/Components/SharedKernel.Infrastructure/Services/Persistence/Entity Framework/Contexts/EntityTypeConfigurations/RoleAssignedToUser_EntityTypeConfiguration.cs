using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations {

    public class RoleAssignedToUser_EntityTypeConfiguration : IEntityTypeConfiguration<RoleAssignedToUser> {

        /// <summary>
        /// Configura la entidad `RolesAssignedToUser` y sus propiedades en el modelo de la base de datos.
        /// </summary>
        /// <param name="roleAssignedToUserModelBuilder">Generador de modelo de roles de usuario.</param>
        public void Configure (EntityTypeBuilder<RoleAssignedToUser> roleAssignedToUserModelBuilder) {

            // Mapea la entidad a la tabla 'roles_assigned_to_users'
            roleAssignedToUserModelBuilder.ToTable("roles_assigned_to_users");

            // Configura el identificador único de la entidad
            roleAssignedToUserModelBuilder.Property(roleAssignedToUser => roleAssignedToUser.ID).HasColumnName("id");
            // Configura la clave primaria de la entidad
            roleAssignedToUserModelBuilder.HasKey(roleAssignedToUser => roleAssignedToUser.ID);

            // Configura el ID del usuario asociado al rol
            roleAssignedToUserModelBuilder.Property(roleAssignedToUser => roleAssignedToUser.UserID).HasColumnName("user_id");

            // Configura el ID del rol asociado al usuario
            roleAssignedToUserModelBuilder.Property(roleAssignedToUser => roleAssignedToUser.RoleID).HasColumnName("role_id");

            // Configura la restricción de clave única para user_id y role_id combinados
            roleAssignedToUserModelBuilder.HasIndex(roleAssignedToUser => new { roleAssignedToUser.UserID, roleAssignedToUser.RoleID }).IsUnique();

            // Configura la relación con la entidad Entity
            roleAssignedToUserModelBuilder.HasOne(roleAssignedToUser => roleAssignedToUser.User)
                  .WithMany(user => user.RolesAssignedToUser)
                  .HasForeignKey(roleAssignedToUser => roleAssignedToUser.UserID)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configura el índice para la clave foránea user_id
            // con el objetivo de optimizar la búsqueda de user_role's asociados a un usuario específico.
            roleAssignedToUserModelBuilder.HasIndex(roleAssignedToUser => roleAssignedToUser.UserID).HasDatabaseName("idx_user_roles_user_id");

            // Configura la relación con la entidad Entity
            roleAssignedToUserModelBuilder.HasOne(roleAssignedToUser => roleAssignedToUser.Role)
                  .WithMany(role => role.RoleAssignedToUsers)
                  .HasForeignKey(roleAssignedToUser => roleAssignedToUser.RoleID)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade); // Define el comportamiento de eliminación en cascada

            // Configura el índice para la clave foránea role_id
            // con el objetivo de optimizar la búsqueda de user_role's asociados a un rol específico.
            roleAssignedToUserModelBuilder.HasIndex(roleAssignedToUser => roleAssignedToUser.RoleID).HasDatabaseName("idx_user_roles_role_id");

            // Ejecuta la inicialización de los datos de la base de datos.
            InitializeData(roleAssignedToUserModelBuilder);

        }

        /// <summary>
        /// Inicializa los datos de las entidades en la base de datos.
        /// </summary>
        /// <param name="roleAssignedToUserModelBuilder">Generador de modelo de roles de usuario.</param>
        private static void InitializeData (EntityTypeBuilder<RoleAssignedToUser> roleAssignedToUserModelBuilder) {
            // Semillas de datos para relaciones RolesAssignedToUser
            roleAssignedToUserModelBuilder.HasData([
                new RoleAssignedToUser {
                    ID = 1,
                    UserID = 1,
                    RoleID = 1
                }
            ]);
        }

    }

}