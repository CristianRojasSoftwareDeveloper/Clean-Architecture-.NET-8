#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
 *
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 *
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 *
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 *
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion

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