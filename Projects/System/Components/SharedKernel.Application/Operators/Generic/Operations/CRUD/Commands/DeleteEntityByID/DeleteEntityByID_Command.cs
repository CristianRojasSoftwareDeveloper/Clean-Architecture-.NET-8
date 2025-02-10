using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID {

    /// <summary>
    /// Comando para eliminar una entidad existente por su ID.
    /// </summary>
    [RequiredPermissions(SystemPermissions.DeleteEntityByID)]
    public class DeleteEntityByID_Command : IDeleteEntityByID_Command {

        /// <summary>
        /// Obtiene el ID de la entidad a eliminar.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando para eliminar una entidad por su ID.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        public DeleteEntityByID_Command (int entityID) => ID = entityID;

    }

}