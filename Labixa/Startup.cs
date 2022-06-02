using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Outsourcing.Data;
using Outsourcing.Data.Models;
using Owin;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

[assembly: OwinStartupAttribute(typeof(Labixa.Startup))]
namespace Labixa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console().WriteTo.RollingFile(Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, @"Logs\\myapp{Date}.txt"), retainedFileCountLimit: 31)
            .CreateLogger();
            //.WriteTo.RollingFile(@"C:\Logs\myapp{Date}.txt", retainedFileCountLimit: 10)
            ConfigureAuth(app);
            CreateRoleSandUsers();
        }
        // In this method we will create default User roles and Admin user for login  
        private void CreateRoleSandUsers()
        {
            OutsourcingEntities context = new OutsourcingEntities();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<User>(new UserStore<User>(context));
            // In Startup creating first Admin Role and creating a default Admin User
            if (!RoleManager.RoleExists("Admin"))
            {
                //first we create admin role
                var role = new IdentityRole();
                role.Name = "Admin";
                RoleManager.Create(role);
                //Here create a admin supper user who will maintain website
                var user = new User();
                user.UserName = "Administrator";
                user.Email = "nghiiatran1998@gmail.com";
                user.RoleId = SystemRoles.RoleAdmin;
                user.Activated = true;
                string userPWD = "zaq@1234";
                var chkUser = UserManager.Create(user, userPWD);
                //add default user to tole admin;
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            //create role manager
            if (!RoleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                RoleManager.Create(role);
                //Here create a manager supper user who will maintain website
                var user = new User();
                user.UserName = "Management";
                user.Email = "managementg@gmail.com";
                user.RoleId = SystemRoles.RoleManger;
                user.Activated = true;
                string userPWD = "123456";
                var chkUser = UserManager.Create(user, userPWD);
                //add default user to tole admin;
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Manager");
                }
            }
            //create role employee
            if (!RoleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                RoleManager.Create(role);
                //Here create a manager supper user who will maintain website
                var user = new User();
                user.UserName = "Employment";
                user.Email = "employment@gmail.com";
                user.RoleId = SystemRoles.RoleEmployee;
                user.Activated = true;
                string userPWD = "123456";
                var chkUser = UserManager.Create(user, userPWD);
                //add default user to tole admin;
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Employee");
                }
            }
        }
    }
}
