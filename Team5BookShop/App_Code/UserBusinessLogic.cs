﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserBusinessLogic
/// </summary>
public class UserBusinessLogic
{
    public UserBusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool LoginVerification(string userName, string password)
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            if (context.Users.Where(x => x.UserName == userName).Count() == 1)
            {    //Check if username exists
                if (context.Users.Where(x => x.UserName == userName).Select(y => y.Password).First() == password)
                {      //Check if the password is correct
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    public void RegisterUser(string user, string password)
    {
        using (BookshopEntities context = new BookshopEntities())
        {
            if (user != null && password != null)
            {
                User u = new User();
                u.UserName = user;
                u.Password = password;
                u.UserType = "User";
                context.Users.Add(u);
                context.SaveChanges();
            }
        }
    }
}