using GestionScolarité.Data;
using GestionScolarité.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebApplicationAuthC.Data
{
    public class MyRolesProvider : RoleProvider
    {
        MyDbContext db = new MyDbContext();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles() //
        {
            return db.Accounts.Select(c => c.Role.ToString()).Distinct().ToArray();
            

            /*AUTREMENT:
             List<string> liste = new List<string>();
             foreach (var c in db.Accounts)
             {
                 if(!liste.Contains(c.Role))
                     liste.Add(c.Role);
             }
             return liste.ToArray();*/
        }

        public override string[] GetRolesForUser(string username) //
        {
            return db.Accounts.Where(c => c.UserName == username).Select(c => c.Role.ToString()).Distinct().ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName) //
        {
            return db.Accounts.Where(c => c.UserName == username && c.Role.ToString() == roleName).Any();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}