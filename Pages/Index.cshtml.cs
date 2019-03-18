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
            List<CartItemModel> cartItemSession = HttpContext.Session.Get<List<CartItemModel>>("cart_item_session");
            if (!string.IsNullOrEmpty(_action))
            {
                switch (_action)
                {
                    case "add":
                        if (Request.Form.ContainsKey("quantity") && !string.IsNullOrEmpty(_code))
                        {
                            string _productByCodeQuery = "SELECT * FROM products WHERE code='" + _code + "'";
                            using (var _dbHandle = new dbHandle())
                            {
                                List<ProductItemModel> _productItems; string _connectionErrnoIfAny;
                                if (_dbHandle.RetrieveProductItemsFromQuery(_productByCodeQuery, out _productItems, out _connectionErrnoIfAny) &&
                                    _productItems != null && _productItems.Count > 0)
                                {
                                    var productByCode = _productItems[0];

                                }
                            }
                        }
                        break;
                    case "remove":
                        if (cartItemSession != null && cartItemSession.Count > 0)
                        {
                            foreach (var cartItem in cartItemSession)
                            {
                                if(_code == cartItem.ProductItem.Code)
                                {
                                    HttpContext.Session.Remove(_code);
                                }
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
