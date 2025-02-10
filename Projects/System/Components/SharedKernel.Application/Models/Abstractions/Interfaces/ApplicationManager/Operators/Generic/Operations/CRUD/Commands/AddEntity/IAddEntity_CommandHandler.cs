using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity {

    public interface IAddEntity_CommandHandler<EntityType> : IOperationHandler<IAddEntity_Command<EntityType>, EntityType> where EntityType : IGenericEntity { }

}