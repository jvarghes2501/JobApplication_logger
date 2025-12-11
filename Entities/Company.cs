namespace Entities
{
    public class Company
    {
        public Guid CompanyID { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }

        public string? PositionName { get; set; }
        public string? Website { get; set; }

        public bool? isCoverLetter { get; set; }
        public Guid? LocationID { get; set; }


        // Auditing
        public DateTime CreatedAt { get; set; }
    }
}