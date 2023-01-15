using AMSS.Rest.Booking.DataAccess.Factory;
using AMSS.Rest.Booking.DTO;
using AMSS.Rest.Booking.Service.Authentification;
using AMSS.Rest.Booking.Service.Model.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AMSS.Rest.Booking.Services.Email;

public class ServiceEmail : IServiceEmail
{
    private string apiKey = string.Empty;
    Dictionary<string, string> emailMap;
    private IServiceAuthentification _manage;
    private static Random random;
    private readonly IServiceAccounts serviceAccount;
    public ServiceEmail(IServiceAuthentification manage, IServiceAccounts serviceAccount, string key)
    {
        apiKey = key;
        _manage = manage;
        this.serviceAccount = serviceAccount;
        emailMap = new Dictionary<string, string>();
    }
    public async Task SendCreatedEmailAsync(string emailTo)
    {
        var account = await serviceAccount.SearchByEmailAsync(emailTo);

        if (account != null) throw new Exception("Email does not exits");

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cardetailinggarage@outlook.com");
        var to = new EmailAddress(emailTo);
        var subject = "A new account has been created";
        var body = "";
        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            body,
            SetContent("Account created successfully", emailTo, "", "We are glad to have you here")
            );
        var respone = await client.SendEmailAsync(msg);

