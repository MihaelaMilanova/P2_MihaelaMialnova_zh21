using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class InterestsContext: IDb<Interest, int>
    {
        private MainDBContext dbContext;

        public InterestsContext(MainDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Interest item)
        {
            Field fieldFromDb = dbContext.Fields.Find(item.FieldOfInterest.Id);

            if (fieldFromDb != null) item.FieldOfInterest = fieldFromDb;

            List<User> users = new List<User>(item.Users.Count);
            for (int i = 0; i < item.Users.Count; ++i)
            {
                User userFromDb = dbContext.Users.Find(item.Users[i].Id);
                if (userFromDb != null) users.Add(userFromDb);
                else users.Add(item.Users[i]);
            }
            item.Users = users;

            dbContext.Interests.Add(item);
            dbContext.SaveChanges();
        }

        public void Delete(int key)
        {
            Interest interestFromDb = Read(key);
            dbContext.Interests.Remove(interestFromDb);
            dbContext.SaveChanges();
        }

        public Interest Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Interest> query = dbContext.Interests;
            if (useNavigationalProperties) query = query.Include(o => o.Users).Include(o => o.FieldOfInterest);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            Interest interest = query.FirstOrDefault(o => o.Id == key);

            if (interest == null) throw new ArgumentException($"Interest with id = {key} does not exist!");

            return interest;
        }

        public List<Interest> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            IQueryable<Interest> query = dbContext.Interests;
            if (useNavigationalProperties) query = query.Include(o => o.Users).Include(o => o.FieldOfInterest);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

        public void Update(Interest item, bool useNavigationalProperties = false)
        {
            dbContext.Interests.Update(item);
            dbContext.SaveChanges();
        }
    }
}
