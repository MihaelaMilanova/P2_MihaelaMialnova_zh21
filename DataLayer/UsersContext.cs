using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class UsersContext : IDb<User, int>
	{
		private MainDBContext dbContext;

		public UsersContext(MainDBContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public void Create(User item)
		{
			dbContext.Users.Add(item);
			dbContext.SaveChanges();
		}

		public void Delete(int key)
		{
            User userFromDb = Read(key);
            dbContext.Users.Remove(userFromDb);
            dbContext.SaveChanges();
        }

		public User Read(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
		{
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties) query = query.Include(g => g.Interests);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

			User userFromDb = query.FirstOrDefault(g => g.Id == key);

            if (userFromDb is null) throw new ArgumentException($"User with id = {key} does not exist!");

            return userFromDb;
        }

		public List<User> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = false)
		{
            IQueryable<User> query = dbContext.Users;

            if (useNavigationalProperties) query = query.Include(g => g.Interests);
            if (isReadOnly) query = query.AsNoTrackingWithIdentityResolution();

            return query.ToList();
        }

		public void Update(User item, bool useNavigationalProperties = false)
		{
            User userFromDb = Read(item.Id, useNavigationalProperties);

            dbContext.Entry<User>(userFromDb).CurrentValues.SetValues(item);

            if (useNavigationalProperties)
            {
                List<Interest> interests = new List<Interest>(item.Interests.Count);
                for (int i = 0; i < item.Interests.Count; i++)
                {
                    Interest interestFromDb = dbContext.Interests.Find(item.Interests[i].Id);

                    if (userFromDb is not null) interests.Add(interestFromDb);
                    else interests.Add(item.Interests[i]);
                }

                userFromDb.Interests = interests;
            }

            dbContext.SaveChanges();
        }
	}
}
