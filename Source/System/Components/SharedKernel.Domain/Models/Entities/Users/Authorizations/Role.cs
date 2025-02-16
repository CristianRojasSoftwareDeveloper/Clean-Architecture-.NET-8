
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

using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Utils.Extensions;
using System.Linq.Expressions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Entidad que representa un rol en el sistema.
    /// Contiene información sobre el rol y sus relaciones con usuarios y permisos.
    /// </summary>
    public class Role : GenericEntity {

        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descripción del rol de usuario.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Colección de relaciones entre usuarios y roles.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar usuarios, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<RoleAssignedToUser> RoleAssignedToUsers { get; private set; } = [];

        /// <summary>
        /// Colección de relaciones entre roles y permisos.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar permisos, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<PermissionAssignedToRole> PermissionAssignedToRoles { get; private set; } = [];

        /// <summary>
        /// Devuelve una instancia parcial de la entidad «Role».
        /// Esta instancia contiene únicamente las propiedades públicas seleccionadas
        /// que pueden ser utilizadas en operaciones específicas.
        /// </summary>
        /// <returns>
        /// Un objeto de tipo «Partial<Role>» con las siguientes propiedades seleccionadas:
        /// «Name» y «Description».
        /// </returns>
        public Partial<Role> AsPartial (params Expression<Func<Role, object?>>[] propertyExpressions) => new(this, propertyExpressions.Length > 0 ? propertyExpressions : [
            role => role.Name,
            role => role.Description
        ]);

        /// <summary>
        /// Imprime en la consola la información del rol, incluyendo sus datos básicos, 
        /// los usuarios asociados y los permisos asignados.
        /// </summary>
        public void PrintRole () {
            try {
                // Encabezado que indica el inicio de la información del rol.
                Console.WriteLine("   === Información del Rol ===");

                // Impresión de las propiedades principales del rol.
                Console.WriteLine($"\tID: {ID}");
                Console.WriteLine($"\tName: {Name.FormatStringValue()}");
                Console.WriteLine($"\tDescription: {Description.FormatStringValue()}");

                // Verificación y impresión de usuarios asociados al rol.
                if (RoleAssignedToUsers?.Count > 0) {
                    Console.WriteLine($"\tUsuarios asociados [{RoleAssignedToUsers.Count}]:");
                    foreach (var roleAssignedToUser in RoleAssignedToUsers) {
                        if (roleAssignedToUser.User != null) {
                            // Se imprimen los detalles del usuario si está disponible.
                            Console.WriteLine($"\t\t» ID: {roleAssignedToUser.User.ID}");
                            Console.WriteLine($"\t\t- Username: {roleAssignedToUser.User.Username.FormatStringValue()}");
                            Console.WriteLine($"\t\t- Email: {roleAssignedToUser.User.Email.FormatStringValue()}");
                        } else {
                            // Se imprime el ID del usuario si no está completamente cargado.
                            Console.WriteLine($"\t\t» ID: {roleAssignedToUser.UserID}");
                        }
                    }
                } else {
                    // Indica que no hay usuarios asociados si la colección está vacía o es nula.
                    Console.WriteLine("\tNo hay usuarios asociados.");
                }

                // Verificación y impresión de permisos asignados al rol.
                if (PermissionAssignedToRoles?.Count > 0) {
                    Console.WriteLine($"\tPermisos asignados [{PermissionAssignedToRoles.Count}]:");
                    foreach (var permissionAssignedToRole in PermissionAssignedToRoles) {
                        if (permissionAssignedToRole.Permission != null) {
                            // Se imprimen los detalles del permiso si está disponible.
                            Console.WriteLine($"\t\t» ID: {permissionAssignedToRole.Permission.ID}");
                            Console.WriteLine($"\t\t- Name: {permissionAssignedToRole.Permission.Name.FormatStringValue()}");
                            Console.WriteLine($"\t\t- Description: {permissionAssignedToRole.Permission.Description.FormatStringValue()}");
                        } else {
                            // Se imprime el ID del permiso si no está completamente cargado.
                            Console.WriteLine($"\t\t» ID: {permissionAssignedToRole.PermissionID}");
                        }
                    }
                } else {
                    // Indica que no hay permisos asignados si la colección está vacía o es nula.
                    Console.WriteLine("\tNo hay permisos asignados.");
                }
            } catch (Exception ex) {
                // Manejo de errores durante la impresión de los datos del rol.
                Console.WriteLine($"Ha ocurrido un error al imprimir la información del rol: {ex.Message}");
            }
        }

    }

}