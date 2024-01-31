﻿namespace Domain.DTO
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<InsuranceGetDTO> Insurances { get; set; }
    }
}
