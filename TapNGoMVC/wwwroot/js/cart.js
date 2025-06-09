$(document).ready(function () {
    $(".add-btn").click(function (e) {
        e.preventDefault();

        const id = $(this).data('id');
        const name = $(this).data('name');
        const price = $(this).data('price');

        $.post('/Cart/Add', { itemId: id, name: name, price: price }, () => {
            let qtySpan = $(".quantity[data-id='" + id + "']");
            let current = parseInt(qtySpan.text()) || 0;
            let newQty = current + 1;
            qtySpan.text(newQty);

            $(".item-total[data-id='" + id + "']").text((newQty * price).toFixed(2) + " €");

            updateTotal();
        });
    });

    $(".remove-btn").click(function (e) {
        e.preventDefault();

        const id = $(this).data("id");

        $.post('/Cart/Remove', { itemId: id }, () => {
            let qtySpan = $(".quantity[data-id='" + id + "']");
            let current = parseInt(qtySpan.text()) || 0;
            let newQty = current > 0 ? current - 1 : 0;
            qtySpan.text(newQty);

            let price = parseFloat(qtySpan.data("price")) || 0;
            $(".item-total[data-id='" + id + "']").text((newQty * price).toFixed(2) + " €");

            updateTotal();
        });
    });

    function updateTotal() {
        let total = 0;
        $(".quantity").each(function () {
            let qty = parseInt($(this).text()) || 0;
            let price = parseFloat($(this).data("price")) || 0;
            total += qty * price;
        });
        $("#total-price").text(total.toFixed(2) + " €");
    }
});

