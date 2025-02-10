namespace SharedKernel.Domain.Models.Abstractions.Interfaces {

    // Esta interface define el Tracking de fechas de creación y actualización de entidades.
    public interface ITrackeable {

        DateTime? CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }

    }

}