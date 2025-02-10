namespace SharedKernel.Domain.Models.Abstractions.Attributes {

    /// <summary>
    /// Atributo que permite definir metadatos para los permisos del sistema.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PermissionAttribute : Attribute {

        /// <summary>
        /// Descripción del permiso.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Inicializa una nueva instancia del atributo de metadatos de permiso.
        /// </summary>
        /// <param name="description">Descripción del permiso.</param>
        public PermissionAttribute (string description) =>
            Description = !string.IsNullOrWhiteSpace(description) ? description.Trim() :
                throw new ArgumentException("La descripción del permiso no puede estar vacía.", nameof(description));

    }

}