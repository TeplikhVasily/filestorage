using DbStorage.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FileStorage.Models
{
    public class DocumentViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FileType { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Автор")]
        public User Author { get; set; }

        [Display(Name = "Файл")]
        public byte[] Data { get; set; }

    }
}