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