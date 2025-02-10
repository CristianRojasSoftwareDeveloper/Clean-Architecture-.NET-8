using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities {

    public interface IGetEntities_QueryHandler<EntityType> : IOperationHandler<IGetEntities_Query, List<EntityType>> where EntityType : IGenericEntity { }

}