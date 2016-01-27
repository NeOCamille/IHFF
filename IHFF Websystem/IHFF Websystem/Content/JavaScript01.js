/* Functions */

//(her)laad de wishlist popup rechtsboven
function LoadWishlistPopup() {
    LoadPopup('/Wishlist/WishlistPopup', '#wishlistbasket-inner');
}

//actie voor plusje bij wishlist item
function wbAddAmount(eid, amount, isDiner) {
    //(int wID, int eID, int aantal, bool diner)
    amount = amount + 1; //add one
    $.post("/Wishlist/UpdateAantal", { eID: eid, aantal: amount, diner: isDiner })
        .done(function () {
            LoadWishlistPopup();
        })
        .fail(function () {
            alert("I Failed");
        });
}

//actie voor plusje bij wishlist item
function wbRemoveAmount(eid, amount, isDiner) {
    //(int wID, int eID, int aantal, bool diner)
    amount = amount - 1; //add one
    $.post("/Wishlist/UpdateAantal", { eID: eid, aantal: amount, diner: isDiner })
        .done(function () {
            LoadWishlistPopup();
        })
        .fail(function () {
            alert("Failed");
        });
}

//hiermee kan de inhoud van een class of ID worden aangepast met het resultaat van een AJAX/JQUERY GET, bijvoorbeeld LoadWishlistPopup()
//url in should be like: '/Wishlist/WishlistPopup'
//example LoadPopup('/Wishlist/WishlistPopup', '#wishlistbasket');
function LoadPopup(url, changeClassorId) {
    /* include wishlist popup + timestamp for uncached version */
    $.get(url + '?_=' + (new Date()).getTime())
    .success(function (data) {
        $(changeClassorId).html(data);
    });
}

//vraagt of de wishlist pagina herladen moet worden
function myReload() {
    if (confirm('Wishlist aangepast, wil je herladen?')) {
        location.reload();
    }
}

//laad wishlist bij het invoeren van  een nieuw codewoord
function wishlistLaden() {
    myValue = $("#codewoordbox").val();
    $.post("/Wishlist/WishlistLaden", { codeword: myValue })
        .done(function (data) {
            if (data == "[object XMLDocument]") {
                location.reload();
            } else {
                alert("codewoord incorrect");
            }
        })
        .fail(function () {
            //alert("Failed");
        });
}

//slaat wishlist op met codewoord
function wishlistOpslaan() {
    myValue = $("#codewoordbox2").val();
    $.post("/Wishlist/WishlistOpslaan", { codeword: myValue })
        .done(function () {
            location.reload();
        })
        .fail(function () {
            //alert("Failed");
        });
}

//Voegt evenement toe aan wishlist
function AddEventToWishlist(myValue) {
    $.post("/Wishlist/AddEvenementToWishlist", { id: myValue, aantal: 1 })
    .done(function () {
        //alert("Data Saved");
        LoadWishlistPopup();
        //Loading_icon.gif
        $('#Popop1Content').html("");
        $('.overlay-bg, .overlay-content').hide();
    })
    .fail(function () {
        alert("Failed");
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

    wishlistbasket_toggel = true;
    $('#whishlistlink').click(function () {
        if (wishlistbasket_toggel === true) {
            $('#wishlistbasket').show();
            LoadWishlistPopup()
        } else {
            $('#wishlistbasket').hide();
        }
        wishlistbasket_toggel = !wishlistbasket_toggel;
    });

    $('.addToWishlist').click(function () {
        myValue = $(this).attr('name');
        if ($('#wishlistbasket ul').text() == myValue) 
        $('#wishlistbasket ul').append('<li>'+myValue+'</li>');
    });

    /* test function */
    $('.addToWishlistDB').click(function () {
        myValue = $('#idTextarea').val();
        $.post("/Wishlist/AddEvenementToWishlist", { id: myValue, aantal:1 })
        .done(function () {
            alert("Data Saved");
            LoadWishlistPopup();
        })
        .fail(function () {
            alert("Failed");
        });
    });
});