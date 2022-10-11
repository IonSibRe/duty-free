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
            + '<label for="name">Name</label>'
            + '<input type="text" class="form-control name-control" name="Name" form="insert" required/>'
            + '</td>'
            + '<td class="image image-add">'
            + '<label for="Image" id="image-select-label">Vybrat soubor</label>'
            + '<input type="file" class="form-control image-control" id="Image" name="Image" form="insert" accept=".png, .jpg"/>'
            + '</td>'
            + '<td class="price">'
            + '<label for="Price">Price</label>'
            + '<input type="number" class="form-control price-control" name="Price" form="insert" required/>'
            + '</td>'
            + '<td class="quantity">'
            + '<label for="Quantity">Quantity</label>'
            + '<input type="number" class="form-control quantity-control" name="Quantity" form="insert" required/>'
            + '</td>'
            + '<td class="admin-buttons">'
            + '<input type="submit" class="btn btn-success btn-save" value="Uložit" form="insert" style="width:100%"/>'
            + '<button type="button" class="btn btn-success btn-discard" onclick="removeRow()">Zahodit</button>'
            + '</td>'    
            + '</tr>'
            + '<tr class="product-add-extesion">'
            + '<td class="category-select">'
            + '<select class="form-select">'
            + '<option selected>Zvolte kategorii</option>'
            + '<option value="1">#Svačina</option>'
            + '<option value="2">#Sladké</option>'
            + '<option value="3">#Slané</option>'
            + '<option value="4">#Nápoje</option>'
            + '<option value="5">#Zmrzlina</option>'
            + '</select > '
            + '</td>'
        );   
    }

    $(".admin-add-button").hide();

    rowExists = true;
});

function removeRow() {
    $(".add-row").remove();
    $(".admin-add-button").show();
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

let name, price, quantity, imageUrl, productId;

$(document).on("click", ".admin-edit-button", function () {
    const row = $(this).closest('tr');

    name = row.find('.name').text(),
    price = row.find('.price').text(),
    quantity = row.find('.quantity').text(),
    imageUrl = row.find('.image')[0].src;
    productId = row.attr("id");

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
            $(this).prepend(
                '<input type="number" class="form-control price-edit" name="Price" form="edit" value="' + `${price}` + '"/>'
            );
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
            + '<p class="admin-product-description">#Svačina #Sleva #Novinka</p>'
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
        $(this).prepend(
            '<span class="price">' + `${price}` + '</span> Kč'
        );
    });

    row.find("#quantity").each(function () {
        $(this).empty();
        $(this).prepend(
            '<span class="price">' + `${quantity}` + '</span> ks'
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
