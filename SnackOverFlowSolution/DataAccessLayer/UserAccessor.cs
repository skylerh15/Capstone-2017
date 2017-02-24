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
    public class UserAccessor : IDataAccessor
    {
        public List<User> UserList { get; set; }
        public User UserInstance { get; set; }
        public string CreateScript
        {
            get
            {
                return "sp_create_app_user";
            }
        }

        public string DeactivateScript
        {
            get
            {
                return "sp_delete_app_user";
            }
        }

        public string RetrieveListScript
        {
            get
            {
                return "sp_retrieve_app_user_list";
            }
        }

        public string RetrieveSearchScript
        {
            get
            {
                return "sp_retrieve_app_user_from_search";
            }
        }

        public string RetrieveSingleScript
        {
            get
            {
                return "sp_retrieve_app_user";
            }
        }

        public string UpdateScript
        {
            get
            {
                return "sp_update_app_user";
            }
        }

        public void ReadList(SqlDataReader reader)
        {
            UserList = new List<User>();
            while (reader.Read())
            {
                UserList.Add(new User()
                {
                    UserId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    //Test if the query returned a null value.
                    LastName = (reader.IsDBNull(2) ? null : reader.GetString(2)),
                    Phone = reader.GetString(3),
                    //Test if the query returned a null value.
                    PreferredAddressId = (reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4)),
                    EmailAddress = reader.GetString(5),
                    EmailPreferences = reader.GetBoolean(6),
                    UserName = reader.GetString(9),
                    Active = reader.GetBoolean(10)
                });
            }
        }

        public void ReadSingle(SqlDataReader reader)
        {
            UserInstance = null;
            UserInstance = new User()
            {
                UserId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                //Test if the query returned a null value.
                LastName = (reader.IsDBNull(2) ? null : reader.GetString(2)),
                Phone = reader.GetString(3),
                //Test if the query returned a null value.
                PreferredAddressId = (reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4)),
                EmailAddress = reader.GetString(5),
                EmailPreferences = reader.GetBoolean(6),
                UserName = reader.GetString(9),
                Active = reader.GetBoolean(10)
            };
        }

        public static User RetrieveUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void SetCreateParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@FIRST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@LAST_NAME", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PHONE", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@PREFERRED_ADDRESS_ID", SqlDbType.Int);
            cmd.Parameters.Add("@E_MAIL_ADDRESS", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@E_MAIL_PREFERENCES", SqlDbType.Bit);
            cmd.Parameters.Add("@PASSWORD_HASH", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@PASSWORD_SALT", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@USER_NAME", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit);
            cmd.Parameters["@FIRST_NAME"].Value = UserInstance.FirstName;
            cmd.Parameters["@LAST_NAME"].Value = UserInstance.LastName;
            cmd.Parameters["@PHONE"].Value = UserInstance.Phone;
            cmd.Parameters["@PREFERRED_ADDRESS_ID"].Value = UserInstance.PreferredAddressId;
            cmd.Parameters["@E_MAIL_ADDRESS"].Value = UserInstance.EmailAddress;
            cmd.Parameters["@E_MAIL_PREFERENCES"].Value = UserInstance.EmailPreferences;
            cmd.Parameters["@PASSWORD_HASH"].Value = UserInstance.PasswordHash;
            cmd.Parameters["@PASSWORD_SALT"].Value = UserInstance.PasswordSalt;
            cmd.Parameters["@USER_NAME"].Value = UserInstance.UserName;
            cmd.Parameters["@ACTIVE"].Value = UserInstance.Active;
        }

        public void SetKeyParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetRetrieveSearchParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetUpdateParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public String RetrieveUserSalt (String userName)
        {

            var results = "";
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_retrieve_user_salt", conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar,50);
            cmd.Parameters["@Username"].Value = userName;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    results = reader.GetString(0);
                }
            } catch 
            {
                throw;
            }
            return results;
        }

        public bool Login(String userName, String hash)
        {
            var results = false;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_login", conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Password_Hash", SqlDbType.NVarChar, 64);
            cmd.Parameters["@Username"].Value = userName;
            cmd.Parameters["@Password_Hash"].Value = hash;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingle(reader);
                }
            }
            catch
            {
                throw;
            }
            return (null!=UserInstance);
        }
    }
}