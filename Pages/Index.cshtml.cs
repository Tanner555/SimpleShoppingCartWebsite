using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreWebsiteTest1.OtherClasses;
using dbHandle = CoreWebsiteTest1.OtherClasses.MyDBController;
using CoreWebsiteTest1.Models;

namespace CoreWebsiteTest1.Pages
{
    public class IndexModel : PageModel
    {
        public string cartItemSessionKey = "cart_item_session";

        public void OnGet()
        {
            CreateCartSession(false);
        }

        public void OnPost()
        {
            CreateCartSession(true);
        }

        private void CreateCartSession(bool bIsPost)
        {
            string _action = HttpContext.Request.Query["action"].ToString();
            string _code = HttpContext.Request.Query["code"].ToString();
            List<CartItemModel> cartItemSession = HttpContext.Session.Get<List<CartItemModel>>(cartItemSessionKey);
            if (!string.IsNullOrEmpty(_action))
            {
                switch (_action)
                {
                    case "add":
                        if (Request.Form.ContainsKey("quantity") && !string.IsNullOrEmpty(_code))
                        {
                            int _quantity = int.Parse(Request.Form["quantity"]);
                            string _productByCodeQuery = "SELECT * FROM products WHERE code='" + _code + "'";
                            List<ProductItemModel> _productItems; string _connectionErrnoIfAny;
                            if (MyModelsHandler.RetrieveProductItemsFromQuery(_productByCodeQuery, out _productItems, out _connectionErrnoIfAny) &&
                                _productItems != null && _productItems.Count > 0)
                            {
                                var productByCode = _productItems[0];
                                var cartItemByCode = new CartItemModel(productByCode, _quantity);
                                if (cartItemSession != null && cartItemSession.Count > 0)
                                {
                                    //Cart Session Exists
                                    bool _bProductByCodeInArray = false;
                                    for (int i = 0; i < cartItemSession.Count; i++)
                                    {
                                        //Checking If CartItemByCode is Inside Current Session
                                        if (cartItemSession[i].ProductItem.Code == cartItemByCode.ProductItem.Code)
                                        {
                                            //Product Is Already In Session, Just Modify The Quantity Property
                                            _bProductByCodeInArray = true;
                                            if (cartItemSession[i].Quantity <= 0) cartItemSession[i].Quantity = 0;

                                            cartItemSession[i].Quantity += _quantity;
                                            //Update Session With New Cart Item Quantity
                                            HttpContext.Session.Set<List<CartItemModel>>(cartItemSessionKey, cartItemSession);
                                            break;
                                        }
                                    }

                                    if (_bProductByCodeInArray == false)
                                    {
                                        //Product Is Not In Session, Add It.
                                        cartItemSession.Add(cartItemByCode);
                                        HttpContext.Session.Set<List<CartItemModel>>(cartItemSessionKey, cartItemSession);
                                    }
                                }
                                else
                                {
                                    //Cart Session List Doesn't Exist, We Need To Create It.
                                    HttpContext.Session.Clear();
                                    var _newCartSession = new List<CartItemModel>() { cartItemByCode };
                                    HttpContext.Session.Set<List<CartItemModel>>(cartItemSessionKey, _newCartSession);
                                }
                            }
                        }
                        break;
                    case "remove":
                        if (cartItemSession != null && cartItemSession.Count > 0)
                        {
                            CartItemModel _cartItemToRemove = null;
                            foreach (var cartItem in cartItemSession)
                            {
                                if(_code == cartItem.ProductItem.Code)
                                {
                                    _cartItemToRemove = cartItem;
                                }
                            }
                            if(_cartItemToRemove != null)
                            {
                                cartItemSession.Remove(_cartItemToRemove);
                                HttpContext.Session.Set<List<CartItemModel>>(cartItemSessionKey, cartItemSession);
                            }
                        }
                        break;
                    case "empty":
                        HttpContext.Session.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        //[HttpGet()]
        //public IActionResult Get([FromQuery(Name = "page")] string page)
        //{

        //}
    }
}
