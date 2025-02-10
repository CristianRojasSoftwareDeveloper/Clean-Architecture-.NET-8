namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID {

    public interface IGetEntityByID_Query : IOperation {

        int ID { get; }

        bool EnableTracking { get; }

    }

}