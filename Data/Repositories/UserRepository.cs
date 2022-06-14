using BankNTProject.Configurations;
using BankNTProject.Data.IRepositories;
using BankNTProject.Domain.Entities;
using Newtonsoft.Json;
using System.IO;

namespace BankNTProject.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository()
        {
            dynamic info = ReadFromAppSettings();
            path = info.Database.Users.Path;
            lastId = info.Database.Users.LastId;
        }
        protected override void SaveToAppSettings()
        {
            dynamic info = ReadFromAppSettings();
            info.Database.Users.LastId = lastId;

            string json = JsonConvert.SerializeObject(info, Formatting.Indented);
            File.WriteAllText(Constants.APP_SETTINGS_PATH, json);
        }
    }
}
