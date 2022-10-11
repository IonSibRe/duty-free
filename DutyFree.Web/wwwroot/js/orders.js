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

const ordersOrderTr = document.querySelectorAll(".orders-order-tr");
let totalPrice = 0;

ordersOrderTr.forEach(order => {
    totalPrice += parseInt(order.getAttribute("data-price"));
})

updateTotalPrice();

document.querySelectorAll(".orders-tabulka-button").forEach(removeBtn => {
    removeBtn.addEventListener("click", (e) => {
        totalPrice -= parseInt(e.target.parentNode.parentNode.getAttribute("data-price"));
        updateTotalPrice();
        e.target.parentNode.parentNode.remove();
    })
})

function updateTotalPrice() {
    document.querySelector(".orders-total-td").textContent = totalPrice + " kč";
}