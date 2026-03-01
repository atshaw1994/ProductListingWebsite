// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeQty(productId, adjustment) {
    const qtySpan = document.getElementById(`mini-qty-${productId}`);
    const addBtn = document.getElementById(`add-btn-${productId}`);
    const miniRow = document.getElementById(`mini-row-${productId}`);
    
    if (!qtySpan) return;

    const currentQty = parseInt(qtySpan.innerText);
    const stockLimit = parseInt(qtySpan.getAttribute('data-stock'));
    const newQty = currentQty + adjustment;

    if (adjustment > 0 && newQty > stockLimit) return;
    if (adjustment < 0 && newQty < 0) return;

    // 1. Instant local update for the number
    qtySpan.innerText = newQty;

    fetch(`/Cart/UpdateQuantity?id=${productId}&adjustment=${adjustment}`, {
        method: 'POST'
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            const navBadge = document.getElementById('nav-cart-count');
            if (navBadge) {
                // Update the number
                navBadge.innerText = data.itemCount;

                // Logic check: if 0, hide it. if > 0, show it.
                if (data.itemCount > 0) {
                    navBadge.classList.remove('d-none');
                } else {
                    navBadge.classList.add('d-none');
                }
            }

            // Handle the item row removal...
            if (data.removed && miniRow) {
                miniRow.remove();
            }

            // Final sync
            document.getElementById('update-cart-btn')?.click();
        }
    });
}

function removeItem(productId) {
    if (!confirm("Remove this item from your cart?")) return;

    fetch(`/Cart/Remove/${productId}`, {
        method: 'POST', // Ensure your Controller action allows [HttpPost]
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Find the rows in both the main table and the mini-cart
                const row = document.getElementById(`row-${productId}`);
                const miniRow = document.getElementById(`mini-row-${productId}`);

                // Apply a fade-out effect before removing
                [row, miniRow].forEach(el => {
                    if (el) {
                        el.style.transition = "opacity 0.3s ease, transform 0.3s ease";
                        el.style.opacity = "0";
                        el.style.transform = "translateX(20px)";
                        setTimeout(() => el.remove(), 300);
                    }
                });

                // Update totals and badge
                document.getElementById('nav-cart-count').innerText = data.itemCount;
                document.getElementById('cart-grand-total').innerText = data.cartTotal;

                // Update modal subtotal if it exists
                const modalSubtotal = document.querySelector('#cartModal .modal-footer .fs-5');
                if (modalSubtotal) modalSubtotal.innerText = data.cartTotal;

                // If cart is now empty, reload to show the "Empty Cart" view
                if (data.itemCount === 0) {
                    location.reload();
                }
            }
        });
}

function updateUI(productId, adjustment, data) {
    // 1. Update Navbar and Modal counts
    document.getElementById('nav-cart-count').innerText = data.itemCount;

    // 2. Update Main Cart Page row
    const qtySpan = document.getElementById(`qty-${productId}`);
    if (qtySpan) {
        qtySpan.innerText = parseInt(qtySpan.innerText) + adjustment;
        document.getElementById(`total-${productId}`).innerText = data.itemTotal;
        document.getElementById('cart-grand-total').innerText = data.cartTotal;
    }

    // 3. Update Mini-Cart Modal
    const miniQty = document.getElementById(`mini-qty-${productId}`);
    if (miniQty) {
        miniQty.innerText = parseInt(miniQty.innerText) + adjustment;
        document.getElementById(`mini-total-${productId}`).innerText = data.itemTotal;
        const modalSubtotal = document.querySelector('#cartModal .modal-footer .fs-5');
        if (modalSubtotal) modalSubtotal.innerText = data.cartTotal;
    }

    // Handle removal if count hits 0
    if (data.removed) {
        document.getElementById(`row-${productId}`)?.remove();
        document.getElementById(`mini-row-${productId}`)?.remove();
        if (data.itemCount === 0) location.reload();
    }
}