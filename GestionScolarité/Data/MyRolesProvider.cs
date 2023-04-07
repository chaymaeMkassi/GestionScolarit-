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
            return db.Users.Select(c => c.Role.ToString()).Distinct().ToArray();
           
           
        }

        public override string[] GetRolesForUser(string id) //
        {
            return db.Users.Where(c => c.Id.ToString() == id).Select(c => c.Role.ToString()).Distinct().ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string id, string roleName) //
        {
            return db.Users.Where(c => c.Id.ToString() == id && c.Role.ToString() == roleName).Any();
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