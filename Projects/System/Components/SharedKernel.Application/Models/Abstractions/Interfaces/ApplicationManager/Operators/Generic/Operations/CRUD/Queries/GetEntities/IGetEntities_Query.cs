namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities {

    public interface IGetEntities_Query : IOperation {

        bool EnableTracking { get; }

    }

}