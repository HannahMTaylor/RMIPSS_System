using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Controllers;

public class PdfUploadController
{
    private readonly IPdfUploadRepository _pdfRepo;

    public PdfUploadController(IPdfUploadRepository pdfRepo)
    {
        _pdfRepo = pdfRepo;
    }

    public async Task<IActionResult> Index()
    {
        //instead of returning all, how do I return just name of file uploaded? and view it within the form?
        return View(await _pdfRepo.ReadAllAsync());

    }

    public async Task<IActionResult> Data(int id)
    {
        var pdf = await _pdfRepo.ReadAsync(id);
        if (pdf == null || pdf.Data == null)
        {
            return NotFound();
        }
        MemoryStream ms = new MemoryStream(pdf.Data);
        return await Task.Run(() => new FileStreamResult(ms, pdf.ContentType));
    }

    private IActionResult NotFound()
    {
        throw new NotImplementedException();
    }
}
