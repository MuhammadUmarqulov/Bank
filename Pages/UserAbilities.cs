using BankNTProject.Domain.Enums;
using BankNTProject.Service.DTOs.CreditDTOs;
using BankNTProject.Service.DTOs.TransientDTOs;
using BankNTProject.Service.DTOs.UserDTOs;
using BankNTProject.Service.Interfaces;
using BankNTProject.Service.Services;
using Sharprompt;
using System;

namespace BankNTProject.Pages
{
    public class UserAbilities
    {
        private readonly IUserService userService;

        public UserAbilities(Guid id)
        {
            userService = new UserService();
            GetUserMains(id);
        }

        public void GetUserMains(Guid id)
        {
            var mainChoise = Prompt.Select("Select one of this: ", new[] { "Update", "Log out", "Get Credit", "Take new Card", "Make a deposit" });

            switch (mainChoise)
            {
                case "Update":
                    UpdateUser(id);
                    goto again;
                case "Log out":
                    LogOut();
                    goto again;

                case "Get Credit":
                    GetCredit(id);
                    goto again;

                case "Take new Card":
                    TakeNewCard(id);
                    goto again;

                case "Make a deposit":
                    MakeADeposit(id);
                    goto again;

            }
        again:
            GetUserMains(id);
        }

        private void MakeADeposit(Guid id)
        {
            var deposit = new TransientForCreation();
        TakeAgain:
            try
            {
                deposit.Amount = Prompt.Input<decimal>("Enter Amount of deposit: ");
                deposit.Duration = Prompt.Input<int>("Enter Duration of deposit: ");
            }
            catch
            {
                Console.WriteLine("Invalid input");
                goto TakeAgain;
            }
            userService.AddTransient(id, deposit);
        }

        private void TakeNewCard(Guid id)
        {
            var typeCard = Prompt.Select("Select one of this: ", new[] { "Humo", "Uzcard" });

            userService.AddCard(id, (TypeCard)Enum.Parse(typeof(TypeCard), typeCard, true));
        }

        private void GetCredit(Guid id)
        {
            var credit = new CreditForCreation();
        TakeCreaditAgain:
            try
            {
                credit.Amount = Prompt.Input<decimal>("Enter amount of credit: ");
                credit.Duration = Prompt.Input<float>("Enter duration of credit: ");
            }
            catch
            {
                Console.WriteLine("Invalid input");
                goto TakeCreaditAgain;
            }
            userService.AddCredit(id, credit);
        }

        private void LogOut()
        {
            new MainPage();
        }

        private void UpdateUser(Guid id)
        {
            var updatedUser = new UserForUpdate();

        UpdateUserAgain:
            try
            {
                updatedUser.Username = Prompt.Input<string>("Enter new username: ");
                updatedUser.Password = Prompt.Password("Enter new password: ");
            }
            catch
            {
                Console.WriteLine("Invalid input");
                goto UpdateUserAgain;
            }
            userService.Update(id, updatedUser);
        }
    }
}
