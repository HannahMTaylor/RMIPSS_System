namespace RMIPSS_System.Models.Entities;

public class Referrer
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string RelationshipToStudent { get; set; }
    public Student Student { get; set; }
    public string Phone {  get; set; }
    public string Email { get; set; }
    public DateOnly DateFilledReferral { get; set; }
}
