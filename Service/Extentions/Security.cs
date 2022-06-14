using BankNTProject.Configurations;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BankNTProject.Service.Extentions
{
    public static class Security
    {

        public static bool IsValidPhoneNumber(this string phoneNumber)
           => Regex.IsMatch(phoneNumber, @"(?:[0-9]{2}\ [0-9]{3}\ [0-9]{2}\ [0-9]{2})");

        public static bool IsValidCardNumber(this string cardNumber)
            => Regex.IsMatch(cardNumber, @"(?:[9860]||[8600]\ [0-9]{4}\ [0-9]{4}\ [0-9]{4})");
        public static bool IsValidPassword(this string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "Password should not be empty";
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one lower case letter.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one numeric value.";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Password should contain at least one special case character.";
                return false;
            }
            else
            {
                ErrorMessage = "Invalid Email";
                return true;
            }
        }

        // this Extention function is checking email validity
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string GetHashVersion(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static bool IsAdmin(this string username, string password)
        {
            dynamic admin = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(Constants.APP_SETTINGS_PATH));
            string user = admin.Database.Admin.Username;
            string pass = admin.Database.Admin.Password;

            if (user == username && pass == password.GetHashVersion())
                return true;
            else
                return false;
        }


    }
}
