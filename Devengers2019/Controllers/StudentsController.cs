using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using Devengers2019.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Devengers2019.Controllers
{
    public class StudentsController : Controller
    {
        [ActionName("Index")]
        public async Task<ActionResult> Index(string searchParam, string searchType)
        {
            IEnumerable<Student> students;

            if (searchType == "StudentNo")

            {

                students = await DocumentDBRepository<Student>.GetItemsAsync(s => s.StudentNo.Contains(searchParam) && s.IsActive);

            }

            else if (searchType == "Name")

            {

                students = await DocumentDBRepository<Student>.GetItemsAsync(s => s.Name.ToUpper().Contains(searchParam.ToUpper()) && s.IsActive);

            }

            else if (searchType == "Surname")

            {

                students = await DocumentDBRepository<Student>.GetItemsAsync(s => s.Surname.ToUpper().Contains(searchParam.ToUpper()) && s.IsActive);

            }

            else

            {

                students = await DocumentDBRepository<Student>.GetItemsAsync(s => s.IsActive || !s.IsActive);

            }

            return View(students);


        }
        [ActionName("ExportData")]

        public async Task<ActionResult> ExportData()
        {
            //Student s = new Student();
            var items = await DocumentDBRepository<Student>.GetItemsAsync(s=> s.IsActive || !s.IsActive);

            GridView gv = new GridView();
            gv.DataSource = items.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.doc");
            Response.ContentType = "application/vnd.ms-word ";
            Response.Charset = string.Empty;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");

        }

        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,StudentNo,Name,Surname,Email,Telephone,Mobile,isActive, ImageUrl")] Student student, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {
                foreach (string file in Request.Files)
                {
                    uploadFile = Request.Files[file];
                }

                BlobManager BlobManagerObj = new BlobManager("picture");
                string FileAbsoluteUri = BlobManagerObj.UploadFile(uploadFile);

                student.ImageUrl = FileAbsoluteUri;

                await DocumentDBRepository<Student>.CreateItemAsync(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "StudentID,StudentNo,Name,Surname,Email,TelephoneNo,Mobile,IsActive,ImageUrl")] Student item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Student>.UpdateItemAsync(item.StudentID, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student item = await DocumentDBRepository<Student>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student item = await DocumentDBRepository<Student>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind(Include = "StudentID")] string id)
        {
            Student item = await DocumentDBRepository<Student>.GetItemAsync(id);
            if (item.ImageUrl != null)
            {
                BlobManager BlobManagerObj = new BlobManager("picture");
                BlobManagerObj.DeleteBlob(item.ImageUrl);
            }
            await DocumentDBRepository<Student>.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            Student item = await DocumentDBRepository<Student>.GetItemAsync(id);
            return View(item);
        }
        //public async Task<ActionResult> EmailAsync(string id)
        //{
        //    Student item = await DocumentDBRepository<Student>.GetItemAsync(id);
        //    exporttoexcel(item);
        //    return View(item);
        //}

    }
}
