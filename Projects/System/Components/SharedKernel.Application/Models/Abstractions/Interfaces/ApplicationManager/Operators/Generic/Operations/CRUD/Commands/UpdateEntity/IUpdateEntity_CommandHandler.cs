using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity {

    public interface IUpdateEntity_CommandHandler<EntityType> : IOperationHandler<IUpdateEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity { }

}