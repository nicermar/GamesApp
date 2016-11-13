using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMSTYS.Repositories;
using System.IO;
using Newtonsoft.Json;

namespace GMSTYS.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new ()
    {
        private readonly string RepositoryFilePath;
        public Repository(string RepositoryFolder)
        {
            var modelName = typeof(TEntity).Name;
            if (string.IsNullOrEmpty(RepositoryFolder))
            {
                throw new InvalidOperationException("The repository folder does not exist");
            }

            if (!Directory.Exists(RepositoryFolder))
            {
                try
                {
                    Directory.CreateDirectory(RepositoryFolder);
                    
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The repository directory could not be created.", ex);
                }
            }
            try 
            {
                var repositoryFileName = string.Format("{0}.json", typeof(TEntity).Name);
                RepositoryFilePath = Path.Combine(RepositoryFolder, repositoryFileName);
                FileInfo file = new FileInfo(RepositoryFilePath);
                DirectoryInfo directory = new DirectoryInfo(RepositoryFolder);
                if (!file.Exists)
                {
                    file.Create().Dispose();
                }
            } 
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("The persistence file for model '{0}' could not be created.", modelName), ex);
            }
        }

        public void Add(TEntity model)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(RepositoryFilePath))
            {
                 json = r.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(json))
            {
                 string jsonToSave = string.Empty;
                 List<TEntity> items = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json).ToList();
                if (model.IsNew)
                {
                    items.Add(model);
                }
                else
                {
                    var itemToChange = items.Where(x => x.Id == model.Id).FirstOrDefault();
                    int indexToChange = items.IndexOf(itemToChange);
                    if (indexToChange != 1)
                    {
                        items[indexToChange] = model;
                    }
                }
                jsonToSave = JsonConvert.SerializeObject(items.ToArray());
                System.IO.File.WriteAllText(RepositoryFilePath, jsonToSave);

            }
            else
            {
                List<TEntity> newList = new List<TEntity>();
                newList.Add(model);
                string jsonToSave = JsonConvert.SerializeObject(newList.ToArray());
                System.IO.File.WriteAllText(RepositoryFilePath, jsonToSave);
            }
        }
        public void Remove(int id)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(RepositoryFilePath))
            {
                 json = r.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(json))
            {
                List<TEntity> items = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json).ToList();
                List<TEntity> itemsToSave = items.Where(x => x.Id != id).ToList();
                string jsonToSave = string.Empty;
                if (itemsToSave.Count > 0)
                {
                    jsonToSave = JsonConvert.SerializeObject(itemsToSave.ToArray());
                }

                System.IO.File.WriteAllText(RepositoryFilePath, jsonToSave);
            }
        }

        public TEntity Get(int id)
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(RepositoryFilePath))
            {
                 json = r.ReadToEnd();
               
            }
            if (!string.IsNullOrEmpty(json))
            {

                IEnumerable<TEntity> items = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json);
                TEntity item = items.Where(x => x.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return item;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            string json = string.Empty;
            using (StreamReader r = new StreamReader(RepositoryFilePath))
            {
                 json = r.ReadToEnd();
               
            }
            if (!string.IsNullOrEmpty(json))
            {
                IEnumerable<TEntity> items = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(json);
                return items;
            }
            else
            {
                return null;
            }
            
        }

    }
}
