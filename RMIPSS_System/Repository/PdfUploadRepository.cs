using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Models.OtherModels;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class PdfUploadRepository : IPdfUploadRepository
{
    private readonly ApplicationDbContext _db;

    public PdfUploadRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Delete(int id)
    {
        var pdf = Read(id);

        if(pdf != null)
        {
            _db.PdfUploads.Remove(pdf);
            _db.SaveChanges();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var pdf = await ReadAsync(id);
        if (pdf != null)
        {
            _db.PdfUploads.Remove(pdf);
            await _db.SaveChangesAsync();
        }

    }

    public async Task<PdfUpload?> ReadAsync(int id)
    {
        return await _db.PdfUploads.FirstOrDefaultAsync(pdf => pdf.Id == id);
    }


    public PdfUpload? Read(int id)
    {
        var pdf = _db.PdfUploads.FirstOrDefault(p => p.Id == id);
        return pdf;
    }

    public PdfUpload Upload(IFormFile uploadedPdf, string? name = null)
    {
        MemoryStream ms = new();
        uploadedPdf.OpenReadStream().CopyTo(ms);
        name ??= uploadedPdf.Name;

        PdfUpload pdfEntity = new()
        {
            Id = 0,
            Name = name,
            Data = ms.ToArray(),
            ContentType = uploadedPdf.ContentType
        };

        _db.PdfUploads.Add(pdfEntity);

        _db.SaveChanges();
        return pdfEntity;
    }

    public async Task<PdfUpload> UploadAsync(IFormFile uploadedPdf, string? name = null)
    {
        MemoryStream ms = new();
        await uploadedPdf.OpenReadStream().CopyToAsync(ms);
        name ??= uploadedPdf.Name;

        PdfUpload pdfEntity = new()
        {
            Id = 0,
            Name = name,
            Data = ms.ToArray(),
            ContentType = uploadedPdf.ContentType
        };

        _db.PdfUploads.Add(pdfEntity);

        await _db.SaveChangesAsync();
        return pdfEntity;
    }


}
