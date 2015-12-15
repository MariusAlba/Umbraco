

using AarhusWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Net.Mail;
using Umbraco.Core.Models;

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
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage(); 
            }
            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Comment");

            comment.SetValue("name", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("message", model.Message);

            Services.ContentService.Save(comment);

                
             MailMessage message = new MailMessage();
                message.To.Add(model.Email);
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = model.Message + "\n my email is: " + model.Email;

                using (SmtpClient smtp = new SmtpClient())

                    TempData["success"] = true;

                return RedirectToCurrentUmbracoPage();
            }
        }
    }


    

