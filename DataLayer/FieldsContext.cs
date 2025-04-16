using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FieldsContext : IDb<Field, int>
    {
        private MainDBContext dbContext;

        public FieldsContext(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Field item)
        {
            dbContext.Fields.Add(item);
            dbContext.SaveChanges();
        }

        public Field Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Field> query = dbContext.Fields;

            if (useNavigationalProperties) query = query.Include(g => g.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Field field = query.FirstOrDefault(g => g.Id == key);
            if (field is null) throw new ArgumentException($"Field with id = {key} does not exist!");

            return field;
        }

        public List<Field> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Field> query = dbContext.Fields;

            if (useNavigationalProperties) query = query.Include(g => g.Users);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Field item, bool useNavigationalProperties = false)
        {
            Field fieldFromDb = Read(item.Id, useNavigationalProperties);

            dbContext.Entry<Field>(fieldFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<User> users = new List<User>(item.Users.Count);
                for (int i = 0; i < item.Users.Count; i++)
                {
                    User userFromDb = dbContext.Users.Find(item.Users[i].Id);

                    if (userFromDb is not null) users.Add(userFromDb);
                    else users.Add(item.Users[i]);
                }

                fieldFromDb.Users = users;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Field fieldFromDb = Read(key);
            dbContext.Fields.Remove(fieldFromDb);
            dbContext.SaveChanges();
        }
    }
}
