using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.Models
{
    public class Article
    {        
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Hidden { get; set; }
    }
}