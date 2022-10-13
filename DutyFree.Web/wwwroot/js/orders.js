// Datepicker
function getFirstDayOfMonth() {
    const now = new Date();
    const firstDay = new Date(now.getFullYear(), now.getMonth(), 1);
    return firstDay;
}

$(function () {
    $("#datepicker-od").datepicker({
    });
    $("#datepicker-od").datepicker("setDate", getFirstDayOfMonth());

    $("#datepicker-do").datepicker();
    $("#datepicker-do").datepicker("setDate", "today");
});

var totalPrice = 0;

$(".orders-order-tr").each(function () {
    totalPrice += parseInt($(this).attr("data-price"));
    console.log(totalPrice);
})

updateTotalPrice();

$(document).on("click", ".orders-tabulka-button", function () {
    totalPrice -= parseInt($(this).closest("tr").attr("data-price"));
    updateTotalPrice()
});

function updateTotalPrice() {
    $(".orders-total-td").text(`${totalPrice} Kč`);
}