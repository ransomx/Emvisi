using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emvisi.Models
{
    public class MapViewModels
    {
        [Display(Name = "Choose region")]
        public IEnumerable<SelectListItem> Regions { get; set; }
        [Display(Name = "Choose activities")]
        public IEnumerable<SelectListItem> Activities { get; set; }
        [Display(Name = "Choose city")]
        public IEnumerable<SelectListItem> Cities { get; set; }

        public string SelectRegion { get; set; }
        public string SelectCity { get; set; }
        public string SelectActivity { get; set; }

        [Display(Name = "Your search..")]
        public string Search { get; set; }

    }
}