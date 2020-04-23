using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using TnpdModels;


namespace TnpdModels
{
    public class Statistics
    {
        int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        List<StatisticsItem> _item;
        public List<StatisticsItem> item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }
    }


    public class StatisticsItem
    {
        string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }


        int _count;
        public int count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }

        double _percent;
        public double percent 
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
            }
        }

    }
}