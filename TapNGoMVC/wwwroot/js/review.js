let selectedRating = 0;

document.querySelectorAll('.star-rating .star').forEach(star => {
    star.addEventListener('click', function () {

        selectedRating = parseInt(this.getAttribute('data-value'));

        document.getElementById('rating').value = selectedRating;

        document.querySelectorAll('.star-rating .star').forEach(s => {
            const value = parseInt(s.getAttribute('data-value'));
            if (value <= selectedRating) {
                s.classList.add('selected');
            } else {
                s.classList.remove('selected');
            }
        });
    });
});

document.getElementById('confirmReviewBtn').addEventListener('click', function () {
    const comment = document.getElementById('reviewComment').value;
    const rating = document.getElementById('rating').value;
    const orderId = document.getElementById('orderId').value;

    fetch('/Review/SubmitReview', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify({
            comment: comment,
            rating: rating,
            orderId: orderId
        })
    })
        .then(res => {
            if (res.ok) {

                document.getElementById('reviewComment').value = '';
                document.getElementById('rating').value = 0;
                selectedRating = 0;

                document.querySelectorAll('.star-rating .star').forEach(s => {
                    s.classList.remove('selected');
                });

                const modal = bootstrap.Modal.getInstance(document.getElementById('reviewModal'));
                modal.hide();
            }
            else {
                alert('Greška!');
            }
        })
});