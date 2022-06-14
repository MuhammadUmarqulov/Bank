using BankNTProject.Service.DTOs.UserDTOs;
using System;

namespace BankNTProject.Service.DTOs.CreditDTOs
{
    public class CreditViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }
        public decimal Amount { get; set; }
        public float Duration { get; set; }
    }
}
