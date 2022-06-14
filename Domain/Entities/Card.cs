using BankNTProject.Domain.Enums;
using BankNTProject.Service.DTOs.CardDTOs;

namespace BankNTProject.Domain.Entities
{
    public class Card : Auditable
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public TypeCard TypeCard { get; set; }

        public static implicit operator CardViewModel(Card card)
            => card is null ? null : new CardViewModel
            {
                Id = card.PrimeryKey,
                CardNumber = card.CardNumber,
                CardHolderName = card.CardHolderName,
                ExpirationDate = card.ExpirationDate,
                TypeCard = card.TypeCard
            };
    }
}
