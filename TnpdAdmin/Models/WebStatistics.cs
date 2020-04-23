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
using Tnpd.Models;


namespace Tnpd.Models
{
    public class WebStatistics
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

        string _account;
        public string account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        string _dep;
        public string dep
        {
            get
            {
                return _dep;
            }
            set
            {
                _dep = value;
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


        int _count1;
        public int count1
        {
            get
            {
                return _count1;
            }
            set
            {
                _count1= value;
            }
        }

        int _count2;
        public int count2
        {
            get
            {
                return _count2;
            }
            set
            {
                _count2 = value;
            }
        }

        int _count3;
        public int count3
        {
            get
            {
                return _count3;
            }
            set
            {
                _count3 = value;
            }
        }

        int _count4;
        public int count4
        {
            get
            {
                return _count4;
            }
            set
            {
                _count4 = value;
            }
        }

        int _sum;
        public int sum
        {
            get
            {
                return _sum;
            }
            set
            {
                _sum = value;
            }
        }
    }


}