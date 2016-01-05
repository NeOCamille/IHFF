using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    interface IMedewerkerRepository
    {
        void AddMedewerker(Medewerker medewerker);
        Medewerker GetMedewerker(string gebruikersNaam, string passWord);
        List<Wishlist> ShowDataManagement(Medewerker medewerker);
        List<Diner> ShowDataDiners(Medewerker medewerker);
        void DeleteWishlist(int? wishlistID);
        Wishlist EditWishlistID(int? wishlistID);
        void EditWishlist(Wishlist newWishlist);
    }
}