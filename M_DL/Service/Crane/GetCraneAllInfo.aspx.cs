//
//文件名：    GetCraneAllInfo.aspx.cs
//功能描述：  获取门吊所有数据
//创建时间：  2018/4/19
//作者：      
//修改时间：  
//修改描述：  暂无
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Leo;
using ServiceInterface.Common;

namespace M_YKT_Ysfw.Service.Crane
{
    public partial class GetCraneAllInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strSql =
                    string.Format(@"select id,userkey,data,datestamp,createtime 
                                    from TB_DL_CRANE 
                                    order by createtime desc");
                var dt = new Leo.Oracle.DataAccess(RegistryKey.KeyPathHarbor).ExecuteTable(strSql);
                if (dt.Rows.Count <= 0)
                {
                    Json = JsonConvert.SerializeObject(new DicPackage(true, null, "暂无数据").DicInfo());
                    return;
                }

                string[,] strArray = new string[dt.Rows.Count, 5];
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    strArray[iRow, 0] = Convert.ToString(dt.Rows[iRow]["id"]);
                    strArray[iRow, 1] = Convert.ToString(dt.Rows[iRow]["userkey"]);
                    strArray[iRow, 2] = Convert.ToString(dt.Rows[iRow]["data"]);
                    strArray[iRow, 3] = Convert.ToString(dt.Rows[iRow]["datestamp"]);
                    strArray[iRow, 4] = Convert.ToString(dt.Rows[iRow]["createtime"]);
                }

                Json = JsonConvert.SerializeObject(new DicPackage(true, strArray, null).DicInfo());
            }
            catch (Exception ex)
            {
                Json = JsonConvert.SerializeObject(new DicPackage(false, null, string.Format("{0}：获取门吊所有数据数据发生异常。{1}", ex.Source, ex.Message)).DicInfo());
            }
        }

        protected string Json;
    }
}