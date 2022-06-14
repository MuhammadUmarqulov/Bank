using BankNTProject.Service.DTOs.UserDTOs;
using System;

namespace BankNTProject.Service.DTOs.TransientDTOs
{
    public class TransientViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
    }
}
