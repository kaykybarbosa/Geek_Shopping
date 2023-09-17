using AutoMapper;
using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response;
using GeekShopping.CartApi.Interfaces.IRepositories;
using GeekShopping.CartApi.Interfaces.IServices;
using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            CartHeader header = await _cartRepository.FindCartHeader(userId);

            if (header != null)
            {
                header.CouponCode = couponCode;
                await _cartRepository.UpdateHeader(header);

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            CartHeader header = await _cartRepository.FindCartHeader(userId);

            if (header != null)
            {
                header.CouponCode = "";
                await _cartRepository.UpdateHeader(header);

                return true;
            }

            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            CartHeader cartHeader = await _cartRepository.FindCartHeader(userId);
            if (cartHeader != null)
            {
                await _cartRepository.RemoveCartDetailRange(cartHeader.Id);

                await _cartRepository.RemoveCartHeader(cartHeader);

                return true;
            }

            return false;
        }

        public async Task<CartResponse> FindCartByUserId(string userId)
        {
            CartHeader cartHeader = await _cartRepository.FindCartHeader(userId);
            IEnumerable<CartDetail> cartDetails = await _cartRepository.FindCartDetails(cartHeader.Id);

            Cart cart = new()
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            };

            return _mapper.Map<CartResponse>(cart);
        }

        public async Task<CartResponse> SaveOrUpdateCart(CartRequest cartRequest)
        {
            Cart cart = _mapper.Map<Cart>(cartRequest);

            //Check if the product is alreay saved in database, if it does not exist the save
            Product product = await _cartRepository.FindProductById(cart.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                await _cartRepository.CreateProduct(cart.CartDetails.FirstOrDefault().Product);
            }

            //Check if CartHeader is null
            CartHeader cartHeader = await _cartRepository.FindCartHeaderNoTracking(cart.CartHeader.UserId);
            if (cartHeader == null)
            {
                //Create CartHeader and CartDetails
                await _cartRepository.CreateCartHeader(cart.CartHeader);

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;

                await _cartRepository.CreateCartDetails(cart.CartDetails.FirstOrDefault());
            }
            else
            {
                //If CartHeader is not null
                //Check if CartHeader has some product
                var detailtId = cart.CartDetails.FirstOrDefault().ProductId;
                var headerId = cartHeader.Id;

                CartDetail cartDetails = await _cartRepository.FindCartDetailNoTracking(detailtId, headerId);

                if (cartDetails == null)
                {
                    //Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;

                    CartDetail cartDetailSaved = cart.CartDetails.FirstOrDefault();
                    await _cartRepository.CreateCartDetails(cartDetailSaved);
                }
                else
                {
                    //Update product count and CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetails.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetails.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetails.CartHeaderId;

                    await _cartRepository.UpdateCartDetail(cart.CartDetails.FirstOrDefault());
                }
            }

                return _mapper.Map<CartResponse>(cart);
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _cartRepository.FindCartDetail(cartDetailsId);

                IEnumerable<CartDetail> cartDetails = await _cartRepository.FindCartDetails(cartDetail.CartHeaderId);

                if (cartDetails == null)
                {
                    return false;
                }

                int total = 0;
                foreach (var items in cartDetails)
                {
                    total++;
                }

                await _cartRepository.RemoveCartDetail(cartDetail);

                if (total == 1)
                {
                    CartHeader cartHeaderToRemove = await _cartRepository.FindCartHeader(cartDetail.CartHeaderId);

                    await _cartRepository.RemoveCartHeader(cartHeaderToRemove);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}