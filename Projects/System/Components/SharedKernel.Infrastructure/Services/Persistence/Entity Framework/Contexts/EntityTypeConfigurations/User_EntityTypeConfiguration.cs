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