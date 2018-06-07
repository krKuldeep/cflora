using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using cflora.DAL;
using cflora.Models;
using cflora.App_Code;

namespace cflora.BLL
{
    public class CommonBLL
    {
        CommonDAL oCommonDAL = new CommonDAL();

        #region(FETCH PRODUCT LIST)
        public List<ProductModels> FetchProductList(string sSectionName)
        {
            List<ProductModels> _ProductModel = null;
            try
            {
                _ProductModel = oCommonDAL.FetchProductList(sSectionName);
            }
            catch (Exception ex)
            {
                cflora.App_Code.ApplicationError.LogErrors(ex);
            }
            return _ProductModel;
        }

        public List<ProductModels> FetchProductList(string sSectionName, string id)
        {
            List<ProductModels> _ProductModel = null;
            try
            {
                _ProductModel = oCommonDAL.FetchProductList(sSectionName, id);
            }
            catch (Exception ex)
            {
                cflora.App_Code.ApplicationError.LogErrors(ex);
            }
            return _ProductModel;
        }
        #endregion

        #region(FETCH PRODUCT LIST)
        public List<MetaModels> FetchMetaData(string sSectionName, int id)
        {
            List<MetaModels> _MetaModels = null;
            try
            {
                _MetaModels = oCommonDAL.FetchMetaData(sSectionName, id);
            }
            catch (Exception ex)
            {
                cflora.App_Code.ApplicationError.LogErrors(ex);
            }
            return _MetaModels;
        }
        #endregion

        #region(SELECT SINGLE COLUMN DATA)
        public object SelectSingleData(string SectionName, string ValueData)
        {
            return oCommonDAL.SelectSingleData(SectionName, ValueData);
        }
        #endregion

        #region(SELECT DATA SET FROM SECTION WISE)
        public DataSet MasterSelectDataSet(string sSectionName)
        {
            return oCommonDAL.MasterSelectDataSet(sSectionName);
        }

        public DataSet MasterSelectDataSet(string sSectionName, int id)
        {
            return oCommonDAL.MasterSelectDataSet(sSectionName, id);
        }

        public DataSet MasterSelectDataSet(string sSectionName, string sId)
        {
            return oCommonDAL.MasterSelectDataSet(sSectionName, sId);
        }

        #endregion

        #region(SELECT DATA TABLE FROM SECTION WISE)
        public DataTable MasterSelectData(string sSectionName)
        {
            return oCommonDAL.MasterSelectData(sSectionName);
        }

        public DataTable MasterSelectData(string sSectionName, int id)
        {
            return oCommonDAL.MasterSelectData(sSectionName, id);
        }

        public DataTable MasterSelectData(string sSectionName, string sId)
        {
            return oCommonDAL.MasterSelectData(sSectionName, sId);
        }

        public DataTable MasterSelectData(string sSectionName, string FirstId, string SecondId)
        {
            return oCommonDAL.MasterSelectData(sSectionName, FirstId, SecondId);
        }
        #endregion

        #region(SELECT HOME META DATA)
        public IDataReader FetchHomeMeta(int id)
        {
            return oCommonDAL.FetchHomeMeta(id);
        }
        #endregion

        #region(GET TIME FRAME)
        public DataTable GetTimeFrameData(int id, string SelectDT)
        {
            return oCommonDAL.GetTimeFrameData(id, SelectDT);
        }
        #endregion

        #region(INSERT SHIPPING DETAIL)
        public int InsertShippingDetail(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertShippingDetail(_objProperty);
        }
        #endregion

        #region(INSERT USER PROFILE DETAIL)
        public int InsertUserProfDetail(PropertyModels _objProperty, out string sStatus)
        {
            return oCommonDAL.InsertUserProfDetail(_objProperty, out sStatus);
        }
        #endregion

        #region(INSERT ORDER LOGIN)
        public int InsertOrderLogin(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertOrderLogin(_objProperty);
        }
        #endregion

        #region(INSERT SUCCESS ORDER THROUGH LOGIN)
        public int InsertSuccessOrder(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertSuccessOrder(_objProperty);
        }

        public int InsertSuccessOrderDisc(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertSuccessOrderDisc(_objProperty);
        }
        #endregion

        #region(INSERT SIGN UP)
        public int InsertSignUp(PropertyModels _objProperty, out string sStatus)
        {
            return oCommonDAL.InsertSignUp(_objProperty, out sStatus);
        }

        public int InsertUpdateProfile(PropertyModels _objProperty, out string sStatus)
        {
            return oCommonDAL.InsertUpdateProfile(_objProperty, out sStatus);
        }
        #endregion

        #region(ADMIN SEARCH PRODUCT)
        public DataTable SearchProductDetail(PropertyModels _objProperty)
        {
            return oCommonDAL.SearchProductDetail(_objProperty);
        }
        #endregion

        #region(INSERT ORDER DETAIL)
        public int InsertOrderDetail(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertOrderDetail(_objProperty);
        }
        #endregion

        #region(ADD AMOUNT IN WALLET)
        public int AddAmountInWallet(PropertyModels _objProperty)
        {
            return oCommonDAL.AddAmountInWallet(_objProperty);
        }
        #endregion

        #region(INSERT CANCEL ORDER THROUGH LOGIN)
        public int InsertCancelOrder(PropertyModels _objProperty)
        {
            return oCommonDAL.InsertCancelOrder(_objProperty);
        }
        #endregion

        #region(INSERT ORDER HISTORY DETAIL)
        public int InsertOrderHistroy(PropertyModels _objProperty, out string status)
        {
            return oCommonDAL.InsertOrderHistroy(_objProperty, out status);
        }
        #endregion

        #region(CHECK USER NAME AND PASSWORD FOR LOGIN PAGE)
        public int CheckLogin(PropertyModels _objProperty, out string status)
        {
            return oCommonDAL.CheckLogin(_objProperty, out status);
        }
        #endregion
    }
}