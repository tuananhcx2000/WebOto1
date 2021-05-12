using Car.Models;
using Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    public class TintucController : Controller
    {
        private WebOtoEntities db = new WebOtoEntities();
        // GET: Tintuc
        public ActionResult Index(int? page)
        {
            int PageSize = 3;
            int PageNumber = (page ?? 1);

            var sp = db.Blogs.OrderByDescending(n => n.NgayCapNhat).ToList();
            return View(sp.ToPagedList(PageNumber, PageSize));
        }
        public ActionResult BlogDetails(int blogid)
        {
            var blog = db.Blogs.Find(blogid);
            return View(blog);
        }
        [HttpPost]
        public ActionResult Index(string fullName, string email, string message)
        {
            fullName = Request.Form["name"];
            email = Request.Form["email"];
            message = Request.Form["message"];
            if (ModelState.IsValid)
            {
                string ct = System.IO.File.ReadAllText(Server.MapPath("~/Content/templateMail/Mail.html"));
                ct = ct.Replace("{{CustomerName}}", fullName);
                ct = ct.Replace("{{EmailName}}", email);
                ct = ct.Replace("{{Content}}", message);
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new ModelMail().SendMail(toEmail, "Nội dung phản hồi", ct);
                new ModelMail().SendMail(email, "Nội dung phản hồi", ct);

            }
            else { }
            return View();
        }
    }
}