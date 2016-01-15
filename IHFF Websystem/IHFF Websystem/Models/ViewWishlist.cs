using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class ViewWishlist
    {
        public string name;
        public DateTime starttijd;
        public DateTime eindttijd;
        public string beschrijving;
        public string prijs;
        public string regiseur;
        public string locatie;
        public int evenementID;
        public int dinerID;

        public ViewWishlist()
        {

        }
    }
}