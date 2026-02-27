using Microsoft.AspNetCore.Mvc;
using ProductListingWebsite.Data;
using ProductListingWebsite.Helpers;
using ProductListingWebsite.Models;

public class CartController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context) => _context = context;

    public IActionResult Index()
    {
        // Retrieve cart from session or create new list
        var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? [];
        return View(cart);
    }

    public async Task<IActionResult> AddToCart(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? [];

        var cartItem = cart.FirstOrDefault(c => c.ProductId == id);
        if (cartItem == null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                ImageUrl = product.ImageUrl
            });
        }
        else
        {
            cartItem.Quantity++;
        }

        HttpContext.Session.SetJson("Cart", cart);
        return RedirectToAction("Storefront", "Products"); // Send back to shop
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int id, int adjustment)
    {
        var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var item = cart.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
        {
            item.Quantity += adjustment;

            // Remove item if quantity drops to 0
            if (item.Quantity <= 0)
            {
                cart.Remove(item);
            }
        }

        HttpContext.Session.SetJson("Cart", cart);

        // Return the new values so JS can update the UI
        return Json(new
        {
            success = true,
            itemCount = cart.Sum(x => x.Quantity),
            itemTotal = item?.Total.ToString("C") ?? "$0.00",
            cartTotal = cart.Sum(x => x.Total).ToString("C"),
            removed = item?.Quantity <= 0
        });
    }

    public IActionResult Remove(int id)
    {
        var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? [];

        var item = cart.FirstOrDefault(x => x.ProductId == id);
        if (item != null)
        {
            cart.Remove(item);
        }

        HttpContext.Session.SetJson("Cart", cart);
        return RedirectToAction(nameof(Index));
    }
}