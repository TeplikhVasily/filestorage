using DbStorage.Models;
using DbStorage.NHibernate.Repositiry;
using DbStorage.Repository;
using FileStorage.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.Controllers
{

    [Authorize]
    public class DocumentController : Controller
    {
        #region Protected Members

        protected IDocumentRepository DocumentRepository { get; set; }

        protected IUserRepository UserRepository { get; set; }

        #endregion

        public DocumentController()
        {
            DocumentRepository = new NHDocumentRepository();
            UserRepository = new NHUserRepository();
        }

        public ActionResult Documents()
        {
            var docs = DocumentRepository.GetAll();
            return View(docs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Загрука нового файла
        [HttpPost]
        public ActionResult Create(DocumentModel documentModel, HttpPostedFileBase uploadDocument)
        {
            if (ModelState.IsValid && uploadDocument != null)
            {
                // получаем тип файла
                string fileType = Path.GetExtension(uploadDocument.FileName);
                // сохраняем файл в папку Files в проекте
                uploadDocument.SaveAs(Server.MapPath($"~/Files/{documentModel.Name}{fileType}"));

                byte[] documentData = null;
                // считываем переданный файл в массив байт
                using (var binaryReader = new BinaryReader(uploadDocument.InputStream))
                {
                    documentData = binaryReader.ReadBytes(uploadDocument.ContentLength);
                }

                var curUser = UserRepository.GetByLogin(User.Identity.Name);

                var nonUniq = DocumentRepository.GetAll()
                    .Any(u => u.Name == documentModel.Name);

                if (nonUniq)
                {
                    ModelState.AddModelError("", "Файл с таким именем уже существует!");
                    return View();
                }

                var doc = new Document()
                {
                    Name = documentModel.Name,
                    FileType = fileType,
                    CreationDate = DateTime.Now,
                    Author = curUser,
                    Data = documentData
                };
                DocumentRepository.Save(doc);

                return RedirectToAction("Documents", "Document");
                //return PartialView("Documents", doc);
            }
            return View();
        }

        //Поиск файла
        [HttpPost]
        public ActionResult DocumentSearch(string name)
        {

            var documents = DocumentRepository.GetByName(name).ToList();
            if (documents.Count <= 0)
            {
               return RedirectToAction("DocumentNotFound"); 
            }
            return View("Documents", documents);
        }

        //Файл не найден
        public ActionResult DocumentNotFound()
        {
            return View();
        }


        //Загрузка файла
        public FileResult GetBytes(long id)
        {
            var documents = DocumentRepository.Get(id);

            byte[] mas = documents.Data;
            string file_type = documents.FileType;
            string file_name = $"{documents.Name}{ documents.FileType}";
            return File(mas, file_type, file_name);
        }

        //Удаление файла из БД и с сервера из /Files
        public ActionResult Delete(long id)
        {
            var document = DocumentRepository.Get(id);
            if (document == null)
                return RedirectToAction("Documents");

            DocumentRepository.Delete(id);

            System.IO.File.Delete(Server.MapPath($"~/Files/{document.Name}{document.FileType}"));


            return RedirectToAction("Documents");
        }

        #region Сортировка
        public ActionResult SortByName(string type)
        {
            var docs = DocumentRepository.GetAll();

            if (type == "up")
            {

                return View("Documents", docs.OrderBy(u => u.Name));                   
            }
            else
            {
                return View("Documents", docs.OrderByDescending(u => u.Name));
            }
        }

        public ActionResult SortByAuthor(string type)
        {
            var docs = DocumentRepository.GetAll();

            if (type == "up")
            {

                return View("Documents", docs.OrderBy(u => u.Author));
            }
            else
            {
                return View("Documents", docs.OrderByDescending(u => u.Author));
            }
        }

        public ActionResult SortByCreateDate(string type)
        {
            var docs = DocumentRepository.GetAll();

            if (type == "up")
            {

                return View("Documents", docs.OrderBy(u => u.CreationDate));
            }
            else
            {
                return View("Documents", docs.OrderByDescending(u => u.CreationDate));
            }
        }
        #endregion
    }
}

