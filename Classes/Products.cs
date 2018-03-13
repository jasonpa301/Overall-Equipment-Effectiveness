using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oeeps
{
    public class Products
    {
        private string productName;
        SqlDbConnection con = new SqlDbConnection();

        public Products()
        {

        }

        public Products(string _productName)
        {
            productName = _productName;
        }

        public void AddProduct()
        {
            con.SetCommand("Insert into Products (ProductName) Values (@Pn)");
            con.com.Parameters.AddWithValue("@Pn", productName);
            con.NonQueryEx();
        }

        public void RetrieveProductData(int productId)
        {
            con.SetCommand("SELECT ProductName FROM Products WHERE Product_ID = " + productId);
            productName = con.StringScalar();
        }

        public void UpdateProductValues(string _productName)
        {
            if (_productName != "")
                productName = _productName;
        }

        public void UpdateProduct(int productId)
        {
            con.SetCommand("UPDATE Products SET ProductName = @pn WHERE Product_ID = " + productId);
            con.com.Parameters.AddWithValue("@pn", productName);
            con.NonQueryEx();
        }

        public void DeleteProduct(int productId)
        {
            con.SetCommand("Delete FROM Products WHERE Product_ID = " + productId);
            con.NonQueryEx();
        }

        public void CloseConnection()
        {
            con.CloseCon();

        }
    }
}