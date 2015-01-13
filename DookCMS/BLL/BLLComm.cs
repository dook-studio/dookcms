using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BLLComm
    {
        //public static readonly BLLComm instance = new BLLComm();//str = string.Format("<strong style='color:#777;font-weight:normal'>{0}</strong>", model.cname) + str;
        private string str = "";
        private List<Dukey.Model.Channel> list = StringHelper.DataTableToList<Dukey.Model.Channel>(DataBaseHelper.instance.GetListByCache("channel","channel_cache").Tables[0]);
        public string GetNavLink(int id, int curid, string patternidstr)
        {
            if (list != null)
            {
                Dukey.Model.Channel model = list.Find(item => (item.bid == id && item.isshow==true));
                if (model != null)
                {
                    if (id == curid)
                    {
                        if (model.fid == 0)
                        {
                            //str = string.Format("<strong style='color:#777;font-weight:normal'>{0}</strong>", model.cname) + str;
                           // str = string.Format("<strong style='color:#777;font-weight:normal'>{0}</strong>", model.cname) + str;
                        }
                        else
                        {
                            str = string.Format("<strong style='color:#777;font-weight:normal'>{0}</strong>", model.cname) + str;
                            return GetNavLink(model.fid, curid, patternidstr);
                        }
                    }
                    else
                    {
                        if (model.fid == 0)
                        {
                            str = string.Format("<a  href='/{2}.ashx?bid={0}'>{1}</a> &rsaquo; ", model.bid, model.cname, patternidstr) + str;
                            //str = string.Format("<strong style='color:#777;font-weight:normal'>{0}</strong>", model.cname) + str;                       
                        }
                        else
                        {
                            str = string.Format("<a href='/{2}.ashx?bid={0}'>{1}</a> &rsaquo; ", model.bid, model.cname, patternidstr) + str;
                            return GetNavLink(model.fid, curid, patternidstr);
                        }
                    }
                    return str.ToString();
                }
            }
            return "";
        }

        public string GetNavLinkDetail(int id, int curid, string patternidstr)
        {
            if (list != null)
            {
                Dukey.Model.Channel model = list.Find(item => (item.bid == id && item.isshow==true));
                if (model != null)
                {
                    if (model.fid == 0)
                    {
                        str = string.Format("<a  href='/{2}.ashx?bid={0}'>{1}</a> &rsaquo; ", model.bid, model.cname, patternidstr) + str;
                    }
                    else
                    {
                        str = string.Format("<a href='/{2}.ashx?bid={0}'>{1}</a> &rsaquo; ", model.bid, model.cname, patternidstr) + str;
                        return GetNavLink(model.fid, curid, patternidstr);
                    }
                    return str.ToString();
                }
            }
            return "";
        }
    }
}
