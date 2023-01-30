using KlementWeb.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KlementWeb.Models.ArticleViewModels
{
    public class ArticleViewModel
    {
        //public Article Article { get; set; }
        public List<Article> Articles { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Display(Name = "Nadpis")]
        [StringLength(255, ErrorMessage = "Maximálně můžete zadat 255 znaků")]
        [Required(ErrorMessage = "Vyplňte url")]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [RegularExpression(@"^[a-z0-9\-]+$", ErrorMessage = "Povolena jsou malá písmena bez diakritiky a číslice")]
        public string Url { get; set; }

        [Display(Name = "Popisek")]
        public string Description { get; set; }

        [Display(Name = "Obsah")]
        [Required(ErrorMessage = "Vyplňte text článku")]
        public string Content { get; set; }

        [Display(Name = "Nahrajte titulkový obrázek")]
        public IFormFile TitlePicture { get; set; }

      /*  public ArticleViewModel() 
        { }
        public ArticleViewModel(int id, string title, string url, string description, string content, IFormFile titlePicture)
        {
            Id = id;
            Title = title;
            Url = url;
            Description = description;
            Content = content;
            TitlePicture = titlePicture;


        }*/
    }
}
