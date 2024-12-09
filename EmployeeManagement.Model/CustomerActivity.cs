namespace EmployeeManagement.Data
{
    public class CustomerActivity
    {
        public CustomerActivity(string customerId, DateTime activityDate)
        {
            CustomerId = customerId;
            ActivityDate = activityDate;
        }

        public string CustomerId { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}