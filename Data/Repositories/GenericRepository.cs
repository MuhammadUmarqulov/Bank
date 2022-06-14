using BankNTProject.Configurations;
using BankNTProject.Data.IRepositories;
using BankNTProject.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankNTProject.Data.Repositories
{
    public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        protected string path;
        protected long lastId;

        public TSource Create(TSource entity)
        {
            var entities = GetAll();


            entity.Id = ++lastId;
            entity.CreatedAt = DateTime.Now;

            File.WriteAllText(path, JsonConvert.SerializeObject(entities.Append(entity), Formatting.Indented));
            SaveToAppSettings();

            return entity;
        }


        public bool Delete(Guid id)
        {
            var entities = GetAll();

            var entity = entities.FirstOrDefault(p => p.PrimeryKey == id);

            if (entity is null)
                return false;

            entities = entities.Where(p => p.PrimeryKey != id);

            string json = JsonConvert.SerializeObject(entities);
            File.WriteAllText(path, json);

            return true;
        }

        public TSource Get(Func<TSource, bool> predicate)
            => GetAll().FirstOrDefault(predicate);

        public IEnumerable<TSource> GetAll()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }

            string json = File.ReadAllText(path);

            if (string.IsNullOrEmpty(json))
            {
                File.WriteAllText(path, "[]");
                json = "[]";
            }

            return JsonConvert.DeserializeObject<IEnumerable<TSource>>(json);
        }

        public TSource Update(Guid id, TSource entity)
        {
            entity.UpdatedAt = DateTime.Now;
            var entities = GetAll()
                .Select(p => p.PrimeryKey == entity.PrimeryKey ? entity : p);


            var json = JsonConvert.SerializeObject(entities, Formatting.Indented);
            File.WriteAllText(path, json);

            return entity;
        }

        protected virtual void SaveToAppSettings()
        { }
        protected dynamic ReadFromAppSettings()
        {
            string json = File.ReadAllText(Constants.APP_SETTINGS_PATH);
            return JsonConvert.DeserializeObject<dynamic>(json);
        }

    }
}
