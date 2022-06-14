using BankNTProject.Data.IRepositories;
using BankNTProject.Data.Repositories;
using BankNTProject.Service.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankNTProject.Domain.Entities
{
    public class User : Auditable
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Guid> CreditsId { get; set; } = Enumerable.Empty<Guid>();
        public IEnumerable<Guid> CardsId { get; set; } = Enumerable.Empty<Guid>();
        public IEnumerable<Guid> TransientsId { get; set; } = Enumerable.Empty<Guid>();
        public string Username { get; set; }

        public static explicit operator UserViewModel(User user)
        {
            ICreditRepository creditRepository = new CreditRepository();
            ICardRepository cardRepository = new CardRepository();
            ITransientRepository transientRepository = new TransientRepository();

            if (user is null)
                return null;

            var conv = new UserViewModel()
            {
                Id = user.PrimeryKey,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            };

            if (user.CreditsId != null)
                conv.Credits = user.CreditsId.Select(x => creditRepository.Get(p => p.PrimeryKey == x));
            else
                conv.Credits = new List<Credit>();

            if (user.CardsId != null)
                conv.Cards = user.CardsId.Select(x => cardRepository.Get(p => p.PrimeryKey == x));
            else
                conv.Cards = new List<Card>();

            if (user.TransientsId != null)
                conv.Transients = user.TransientsId.Select(x => transientRepository.Get(p => p.PrimeryKey == x));
            else
                conv.Transients = new List<Transient>();

            return conv;

        }
    }
}
