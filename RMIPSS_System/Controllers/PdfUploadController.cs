using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMIPSS_System.Repository.IRepository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMIPSS_System.Models;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;
using RMIPSS_System.Models.OtherModels;


namespace RMIPSS_System.Controllers;

public class PdfUploadController
{

    /*
    private readonly IPdfUploadRepository _pdfRepo;

    public PdfUploadController(IPdfUploadRepository pdfRepo)
    {
        _pdfRepo = pdfRepo;
    }


    public async Task<IActionResult> ShowDocument(int id)
    {
        var pdf = await _pdfRepo.ReadAsync(id);

        if (pdf != null)
        {
            if (pdf.Data != null)
            {
                return File(pdf.Data, "application/pdf");
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost, ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(PdfUpload pdf, IFormFile document)
    {
        if (document != null)
        {
            if (document.Length > 0)
            {
                using var fileStream = new MemoryStream();
                await document.CopyToAsync(fileStream);
                pdf.Data = fileStream.ToArray();

                // Remove the ModelState error for Document
                ModelState.Remove("Document");
            }

            if (ModelState.IsValid)
            {
                // Store pdf in database
                return RedirectToAction("Index", "Home");
            }
        }
        return View(new ApplicantVM(pdf));
    }

    public async Task<IActionResult> Document(int id)
    {
        var pdf = await _pdfRepo.ReadAsync(id);

        if (pdf != null)
        {
            if (pdf.Data != null)
            {
                MemoryStream ms = new(pdf.Data);
                return await Task.Run(() => new FileStreamResult(ms, "application/pdf"));
            }
        }
        return NotFound();
    }

    private IActionResult NotFound()
    {
        throw new NotImplementedException();
    }

    */
}
