/* Functions */
function LoadWishlistPopup() {
    /* include wishlist popup + timestamp for uncached version */
    $.get('/Wishlist/WishlistPopup' + '?_=' + (new Date()).getTime())
    .success(function (data) {
        $('#wishlistbasket').html(data);
    });
}


/* Standalone code */
$(document).ready(function () {
    LoadWishlistPopup();

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

    /* ### WHISLIST ### */
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

    $('.addToWishlist').click(function () {
        myValue = $(this).attr('name');
        if ($('#wishlistbasket ul').text() == myValue) 
        $('#wishlistbasket ul').append('<li>'+myValue+'</li>');
    });

    /* test function */
    $('.addToWishlistDB').click(function () {
        myValue = $('#idTextarea').val();
        $.post("/Wishlist/AddEvenementToWishlist", { id: myValue })
        .done(function () {
            alert("Data Saved");
            LoadWishlistPopup();
        })
        .fail(function () {
            alert("Failed");
        });
    });
});