using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.Models
{
    public static class Message
    {
        public const string EmailRequired = "Please Enter Email Address.";
        public const string FirstNameRequired = "Please Enter First Name.";
        public const string LastNameRequired = "Please Enter Last Name.";
    }
    public static class SessionVariable
    {
        public const string UserID = "USERID";
        public const string UserName = "UserName";
        public const string UserEMail = "UserEMail";
        public const string UserType = "UserType";
    }
}