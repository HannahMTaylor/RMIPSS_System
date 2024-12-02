using RMIPSS_System.Models.OtherModels;
//using System.Drawing.Common; //??

namespace RMIPSS_System.Repository.IRepository;

public interface IPdfUploadRepository
{
    PdfUpload Upload(IFormFile uploadedPdf, string? name = null);
    Task<PdfUpload> UploadAsync(IFormFile uploadedPdf, string? name = null);
    PdfUpload? Read(int id);
    Task<PdfUpload?> ReadAsync(int id);
    void Delete(int id);
    Task DeleteAsync(int id);

}
