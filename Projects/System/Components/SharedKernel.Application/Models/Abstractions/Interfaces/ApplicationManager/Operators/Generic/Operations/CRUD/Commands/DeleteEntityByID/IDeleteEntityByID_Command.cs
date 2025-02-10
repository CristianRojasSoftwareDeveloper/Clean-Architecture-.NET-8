namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID {

    public interface IDeleteEntityByID_Command : IOperation {

        int ID { get; }

    }

}