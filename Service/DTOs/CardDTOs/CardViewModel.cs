using BankNTProject.Domain.Enums;
using System;

namespace BankNTProject.Service.DTOs.CardDTOs
{
    public class CardViewModel
    {
        public Guid Id { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public TypeCard TypeCard { get; set; }
    }
}
