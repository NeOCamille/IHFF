using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    interface IMedewerkerRepository
    {
        void AddMedewerker(Medewerker medewerker);
        Medewerker GetMedewerker(Login loginMedewerker);
        List<Wishlist> GetWishlists();
        List<Diner> GetReserveringen(Medewerker medewerker);
        void DeleteWishlist(int wishlistID);
        Wishlist EditWishlistID(int wishlistID);
        void EditWishlist(Wishlist newWishlist);
        void DeleteReservering(int dinerID);
        List<Special> GetSpecials();
        List<Film> GetFilms();
        List<Locatie> getLocaties();
        void DeleteAccount(int medewerkerID);
    }
}