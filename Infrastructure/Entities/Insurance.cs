namespace Domain.Entities
{
    public class Insurance
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<UserInsurance> UserInsurances { get; set; }
    }
}