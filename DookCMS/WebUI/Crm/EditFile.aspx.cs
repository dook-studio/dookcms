﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Crm_EditFile : Page
{
    public string path = "", fileExt = "html";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                path = Request.QueryString["path"];
                try
                {
                    fileExt = path.Split('.')[1];
                }
                catch { }
                if (fileExt == "htm") fileExt = "html";


                txtContents.Value = Common.FileHelper.ReadFile(Server.MapPath("~/template/" + path), "utf-8");
            }
        }    
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["path"]))        
        {
            try
            {
                string path = Request.QueryString["path"];
                Common.FileHelper.WriteFile(Server.MapPath("~/template/" + path), txtContents.Value.Trim(), "utf-8");
                lblResult.Text = "操作成功! " + DateTime.Now;
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
            }

        }
    }
}
