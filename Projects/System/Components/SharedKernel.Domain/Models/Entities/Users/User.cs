using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using SharedKernel.Domain.Utils.Extensions;
using System.Linq.Expressions;
using System.Text;

namespace SharedKernel.Domain.Models.Entities.Users {

    /// <summary>
    /// Entidad que representa un usuario en el sistema.
    /// Contiene información básica del usuario como nombre, nombre de usuario, correo electrónico, etc.
    /// </summary>
    public class User : GenericEntity, ITrackeable {

        /// <summary>
        /// Nombre de usuario, utilizado para el inicio de sesión.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Contraseña desencriptada del usuario.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Indica si el usuario está activo o desactivado.
        /// Un usuario desactivado se trata como eliminado virtualmente.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Fecha y hora en que el usuario fue creado en el sistema.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora en que el usuario fue actualizado por última vez en el sistema.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Colección de roles asociados al usuario.
        /// Esta propiedad es para uso interno de Entity Framework.
        /// Para modificar roles, use los métodos específicos de la clase.
        /// </summary>
        public ICollection<RoleAssignedToUser> RolesAssignedToUser { get; set; } = [];

        /// <summary>
        /// Devuelve un objeto parcial de tipo «User» que incluye un conjunto controlado de propiedades seleccionadas para modificaciones seguras.
        /// </summary>
        /// <remarks>
        /// Este método asegura que solo se expongan las propiedades seleccionadas para actualizaciones parciales, 
        /// protegiendo propiedades sensibles o no modificables como «RolesAssignedToUser» o las marcas de tiempo.
        /// 
        /// Propiedades expuestas:
        /// - «Username»: Identificador único del usuario para el inicio de sesión.
        /// - «Email»: Dirección de correo electrónico del usuario.
        /// - «Name»: Nombre del usuario.
        /// - «Password»: Contraseña no encriptada del usuario.
        /// - «IsActive»: Estado del usuario, que indica si está activo o desactivado.
        /// 
        /// Nota: Este enfoque es útil para evitar exposiciones accidentales de datos críticos o relaciones complejas al realizar actualizaciones parciales.
        /// </remarks>
        /// <returns>Un objeto parcial de tipo «Partial<User>» que contiene las propiedades seleccionadas.</returns>
        public Partial<User> AsPartial (params Expression<Func<User, object?>>[] propertyExpressions) => new(this, propertyExpressions.Length > 0 ? propertyExpressions : [
            user => user.Username,
            user => user.Email,
            user => user.Name,
            user => user.Password,
            user => user.IsActive
        ]);

        /// <summary>
        /// Construye y muestra la información del usuario en un único mensaje compuesto,
        /// incluyendo sus datos básicos, estado y los roles asociados.
        /// Nota: No se debe incluir información sensible como contraseñas en sistemas productivos.
        /// </summary>
        public void PrintUser () {
            try {
                // Utilizamos StringBuilder para construir el mensaje compuesto.
                var userMessage = new StringBuilder();

                // Encabezado que indica el inicio de la información del usuario.
                userMessage.AppendLine("\n«=== Información del Usuario ===»");

                // Propiedades principales del usuario.
                userMessage.AppendLine($"ID: {ID}");
                userMessage.AppendLine($"Username: {Username.FormatStringValue()}");
                userMessage.AppendLine($"Email: {Email.FormatStringValue()}");
                userMessage.AppendLine($"Name: {Name.FormatStringValue()}");

                // Contraseña (solo para referencia técnica, no incluir en entornos sensibles).
                userMessage.AppendLine($"Password: {Password.FormatStringValue()}");

                // Estado del usuario.
                userMessage.AppendLine($"IsActive: {IsActive?.ToString().FormatStringValue()}");

                // Fechas de creación y última actualización.
                userMessage.AppendLine($"Created At: {CreatedAt.FormatDateTime()}");
                userMessage.AppendLine($"Updated At: {UpdatedAt.FormatDateTime()}");

                // Verificación y construcción de la información de roles asociados.
                if (RolesAssignedToUser?.Count > 0) {
                    userMessage.AppendLine($"Roles asignados [{RolesAssignedToUser.Count}]:");
                    foreach (var roleAssignedToUser in RolesAssignedToUser) {
                        if (roleAssignedToUser.Role != null) {
                            // Se imprimen los detalles del rol si está disponible.
                            userMessage.AppendLine($"\t» ID: {roleAssignedToUser.Role.ID}");
                            userMessage.AppendLine($"\t» Name: {roleAssignedToUser.Role.Name.FormatStringValue()}");
                            userMessage.AppendLine($"\t» Description: {roleAssignedToUser.Role.Description.FormatStringValue()}");
                        } else {
                            // Se imprime el ID del rol cuando no está completamente cargado.
                            userMessage.AppendLine($"\t» ID: {roleAssignedToUser.RoleID}");
                        }
                    }
                } else {
                    // Indica que no hay roles asociados si la colección está vacía o es nula.
                    userMessage.AppendLine("Roles asignados: No hay roles asignados");
                }

                // Encabezado que indica el fin de la información del usuario.
                userMessage.Append("«/=== Información del Usuario ===/»");

                // Imprimimos el mensaje compuesto de una sola vez.
                Console.WriteLine(userMessage.Replace("\n", "\n\t").ToString());
            } catch (Exception ex) {
                // Manejo de errores durante la construcción o impresión del mensaje compuesto.
                Console.WriteLine($"Ha ocurrido un error al imprimir la información del usuario: {ex.Message}");
            }
        }

    }

}