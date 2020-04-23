using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tnpd.Models
{
    /// <summary>
    /// NodeLink 的摘要描述
    /// </summary>
    public class NodeLink
    {
        public string title { get; set; }

        public string url { get; set; }

        public string Tooltip { get; set; }

        public string Target { get; set; }

        public NodeLink()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }
    }
}