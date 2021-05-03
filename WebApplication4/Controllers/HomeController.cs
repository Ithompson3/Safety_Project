
//==================================================================================
//Using section
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls;
using WebApplication4.Models;
//magnum.controllers
namespace Safety.Controllers
{
    public class HomeController : Controller
    {
        private SafetydbEntities db = new SafetydbEntities();

        // GET: Tables
        public ActionResult Index()
        {
            return View(model: db.Table_1.OrderBy(x => x.thing1).ToList());
        }

        //public JsonResult GetSubTbl(int id)
        //{
        //    var data = db.subtable1.Where(x => x.Part == id).ToList();
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

        // GET: Tables/Details/5
        public ActionResult Details(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table = db.Table_1.Find(ID);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Table_1 table)
        {
            if (ModelState.IsValid)
            {
                db.Table_1.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table = db.Table_1.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Table_1 table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "Success" });
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_1 table = db.Table_1.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_1 table = db.Table_1.Find(id);
            db.Table_1.Remove(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tables/Files/ This section is edited copypasta from Zahkl's DB based on engineering drawings table 
        [HttpGet, ActionName("Attachments")]
        public ActionResult Attachments(int id)
        {
            Session.Add("yeet", id.ToString());
            string path = @"C:\Safety\Table_1\" + id.ToString() + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            files = files.OrderBy(x => x.Name).ToArray();

            return View(files);
        }
        //Upload
        [HttpPost]
        public JsonResult Upload()
        {
            string id = (string)Session["yeet"];
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];//file upload 

                    string fileName = Path.GetFileName(file.FileName);

                    string path = @"C:\Safety\Table_1\" + id + "\\";

                    if (Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(path + fileName);
                }
                return Json("Upload sucess ");
            }
            else
                return Json("No File Selected");


        }
        //Download
        public FileResult DownloadFile(string name)
        {
            string filePath = name;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = Path.GetFileName(filePath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //end copypasta
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //end main table

        //begin for subtable1 within the edit button

        // GET: subtable1
        //public ActionResult sub1Index()
        //{
        //    return View(model: db.Table_1.OrderBy(x => x.thing1).ToList());
        //}

        // GET: subtables/Details/5
        //public ActionResult sub1Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Table_1 subtable1 = db.Table_1.Find(id);
        //    if (subtable1 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subtable1);
        //}

        // GET: Tables/Create
        //public ActionResult sub1Create()
        //{
        //    return View();
        //}

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       // [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult sub1Create(subtable1 subtable1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.subtable1.Add(subtable1);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(subtable1);
        //}

        // GET: Tables/Edit/5
        //public ActionResult sub1Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Table subtable1 = db.Tables.Find(id);
        //    if (subtable1 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subtable1);
        //}

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult sub1Edit(Table_1 subtable1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(subtable1).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(new { success = true, message = "Success" });
        //    }
        //    return View(subtable1);
        //}

        // GET: Tables/Delete/5
        //public ActionResult sub1Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Table_1 subtable1 = db.Tables.Find(id);
        //    if (subtable1 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subtable1);
        //}

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult sub1DeleteConfirmed(int id)
        {
            Table_1 subtable1 = db.Table_1.Find(id);
            db.Table_1.Remove(subtable1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}