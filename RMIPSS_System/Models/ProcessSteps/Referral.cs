using RMIPSS_System.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.ProcessSteps;

public class Referral
{
    public int ID {  get; set; }
    public Student Student { get; set; }
    public List<string> ReasonsForReferral { get; set; }
    public IFormFile? MIDScoringSheet { get; set; }

    [MaxLength(560)]
    public string AreasOfConcernAndHelpNeededDescription { get; set; }
    public Referrer Referrer { get; set; }
    public DateOnly ReferralReceived {  get; set; }
    public DateOnly TeamRecommendation {  get; set; }
    public DateOnly DispositionNoticeToReferrer {  get; set; }
    public DateOnly ParentalConsentForEvaluation {  get; set; }
    public DateOnly EvaluationTeamRecommendation {  get; set; }
    public DateOnly ParentNoticeForMeeting {  get; set; }
    public DateOnly ReferredToChildStudyTeam {  get; set; }
    public string Disposition {  get; set; }
    public DateOnly DispositionNoticeToParent {  get; set; }
    public DateOnly ReferralToEvaluationTeam {  get; set; }
    public string Recommendation { get; set; }
    public DateOnly IEPMeeting {  get; set; }

}
