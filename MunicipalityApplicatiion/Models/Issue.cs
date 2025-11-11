using MunicipalityApplicatiion.DataStructures;

namespace MunicipalityApplicatiion.Models
{
    public class Issue
    {
        public int IssueId { get; set; } // unique identifier
        public string IssueLocation { get; set; } = string.Empty;
        public IssueCategory IssueCategory { get; set; }
        public string IssueDescription { get; set; } = string.Empty;
        public StringList Attachments { get; set; } = new StringList();
    }

    public enum IssueCategory
    {
        Sanitation,
        Roads,
        Utilities,
        Safety,
        Other
    }
}