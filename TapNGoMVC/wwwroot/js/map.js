$(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5235/orderHub")
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveOrder", function (orderId) {
        async function getOrder(orderId) {
            try {
                const response = await fetch(`/AdminMap/GetOrder/${orderId}`);
                if (!response.ok) {
                    throw new Error(`Failed to fetch order: ${response.statusText}`);
                }
                const data = await response.json();
                return data;
            } catch (error) {
                console.error("Error fetching order:", error);
                return null;
            }
        }

        getOrder(orderId).then((order) => {
            if (!order || !order.tableNumber) {
                console.warn("Invalid order data received.");
                return;
            }

            const $table = $("#table-" + order.tableNumber);

            if ($table.length) {
                $table.addClass("blink");

                $("#notification")
                    .text("Nova narudžba sa stola " + order.tableNumber)
                    .fadeIn();

                setTimeout(function () {
                    $("#notification").fadeOut();
                }, 5000);
            }
        });
    });

    function showOrderDetails(order) {
        if (!order) {
            console.warn("Order details are missing.");
            return;
        }

        $("#tableNumber").text(order.tableNumber || "—");
        $("#note").text(order.note || "—");
        $("#status").text(order.status || "—");
        $("#totalPrice").text(order.totalPrice ? order.totalPrice.toFixed(2) : "—");

        const modal = new bootstrap.Modal(document.getElementById("orderDetailsModal"));
        modal.show();
    }

    connection.start()
        .catch(err => console.error("Greška pri povezivanju:", err));


    $(".table").on("click", function () {
        let tableId;
        tableId = parseInt($(this).attr("id").split("-")[1]); // Extract table number from ID

        if (!tableId) {
            return;
        }

        async function getOrderByTable(tableId) {
            try {
                const response = await fetch(`/AdminMap/GetOrderByTable/${tableId}`);
                const data = await response.json();
                return data;
            } catch (error) {
                console.error("Error fetching order by table:", error);
                return null;
            }
        }

        getOrderByTable(tableId).then((order) => {
            if (!order || order.id === 0) {
                console.warn("No valid order found for table:", tableId);
                return;
            }

            showOrderDetails(order);
        });

        $(this).removeClass("blink");
    });
});
