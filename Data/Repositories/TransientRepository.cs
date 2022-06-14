using BankNTProject.Configurations;
using BankNTProject.Data.IRepositories;
using BankNTProject.Domain.Entities;
using Newtonsoft.Json;
using System.IO;

namespace BankNTProject.Data.Repositories
{
    public class TransientRepository : GenericRepository<Transient>, ITransientRepository
    {
        public TransientRepository()
        {
            dynamic info = ReadFromAppSettings();

            path = info.Database.Transients.Path;
            lastId = info.Database.Transients.LastId;
        }

        protected override void SaveToAppSettings()
        {
            dynamic info = ReadFromAppSettings();
            info.Database.Transients.LastId = lastId;

            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(Constants.APP_SETTINGS_PATH, json);
        }
    }
}
