using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IHFF_Websystem.Models
{
    public class DinerReserverenModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Start Tijd moet worden ingevuld!")]
        [Display(Name = "Start Tijd")]
        public DateTime StartTijd { get; set; }

        [Required(ErrorMessage = "Eind Tijd moet worden ingevuld!")]
        [Display(Name = "Eind Tijd")]
        public DateTime EindTijd { get; set; }

        [Required(ErrorMessage = "Op Naam Van moet worden ingevuld!")]
        [Display(Name = "Op Naam Van")]
        public string OpNaamVan { get; set; }

        [Required(ErrorMessage = "Aantal moet worden ingevuld!")]
        [Display(Name = "Aantal")]
        public int Aantal { get; set; }

        //[Required]
        //[Display(Name = "FoodFilm")]
        //public bool FoodFilm { get; set; }

        public DinerReserverenModel()
        {

        }

        public DinerReserverenModel(DateTime startTijd, DateTime endTijd, string opNaamVan, int aantal)
        {
            this.StartTijd = startTijd;
            this.EindTijd = endTijd;
            this.OpNaamVan = opNaamVan;
            this.Aantal = aantal;
            //this.FoodFilm =foodfilm;
        }
    }
}
