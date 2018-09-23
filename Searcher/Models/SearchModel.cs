using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Searcher.Models
{
    public class SearchModel
    {
        [Required]
        [Display(Name = "Поиск")]
        public string SearchText { get; set; }
    }
}