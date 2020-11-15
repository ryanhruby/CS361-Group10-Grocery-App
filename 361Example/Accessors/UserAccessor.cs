﻿using _361Example.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _361Example.Accessors
{
    public class UserAccessor : DbContext, IUserAccessor
    {
        private DbSet<User> Account { get; set; }

        public UserAccessor() : base(GetOptions("Server=tcp:grocerywebapp.database.windows.net,1433;Initial Catalog=GroceryWebAppDB;Persist Security Info=False;User ID=grociri;Password=#361_Group10_GroceryApp;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))

        {
            Account = Set<User>();
        }

        private static DbContextOptions GetOptions(String ConnectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), ConnectionString).Options;
        }

        public User Delete(int id)
        {
            if (Exists(id))
            {
                var user = Find(id);
                Account.Remove(user);
                base.SaveChanges();
                return user;
            }

            return null;
        }

        public bool Exists(int id)
        {
            var user = Find(id);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public User Find(int id)
        {
            return Account.Find(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Account;
        }

        public User Insert(User user)
        {
            Account.Add(user);
            base.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            Entry(user).State = EntityState.Modified;
            base.SaveChanges();
        }

        public User GetUserEmail(string email)
        {
            return Account.Where(u => u.email == email).ToArray()[0];
        }
           
        public User Find(String username, String password)
        {
            return Account.Where(u => u.email == username && u.password == password).FirstOrDefault();

        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
