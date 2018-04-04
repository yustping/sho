﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.API.OAuth;
using MxWeiXinPF.Common;
using LitJson;

namespace MxWeiXinPF.Web.api.oauth.kaixin
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
                return;
            }
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();
            JsonData jd = kaixin_helper.get_info(oauth_access_token, "uid,name,logo120,gender,birthday");
            if (jd == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + jd["uid"].ToString() + "\", ");
            str.Append("\"nick\": \"" + jd["name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + jd["logo120"].ToString() + "\", ");
            if (jd["gender"].ToString() == "0")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (jd["gender"].ToString() == "1")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"" + jd["birthday"].ToString() + "\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
    }
}