namespace RMIPSS_System.Models.OtherModels;

public class PdfUpload
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public byte[]? Data { get; set; }
    public int Length { get; set; }
    public string ContentType { get; set; } = string.Empty;
}
