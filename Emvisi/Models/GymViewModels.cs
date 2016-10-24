using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emvisi.Models
{
    public class RegisterGymModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Logo { get; set; }

        [Display(Name = "Choose region")]
        public IEnumerable<SelectListItem> Regions { get; set; }
        [Display(Name = "Choose city")]
        public IEnumerable<SelectListItem> Cities { get; set; }

        public int SelectRegion { get; set; }
        public int SelectCity { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string GeoLat { get; set; }
        public string GeoLoc { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}