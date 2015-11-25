using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class wishlist
    {
        [Required]
        public int wishlistID { get; set; }

        public string codeWoord { get; set; }

        [Required]
        public bool isBetaald { get; set; }

        [Required]
        public bool isOpgehaald { get; set; }

        public string eMail { get; set; }

        [Required]
        public double totaalPrijs { get; set; }

        public wishlist()
        {

        }

        public wishlist(int wishlistID, string codeWoord, bool isBetaald, bool isOpgehaald, string eMail, double totaalPrijs)
        {
            this.wishlistID = wishlistID;
            this.codeWoord = codeWoord;
            this.isBetaald = isBetaald;
            this.isOpgehaald = isOpgehaald;
            this.eMail = eMail;
            this.totaalPrijs = totaalPrijs;
        }
    }
}