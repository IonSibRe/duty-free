// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var rowExists = false;

$(".admin-add-button").click(function () {
    if (!rowExists) {
        const table = $('table tbody');

        table.prepend(
            '<tr class="add-row">'
            + '<td class="name">'
            + '<input type="text" class="form-control name-control" name="Name" form="insert" required/>'
            + '</td>'
            + '<td class="image image-add">'
            + '<label for="Image" id="image-select-label">Vybrat soubor</label>'
            + '<input type="file" class="form-control image-control" id="Image" name="Image" form="insert" accept=".png, .jpg" required/>'
            + '</td>'
            + '<td class="price" id="price-control">'
            + '<input type="number" class="form-control price-control" name="Price" form="insert" required/>'
            + '</td>'
            + '<td class="quantity" colspan="2">'
            + '<input type="number" class="form-control quantity-control" name="Quantity" form="insert" required/>'
            + '</td>' 
            + '</tr>'
        );   

        $(".product-add-extesion").show();
    }

    rowExists = true;
});

$(document).on("change", ".discount-checkbox", function () {
    const extensionRow = $(document).find("#price-control");
    if (this.checked) {       
        extensionRow.empty();
        extensionRow.prepend(
            '<div class="input-group discount-edit" style="align-items: center; gap: 10px;">'
            + '<input type="number" id="price-before" name="Price" form="insert" class="form-control"/>'
            + '<span><i class="fa-solid fa-arrow-right"></i></span>'
            + '<input type="number" id="price-after" name="Price-After" form="insert" value="0" class="form-control"/>'
            + '</div>'
        );
    }

    else {
        extensionRow.empty();
        extensionRow.prepend(
            '<input type="number" class="form-control price-control" name="Price" form="insert" required/>'
        );
    }
});

$(document).on("change", ".new-checkbox", function () {
    if (this.checked) {
        this.value = true;
    }
    else {
        this.value = false;
    }

    console.log(this.value);
});

function removeRow() {
    $(".add-row").remove();
    $(".product-add-extesion").hide();
    rowExists = false;
}

$(".admin-delete-button").click(function () {
    let id = $(this).closest('tr').attr("id");

    $.ajax({
        type: "POST",
        url: "/Admin/Delete",
        data: { "id": id }
    });

    $(this).closest('tr').remove();
});

let name, price, quantity, imageUrl, productId, priceAfter = 0, category;

$(document).on("click", ".admin-edit-button", function () {
    const row = $(this).closest('tr');

    name = row.find('.name').text(),
    price = row.find('#real-price').text(),
    quantity = row.find('.quantity').text(),
    imageUrl = row.find('.image')[0].src;
    productId = row.attr("id");
    priceAfter = row.find('#discount-price').text();
    category = row.find('.description').text();

    if (!rowExists) {

        row.find("#name").each(function () {
            var input = $('<input class="name-edit form-control" type="text" name="Name" form="edit" value="' + `${name}` + '" />');
            $(this).html(input);
        });

        row.find('#image').each(function () {
            $(this).empty();
            $(this).prepend(
                '<img id="img-edit" src="' + `${imageUrl}` + '" alt="produkt"/>'
                + '<label for="Image" id="image-select-label">Vybrat soubor</label>'
                + '<input type="file" class="form-control image-edit" name="Image" id="Image" form="edit" accept=".png, .jpg"/>'
                + '<input name="Id" type="text" value="' + `${productId}` + '" form="edit" style="visibility:hidden; width: 0px;">'
            );
        });

        row.find("#price").each(function () {
            $(this).empty();
            if (priceAfter == 0) {
                $(this).prepend(
                    '<input type="number" class="form-control price-edit" name="Price" form="edit" value="' + `${price}` + '"/>'
                );
            }
            else {
                $(this).prepend(
                    '<div class="input-group discount-edit" style="align-items: center; gap: 10px;">'
                    + '<input type="number" id="price-before" name="Price" form="edit" value="' + `${price}` + '" class="form-control"/>'
                    + '<span><i class="fa-solid fa-arrow-right"></i></span>'
                    + '<input type="number" id="price-after" name="Price-After" form="edit" value="' + `${priceAfter}` + '" class="form-control"/>'
                    + ' Kč</div>'
                );  
            }
        });

        row.find("#quantity").each(function () {
            $(this).empty();
            $(this).prepend(
                '<input type="number" class="form-control quantity-edit" name="Quantity" form="edit" value="' + `${quantity}` + '"/>'
            );
        });

        row.find("#buttons").each(function () {
            $(this).empty();
            $(this).prepend(         
                '<input type="submit" class="btn btn-success btn-save btn-edit-save" form="edit" value="Uložit"/>'
                + '<button type="button" class="btn btn-success btn-discard throwAway">Zahodit</button>'
            );
        });
    }
});

$(document).on("click", ".throwAway", function () {
    const row = $(this).closest('tr');
    console.log(row.attr("id"));

    row.find("#name").each(function () {
        $(this).empty();
        $(this).prepend(
            '<p class="admin-product-name name">' + `${name}` + '</p>'
            + '<p class="admin-product-description">' + `${category}` + '</p>'
        );
    });

    row.find('#image').each(function () {
        $(this).empty();
        $(this).prepend(
            '<img class="image" id="img-edit" src="' + `${imageUrl}` + '" alt="produkt"/>'
        );
    });

    row.find("#price").each(function () {
        $(this).empty();
        if (priceAfter == 0) {
            $(this).prepend(
                '<span class="price" id="real-price">' + `${price}` + '</span>'
                + '<span> Kč</span>'
            );
        }
        else {
            $(this).prepend(
                '<span class="price" id="real-price"><strike>' + `${price}` + '</strike></span>'
                + '<span><strike> Kč </strike></span>'
                + '<span class="price" id="discount-price">' + `${priceAfter}` + '</span>'
                + '<span> Kč</span>'
            );
        }
    });

    row.find("#quantity").each(function () {
        row.find("#quantity").css("display", "");
        $(this).empty();
        $(this).prepend(
            '<span class="quantity">' + `${quantity}` + '</span> ks'
        );
    });

    row.find("#buttons").each(function () {
        $(this).empty();
        $(this).prepend(
            '<a class="admin-edit-button"><i class="fa-solid fa-wrench"></i></a>'
            + '<a class="admin-delete-button"><i class="fa-solid fa-xmark"></i></a>'
        );
    });
});

$(document).on("input", ".image-edit", function () {

    const file = this.files[0];
    console.log(file);
    if (file) {
        let reader = new FileReader();
        reader.onload = function (event) {
            console.log(event.target.result);
            $('#img-edit').attr('src', event.target.result);
        }
        reader.readAsDataURL(file);
    }
});
