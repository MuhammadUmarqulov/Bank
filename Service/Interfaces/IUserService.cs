using BankNTProject.Domain.Entities;
using BankNTProject.Domain.Enums;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using System;
using System.Collections.Generic;

namespace BankNTProject.Service.Interfaces
{
    public interface IUserService
    {
        UserViewModel Create(UserForCreation dto);
        UserViewModel Get(Func<User, bool> predicate);
        IEnumerable<UserViewModel> GetAll();
        bool Delete(Guid id);
        UserViewModel Update(Guid id, UserForUpdate dto);
        UserViewModel AddCredit(Guid id, CreditForCreation dto);
        UserViewModel AddCard(Guid id, TypeCard typeCard);
        UserViewModel IsAlreadyExist(string username, string Password);
        UserViewModel AddTransient(Guid id, TransientForCreation dto);

    }
}
