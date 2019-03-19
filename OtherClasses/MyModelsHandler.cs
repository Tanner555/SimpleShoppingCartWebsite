using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using CoreWebsiteTest1.Models;
using CoreWebsiteTest1.DAL;
using CoreWebsiteTest1.OtherClasses;
using dbHandle = CoreWebsiteTest1.OtherClasses.MyDBController;

namespace CoreWebsiteTest1.OtherClasses
{
    public class MyModelsHandler
    {
        #region Conversion
        public static bool RetrieveProductItemsFromQuery(string query, out List<ProductItemModel> productItems, out string connectionErrnoIfAny)
        {
            using (var _dbHandle = new dbHandle())
            {
                MySqlDataReader _reader = null; connectionErrnoIfAny = "";
                if (_dbHandle.RunQuery(query, out _reader, out connectionErrnoIfAny))
                {
                    productItems = new List<ProductItemModel>();
                    while (_reader.Read())
                    {
                        var _productItem = RetrieveProductItemFromRead(ref _reader);
                        productItems.Add(_productItem);
                    }
                    return true;
                }
                else
                {
                    productItems = null;
                    return false;
                }
            }
        }

        private static ProductItemModel RetrieveProductItemFromRead(ref MySqlDataReader reader)
        {
            var _productItem = new ProductItemModel();
            _productItem.ID = reader.GetInt32(_productItem.GetIDColumn);
            _productItem.Name = reader.GetString(_productItem.GetNameColumn);
            _productItem.PartType = reader.GetString(_productItem.GetPartTypeColumn);
            _productItem.Code = reader.GetString(_productItem.GetCodeColumn);
            _productItem.Image = reader.GetString(_productItem.GetImageColumn);
            _productItem.Price = reader.GetDouble(_productItem.GetPriceColumn);
            return _productItem;
        }

        //private CartItemModel RetrieveCartItemFromRead(ref MySqlDataReader reader, int quantity)
        //{
        //    var _cartItem = new CartItemModel();
        //    _cartItem.ProductItem = RetrieveProductItemFromRead(ref reader);
        //    _cartItem.Quantity = quantity;
        //    return _cartItem;
        //}
        #endregion
    }
}
