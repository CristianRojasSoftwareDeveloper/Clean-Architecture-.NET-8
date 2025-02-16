
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