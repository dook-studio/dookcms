/* ***********************************************
 * Author		:  钟健
 * Date		:   2012年11月23日17:34:23
 * Description	:  仿百度google分页条显示规则
 *
 * ***********************************************/

using System;
using System.Text;
namespace Common
{
    /// <summary>
    /// 分页条
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// 分页条
        /// </summary>
        /// <param name="itemstr">页码html</param>
        /// <param name="cur_itemstr">当前页面html</param>
        /// <param name="pageindex">当前页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="total">总数</param>
        /// <param name="item_count">最大允许的页码个数</param>
        /// <param name="showtype">方式有:first pre pageno next last五种显示.默认都存在.</param>
        /// <returns>整个分页的html</returns>
        public static string Page(string itemstr, string cur_itemstr, int pageindex, int pageSize, int total, int item_count,string showtype,string[] showtypehtml)
        {
            showtype = string.IsNullOrEmpty(showtype) ? "first,pre,pageno,next,last" : showtype;
            if (showtypehtml == null || showtypehtml.Length == 0)
            {
                showtypehtml = new string[] { itemstr, itemstr, itemstr, itemstr };
            }
            StringBuilder str = new StringBuilder();
            if (string.IsNullOrEmpty(itemstr))
            {
                itemstr = "<a href=\"list_~index~\">~itemname~</a>";//每一项的html代码
            }
            if (string.IsNullOrEmpty(cur_itemstr))
            {
                cur_itemstr = "<span>~itemname~</span>";//选中的项html代码
            }
            int startpage = 1, endpage = 1;
            int pageCount = pageSize > 0 ? (int)Math.Ceiling((double)total / pageSize) : total;
            int startIndex = (pageindex - 1) * pageSize + 1;//起始项索引
            int endIndex = pageindex * pageSize;
            endIndex = endIndex > total ? total : endIndex;//结束项索引
            if (startIndex > endIndex) startIndex = endIndex;

            if (pageindex == 1)
            {

            }
            else if (pageindex > 1)
            {
                string a = showtypehtml[0].Replace("~index~", "1".ToString()).Replace("~itemname~", "首页");
                string b = showtypehtml[1].Replace("~index~", (pageindex - 1).ToString()).Replace("~itemname~", "上一页");
                if (showtype.Contains("first"))
                {
                    str.Append(a);
                }
                if (showtype.Contains("pre"))
                {
                    str.Append(b);
                }
            }

            //构造第一页
            //构造上一页

            //构造中间的数字项
            /** 总页数数是否小于4 */
            StringBuilder items = new StringBuilder();
            if (showtype.Contains("pageno"))
            {
                if (pageCount < (item_count / 2 + 1))
                {
                    startpage = 1; // 页面中起始页就是1  
                    endpage = pageCount;// 页面中的最终页就是总页数               
                }
                else
                {
                    /** else中是总页数大于4的情况 */
                    /** 首先当前页的值是否小于等于4 */
                    if (pageindex <= (item_count / 2 + 1))
                    {
                        startpage = 1;
                        endpage = pageindex + 2;
                        /** 判断页面的最终页是否大于总页数 */
                        if (endpage >= pageCount)
                        {
                            endpage = pageCount;
                        }
                    }
                    else
                    {
                        startpage = pageindex - 3;
                        endpage = pageindex + 2;

                        if (endpage >= pageCount)
                        {
                            endpage = pageCount;
                            if (pageCount < item_count)
                            {
                                startpage = 1;
                            }
                            else
                            {
                                startpage = endpage - 5;
                            }
                        }
                    }
                }
                for (int i = startpage; i <= endpage; i++)
                {
                    if (pageindex == i)
                    {
                        string a = cur_itemstr.Replace("~index~", i.ToString()).Replace("~itemname~", i.ToString());
                        items.Append(a);

                    }
                    else
                    {
                        string a = itemstr.Replace("~index~", i.ToString()).Replace("~itemname~", i.ToString());

                        items.Append(a);
                    }
                }
           
            str.Append(items);
            }
            if (pageindex == pageCount)
            {
                //str.Append(string.Format(cur_itemstr, "下一页"));
                //str.Append(string.Format(cur_itemstr, "末页"));
            }
            else if (pageindex < pageCount && pageCount > 1)
            {               
                string a = showtypehtml[2].Replace("~index~", (pageindex + 1).ToString()).Replace("~itemname~", "下一页");
                string b = showtypehtml[3].Replace("~index~", pageCount.ToString()).Replace("~itemname~", "末页");
                if (showtype.Contains("next"))
                {
                    str.Append(a);
                }
                if (showtype.Contains("last"))
                {
                    str.Append(b);
                }
            }
            return str.ToString();
        }
    }
}
