using BankNTProject.Configurations;
using BankNTProject.Data.IRepositories;
using BankNTProject.Domain.Entities;
using Newtonsoft.Json;
using System.IO;

namespace BankNTProject.Data.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository()
        {
            dynamic info = ReadFromAppSettings();

            path = info.Database.Cards.Path;
            lastId = info.Database.Cards.LastId;
        }

        protected override void SaveToAppSettings()
        {
            dynamic info = ReadFromAppSettings();
            info.Database.Cards.LastId = lastId;

            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(Constants.APP_SETTINGS_PATH, json);
        }
    }
}
