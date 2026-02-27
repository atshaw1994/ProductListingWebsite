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