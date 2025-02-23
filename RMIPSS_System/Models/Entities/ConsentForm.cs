using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.Entities;

public class ConsentForm
{
    
    public int Id { get; set; }

    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [MaxLength(150)] public string To { get; set; } = String.Empty;

    [MaxLength(150)] public string From { get; set; } = String.Empty;

    public Boolean? Evaluation { get; set; }

    public ConsentOption ConsentOption { get; set; } = ConsentOption.NotSpecified;

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public Boolean Status { get; set; }

    public DateOnly SubmittedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}