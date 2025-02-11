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

using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Utils.Extensions;
using System.Linq.Expressions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa un permiso en el sistema.
    /// Contiene información sobre el permiso y sus relaciones con roles.
    /// </summary>
    public class Permission : GenericEntity {

        /// <summary>
        /// Nombre del permiso.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descripción del permiso de usuario.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Colección de relaciones entre roles y permisos.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar roles, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<PermissionAssignedToRole> PermissionAssignedToRoles { get; private set; } = [];

        /// <summary>
        /// Devuelve una instancia parcial de la entidad «Permission».
        /// Esta instancia contiene únicamente las propiedades públicas seleccionadas
        /// que pueden ser utilizadas en operaciones específicas.
        /// </summary>
        /// <returns>
        /// Un objeto de tipo «Partial<Permission>» con las siguientes propiedades seleccionadas:
        /// «Name» y «Description».
        /// </returns>
        public Partial<Permission> AsPartial (params Expression<Func<Permission, object?>>[] propertyExpressions) => new(this, propertyExpressions.Length > 0 ? propertyExpressions : [
            permission => permission.Name,
            permission => permission.Description
        ]);

        /// <summary>
        /// Imprime en la consola la información del permiso, incluyendo sus datos básicos 
        /// y los roles asociados.
        /// </summary>
        public void PrintPermission () {
            try {
                // Encabezado que indica el inicio de la información del permiso.
                Console.WriteLine("   === Información del Permiso ===");
                // Impresión de las propiedades principales del permiso.
                Console.WriteLine($"\tID: {ID}");
                Console.WriteLine($"\tName: {Name.FormatStringValue()}");
                Console.WriteLine($"\tDescription: {Description.FormatStringValue()}");
                // Verificación y impresión de roles asociados al permiso.
                if (PermissionAssignedToRoles?.Count > 0) {
                    Console.WriteLine($"\tRoles asociados [{PermissionAssignedToRoles.Count}]:");
                    foreach (var permissionAssignedToRole in PermissionAssignedToRoles) {
                        if (permissionAssignedToRole.Role != null) {
                            // Se imprimen los detalles del rol si está disponible.
                            Console.WriteLine($"\t\t» ID: {permissionAssignedToRole.Role.ID}");
                            Console.WriteLine($"\t\t- Name: {permissionAssignedToRole.Role.Name.FormatStringValue()}");
                            Console.WriteLine($"\t\t- Description: {permissionAssignedToRole.Role.Description.FormatStringValue()}");
                        } else {
                            // Se imprime el ID del rol si no está completamente cargado.
                            Console.WriteLine($"\t\t» ID: {permissionAssignedToRole.RoleID}");
                        }
                    }
                } else {
                    // Indica que no hay roles asociados si la colección está vacía o es nula.
                    Console.WriteLine("\tNo hay roles asociados.");
                }
            } catch (Exception ex) {
                // Manejo de errores durante la impresión de los datos del permiso.
                Console.WriteLine($"Ha ocurrido un error al imprimir la información del permiso: {ex.Message}");
            }
        }

    }

}