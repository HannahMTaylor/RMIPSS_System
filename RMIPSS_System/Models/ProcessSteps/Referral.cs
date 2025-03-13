using RMIPSS_System.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.ProcessSteps;

public class Referral
{
    public int ID {  get; set; }

    public int StudentId { get; set; }

    //section 1 of form
    public Student? Student { get; set; }

    //section two of form
    public string? ReasonsForReferral { get; set; } = string.Empty;
    public string? OtherReasonsForReferral { get; set; }
    //this has not been added to Db table Referrals yet bc not working
    // public PdfUpload? MIDScoringSheet { get; set; }

    [MaxLength(560)]
    public string? AreasOfConcernAndHelpNeededDescription { get; set; }

    //section 3
    public int ReferrerId { get; set; }
    public ReferrerPerson? Referrer { get; set; }

    //section 4: school use only
    public DateOnly ReferralReceived {  get; set; }
    public DateOnly TeamRecommendation {  get; set; }
    public DateOnly DispositionNoticeToReferrer {  get; set; }
    public DateOnly ParentalConsentForEvaluation {  get; set; }
    public DateOnly EvaluationTeamRecommendation {  get; set; }
    public DateOnly ParentNoticeForMeeting {  get; set; }
    public DateOnly ReferredToChildStudyTeam {  get; set; }

    public string? Disposition {  get; set; }
    public DateOnly DispositionNoticeToParent {  get; set; }
    public DateOnly ReferralToEvaluationTeam {  get; set; }
    public string? Recommendation { get; set; }
    public DateOnly IEPMeeting {  get; set; }

}
