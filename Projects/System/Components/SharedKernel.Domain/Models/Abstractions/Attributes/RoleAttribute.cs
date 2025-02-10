using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Domain.Models.Abstractions.Attributes {

    /// <summary>
    /// Atributo que permite definir metadatos para los roles del sistema.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RoleAttribute : Attribute {

        /// <summary>
        /// Descripción del rol.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Permisos de acceso asociados al rol.
        /// </summary>
        public SystemPermissions[] Permissions { get; }

        /// <summary>
        /// Inicializa una nueva instancia del atributo de metadatos de rol.
        /// </summary>
        /// <param name="description">Descripción del rol.</param>
        /// <param name="permissions">Permisos de acceso asociados al rol.</param>
        /// <exception cref="ArgumentException">Se lanza cuando description está vacío.</exception>
        public RoleAttribute (string description, params SystemPermissions[] permissions) {
            Description = !string.IsNullOrWhiteSpace(description) ? description.Trim() :
                throw new ArgumentException("La descripción del rol no puede estar vacía.", nameof(description));
            Permissions = permissions ?? [];
        }

    }

}