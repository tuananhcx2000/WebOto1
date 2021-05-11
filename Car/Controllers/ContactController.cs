using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string fullName, string email, string subject, string content)
        {
            fullName = Request.Form["name"];
            email = Request.Form["email"];
            subject = Request.Form["subject"];
            content = Request.Form["message"];
            if (ModelState.IsValid)
            {
                string ct = System.IO.File.ReadAllText(Server.MapPath("~/Content/templateMail/Mail.html"));
                ct = ct.Replace("{{CustomerName}}", fullName);
                ct = ct.Replace("{{EmailName}}", email);
                ct = ct.Replace("{{Subject}}", subject);
                ct = ct.Replace("{{Content}}", content);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new ModelMail().SendMail(toEmail, "Nội dung phản hồi", ct);
                new ModelMail().SendMail(email, "Nội dung phản hồi", ct);

            }
            else { }
            return View();
        }
    }
}