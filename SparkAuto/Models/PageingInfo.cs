using System;

namespace SparkAuto.Models
{
    public class PageingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
       
        public string UrlParam { get; set; }
    }
}
