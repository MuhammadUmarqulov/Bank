using BankNTProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BankNTProject.Service.DTOs.UserDTOs
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Credit> Credits { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<Transient> Transients { get; set; }

        public string Username { get; set; }

    }
}
