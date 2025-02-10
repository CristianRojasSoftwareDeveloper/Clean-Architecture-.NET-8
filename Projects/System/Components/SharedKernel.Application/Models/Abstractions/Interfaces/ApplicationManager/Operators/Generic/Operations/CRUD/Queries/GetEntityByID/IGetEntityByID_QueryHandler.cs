using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID {

    public interface IGetEntityByID_QueryHandler<EntityType> : IOperationHandler<IGetEntityByID_Query, EntityType> where EntityType : IGenericEntity { }

}