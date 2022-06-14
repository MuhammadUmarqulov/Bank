using BankNTProject.Data.Repositories;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using System;

namespace BankNTProject.Domain.Entities
{
    public class Transient : Auditable
    {

        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public int Persantage { get; set; } = 15;

        public static implicit operator TransientViewModel(Transient trans)
        {
            var _userRepository = new UserRepository();
            return trans is null ? null : new TransientViewModel
            {
                Amount = trans.Amount,
                Duration = trans.Duration,
                User = (UserViewModel)_userRepository.Get(p => p.PrimeryKey == trans.UserId),
                Id = trans.PrimeryKey,
            };
        }
    }
}
