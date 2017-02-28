﻿using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductOrderAccessor
    {
        /// <summary>
        /// Victor Algarin
        /// Created 2017/2/10
        /// 
        /// Retrieves details for a specific order through an order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>ProductOrder</returns>
        public static ProductOrder RetrieveOrderByID(int orderID)
        {
            var order = new ProductOrder();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);
            cmd.Parameters["@ORDER_ID"].Value = orderID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    order = new ProductOrder()
                    {
                        OrderId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        OrderTypeId = reader.GetString(2),
                        AddressType = reader.GetString(3),
                        DeliveryTypeId = reader.GetString(4),
                        Amount = reader.GetDecimal(5),
                        OrderDate = reader.GetDateTime(6),
                        DateExpected = reader.GetDateTime(7),
                        Discount = reader.GetDecimal(8),
                        OrderStatusId = reader.GetString(9),
                        UserAddressId = reader.GetInt32(10),
                        HasArrived = reader.GetBoolean(11)
                    };
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return order;
        }
        public static List<ProductOrder> RetrieveProductOrdersByStatus(string Status)
        {
            var ProductsByStatusList = new List<ProductOrder>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PRODUCT_ORDER_by_status";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    var CurrentProductOrder = new ProductOrder()
                    {
                        OrderId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        OrderTypeId = reader.GetString(2),
                        AddressType = reader.GetString(3),
                        DeliveryTypeId = reader.GetString(4),
                        Amount = reader.GetDecimal(5),
                        OrderDate = reader.GetDateTime(6),
                        DateExpected = reader.GetDateTime(7),
                        Discount = reader.GetDecimal(8),
                        OrderStatusId = reader.GetString(9),
                        UserAddressId = reader.GetInt32(10),
                        HasArrived = reader.GetBoolean(11)
                    };
                    ProductsByStatusList.Add(CurrentProductOrder);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return ProductsByStatusList;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/16
        /// 
        /// Updates the status for the provided order
        /// </summary>
        /// <param name="orderID">The ProductOrderId related to the product order that should be updated</param>
        /// <param name="newStatus">The new status that should be given to the product order</param>
        /// <returns>int rows affected</returns>
        public static int UpdateProductOrderStatus(int orderID, string newStatus)
        {
            int result = 0;

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_order_status";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Adding parameters

            cmd.Parameters.AddWithValue("@ORDER_ID", orderID);
            cmd.Parameters.AddWithValue("@NEW_ORDER_STATUS_ID", newStatus);

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                // Storing the amount of rows that were affected by the stored procedure
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
