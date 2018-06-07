using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using cflora.Models;
using cflora.App_Code;
//using cflora.App_Code;

namespace cflora.DAL
{
    public class CommonDAL
    {
        #region (GLOBAL VARIABLE DECLARATION)
        public static readonly string _connectionString = string.Empty;
        protected SqlConnection oConnection;
        protected SqlCommand oCommand;
        protected SqlCommandBuilder oCommandBuilder;
        protected SqlDataAdapter oDataAdapter;
        protected SqlDataReader oDataReader;
        protected DataTable oDataTable;
        protected DataSet oDataSet;
        protected DataRow oDataRow;
        protected DataColumn oDataColumn;
        protected SqlTransaction oTransaction;
        protected SqlConnectionStringBuilder oBuilder;
        #endregion

        #region (DEFAULT CONSTRUCTOR)
        static CommonDAL()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new Exception("No connection string configured in Web.Config file");
            }
        }
        #endregion

        #region(FETCH PRODUCT LIST)
        public List<ProductModels> FetchProductList(string sSectionName)
        {
            List<ProductModels> _ProductModels = new List<ProductModels>();
            oBuilder = new SqlConnectionStringBuilder(_connectionString);
            oConnection = new SqlConnection(oBuilder.ConnectionString);
            oDataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    oBuilder = new SqlConnectionStringBuilder(_connectionString);
                    oConnection = new SqlConnection(oBuilder.ConnectionString);
                    oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectData", oConnection);
                    oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                    using (oConnection)
                    {
                        oConnection.Open();
                        oDataAdapter.Fill(oDataTable);
                    }
                    foreach (DataRow row in oDataTable.Rows)
                    {
                        ProductModels PM = new ProductModels();
                        PM.P_NAME = row["P_NAME"].ToString();
                        PM.URL_NAME = row["P_NAME"].ToString();
                        PM.P_IMAGE_THUMB = row["P_IMAGE_THUMB"].ToString();
                        PM.P_PRICE = Convert.ToDecimal(row["P_PRICE"]);
                        _ProductModels.Add(PM);
                    }
                }
                catch (Exception ex)
                {
                    ApplicationError.LogErrors(ex);
                    oConnection.Close();
                }
                return _ProductModels;
            }
        }

        public List<ProductModels> FetchProductList(string sSectionName, string id)
        {
            List<ProductModels> _ProductModels = new List<ProductModels>();
            oBuilder = new SqlConnectionStringBuilder(_connectionString);
            oConnection = new SqlConnection(oBuilder.ConnectionString);
            oDataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    oBuilder = new SqlConnectionStringBuilder(_connectionString);
                    oConnection = new SqlConnection(oBuilder.ConnectionString);
                    oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataByStr", oConnection);
                    oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                    oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                    using (oConnection)
                    {
                        oConnection.Open();
                        oDataAdapter.Fill(oDataTable);
                    }
                    foreach (DataRow row in oDataTable.Rows)
                    {
                        ProductModels PM = new ProductModels();
                        PM.P_NAME = row["P_NAME"].ToString();
                        PM.URL_NAME = row["P_NAME"].ToString();
                        PM.P_IMAGE_THUMB = row["P_IMAGE_THUMB"].ToString();
                        PM.P_PRICE = Convert.ToDecimal(row["P_PRICE"]);
                        _ProductModels.Add(PM);
                    }
                }
                catch (Exception ex)
                {
                    ApplicationError.LogErrors(ex);
                    oConnection.Close();
                }
                return _ProductModels;
            }
        }
        #endregion

        #region(FETCH META DATA)
        public List<MetaModels> FetchMetaData(string sSectionName, int id)
        {
            List<MetaModels> _MetaModels = new List<MetaModels>();
            oBuilder = new SqlConnectionStringBuilder(_connectionString);
            oConnection = new SqlConnection(oBuilder.ConnectionString);
            oDataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    oBuilder = new SqlConnectionStringBuilder(_connectionString);
                    oConnection = new SqlConnection(oBuilder.ConnectionString);
                    oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataById", oConnection);
                    oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                    oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                    using (oConnection)
                    {
                        oConnection.Open();
                        oDataAdapter.Fill(oDataTable);
                    }
                    foreach (DataRow row in oDataTable.Rows)
                    {
                        MetaModels mm = new MetaModels();
                        mm.Title = row["FT_NAME"].ToString();
                        mm.Keywords = row["FT_NAME"].ToString();
                        mm.Descption = row["FT_NAME"].ToString();
                        _MetaModels.Add(mm);
                    }
                }
                catch (Exception ex)
                {
                    ApplicationError.LogErrors(ex);
                    oConnection.Close();
                }
                return _MetaModels;
            }
        }
        #endregion

        #region(SELECT SINGLE COLUMN DATA)
        public object SelectSingleData(string SectionName, string ValueData)
        {
            //int iVaule = 0;
            object obj = new object();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oCommand = new SqlCommand("Jobs_SelectSingleData", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@SectionName", SectionName);
                oCommand.Parameters.AddWithValue("@ValueData", ValueData);
                using (oConnection)
                {
                    oConnection.Open();
                    obj = oCommand.ExecuteScalar();
                    //if (obj != null)
                    //{
                    //    iVaule = (int)obj;
                    //}
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            finally
            {
                if (oConnection.State == ConnectionState.Open)
                    oConnection.Close();
            }
            return obj;
        }
        #endregion

        #region(SELECT DATA SET FROM SECTION WISE)
        public DataSet MasterSelectDataSet(string sSectionName)
        {
            oDataSet = new DataSet();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectData", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataSet);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataSet;
        }

        public DataSet MasterSelectDataSet(string sSectionName, int id)
        {
            oDataSet = new DataSet();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataById", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataSet);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataSet;
        }


        public DataSet MasterSelectDataSet(string sSectionName, string sId)
        {
            oDataSet = new DataSet();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataByStr", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", sId);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataSet);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataSet;
        }
        #endregion

        #region(SELECT DATA TABLE FROM SECTION WISE)
        public DataTable MasterSelectData(string sSectionName)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectData", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }

        public DataTable MasterSelectData(string sSectionName, int id)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataById", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }

        public DataTable MasterSelectData(string sSectionName, string sId)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataByStr", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", sId);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }

        public DataTable MasterSelectData(string sSectionName, string FirstId, string SecondId)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataByStr2", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionName", sSectionName);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FirstId", FirstId);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SecondId", SecondId);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }
        #endregion

        #region(SELECT HOME META DATA)
        public IDataReader FetchHomeMeta(int id)
        {
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_MasterSelectDataById", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                oConnection.Open();
                oCommand.Connection = oConnection;
                oCommand.CommandText = "select * from FT_META where ID=@ID";
                oDataReader = oCommand.ExecuteReader();
                if (oDataReader.Read())
                {
                    //this.META_TITLE = dr["META_TITLE"].ToString();
                    //this.META_KEYWORD = dr["META_KEYWORD"].ToString();
                    //this.META_DESCRIPTION = dr["META_DESCRIPTION"].ToString();
                }
                oDataReader.Close();
                oConnection.Close();
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                if (oConnection.State == ConnectionState.Open) oConnection.Close();
            }
            return oDataReader;
        }
        #endregion

        #region(GET TIME FRAME)
        public DataTable GetTimeFrameData(int id, string SelectDT)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_GetTimeFrameData", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SectionId", id);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@SelectDT", SelectDT);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }
        #endregion

        #region (INSERT SHIPPING DETAIL)
        public int InsertShippingDetail(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertShippingDetail", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
            oCommand.Parameters.AddWithValue("@FT_ADD", obj_BLL.FT_ADD);
            oCommand.Parameters.AddWithValue("@FT_STATEID", obj_BLL.FT_STATEID);
            oCommand.Parameters.AddWithValue("@FT_STATENAME", obj_BLL.FT_STATENAME);
            oCommand.Parameters.AddWithValue("@FT_CITY", obj_BLL.FT_CITY);
            oCommand.Parameters.AddWithValue("@FT_PIN", obj_BLL.FT_PIN);
            oCommand.Parameters.AddWithValue("@FT_COUNTRY", obj_BLL.FT_COUNTRY);
            oCommand.Parameters.AddWithValue("@FT_MOBILE", obj_BLL.FT_MOBILE);
            oCommand.Parameters.AddWithValue("@FT_TITLE", obj_BLL.FT_TITLE);

            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();

                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (INSERT USER PROFILE DETAIL)
        public int InsertUserProfDetail(PropertyModels obj_BLL, out string sStatus)
        {
            string dStatus = string.Empty;
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertUserProfDetail", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
            oCommand.Parameters.AddWithValue("@FT_ADD", obj_BLL.FT_ADD);
            oCommand.Parameters.AddWithValue("@FT_STATENAME", obj_BLL.FT_STATENAME);
            oCommand.Parameters.AddWithValue("@FT_CITY", obj_BLL.FT_CITY);
            oCommand.Parameters.AddWithValue("@FT_PIN", obj_BLL.FT_PIN);
            oCommand.Parameters.AddWithValue("@FT_COUNTRY", obj_BLL.FT_COUNTRY);
            oCommand.Parameters.AddWithValue("@FT_MOBILE", obj_BLL.FT_MOBILE);
            oCommand.Parameters.Add("@sStatus", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();
                dStatus = oCommand.Parameters["@sStatus"].Value.ToString();
                oConnection.Close();
            }
            sStatus = dStatus;
            return iRowAffected;
        }
        #endregion

        #region (INSERT ORDER LOGIN)
        public int InsertOrderLogin(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertOrderLogin", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@FT_EMAIL", obj_BLL.FT_EMAIL);
            oCommand.Parameters.AddWithValue("@FT_PWD", obj_BLL.FT_PWD);
            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();

                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (INSERT SUCCESS ORDER THROUGH LOGIN)
        public int InsertSuccessOrder(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertSuccessOrder", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_ORDERID", obj_BLL.FT_ORDERID);
            oCommand.Parameters.AddWithValue("@FT_COUPONNO", obj_BLL.FT_COUPONNO);

            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();

                oConnection.Close();
            }
            return iRowAffected;
        }

        public int InsertSuccessOrderDisc(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertSuccessOrderDisc", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_ORDERID", obj_BLL.FT_ORDERID);
            oCommand.Parameters.AddWithValue("@FT_COUPONNO", obj_BLL.FT_COUPONNO);

            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();

                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (INSERT SIGN UP)
        public int InsertSignUp(PropertyModels obj_BLL, out string sStatus)
        {
            string dStatus = string.Empty;
            int iRowAffected = 0;
            try
            {
                oConnection = new SqlConnection(_connectionString);
                oCommand = new SqlCommand("Jobs_InsertSignUp", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                //oCommand.Parameters.AddWithValue("@FT_TITLE", obj_BLL.FT_TITLE);
                oCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
                oCommand.Parameters.AddWithValue("@FT_ADD", obj_BLL.FT_ADD);
                oCommand.Parameters.AddWithValue("@FT_CITY", obj_BLL.FT_CITY);
                //oCommand.Parameters.AddWithValue("@FT_STATEID", obj_BLL.FT_STATEID);
                oCommand.Parameters.AddWithValue("@FT_STATENAME", obj_BLL.FT_STATENAME);
                oCommand.Parameters.AddWithValue("@FT_PIN", obj_BLL.FT_PIN);
                oCommand.Parameters.AddWithValue("@FT_COUNTRY", obj_BLL.FT_COUNTRY);
                oCommand.Parameters.AddWithValue("@FT_MOBILE", obj_BLL.FT_MOBILE);
                //oCommand.Parameters.AddWithValue("@FT_DOB", obj_BLL.FT_DOB);
                oCommand.Parameters.AddWithValue("@FT_EMAIL", obj_BLL.FT_EMAIL);
                oCommand.Parameters.AddWithValue("@FT_PWD", obj_BLL.FT_PWD);
                oCommand.Parameters.AddWithValue("@FT_REFBY", obj_BLL.FT_COUPONNO);
                //oCommand.Parameters.AddWithValue("@FT_GENDER", obj_BLL.FT_GENDER);
                oCommand.Parameters.Add("@sStatus", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

                using (oConnection)
                {
                    oConnection.Open();
                    iRowAffected = oCommand.ExecuteNonQuery();
                    dStatus = oCommand.Parameters["@sStatus"].Value.ToString();
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            sStatus = dStatus;
            return iRowAffected;
        }

        public int InsertUpdateProfile(PropertyModels obj_BLL, out string sStatus)
        {
            string dStatus = string.Empty;
            int iRowAffected = 0;
            try
            {
                oConnection = new SqlConnection(_connectionString);
                oCommand = new SqlCommand("Jobs_InsertUpdateProfile", oConnection);
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
                oCommand.Parameters.AddWithValue("@FT_TITLE", obj_BLL.FT_TITLE);
                oCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
                oCommand.Parameters.AddWithValue("@FT_ADD", obj_BLL.FT_ADD);
                oCommand.Parameters.AddWithValue("@FT_CITY", obj_BLL.FT_CITY);
                oCommand.Parameters.AddWithValue("@FT_STATENAME", obj_BLL.FT_STATENAME);
                oCommand.Parameters.AddWithValue("@FT_PIN", obj_BLL.FT_PIN);
                oCommand.Parameters.AddWithValue("@FT_COUNTRY", obj_BLL.FT_COUNTRY);
                oCommand.Parameters.AddWithValue("@FT_MOBILE", obj_BLL.FT_MOBILE);
                oCommand.Parameters.AddWithValue("@FT_EMAIL", obj_BLL.FT_EMAIL);
                oCommand.Parameters.AddWithValue("@FT_DOB", obj_BLL.FT_DOB);
                oCommand.Parameters.AddWithValue("@FT_GENDER", obj_BLL.FT_GENDER);
                oCommand.Parameters.Add("@sStatus", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

                using (oConnection)
                {
                    oConnection.Open();
                    iRowAffected = oCommand.ExecuteNonQuery();
                    dStatus = oCommand.Parameters["@sStatus"].Value.ToString();
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
            }
            sStatus = dStatus;
            return iRowAffected;
        }
        #endregion

        #region(ADMIN SEARCH PRODUCT)
        public DataTable SearchProductDetail(PropertyModels obj_BLL)
        {
            oDataTable = new DataTable();
            try
            {
                oBuilder = new SqlConnectionStringBuilder(_connectionString);
                oConnection = new SqlConnection(oBuilder.ConnectionString);
                oDataAdapter = new SqlDataAdapter("Jobs_SearchProductDetail", oConnection);
                oDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_FromAmt", obj_BLL.FT_FromAmt);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_ToAmt", obj_BLL.FT_ToAmt);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_PROD_CODE", obj_BLL.FT_PROD_CODE);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_CAT_ID", obj_BLL.FT_CAT_ID);
                oDataAdapter.SelectCommand.Parameters.AddWithValue("@FT_SUBCAT_ID", obj_BLL.FT_SUBCAT_ID);
                using (oConnection)
                {
                    oConnection.Open();
                    oDataAdapter.Fill(oDataTable);
                }
            }
            catch (Exception ex)
            {
                ApplicationError.LogErrors(ex);
            }
            return oDataTable;
        }
        #endregion

        #region (INSERT ORDER DETAIL)
        public int InsertOrderDetail(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertOrderDetail", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            oCommand.Parameters.AddWithValue("@FT_ORDERNO", obj_BLL.FT_ORDERNO);
            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_OR_DATE", obj_BLL.FT_OR_DATE);
            oCommand.Parameters.AddWithValue("@FT_OR_NOOFITEMS", obj_BLL.FT_OR_NOOFITEMS);
            oCommand.Parameters.AddWithValue("@FT_OR_PRICE", obj_BLL.FT_OR_PRICE);
            oCommand.Parameters.AddWithValue("@FT_OR_DELIVERYDATE", obj_BLL.FT_OR_DELIVERYDATE);
            oCommand.Parameters.AddWithValue("@FT_OR_DISC_COUPON", obj_BLL.FT_OR_DISC_COUPON);
            oCommand.Parameters.AddWithValue("@FT_OR_DISCOUNT", obj_BLL.FT_OR_DISCOUNT);
            oCommand.Parameters.AddWithValue("@FT_OR_SHIPPING_AMT", obj_BLL.FT_OR_SHIPPING_AMT);
            oCommand.Parameters.AddWithValue("@FT_OR_TOT_PAYABLE", obj_BLL.FT_OR_TOT_PAYABLE);
            oCommand.Parameters.AddWithValue("@FT_OR_USER_STATUS", obj_BLL.FT_OR_USER_STATUS);
            oCommand.Parameters.AddWithValue("@FT_OR_STATUS", obj_BLL.FT_OR_STATUS);
            oCommand.Parameters.AddWithValue("@FT_OR_RCPNT_MSG", obj_BLL.FT_OR_RCPNT_MSG);
            oCommand.Parameters.AddWithValue("@FT_OR_OTHER_PREF", obj_BLL.FT_OR_OTHER_PREF);
            oCommand.Parameters.AddWithValue("@FT_OR_DELIVERYTIME", obj_BLL.FT_OR_DELIVERYTIME);
            oCommand.Parameters.AddWithValue("@FT_OR_TIMEFRAME", obj_BLL.FT_OR_TIMEFRAME);
            oCommand.Parameters.AddWithValue("@FT_OR_WALLETSTATUS", obj_BLL.FT_OR_WALLETSTATUS);
            oCommand.Parameters.AddWithValue("@FT_OR_WALLET_OPENAMT", obj_BLL.FT_OR_WALLET_OPENAMT);
            oCommand.Parameters.AddWithValue("@FT_OR_WALLET_DEBITAMT", obj_BLL.FT_OR_WALLET_DEBITAMT);
            oCommand.Parameters.AddWithValue("@FT_OR_WALLET_CLOSEAMT", obj_BLL.FT_OR_WALLET_CLOSEAMT);

            oCommand.Parameters.AddWithValue("@FT_NAME", obj_BLL.FT_NAME);
            oCommand.Parameters.AddWithValue("@FT_ADD", obj_BLL.FT_ADD);
            oCommand.Parameters.AddWithValue("@FT_STATENAME", obj_BLL.FT_STATENAME);
            oCommand.Parameters.AddWithValue("@FT_STATEID", obj_BLL.FT_STATEID);
            oCommand.Parameters.AddWithValue("@FT_CITY", obj_BLL.FT_CITY);
            oCommand.Parameters.AddWithValue("@FT_PIN", obj_BLL.FT_PIN);
            oCommand.Parameters.AddWithValue("@FT_COUNTRY", obj_BLL.FT_COUNTRY);
            oCommand.Parameters.AddWithValue("@FT_MOBILE", obj_BLL.FT_MOBILE);

            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();
                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (ADD AMOUNT IN WALLET)
        public int AddAmountInWallet(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_AddAmountInWallet", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;

            oCommand.Parameters.AddWithValue("@ID", obj_BLL.ID);
            oCommand.Parameters.AddWithValue("@ORDERNO", obj_BLL.FT_ORDERNO);
            oCommand.Parameters.AddWithValue("@CREDIT_AMT", obj_BLL.FT_OR_WALLET_CREDITAMT);
            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();
                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (INSERT SUCCESS ORDER THROUGH LOGIN)
        public int InsertCancelOrder(PropertyModels obj_BLL)
        {
            int iRowAffected = 0;
            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertCancelOrder", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_ORDERID", obj_BLL.FT_ORDERID);
            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();

                oConnection.Close();
            }
            return iRowAffected;
        }
        #endregion

        #region (INSERT ORDER HISTORY DETAIL)
        public int InsertOrderHistroy(PropertyModels obj_BLL, out string status)
        {
            int iRowAffected = 0;
            string status_out = string.Empty;

            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_InsertOrderHistroy", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@FT_ORDERNO", obj_BLL.FT_ORDERNO);
            oCommand.Parameters.AddWithValue("@FT_USERID", obj_BLL.FT_USERID);
            oCommand.Parameters.AddWithValue("@FT_DEBITAMT", obj_BLL.FT_OR_WALLET_DEBITAMT);
            oCommand.Parameters.Add("@sStatus", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();
                status_out = oCommand.Parameters["@sStatus"].Value.ToString();
                oConnection.Close();
            }
            status = status_out;
            return iRowAffected;
        }
        #endregion

        #region (CHECK USER NAME AND PASSWORD FOR LOGIN PAGE)
        public int CheckLogin(PropertyModels obj_BLL, out string status)
        {
            int iRowAffected = 0;
            string status_out = string.Empty;

            oConnection = new SqlConnection(_connectionString);
            oCommand = new SqlCommand("Jobs_CheckLoginUser", oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.AddWithValue("@LoginId", obj_BLL.LoginId);
            oCommand.Parameters.AddWithValue("@Password", obj_BLL.Password);
            oCommand.Parameters.Add("@sStatus", SqlDbType.VarChar, 64).Direction = ParameterDirection.Output;
            using (oConnection)
            {
                oConnection.Open();
                iRowAffected = oCommand.ExecuteNonQuery();
                status_out = oCommand.Parameters["@sStatus"].Value.ToString();
                oConnection.Close();
            }
            status = status_out;
            return iRowAffected;
        }
        #endregion
    }
}