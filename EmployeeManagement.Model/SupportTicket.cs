
namespace EmployeeManagement.Data
{
    public class SupportTicket
    {

        public SupportTicket(string category, DateTime supportDateTime)
        {
            Category = category;
            SupportDateTime = supportDateTime;
        }

        public string Category { get; set; }
        public DateTime SupportDateTime { get; set; }

    }
}