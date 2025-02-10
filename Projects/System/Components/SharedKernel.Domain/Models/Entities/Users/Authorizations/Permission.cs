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