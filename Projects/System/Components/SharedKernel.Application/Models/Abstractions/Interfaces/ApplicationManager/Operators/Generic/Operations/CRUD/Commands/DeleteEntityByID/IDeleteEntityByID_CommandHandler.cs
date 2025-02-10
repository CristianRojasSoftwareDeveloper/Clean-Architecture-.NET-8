using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID {

    public interface IDeleteEntityByID_CommandHandler<EntityType> : IOperationHandler<IDeleteEntityByID_Command, EntityType> where EntityType : IGenericEntity { }

}