        if (!respone.IsSuccessStatusCode) throw new Exception("Email Send Error");
    }
    public async Task SendRentMadeEmailAsync(int userId)
    {
        var user = await serviceAccount.SearchByIdAsync(userId);
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cardetailinggarage@outlook.com");
        var to = new EmailAddress(user.Email);
        var subject = "A new reny has been made";
        var body = "";
        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            body,
            SetContent(
             "Rent has been made succesfully",
             "",
             "Enjoy the ride",
             "You will recive a email form us when the invoice is ready")
            );
        var respone = await client.SendEmailAsync(msg);

        if (!respone.IsSuccessStatusCode) throw new Exception("Email Send Error");
    }
    public async Task SendRentFinishedEmailAsync(int userId)
    {
        var user = await serviceAccount.SearchByIdAsync(userId);
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cardetailinggarage@outlook.com");
        var to = new EmailAddress(user.Email);
        var subject = "Rent is done";
        var body = "";
        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            body,
            SetContent(
             "This rent has ended",
             "",
             "In your account, at my invoices section you can download your invoice",
             "We are glad to have you here")
            );
        var respone = await client.SendEmailAsync(msg);

        if (!respone.IsSuccessStatusCode) throw new Exception("Email Send Error");
    }
    public async Task SendForgotPasswordEmailAsync(string emailTo)
    {
        var account = await serviceAccount.SearchByEmailAsync(emailTo);

        if (account != null) throw new Exception("Email does not exits");

        var key = RandomString(7);

        if (!emailMap.ContainsKey(key))
            emailMap.Add(key, emailTo);


        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("cardetailinggarage@outlook.com");
        var to = new EmailAddress(emailTo);
        var subject = "Reset password";
        var body = "";
        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            body,
            SetContent(
             "Login to your account with a unic code",
             "",
             "Use this code " + key + " to login",
             "After you login, we recomand to change your password")
            );
        var respone = await client.SendEmailAsync(msg);

        if (!respone.IsSuccessStatusCode) throw new Exception("Email Send Error");
    }
    public async Task<dynamic> GetTokenForForgotPasswordAsync(string email, string key)
    {
        if (string.IsNullOrEmpty(key) || !emailMap.ContainsKey(key)) throw new Exception("Invalid code");

        var model = await serviceAccount.SearchByUserNameAsync(emailMap.GetValueOrDefault(key)) ?? throw new Exception("Email not found");

        if (model.Email != email) throw new Exception("Invalid code");

        emailMap.Remove(key);

        return await _manage.GenerateTokenAsync(
            new AuthDto()
            {
                Password = model.Password,
                UserName = model.UserName
            });
    }
    private static string RandomString(int length)
    {
        random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    private string SetContent(string titleMessage, string values, string info1, string info2)
    {
        var footer = "© 2022 Car Detailing Garage .";
        var style = "<style> #yiv6451144479 .yiv6451144479awl a { color: #FFFFFF; text-decoration: none; } #yiv6451144479 .yiv6451144479abml a { color: #000000; font-family: Roboto-Medium, Helvetica, Arial, sans-serif; font-weight: bold; text-decoration: none; } #yiv6451144479 .yiv6451144479adgl a { color: rgba(0, 0, 0, 0.87); text-decoration: none; } #yiv6451144479 .yiv6451144479afal a { color: #b0b0b0; text-decoration: none; } @media screen and (min-width:600px) { #yiv6451144479 .yiv6451144479v2sp { padding: 6px 30px 0px; } #yiv6451144479 .yiv6451144479v2rsp { padding: 0px 10px; } } @media screen and (min-width:600px) { #yiv6451144479 .yiv6451144479mdv2rw { padding: 40px 40px; } } </style>";
        return @$"<div class='jb_0 X_6MGW N_6Fd5'> <div> <div id='yiv6451144479'> {style} <style class='darkreader darkreader--sync' media='screen'></style> <div> <table width='100%' height='100%' style='min-width:348px;' border='0' cellspacing='0' cellpadding='0' lang='en'> <tbody> <tr height='32' style='min-height:32px;'> <td></td> </tr> <tr align='center'> <td> <div> <div></div> </div> <table border='0' cellspacing='0' cellpadding='0' style='padding-bottom:20px;max-width:516px;min-width:220px;'> <tbody> <tr> <td width='8' style='width:8px;'></td> <td> <div style='border-style: solid; border-width: thin; border-color: rgb(218, 220, 224); border-radius: 8px; padding: 40px 20px; --darkreader-inline-border-top:#3a3e41; --darkreader-inline-border-right:#3a3e41; --darkreader-inline-border-bottom:#3a3e41; --darkreader-inline-border-left:#3a3e41;' align='center' class='yiv6451144479mdv2rw' data-darkreader-inline-border-top='' data-darkreader-inline-border-right='' data-darkreader-inline-border-bottom='' data-darkreader-inline-border-left=''> <div style='border-bottom: thin solid rgb(218, 220, 224); color: rgba(0, 0, 0, 0.87); line-height: 32px; padding-bottom: 24px; text-align: center; --darkreader-inline-border-bottom:#3a3e41; --darkreader-inline-color:rgba(232, 230, 227, 0.87);' data-darkreader-inline-border-bottom='' data-darkreader-inline-color=''> <div style='font-size:24px;'> {titleMessage} </div> <table align='center' style='margin-top:8px;'> <tbody> <tr style='line-height:normal;'> <td><a rel='nofollow noopener noreferrer' style='color: rgba(0, 0, 0, 0.87); font-size: 14px; line-height: 20px; --darkreader-inline-color:rgba(232, 230, 227, 0.87);' data-darkreader-inline-color=''>{values}</a> </td> </tr> </tbody> </table> </div> <div style='font-family: Roboto-Regular, Helvetica, Arial, sans-serif; font-size: 14px; color: rgba(0, 0, 0, 0.87); line-height: 20px; padding-top: 20px; text-align: center; --darkreader-inline-color:rgba(232, 230, 227, 0.87);' data-darkreader-inline-color=''> <h3>{info1}</h3> {info2} </div> </div> <div style='text-align:center;'> <div style='font-family: Roboto-Regular, Helvetica, Arial, sans-serif; color: rgba(0, 0, 0, 0.54); font-size: 11px; line-height: 18px; padding-top: 12px; text-align: center; --darkreader-inline-color:rgba(232, 230, 227, 0.54);' data-darkreader-inline-color=''> <div>You received this email to let you know about important changes to your Account and services.</div> <div style='direction:ltr;'>{footer}, </div> </div> </div> </td> <td width='8' style='width:8px;'></td> </tr> </tbody> </table> </td> </tr> <tr height='32' style='min-height:32px;'> <td></td> </tr> </tbody> </table> </div> </div> </div> </div>";
    }
}
