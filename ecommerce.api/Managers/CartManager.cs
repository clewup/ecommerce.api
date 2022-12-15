using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Managers.Data;

namespace ecommerce.api.Managers;

public class CartManager
{
    private readonly CartDataManager _cartDataManager;
    private readonly IMapper _mapper;

    public CartManager(IMapper mapper, CartDataManager cartDataManager)
    {
        _mapper = mapper;
        _cartDataManager = cartDataManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _cartDataManager.GetCarts();

        return _mapper.Map<List<CartModel>>(carts);;

    }
    
    public async Task<CartModel?> GetCart(Guid id)
    {
        var cart = await _cartDataManager.GetCart(id);
        
        if (cart == null)
            return null;

        return _mapper.Map<CartModel>(cart);

    }
    
    public async Task<CartModel?> GetUserCart(UserModel user)
    {
        var cart = await _cartDataManager.GetUserCart(user);
        
        if (cart == null)
            return null;

        return _mapper.Map<CartModel>(cart);
    }
    
    public async Task<CartModel> CreateCart(CartModel cart, UserModel user)
    {
        var createdCart = await _cartDataManager.CreateCart(cart, user);
        
        return _mapper.Map<CartModel>(createdCart);
    }

    public async Task<CartModel> UpdateCart(CartModel cart, UserModel user)
    {
        var updatedCart = await _cartDataManager.UpdateCart(cart, user);

        return _mapper.Map<CartModel>(updatedCart);
    }
}