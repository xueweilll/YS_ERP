﻿using System;
using System.Collections.Generic;
using System.Text;
using CZZD.ERP.IDAL;
using CZZD.ERP.DBUtility;
using System.Data.SqlClient;
using CZZD.ERP.Common;
using System.Data;

namespace CZZD.ERP.SQLServerDAL
{
    public class CustomerReportedManage:ICustomerReported
    {
        public CustomerReportedManage()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CUSTOMER_CODE, string CUSTOMER_REPORTED_CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from BASE_CUSTOMER_REPORTED");
            strSql.Append(" where CUSTOMER_CODE=@CUSTOMER_CODE and CUSTOMER_REPORTED_CODE=@CUSTOMER_REPORTED_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CUSTOMER_CODE;
            parameters[1].Value = CUSTOMER_REPORTED_CODE;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(CZZD.ERP.Model.BaseCustomerReportedTable model)
        {
            StringBuilder strSql = null;
            int rows = 0;
            if (Exists(model.CUSTOMER_CODE, model.CUSTOMER_REPORTED_CODE))
            {
                #region 更新
                strSql = new StringBuilder();
                strSql.Append("update BASE_CUSTOMER_REPORTED set ");
                strSql.Append("REPORTED_DATE=@REPORTED_DATE,");
                strSql.Append("EFFECTIVE_DATE=@EFFECTIVE_DATE,");
                strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
                strSql.Append("CREATE_USER=@CREATE_USER,");
                strSql.Append("CREATE_DATE_TIME=GETDATE(),");
                strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
                strSql.Append("LAST_UPDATE_TIME=GETDATE()");
                strSql.Append(" where CUSTOMER_CODE=@CUSTOMER_CODE and CUSTOMER_REPORTED_CODE=@CUSTOMER_REPORTED_CODE ");
                SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@REPORTED_DATE", SqlDbType.DateTime),
                    new SqlParameter("@EFFECTIVE_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
                    new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
                parameters[0].Value = model.CUSTOMER_CODE;
                parameters[1].Value = model.CUSTOMER_REPORTED_CODE;
                parameters[2].Value = model.REPORTED_DATE;
                parameters[3].Value = model.EFFECTIVE_DATE;
                parameters[4].Value = model.STATUS_FLAG;
                parameters[5].Value = model.CREATE_USER;
                parameters[6].Value = model.LAST_UPDATE_USER;

                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                #endregion
            }
            else
            {
                strSql = new StringBuilder();
                strSql.Append("insert into BASE_CUSTOMER_REPORTED(");
                strSql.Append("CUSTOMER_CODE,CUSTOMER_REPORTED_CODE,REPORTED_DATE,STATUS_FLAG,CREATE_USER,CREATE_DATE,LAST_UPDATE_USER,LAST_UPDATE_TIME,EFFECTIVE_DATE)");
                strSql.Append(" values (");
                strSql.Append("@CUSTOMER_CODE,@CUSTOMER_REPORTED_CODE,@REPORTED_DATE,@STATUS_FLAG,@CREATE_USER,GETDATE(),@LAST_UPDATE_USER,GETDATE(),@EFFECTIVE_DATE)");
                SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,20),
				    new SqlParameter("@REPORTED_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@CREATE_USER", SqlDbType.VarChar,20),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20),
                    new SqlParameter("@EFFECTIVE_DATE", SqlDbType.DateTime) };
                parameters[0].Value = model.CUSTOMER_CODE;
                parameters[1].Value = model.CUSTOMER_REPORTED_CODE;
                parameters[2].Value = model.REPORTED_DATE;
                parameters[3].Value = model.STATUS_FLAG;
                parameters[4].Value = model.CREATE_USER;
                parameters[5].Value = model.LAST_UPDATE_USER;
                parameters[6].Value = model.EFFECTIVE_DATE;
                rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(CZZD.ERP.Model.BaseCustomerReportedTable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BASE_CUSTOMER_REPORTED set ");
            strSql.Append("REPORTED_DATE=@REPORTED_DATE,");
            strSql.Append("EFFECTIVE_DATE=@EFFECTIVE_DATE,");
            strSql.Append("STATUS_FLAG=@STATUS_FLAG,");
            strSql.Append("LAST_UPDATE_USER=@LAST_UPDATE_USER,");
            strSql.Append("LAST_UPDATE_TIME=GETDATE()");
            strSql.Append(" where CUSTOMER_CODE=@CUSTOMER_CODE and CUSTOMER_REPORTED_CODE=@CUSTOMER_REPORTED_CODE ");
            SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,20),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,20),
                    new SqlParameter("@REPORTED_DATE", SqlDbType.DateTime),
                    new SqlParameter("@EFFECTIVE_DATE", SqlDbType.DateTime),
					new SqlParameter("@STATUS_FLAG", SqlDbType.Int,4),
					new SqlParameter("@LAST_UPDATE_USER", SqlDbType.VarChar,20)};
            parameters[0].Value = model.CUSTOMER_CODE;
            parameters[1].Value = model.CUSTOMER_REPORTED_CODE;
            parameters[2].Value = model.REPORTED_DATE;
            parameters[3].Value = model.EFFECTIVE_DATE;
            parameters[4].Value = model.STATUS_FLAG;
            parameters[5].Value = model.LAST_UPDATE_USER;         

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CUSTOMER_CODE, string CUSTOMER_REPORTED_CODE)
        {
            StringBuilder strSql = new StringBuilder();           
            strSql.AppendFormat("update BASE_CUSTOMER_REPORTED  set STATUS_FLAG = {0}", CConstant.DELETE);
            strSql.Append(" where CUSTOMER_CODE=@CUSTOMER_CODE and  CUSTOMER_REPORTED_CODE=@CUSTOMER_REPORTED_CODE");
            SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CUSTOMER_CODE;
            parameters[1].Value = CUSTOMER_REPORTED_CODE;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CZZD.ERP.Model.BaseCustomerReportedTable GetModel(string CUSTOMER_CODE, string CUSTOMER_REPORTED_CODE)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 CUSTOMER_CODE,CUSTOMER_REPORTED_CODE,REPORTED_DATE,STATUS_FLAG,CREATE_USER,CREATE_DATE,LAST_UPDATE_USER,LAST_UPDATE_TIME,EFFECTIVE_DATE from BASE_CUSTOMER_REPORTED ");
            strSql.Append(" where CUSTOMER_CODE=@CUSTOMER_CODE and CUSTOMER_REPORTED_CODE=@CUSTOMER_REPORTED_CODE ");
            strSql.AppendFormat(" and STATUS_FLAG <> {0}", CConstant.DELETE);
            SqlParameter[] parameters = {
					new SqlParameter("@CUSTOMER_CODE", SqlDbType.VarChar,50),
					new SqlParameter("@CUSTOMER_REPORTED_CODE", SqlDbType.VarChar,50)};
            parameters[0].Value = CUSTOMER_CODE;
            parameters[1].Value = CUSTOMER_REPORTED_CODE;

            CZZD.ERP.Model.BaseCustomerReportedTable model = new CZZD.ERP.Model.BaseCustomerReportedTable();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.CUSTOMER_CODE = ds.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
                model.CUSTOMER_REPORTED_CODE = ds.Tables[0].Rows[0]["CUSTOMER_REPORTED_CODE"].ToString();
                if (ds.Tables[0].Rows[0]["REPORTED_DATE"].ToString() != "")
                {
                    model.REPORTED_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["REPORTED_DATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString() != "")
                {
                    model.STATUS_FLAG = int.Parse(ds.Tables[0].Rows[0]["STATUS_FLAG"].ToString());
                }
                model.CREATE_USER = ds.Tables[0].Rows[0]["CREATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["CREATE_DATE"].ToString() != "")
                {
                    model.CREATE_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString());
                }
                model.LAST_UPDATE_USER = ds.Tables[0].Rows[0]["LAST_UPDATE_USER"].ToString();
                if (ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString() != "")
                {
                    model.LAST_UPDATE_TIME = DateTime.Parse(ds.Tables[0].Rows[0]["LAST_UPDATE_TIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString() != "")
                {
                    model.EFFECTIVE_DATE = DateTime.Parse(ds.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CUSTOMER_CODE,CUSTOMER_NAME,CUSTOMER_REPORTED_CODE,CUSTOMER_REPORTED_NAME,REPORTED_DATE,STATUS_FLAG,CREATE_NAME,CREATE_DATE,LAST_UPDATE_NAME,LAST_UPDATE_TIME,EFFECTIVE_DATE ");
            strSql.Append(" FROM base_customer_reported_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得分页数据总的记录条数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from base_customer_reported_view");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 获得分页数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CUSTOMER_CODE asc");
            }
            strSql.Append(")AS Row, T.* from base_customer_reported_view T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
       

        #endregion  Method
    }
}
