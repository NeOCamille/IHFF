using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    public class Popup
    {
        Evenement evenement { get; set; }

        public void OpenPopup(int eventId)
        {
            //this.evenement = evenement where evenementID == eventId;
            //popup.lblTitel.Text = evenement.naam;
            //...
            //...
            //popup view open.
        }

        public void AddToWishlist()
        {
            //wishlist.add(evenement)...
        }
    }
}