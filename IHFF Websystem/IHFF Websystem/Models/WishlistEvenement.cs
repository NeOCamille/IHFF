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

        public WishlistEvenement()
        {
            //..
        }

        public WishlistEvenement(int wishlistID, int evenementID)
        {
            this.wishlistID = wishlistID;
            this.evenementID = evenementID;
        }
    }
}