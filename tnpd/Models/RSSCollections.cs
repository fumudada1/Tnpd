using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Collections;
using System.Xml;

namespace tnpd.Models
{
    /// <summary>
    /// RSSCollections 的摘要描述
    /// </summary>
    public class RSSCollections : ICollection
    {
        internal RSSItem[] _array;
        internal int _size;
        private int _capacity;

        private const int _MinimumGrow = 4;

        public RSSCollections()
        {
            _array = new RSSItem[16];
            _capacity = 0;
            _size = 0;
        }

        #region ICollection 成員

        public virtual void CopyTo(Array array, int index)
        {
            throw new Exception("這個實作不支援這個方法");
        }

        public virtual int Count
        {
            get { return _size; }
        }

        public virtual Object SyncRoot
        {
            get { return this; }
        }

        public virtual bool IsSynchronized
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable 成員

        public virtual IEnumerator GetEnumerator()
        {
            return new RssItemEnumerator(this);
        }

        public bool Add(RSSItem objRSSitem)
        {


            if (_size == _array.Length)
            {
                int newcapacity = _array.Length * 2;

                if (newcapacity < _array.Length + _MinimumGrow)
                {
                    newcapacity = _array.Length + _MinimumGrow;
                }

                SetCapacity(newcapacity);
            }

            _array[_capacity] = objRSSitem;
            _capacity = (_capacity + 1) % _array.Length;
            _size = _size + 1;

            return false;
        }

        private void SetCapacity(int capacity)
        {
            RSSItem[] newArray = new RSSItem[capacity + 1];

            if (_size > 0)
            {
                if (_capacity > 0)
                {
                    Array.Copy(_array, 0, newArray, 0, _size);
                }
                else
                {
                    Array.Copy(_array, 0, newArray, 0, _array.Length);
                    Array.Copy(_array, 0, newArray, _array.Length, _capacity);
                }
            }

            _array = newArray;
            _capacity = _size;
        }

        public void Clear()
        {
            if (_capacity > 0)
                Array.Clear(_array, 0, _size);
            else
            {
                Array.Clear(_array, 0, _array.Length);
                Array.Clear(_array, 0, _capacity);
            }

            _capacity = 0;
            _size = 0;
        }


        internal Object GetElement(int i)
        {
            return _array[i % _array.Length];
        }




        #endregion
    }
    class RssItemEnumerator : IEnumerator
    {

        private RSSCollections _ec;
        private int _index;
        private Object currentElement;

        internal RssItemEnumerator(RSSCollections ec)
        {
            _ec = ec;

            _index = 0;
            currentElement = _ec._array;

            if (_ec._size == 0)
                _index = -1;
        }

        public virtual bool MoveNext()
        {

            if (_index < 0)
            {
                currentElement = _ec._array;

                return false;
            }
            currentElement = _ec.GetElement(_index);
            _index++;

            if (_index == _ec._size)
                _index = -1;

            return true;
        }

        public virtual void Reset()
        {

            if (_ec._size == 0)
                _index = -1;
            else
                _index = 0;

            currentElement = _ec._array;
        }

        public virtual Object Current
        {
            get
            {
                if (currentElement == _ec._array)
                {
                    if (_index == 0)
                        throw new InvalidOperationException("Invalid Operation");
                    else
                        throw new InvalidOperationException("Invalid operation");
                }

                return currentElement;
            }
        }
    }

    /// <summary>
    /// RSS 元素
    /// </summary>
    public class RSSItem
    {
        private string _Title;

        public string title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        private string _Link;

        public string link
        {
            get
            {
                return _Link;
            }
            set
            {
                _Link = value;
            }
        }
        private string _PubDate;

        public string pubDate
        {
            get
            {
                return _PubDate;
            }
            set
            {
                _PubDate = value;
            }
        }
        private string _Description;

        public string description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }
        private string _Guid;

        public string guid
        {
            get
            {
                return _Guid;
            }
            set
            {
                _Guid = value;
            }
        }

        private string _ActiveDate;

