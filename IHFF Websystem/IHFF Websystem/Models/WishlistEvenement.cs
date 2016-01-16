using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class WishlistEvenement
    {
        // blijkbaar moet er een ID gebruikt worden die als  [Key] dient
        public int ID { get; set; }

        [Required]
        public int wishlistID { get; set; }

        [Required]
        public int evenementID { get; set; }
                
        [Required]
        public int aantal { get; set; }

        public WishlistEvenement()
        {
            //..HAllo
            //Test
        }

        public WishlistEvenement(int wishlistID, int evenementID, int aantal)
        {
            this.wishlistID = wishlistID;
            this.evenementID = evenementID;
            this.aantal = aantal;
        }
    }
}