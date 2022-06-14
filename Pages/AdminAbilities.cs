using BankNTProject.Service.Interfaces;
using BankNTProject.Service.Services;
using Sharprompt;
using System;
using System.Collections.Generic;

namespace BankNTProject.Pages
{
    public class AdminAbilities
    {
        IAdminService adminService = new AdminService();

        public AdminAbilities()
        {
            GetMains();
        }

        private void GetMains()
        {
        Begin:
            var selection = Prompt.Select("Select one of them",
                new[]
                {
                    "Get All Users", "Get All Cards", "Get All Credits", "Get All Deposits", "Get User", "Get Card", "Log out"
                });

            IEnumerable<dynamic> result = new List<dynamic>();

            if (selection == "Log out")
                new MainPage();

            switch (selection)
            {
                case "Get All Users":
                    result = adminService.GetAllUsers();
                    break;
                case "Get All Cards":
                    result = adminService.GetAllCards();
                    break;

                case "Get All Credits":
                    result = adminService.GetAllCredits();
                    break;

                case "Get All Deposits":
                    result = adminService.GetAllTransients();
                    break;
            }

            if (result is not null)
            {
                foreach (var res in result)
                    Console.WriteLine(res.Id);
            }

            if (selection == "Get User")
            {
                var choise = Prompt.Select("Select Getting type: ", new[] { "Id", "Email" });

                switch (choise)
                {
                    case "Id":
                        var id = Prompt.Input<long>("Enter Id: ");
                        var ires = adminService.GetUser(p => p.Id == id);
                        Console.WriteLine(ires is null ? "User not found" : ires.Email);
                        break;

                    case "Email":
                        var email = Prompt.Input<string>("Enter Email: ");
                        var eres = adminService.GetUser(p => p.Email == email);
                        Console.WriteLine(eres is null ? "User not found" : eres.Id);
                        break;
                }
                goto Begin;
            }

            if (selection == "Get Card")
            {
                var choise = Prompt.Select("Select Getting type: ", new[] { "Id", "Card Number" });

                switch (choise)
                {
                    case "Id":
                        var id = Prompt.Input<long>("Enter Id: ");
                        var cardRes = adminService.GetCard(p => p.Id == id);
                        Console.WriteLine(cardRes is null ? "Card Not Found" : cardRes.CardHolderName);
                        break;

                    case "Card Number":
                        var number = Prompt.Input<string>("Enter Number: ");
                        var cardRes2 = adminService.GetCard(p => p.CardNumber == number);
                        Console.WriteLine(cardRes2 is null ? "Card Not Found" : cardRes2.CardHolderName);
                        break;

                }
                goto Begin;
            }
            goto Begin;

        }
    }
}
