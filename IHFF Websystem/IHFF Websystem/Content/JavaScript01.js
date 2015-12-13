$(document).ready(function () {
    $("#banner > div:gt(0)").hide();

    setInterval(function () {
        $('#banner > div:first')
          .fadeOut(1000)
          .next()
          .fadeIn(1000)
          .end()
          .appendTo('#banner');
    }, 3000);
    /* oke js werkt nu weg met deze stomme melding
    alert();
    */

    /* Hide wishlist basket */
    $('#wishlistbasket').hide();

    wishlistbasket_toggel = false;
    $('#whishlistlink').click(function () {
        if (wishlistbasket_toggel == true) {
            $('#wishlistbasket').show();
        } else {
            $('#wishlistbasket').hide();
        }
        wishlistbasket_toggel = !wishlistbasket_toggel
    });
});