using ServerDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerDB.Repositories
{
    public class UserRepository
    {
        private readonly ServerDataBase context;

        public UserRepository(ServerDataBase context)
        {
            this.context = context;
        }

        public User AddUser(string username, string surname, int age, string city, string address, string postalCode)
        {
            var user = new User
            {
                Username = username,
                Surname = surname,
                Age = age,
                City = city,
                Address = address,
                PostalCode = postalCode
            };

            context.Users.Add(user);
            context.SaveChanges();
            context.Users.Where(u => u.Username == "Jozko");

            return user;
        }
    }
}