        public string activeDate
        {
            get
            {
                return _ActiveDate;
            }
            set
            {
                _ActiveDate = value;
            }
        }
        public RSSItem(string strTitle, string strLink, DateTime dtePubDate, string strGuid)
        {
            _Title = strTitle;
            _Link = strLink;
            _PubDate = dtePubDate.ToString("R");
            _Guid = strGuid;


        }
        public RSSItem(string strTitle, string strLink, DateTime dtePubDate, string strGuid, string strDescription)
        {
            _Title = strTitle;
            _Link = strLink;
            _PubDate = dtePubDate.ToString("R");
            _Guid = strGuid;
            _Description = strDescription;
        }

    }
    /// <summary>
    /// 輸出RSS
    /// </summary>
    public class RSSToPage
    {

        protected string _ChannelTitle;

        public string ChannelTitle
        {
            get
            {
                return _ChannelTitle;
            }
            set
            {
                _ChannelTitle = value;
            }
        }
        private string _ChannelLink;

        public string ChannelLink
        {
            get
            {
                return _ChannelLink;
            }
            set
            {
                _ChannelLink = value;
            }
        }
        private string _ChannelDescription;

        public string ChannelDescription
        {
            get
            {
                return _ChannelDescription;
            }
            set
            {
                _ChannelDescription = value;
            }
        }
        private string _ChannelLanguage = "zh-tw";
        private string _ChannelCopyright;

        public string ChannelCopyright
        {
            get
            {
                return _ChannelCopyright;
            }
            set
            {
                _ChannelCopyright = value;
            }
        }
        private const string _ChannelTTL = "20";
        public RSSCollections rssCollections = new RSSCollections();

        public RSSToPage(string title)
        {
            _ChannelTitle = title;
        }

        public void WriteToPage()
        {

            StringWriter sw = new StringWriter();
            XmlTextWriter objX = new XmlTextWriter(HttpContext.Current.Response.OutputStream, System.Text.Encoding.UTF8);
            //XmlWriterSettings xset = objX.Settings;
            //xset.Encoding=System.Text.Encoding.UTF8;

            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", _ChannelTitle);
            objX.WriteElementString("link", _ChannelLink);
            objX.WriteElementString("description", _ChannelDescription);
            objX.WriteElementString("language", _ChannelLanguage);
            objX.WriteElementString("copyright", _ChannelCopyright);
            objX.WriteElementString("ttl", _ChannelTTL);
            foreach (RSSItem item in rssCollections)
            {
                objX.WriteStartElement("item");


                if (!string.IsNullOrEmpty(item.link))
                {
                    objX.WriteElementString("link", item.link);
                }
                if (!string.IsNullOrEmpty(item.activeDate))
                {
                    objX.WriteElementString("ActiveDate", item.activeDate);
                }
                if (!string.IsNullOrEmpty(item.title))
                {
                    objX.WriteElementString("title", item.title);
                }
                if (!string.IsNullOrEmpty(item.pubDate))
                {
                    objX.WriteElementString("pubDate", item.pubDate);
                }
                if (!string.IsNullOrEmpty(item.guid))
                {
                    objX.WriteElementString("guid", item.guid);
                }

                if (!string.IsNullOrEmpty(item.description))
                {
                    objX.WriteStartElement("description");
                    objX.WriteCData(item.description);
                    objX.WriteEndElement();
                }
                objX.WriteEndElement();

            }
            objX.WriteEndElement();
            objX.WriteEndElement();
            objX.WriteEndDocument();
            objX.Flush();
            objX.Close();

            return;
        }

        public string GetRssContent()
        {

            StringWriter sw = new StringWriter();
            XmlTextWriter objX = new XmlTextWriter(sw);
            XmlWriterSettings xset = objX.Settings;

            objX.WriteStartDocument();
            objX.WriteStartElement("rss");
            objX.WriteAttributeString("version", "2.0");
            objX.WriteStartElement("channel");
            objX.WriteElementString("title", _ChannelTitle);
            objX.WriteElementString("link", _ChannelLink);
            objX.WriteElementString("description", _ChannelDescription);
            objX.WriteElementString("language", _ChannelLanguage);
            objX.WriteElementString("copyright", _ChannelCopyright);
            objX.WriteElementString("ttl", _ChannelTTL);
            foreach (RSSItem item in rssCollections)
            {
                objX.WriteStartElement("item");


                if (!string.IsNullOrEmpty(item.link))
                {
                    objX.WriteElementString("link", item.link);
                }
                if (!string.IsNullOrEmpty(item.activeDate))
                {
                    objX.WriteElementString("ActiveDate", item.activeDate);
                }
                if (!string.IsNullOrEmpty(item.title))
                {
                    objX.WriteElementString("title", item.title);
                }
                if (!string.IsNullOrEmpty(item.pubDate))
                {
                    objX.WriteElementString("pubDate", item.pubDate);
                }
                if (!string.IsNullOrEmpty(item.guid))
                {
                    objX.WriteElementString("guid", item.guid);
                }

                //if (!string.IsNullOrEmpty(item.description))
                //{
                    objX.WriteStartElement("description");
                    objX.WriteCData(item.description);
                    objX.WriteEndElement();
                //}
                objX.WriteEndElement();

            }
            objX.WriteEndElement();
            objX.WriteEndElement();
            objX.WriteEndDocument();
            objX.Flush();
            objX.Close();

            return sw.ToString().Replace("utf-16", "utf-8");

        }
    }
}
