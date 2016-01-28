using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFF_Websystem.Models
{
    interface IWishlistRepository
    {
        
        Wishlist NewWishlist();
        void AddDiner(Diner diner);
        void AddEvenement(int wishlistID, int evenementID, int aantal);
        Popup GetPopup(int ID);
        Locatie GetLocatie(int ID);
        Film GetFilm(int ID);
        IEnumerable<Locatie> GetAllLocaties();
        IEnumerable<Film> GetAllFilms();
        IEnumerable<Tuple<Film, int>> GetAllWishlistFilms(int wishlistID);
        IEnumerable<Tuple<Special, int>> GetAllWishlistSpecials(int wishlistID);
        IEnumerable<Diner> GetAllWishlistDiners(int wishlistID);
        IEnumerable<WishlistEvenement> GetAllWishlistEvenements(int wishlistID);
        int CheckAvailabilityEvenement(int myEvenementID);
        int CheckAvailabilityDiner(int mydinerID);
        void UpdateAantal_WE(int evenementID, int wishlistID, int aantal);
        void UpdateAantal_D(int dinerID, int aantal);
        void UpdatecodeWoord(int wishlistID, string codewoord);
        void CreateFilm(string evenementNaam, DateTime startTijd, string beschrijving, double prijs, string regisseur, int locatieID);
        List<Evenement> GetMyWishlistEvenements(int wishlistID);
        List<Diner> Getmywishlistdiner(int wishlistID);
        WishlistEvenement GetWishlistEvenement(int wishlistID, int evenementID);
        void DeleteWishlist(Wishlist wishlist);
        void WishListReserveren(Wishlist wishlist);
        Wishlist GetWishList(string codewoord);
        Wishlist GetWishList(int wishlistID);
        IEnumerable<Evenement> GetDagprogramma(string dag);
        double GetWishlistTotalPrice(int wishlistID);
    }
}
