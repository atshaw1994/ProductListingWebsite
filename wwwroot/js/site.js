// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeQty(productId, adjustment) {
    fetch(`/Cart/UpdateQuantity?id=${productId}&adjustment=${adjustment}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                if (data.removed) {
                    document.getElementById(`row-${productId}`).remove();
                    if (data.itemCount === 0) location.reload(); // Reload to show "Empty Cart" message
                } else {
                    // Update the specific item quantity and total
                    document.getElementById(`qty-${productId}`).innerText = 
                        parseInt(document.getElementById(`qty-${productId}`).innerText) + adjustment;
                    document.getElementById(`total-${productId}`).innerText = data.itemTotal;
                }

                // Update the grand total and the navbar badge
                document.getElementById('cart-grand-total').innerText = data.cartTotal;
                const badge = document.querySelector('.navbar .badge');
                if (badge) badge.innerText = data.itemCount;
            }
        });
} 