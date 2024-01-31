namespace Domain.Entities
{
    public class UserInsurance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }
    }
}
