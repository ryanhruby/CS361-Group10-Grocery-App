﻿using _361Example.Accessors;
using _361Example.Models;
using IdentityServer4.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace _361Example.Engines
{
    // This is the engine we used to unit test
    public class UserEngine : IUserEngine
    {
        private readonly IUserAccessor _userAccessor;

        public UserEngine(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userAccessor.GetAllUsers();
        }

        //GetUser will return null if the User isn't found
        public User GetUser(int id)
        {
            return _userAccessor.Find(id);

        }

        //Returns the user if found, null if not
        public User VerifyUser(String username, String password)
        {
            return _userAccessor.Find(username, password);
        }

        public User InsertUser(User user)
        {
            List<User> allUsers = GetAllUsers().ToList();

            foreach (User u in allUsers)
            {
                if (u.Id == user.Id)
                {
                    throw new DuplicateNameException();
                }
                else if (u.email == user.email)
                {
                    throw new DuplicateNameException();
                }
            }

            _userAccessor.Insert(user);
            _userAccessor.SaveChanges();

            return user;
        }

        public User UpdateUser(User user)
        {
            _userAccessor.Update(user);

            if (GetUser(user.Id) != user)
            {
                return null;
            }

            return user;
        }

        //Returns null if the User doesn't exist and can't be deleted
        public User DeleteUser(User user)
        {
            return _userAccessor.Delete(user.Id);
        }

        public User GetUserEmail(string email)
        {
            return _userAccessor.GetUserEmail(email);

        }
    }
        

}

