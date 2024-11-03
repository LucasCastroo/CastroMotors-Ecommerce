using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CastroMotors.Models;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace CastroMotors
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Host = System.Configuration.ConfigurationManager.AppSettings["EmailHost"];
                smtpClient.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                smtpClient.EnableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailEnableSsl"]);
                smtpClient.Credentials = new NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["EmailUsername"],
                    System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["EmailUsername"], "Castro Motors"),
                    Subject = message.Subject,
                    Body = message.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(message.Destination);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            // Configuração de validação de usuário
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configuração de validação de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configuração de bloqueio de usuário
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Provedores de autenticação de dois fatores
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é {0}"
            });

            // Configuração do serviço de email para redefinição de senha
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            // Configuração do gerador de tokens para redefinição de senha e autenticação de dois fatores
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
