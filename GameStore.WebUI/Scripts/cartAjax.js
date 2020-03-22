
var UpdateCart = function () {
    $("#loaderDiv").hide();
    $("#myModal").modal("hide");
    $("#cartList").load("/Cart/Summary");
    $("#cartTable").load("/Cart/Table");
}

var AddGame = function (gameId) {

    $.ajax({
        type: "POST",
        url: "/Cart/AddToCart",
        cache: $.ajaxSetup({ cache: false }),
        data: { gameId: gameId },
        success: function () {
            UpdateCart();
        }
    });
}

var DeleteGame = function (gameId) {

    $("#loaderDiv").show();

    $.ajax({
        type: "POST",
        url: "/Cart/RemoveFromCart",
        cache: $.ajaxSetup({ cache: false }),
        data: { gameId: gameId },
        success: function () {
            UpdateCart();
        }
    });
}

var ConfirmDelete = function (gameId) {

    $("#hiddenGameId").val(gameId);
    $("#myModal").modal('show');
}

var Delete = function () {

    $("#loaderDiv").show();

    var gameId = $("#hiddenGameId").val();

    DeleteGame(gameId);
}