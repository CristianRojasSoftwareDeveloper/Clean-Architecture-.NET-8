using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity {

    public interface IUpdateEntity_Command<EntityType> : IOperation where EntityType : IGenericEntity {
        Partial<EntityType> EntityUpdate { get; }
    }

}