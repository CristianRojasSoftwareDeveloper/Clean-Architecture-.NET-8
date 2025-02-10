using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Representa la relación entre un usuario y un rol en el sistema.
    /// Esta clase actúa como una tabla intermedia en una relación de muchos a muchos entre las entidades «User» y «Role».
    /// </summary>
    public class RoleAssignedToUser : GenericEntity {

        /// <summary>
        /// Identificador único del usuario asociado a esta relación.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Instancia del usuario relacionado.
        /// Esta propiedad es utilizada internamente por Entity Framework para las operaciones de navegación y mapeo.
        /// Para establecer esta relación, utilice la propiedad «UserID» en su lugar.
        /// </summary>
        public User? User { get; private set; }

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
        /// Devuelve un objeto parcial de tipo «RoleAssignedToUser» que incluye un conjunto controlado
        /// de propiedades seleccionadas para modificaciones seguras.
        /// </summary>
        /// <remarks>
        /// En este caso, la colección de expresiones está vacía porque, por diseño, no está permitido modificar las claves foráneas
        /// («UserID» y «RoleID») directamente en esta tabla intermedia. Esto asegura que las relaciones solo se modifiquen 
        /// mediante la creación o eliminación de registros en la tabla intermedia.
        /// 
        /// Nota: Si esta tabla intermedia incluyera propiedades adicionales (por ejemplo, una «Fecha de asignación» o un «Estado»), 
        /// dichas propiedades podrían ser incluidas en la colección de expresiones del método «AsPartial».
        /// </remarks>
        /// <returns>Un objeto parcial de tipo «Partial<RoleAssignedToUser>».</returns>
        public Partial<RoleAssignedToUser> AsPartial () => new(this, [ /* Vacío por diseño */ ]);

    }

}