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
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts.EntityTypeConfigurations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;

/// <summary>
/// Clase base abstracta que define el contexto de base de datos común para la aplicación.
/// Proporciona acceso a las entidades y maneja la configuración del modelo de datos,
/// incluyendo el seguimiento automático de fechas de creación y actualización.
/// </summary>
/// <remarks>
/// Implementa el patrón Unit of Work y Repository a través de Entity Framework Core.
/// Se espera que las clases derivadas proporcionen la configuración específica del proveedor de base de datos.
/// </remarks>
/// <param name="options">
/// Opciones de configuración para el contexto de base de datos, 
/// especificando detalles de la conexión y comportamiento de EF Core.
/// </param>
public abstract class ApplicationDbContext (DbContextOptions options) : DbContext(options) {

    /// <summary>
    /// Conjunto de usuarios en la base de datos. Permite operaciones CRUD sobre la entidad User.
    /// </summary>
    public virtual DbSet<User> Users { get; set; } = default!;

    /// <summary>
    /// Conjunto de roles en la base de datos. Permite operaciones CRUD sobre la entidad Role.
    /// </summary>
    public virtual DbSet<Role> Roles { get; set; } = default!;

    /// <summary>
    /// Conjunto de permisos en la base de datos. Permite operaciones CRUD sobre la entidad Permission.
    /// </summary>
    public virtual DbSet<Permission> Permissions { get; set; } = default!;

    /// <summary>
    /// Conjunto de asignaciones de roles a usuarios. Gestiona la relación muchos a muchos entre usuarios y roles.
    /// </summary>
    public virtual DbSet<RoleAssignedToUser> RolesAssignedToUsers { get; set; } = default!;

    /// <summary>
    /// Conjunto de asignaciones de permisos a roles. Gestiona la relación muchos a muchos entre roles y permisos.
    /// </summary>
    public virtual DbSet<PermissionAssignedToRole> PermissionsAssignedToRoles { get; set; } = default!;

    /// <summary>
    /// Conjunto de logs del sistema. Permite el registro y consulta de eventos del sistema.
    /// </summary>
    public virtual DbSet<SystemLog> SystemLogs { get; set; } = default!;

    /// <summary>
    /// Configura el modelo de la base de datos usando Fluent API.
    /// </summary>
    /// <param name="modelBuilder">Constructor del modelo de base de datos.</param>
    /// <exception cref="ArgumentNullException">Se lanza si <paramref name="modelBuilder"/> es null.</exception>
    protected override void OnModelCreating (ModelBuilder modelBuilder) {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfiguration(new User_EntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new Role_EntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new Permission_EntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleAssignedToUser_EntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionsAssignedToRole_EntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SystemLog_EntityTypeConfiguration());
    }

    /// <summary>
    /// Actualiza las propiedades de seguimiento (<see cref="ITrackeable.CreatedAt"/> y <see cref="ITrackeable.UpdatedAt"/>)
    /// de las entidades que implementan <see cref="ITrackeable"/> antes de guardar los cambios.
    /// </summary>
    private void UpdateTrackingProperties () {
        var dateTimeUtcNow = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<ITrackeable>().Where(entityEntry => entityEntry.State is EntityState.Added or EntityState.Modified)) {
            entry.Entity.UpdatedAt = dateTimeUtcNow;
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = dateTimeUtcNow;
        }
    }

    /// <summary>
    /// Guarda los cambios realizados en el contexto de forma asíncrona, aplicando las actualizaciones de seguimiento.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación de la operación.</param>
    /// <returns>Número de entidades afectadas en la base de datos.</returns>
    public override async Task<int> SaveChangesAsync (CancellationToken cancellationToken = default) {
        UpdateTrackingProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }

}