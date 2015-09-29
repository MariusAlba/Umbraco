using AarhusWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Net.Mail;

namespace AarhusWeb.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        //
        // GET: /ContactFormSurface/

        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFromSubmit(ContactForm model)
        {
            
            MailMessage message = new MailMessage();
            message.To.Add(model.Email);
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message + "\n my email is: " + model.Email;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com"; smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("kleemannmarius@gmail.com", "Atlasshrugged1987");
                smtp.EnableSsl = true;

                smtp.Send(message);
            }

            return RedirectToCurrentUmbracoPage();
        }

    }
}
