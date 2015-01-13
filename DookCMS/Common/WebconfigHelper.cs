using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web.Configuration;

namespace Common
{
    public static class WebconfigHelper
    {
        public static bool IsExistKey(string key)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/xml/web.config");

            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");

            appSection.Settings.Add("Test", "Hello");
            config.Save(ConfigurationSaveMode.Modified);

            config.AppSettings.Settings.Remove(key);


            return true;
        }

        public static void SetValue(string key, string value)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/xml/");
            
            //ConnectionStringSettings test = new ConnectionStringSettings("test1", "test1", "test1.dll");

            //config.ConnectionStrings.ConnectionStrings.Add(test);
            
            //<add src="~/WebPart/ProductCate.ascx" tagName="cate" tagPrefix="uc" />
            TagPrefixInfo dd=new TagPrefixInfo("uc","","","cate","~/WebPart/ProductCate.ascx");
            //config.SectionGroups["system.web"]
            SystemWebSectionGroup t = new SystemWebSectionGroup();
            t.Pages.Controls.Add(dd);
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
