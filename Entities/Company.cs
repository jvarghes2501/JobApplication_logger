namespace Entities
{
    public class Company
    {
        public Guid CompanyID { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Website { get; set; }

      
        public Guid? LocationID { get; set; }

        // Social / review links
        public string? LinkedInUrl { get; set; }
        public string? GlassdoorUrl { get; set; }

        public string? IndeedUrl { get; set; }

        // Auditing
        public DateTime CreatedAt { get; set; }
    }
}