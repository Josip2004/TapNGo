$(document).ready(function () {
    $(".add-btn").click(function (e) {
        e.preventDefault();

        let id = $(this).data("id");

        $.post('/Cart/Add', { itemId: id }, () => {
            let qtySpan = $(".quantity[data-id='" + id + "']");
            let current = parseInt(qtySpan.text()) || 0;
            qtySpan.text(current + 1);
            updateTotal();
        });
    });

    $(".remove-btn").click(function (e) {
        e.preventDefault();

        let id = $(this).data("id");

        $.post('/Cart/Remove', { itemId: id }, () => {
            let qtySpan = $(".quantity[data-id='" + id + "']");
            let current = parseInt(qtySpan.text()) || 0;
            let newQty = current > 0 ? current - 1 : 0;
            qtySpan.text(newQty);
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
