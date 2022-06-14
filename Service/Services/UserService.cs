using AutoMapper;
using BankNTProject.Data.IRepositories;
using BankNTProject.Data.Repositories;
using BankNTProject.Domain.Entities;
using BankNTProject.Domain.Enums;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using BankNTProject.Service.Extentions;
using BankNTProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankNTProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICreditRepository _creditRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ITransientRepository _transactionRepository;
        public UserService()
        {
            _transactionRepository = new TransientRepository();
            _userRepository = new UserRepository();
            _cardRepository = new CardRepository();
            _creditRepository = new CreditRepository();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserForCreation>().ReverseMap();
                cfg.CreateMap<Credit, CreditForCreation>().ReverseMap();
                cfg.CreateMap<User, UserForUpdate>().ReverseMap();
                cfg.CreateMap<Transient, TransientForCreation>().ReverseMap();

            }).CreateMapper();

        }

        public UserViewModel Create(UserForCreation dto)
        {
            if (IsAlreadyExist(dto.Username, dto.Password) is null)
            {
                var user = _mapper.Map<User>(dto);
                user.Password = dto.Password.GetHashVersion();
                var res = _userRepository.Create(user);

                return (UserViewModel)res;
            }
            return null;
        }
        public bool Delete(Guid id)
            => _userRepository.Delete(id);

        public UserViewModel Update(Guid id, UserForUpdate dto)
        {
            var user = _userRepository.Get(p => p.PrimeryKey == id);

            if (user == null)
                return null;

            var newUser = _mapper.Map<User>(dto);

            newUser.PrimeryKey = id;
            newUser.Password = dto.Password.GetHashVersion();
            newUser.CreatedAt = user.CreatedAt;
            newUser.Email = user.Email;
            newUser.FullName = user.FullName;
            newUser.CardsId = user.CardsId;
            newUser.CreditsId = user.CreditsId;
            newUser.TransientsId = user.TransientsId;


            return (UserViewModel)_userRepository.Update(id, newUser);

        }

        public UserViewModel Get(Func<User, bool> predicate)
            => (UserViewModel)_userRepository.Get(predicate);

        IEnumerable<UserViewModel> IUserService.GetAll()
            => _userRepository.GetAll().Select(x => (UserViewModel)x);


        public UserViewModel AddCredit(Guid id, CreditForCreation dto)
        {
            var credit = _mapper.Map<Credit>(dto);

            var result = _userRepository.Get(p => p.PrimeryKey == id);

            result.CreditsId = result.CreditsId is not null ?
                result.CreditsId.Append(credit.PrimeryKey) : new List<Guid>() { credit.PrimeryKey };

            credit.UserId = id;

            _creditRepository.Create(credit);

            return (UserViewModel)_userRepository.Update(id, result);
        }

        public UserViewModel AddCard(Guid id, TypeCard typeCard)
        {
            var result = _userRepository.Get(p => p.PrimeryKey == id);

            Console.WriteLine(id);

            var card = new Card()
            {
                PrimeryKey = id,
                ExpirationDate = (DateTime.Now.Year + 5).ToString() + "/" + DateTime.Now.Month.ToString(),
                CardHolderName = result.FullName,
                TypeCard = typeCard,
                CardNumber = typeCard == TypeCard.Humo ? "9860 " + GetRendomCardNumber() : "8600 " + GetRendomCardNumber()
            };

            _cardRepository.Create(card);

            result.CardsId = result.CardsId is not null ? result.CardsId.Append(card.PrimeryKey) : new List<Guid>()
            {
                card.PrimeryKey,
            };


            return (UserViewModel)_userRepository.Update(id, result);
        }

        public UserViewModel IsAlreadyExist(string username, string Password)
        {
            var user = _userRepository.Get(p => p.Username == username);

            if (user is null)
                return null;

            else if (user.Password == Password.GetHashVersion())
                return (UserViewModel)user;

            return null;
        }

        public UserViewModel AddTransient(Guid id, TransientForCreation dto)
        {
            var transient = _mapper.Map<Transient>(dto);

            var result = _userRepository.Get(p => p.PrimeryKey == id);
            result.TransientsId = result.TransientsId is not null ?
                result.TransientsId.Append(transient.PrimeryKey) : new List<Guid>() { transient.PrimeryKey };

            transient.UserId = id;

            _transactionRepository.Create(transient);

            return (UserViewModel)_userRepository.Update(id, result);
        }

        private string GetRendomCardNumber()
        {
            var random = new Random();
            string cardNumber = "";
            for (int i = 0; i < 12; i++)
            {
                if (i == 4 || i == 8)
                    cardNumber += " ";
                cardNumber += random.Next(0, 9);
            }
            return cardNumber;
        }

    }
}
