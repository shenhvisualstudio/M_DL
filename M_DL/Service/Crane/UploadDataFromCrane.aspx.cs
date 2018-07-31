//
//文件名：    UploadDataFromCrane.aspx.cs
//功能描述：  上传门吊数据
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
    public partial class UploadDataFromCrane : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //设备的唯一标识，可用设备编号或者手机号
            string strUserkey = Request.Params["userkey"];
            //数据
            string strData = Request.Params["data"];
            //时间格式数字（12位）或者时间戳
            string strDate = Request.Params["date"];

            try
            {
                if (strUserkey == null && strData == null && strDate == null)
                {
                    Json = JsonConvert.SerializeObject(new DicPackage(false, null, "参数错误，不能全都为空").DicInfo());
                    return;
                }

                strUserkey = strUserkey == null ? "0" : strUserkey;
                strData = strData == null ? "0" : strData;
                strDate = strDate == null ? "0" : strDate;

                string strSql =
                    string.Format(@"insert into TB_DL_CRANE (USERKEY,DATA,DATESTAMP)
                                    values('{0}','{1}','{2}')",
                                    strUserkey, strData, strDate);
                new Leo.Oracle.DataAccess(RegistryKey.KeyPathHarbor).ExecuteNonQuery(strSql);

                Json = JsonConvert.SerializeObject(new DicPackage(true, null, "上传成功").DicInfo());
            }
            catch (Exception ex)
            {
                Json = JsonConvert.SerializeObject(new DicPackage(false, null, string.Format("{0}：上传门吊数据发生异常。{1}", ex.Source, ex.Message)).DicInfo());
            }
        }
        protected string Json;
    }
}