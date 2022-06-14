using BankNTProject.Service.DTOs.UserDTOs;
using BankNTProject.Service.Extentions;
using BankNTProject.Service.Interfaces;
using BankNTProject.Service.Services;
using Sharprompt;
using System;

namespace BankNTProject.Pages
{
    public class MainPage
    {
        private readonly IUserService _userService;
        private Guid userId;
        public MainPage()
        {
            _userService = new UserService();
            SetMainPage();
        }

        void SetMainPage()
        {
            var mainChoice = Prompt.Select("Select one of them", new[] { "Admin", "Login", "Register", "Quit" });

            if (mainChoice == "Login")
            {
                Console.Clear();
                var user = LoginPage();

                if (user is null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login failed!!!");
                    Console.ResetColor();
                    SetMainPage();
                }
                userId = user.Id;
                UserAbilities userAbilities = new UserAbilities(userId);
                Console.Clear();

            }
            else if (mainChoice == "Register")
            {
                var newUser = RegisterPage();
                if (newUser is null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("REGISTER FAILED");
                    Console.ResetColor();
                    SetMainPage();
                }
                userId = newUser.Id;
                UserAbilities userAbilities = new UserAbilities(userId);
                Console.Clear();
            }
            else if (mainChoice == "Quit")
            {
                Console.WriteLine("\nGoodbye!");
                Environment.Exit(0);
            }
            else if (mainChoice == "Admin")
            {
                string adminUsername = "";
                string adminPassword = "";
            errorAdmin:
                try
                {
                    adminUsername = Prompt.Input<string>("Enter admin Name (Admin) : ");
                    adminPassword = Prompt.Password("Enter admin password (@Admin1011) : ");
                }
                catch { goto errorAdmin; }

                if (adminUsername.IsAdmin(adminPassword))
                    new AdminAbilities();
                else
                {
                    Console.Clear();
                    SetMainPage();
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please try again.");
                SetMainPage();
            }


        }

        private UserViewModel RegisterPage()
        {
            var user = new UserForCreation();

            Console.WriteLine("\n========== Register ==========\n");
            user.FullName = Prompt.Input<string>("Enter full name: ");

        InvalidPassword:
            user.Password = Prompt.Password("Type new password: ");
            if (!user.Password.IsValidPassword(out string ErrorMessage))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + ErrorMessage);
                Console.ResetColor();
                goto InvalidPassword;
            }

            var confirmPassword = Prompt.Password("Type Confirm password: ");
            if (user.Password != confirmPassword)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPasswords do not match. Please try again.");
                Console.ResetColor();
                goto InvalidPassword;
            }

            Console.WriteLine();
            user.Username = Prompt.Input<string>("Enter username: ");

        InvalidEmail:
            Console.WriteLine();
            user.Email = Prompt.Input<string>("Enter email: ");
            if (!user.Email.IsValidEmail())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + ErrorMessage);
                Console.ResetColor();
                goto InvalidEmail;
            }

            var finalResult = _userService.Create(user);

            if (finalResult is null)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This user already exists. Please try again.");
                Console.ResetColor();
                SetMainPage();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("REGISTER SUCCESSFULLY");
                Console.ResetColor();
                return finalResult;
            }


            return finalResult;

        }

        private UserViewModel LoginPage()
        {
            string userName = Prompt.Input<string>("Enter your  user name:");
            string password = Prompt.Password("Enter your password");

            return _userService.IsAlreadyExist(userName, password);
        }

    }
}
