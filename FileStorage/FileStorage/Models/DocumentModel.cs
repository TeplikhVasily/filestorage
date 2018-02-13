using DbStorage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.Models
{
    public class DocumentModel
    {

        [Required(ErrorMessage = "Без имени нельзя")]
        [Display(Name = "Имя документа")]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

    }
}