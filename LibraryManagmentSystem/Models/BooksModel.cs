using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class BooksModel
    {
        [Key]
        [DisplayName("Book ID")]
        public int book_id { get; set; }
        [DisplayName("Title")]
        public string title { get; set; }
        [DisplayName("Author")]
        public string author { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Lended Unit")]
        public int lended_unit { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [DisplayName("Status")]
        public int status_id { get; set; }
        [DisplayName("Image")]
        public string image_path { get; set; }
        [NotMapped]
        [DisplayName("Image File")]
        public IFormFile ImgFile { get; set; }
    }
}
