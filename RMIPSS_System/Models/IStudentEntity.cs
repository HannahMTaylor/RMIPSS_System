using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Models;

public interface IStudentEntity
{
    // Reference to the related student. Adjust the type as needed.
    Student? Student { get; }

    // The unique identifier of the entity.
    int Id { get; }
}