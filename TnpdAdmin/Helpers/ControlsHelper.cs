using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tnpd.Extentions;

namespace Tnpd.Helpers
{
    /// <summary>
    /// 一般控制項 HtmlHelper
    /// </summary>
    public static class ControlsHelper
    {
        
        public static IHtmlString InputSearchBox(this HtmlHelper helper, string name, object value, object htmlAttributes = null)
        {
            return new HtmlString(string.Format("<input id=\"{0}\" name=\"{0}\" type=\"search\" value=\"{1}\" {2}/>", name, value !=null ? value.ToString():"", htmlAttributes.ToAttributeList()));            
        }
        


        /// <summary>
        /// CheckBox(值為 true or false 二選一)
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="id">checkbox id</param>
        /// <param name="isChecked"></param>
        /// <param name="text">label text</param>
        /// <param name="htmlAttributes">checkbox attr</param>
        public static IHtmlString CheckBoxBit(this HtmlHelper helper, string id, string text, bool isChecked, object htmlAttributes = null)
        {
            string format = "<input id=\"{0}\" name=\"{0}\" value=\"true\" {1} type=\"checkbox\" {3}/>{2}";

            var setHash = htmlAttributes.ToAttributeList();

            string attributeList = string.Empty;
            if (setHash != null)
                attributeList = setHash;

            if (attributeList.IndexOf("disabled=\"disabled\"") == -1)
            {
                format += "<input name=\"{0}\" value=\"false\" type=\"hidden\" />";
            }
            else if (isChecked)
            {
                format += "<input name=\"{0}\" value=\"true\" type=\"hidden\" />";
            }

            return new HtmlString(string.Format(format,
                id,
                isChecked ? "checked=\"checked\"" : "",
                string.IsNullOrEmpty(text) ? "" : string.Format("<label id=\"{0}\" for=\"{1}\">{2}</label>", id + "_label", id, text),
                attributeList));
        }

        /// <summary>
        /// 擴展 checkbox 
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="id">checkbox id</param>
        /// <param name="isChecked"></param>
        /// <param name="value">checkbox value</param>
        /// <param name="text">label text</param>
        /// <param name="htmlAttributes">checkbox attr</param>
        public static IHtmlString CheckBoxLabel(this HtmlHelper helper, string id, string value, string text, bool isChecked, object htmlAttributes = null)
        {
            var setHash = htmlAttributes.ToAttributeList();

            string attributeList = string.Empty;
            if (setHash != null)
                attributeList = setHash;

            return new HtmlString(string.Format("<input id=\"{0}\" name=\"{1}\" value=\"{2}\" {3} type=\"checkbox\" {5}/>{4}",
                id,
                id,
                value,
                isChecked ? "checked=\"checked\"" : "",
                string.IsNullOrEmpty(text) ? "" : string.Format("<label id=\"{0}\" for=\"{1}\">{2}</label>", id + "_label", id, text),
                attributeList));
        }

    }
}
