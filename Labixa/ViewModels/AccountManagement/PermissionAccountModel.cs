using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Views.AccountManagement
{
    public class PermissionAccountModel
    {
        [Required(ErrorMessage ="User name must enter")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password must enter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public SystemRoles RolePermission { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Phone { get; set; }
    }
}