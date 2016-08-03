using SQLDataAccess.Enteties;
using SQLDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace TaskManager.ValidationAttributes
{
    public class UniqueAttribute : ValidationAttribute
    {
        private string entityTypeName;
        private string memberName;

        public UniqueAttribute(string entityTypeName)
        {
            this.entityTypeName = entityTypeName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.memberName = validationContext.MemberName;

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            UserRepo RepoUser = new UserRepo();
            List<User> users = RepoUser.GetAll().ToList();


            foreach (var item in users)
            {
                if (item.Username == value.ToString())
                {
                    this.ErrorMessage = "This Username already exists";
                    return false;
                }
                else if (item.Email == value.ToString())
                {
                    this.ErrorMessage = "This Email already exists";
                    return false;
                }
            }

            return true;
        }
    }
}