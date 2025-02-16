
#region GPL v3 License Header
/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 * 
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.engineer@gmail.com»
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