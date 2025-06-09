$(document).ready(function () {
    $(".add-btn").click(function (e) {
        e.preventDefault();

        const id = $(this).data('id');
        const name = $(this).data('name');
        const price = $(this).data('price');

        $.post('/Cart/Add', { itemId: id, name: name, price: price, categoryId: getCategoryIdFromUrl() }, () => {
            let qtySpan = $(".quantity[data-id='" + id + "']");
            let current = parseInt(qtySpan.text()) || 0;
            qtySpan.text(current + 1);
            location.reload();
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
            location.reload();
        });
    });

    function getCategoryIdFromUrl() {
        const params = new URLSearchParams(window.location.search);
        return params.get("categoryId");
    }
});


