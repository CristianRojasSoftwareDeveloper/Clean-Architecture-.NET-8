using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Representa la relación entre un rol y un permiso en el sistema.
    /// Esta clase actúa como una tabla intermedia en una relación de muchos a muchos entre las entidades «Role» y «Permission».
    /// </summary>
    public class PermissionAssignedToRole : GenericEntity {

        /// <summary>
        /// Identificador único del rol asociado a esta relación.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Instancia del rol relacionado.
        /// Esta propiedad es utilizada internamente por Entity Framework para las operaciones de navegación y mapeo.
        /// Para establecer esta relación, utilice la propiedad «RoleID» en su lugar.
        /// </summary>
        public Role? Role { get; private set; }

        /// <summary>
        /// Identificador único del permiso asociado a esta relación.
        /// </summary>
        public int PermissionID { get; set; }

        /// <summary>
        /// Instancia del permiso relacionado.
        /// Esta propiedad es utilizada internamente por Entity Framework para las operaciones de navegación y mapeo.
        /// Para establecer esta relación, utilice la propiedad «PermissionID» en su lugar.
        /// </summary>
        public Permission? Permission { get; private set; }

        /// <summary>
        /// Devuelve un objeto parcial de tipo «PermissionAssignedToRole» que incluye un conjunto controlado
        /// de propiedades seleccionadas para modificaciones seguras.
        /// </summary>
        /// <remarks>
        /// En este caso, la colección de expresiones está vacía porque, por diseño, no está permitido modificar las claves foráneas
        /// («RoleID» y «PermissionID») directamente en esta tabla intermedia. Esto asegura que las relaciones solo se modifiquen 
        /// mediante la creación o eliminación de registros en la tabla intermedia.
        /// 
        /// Nota: Si esta tabla intermedia incluyera propiedades adicionales (por ejemplo, una «Fecha de asignación» o un «Estado»), 
        /// dichas propiedades podrían ser incluidas en la colección de expresiones del método «AsPartial».
        /// </remarks>
        /// <returns>Un objeto parcial de tipo «Partial<PermissionAssignedToRole>».</returns>
        public Partial<PermissionAssignedToRole> AsPartial () => new(this, [ /* Vacío por diseño */ ]);

    }

}