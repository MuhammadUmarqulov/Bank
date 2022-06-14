using BankNTProject.Domain.Entities;
using BankNTProject.Service.DTOs.CardDTOs;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using System;
using System.Collections.Generic;

namespace BankNTProject.Service.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        IEnumerable<CardViewModel> GetAllCards();
        IEnumerable<CreditViewModel> GetAllCredits();
        IEnumerable<TransientViewModel> GetAllTransients();

        UserViewModel GetUser(Func<User, bool> predicate);
        CardViewModel GetCard(Func<Card, bool> predicate);
        CreditViewModel GetCredit(Func<Credit, bool> predicate);
        TransientViewModel GetTransient(Func<Transient, bool> predicate);

    }
}
