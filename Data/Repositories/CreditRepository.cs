using BankNTProject.Configurations;
using BankNTProject.Data.IRepositories;
using BankNTProject.Domain.Entities;
using Newtonsoft.Json;
using System.IO;

namespace BankNTProject.Data.Repositories
{
    public class CreditRepository : GenericRepository<Credit>, ICreditRepository
    {
        public CreditRepository()
        {
            dynamic info = ReadFromAppSettings();

            path = info.Database.Credits.Path;
            lastId = info.Database.Credits.LastId;

        }
        protected override void SaveToAppSettings()
        {
            dynamic info = ReadFromAppSettings();
            info.Database.Credits.LastId = lastId;

            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(Constants.APP_SETTINGS_PATH, json);
        }
    }
}
