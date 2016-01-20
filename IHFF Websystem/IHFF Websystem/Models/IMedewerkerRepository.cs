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
        List<Wishlist> GetWishlists(Medewerker medewerker);
        List<Diner> GetReserveringen(Medewerker medewerker);
        void DeleteWishlist(int? wishlistID);
        Wishlist EditWishlistID(int? wishlistID);
        void EditWishlist(Wishlist newWishlist);
        void DeleteReservering(int? dinerID);
        List<Special> GetSpecials(Medewerker ingelogdeMedewerker);
        List<Film> GetFilms(Medewerker ingelogdeMedewerker);
        List<Locatie> getLocaties();
        void DeleteAccount(int medewerkerID);
    }
}