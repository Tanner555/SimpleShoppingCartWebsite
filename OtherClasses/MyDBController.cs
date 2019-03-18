using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using MySql.Data.MySqlClient;
using myPrivateInfo = CoreWebsiteTest1.PrivateInfo.MyPrivateInfo;
using CoreWebsiteTest1.Models;
using CoreWebsiteTest1.DAL;
using CoreWebsiteTest1.OtherClasses;

namespace CoreWebsiteTest1.OtherClasses
{
    public class MyDBController : IDisposable
    {
        #region Fields
        private MySqlConnection PrivateConn;
        private MySqlCommand MyPrivateSqlCommand;
        private MySqlDataReader MyPrivateSqlDataReader;
        #endregion

        #region Properties
        private string myConnectionString
        {
            //Template:server=servername;uid=userID;password=userPassword;database=databaseName
            get { return myPrivateInfo.mySQLConnectionString; }
        }
        #endregion

        #region Connection
        public bool connectDB(out MySqlConnection conn, out string connectionErrnoIfAny)
        {
            conn = null; connectionErrnoIfAny = "";
            if (PrivateConn != null && !string.IsNullOrEmpty(PrivateConn.Database))
            {
                conn = PrivateConn;
                return true;
            }

            bool bconnectionwassuccessful = false;
            try
            {
                bconnectionwassuccessful = true;
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                bconnectionwassuccessful = false;
                connectionErrnoIfAny = ex.Message;
            }
            return bconnectionwassuccessful;
        }
        #endregion

        #region Queries
        public bool RunQuery(string query, out MySqlDataReader dataReader, out string connectionErrnoIfAny)
        {
            MySqlConnection conn; dataReader = null;
            if (connectDB(out conn, out connectionErrnoIfAny))
            {
                return RunQuery(query, conn, out dataReader, out connectionErrnoIfAny);
            }
            return false;
        }

        public bool RunQuery(string query, MySqlConnection conn, out MySqlDataReader dataReader, out string connectionErrnoIfAny)
        {
            dataReader = null; connectionErrnoIfAny = "";
            try
            {
                if (MyPrivateSqlCommand != null) MyPrivateSqlCommand.Dispose();
                if (MyPrivateSqlDataReader != null) MyPrivateSqlDataReader.Dispose();

                MyPrivateSqlCommand = new MySqlCommand(query, conn);
                MyPrivateSqlDataReader = MyPrivateSqlCommand.ExecuteReader();
                dataReader = MyPrivateSqlDataReader;
                return true;
            }
            catch (Exception ex)
            {
                connectionErrnoIfAny = ex.Message;
                return false;
            }
        }
        #endregion

        #region Conversion
        public bool RetrieveProductItemsFromQuery(string query, out List<ProductItemModel> productItems, out string connectionErrnoIfAny)
        {
            MySqlDataReader _reader = null; connectionErrnoIfAny = "";
            if (RunQuery(query, out _reader, out connectionErrnoIfAny))
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

        private ProductItemModel RetrieveProductItemFromRead(ref MySqlDataReader reader)
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

        private CartItemModel RetrieveCartItemFromRead(ref MySqlDataReader reader, int quantity)
        {
            var _cartItem = new CartItemModel();
            _cartItem.ProductItem = RetrieveProductItemFromRead(ref reader);
            _cartItem.Quantity = quantity;
            return _cartItem;
        }
        #endregion

        #region Disposing
        public void Dispose()
        {
            if (PrivateConn != null)
            {
                PrivateConn.Dispose();
                PrivateConn = null;
            }
            if (MyPrivateSqlCommand != null)
            {
                MyPrivateSqlCommand.Dispose();
                MyPrivateSqlCommand = null;
            }
            if (MyPrivateSqlDataReader != null)
            {
                MyPrivateSqlDataReader.Dispose();
                MyPrivateSqlDataReader = null;
            }

        }
        #endregion

    }
}
