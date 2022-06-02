using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labixa.ViewModels.AccountManagement
{
    public class PermissionEditAccountModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public SystemRoles RoleId { get; set; }
        public bool Activated { get; set; }
    }
}