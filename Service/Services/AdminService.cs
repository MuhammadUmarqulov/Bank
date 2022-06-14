using BankNTProject.Data.IRepositories;
using BankNTProject.Data.Repositories;
using BankNTProject.Domain.Entities;
using BankNTProject.Service.DTOs.CardDTOs;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using BankNTProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankNTProject.Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly ICardRepository cardRepository;
        private readonly IUserRepository userRepository;
        private readonly ICreditRepository creditRepository;
        private readonly ITransientRepository transientRepository;

        public AdminService()
        {
            cardRepository = new CardRepository();
            userRepository = new UserRepository();
            creditRepository = new CreditRepository();
            transientRepository = new TransientRepository();
        }
        public IEnumerable<CardViewModel> GetAllCards()
        {
            var cards = cardRepository.GetAll();
            if (cards == null)
                return null;
            return cardRepository.GetAll().Select(p => (CardViewModel)p);
        }
        public IEnumerable<CreditViewModel> GetAllCredits()
        {
            var cards = creditRepository.GetAll();
            if (cards == null)
                return null;
            return creditRepository.GetAll().Select(p => (CreditViewModel)p);
        }
        public IEnumerable<TransientViewModel> GetAllTransients()
        {
            var cards = transientRepository.GetAll();
            if (cards == null)
                return null;
            return transientRepository.GetAll().Select(p => (TransientViewModel)p);
        }
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var cards = userRepository.GetAll();
            if (cards == null)
                return null;
            return userRepository.GetAll().Select(p => (UserViewModel)p);
        }
        public CardViewModel GetCard(Func<Card, bool> predicate)
            => (CardViewModel)cardRepository.Get(predicate);

        public CreditViewModel GetCredit(Func<Credit, bool> predicate)
            => (CreditViewModel)creditRepository.Get(predicate);

        public TransientViewModel GetTransient(Func<Transient, bool> predicate)
            => (TransientViewModel)transientRepository.Get(predicate);

        public UserViewModel GetUser(Func<User, bool> predicate)
            => (UserViewModel)userRepository.Get(predicate);
    }
}
