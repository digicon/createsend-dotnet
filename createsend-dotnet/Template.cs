﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace createsend_dotnet
{
    public class Template
    {
        private string _templateID;

        public Template(string templateID)
        {
            _templateID = templateID;
        }

        public static string Create(string clientID, string name, string htmlPageUrl, bool zipUrl, string screenshotUrl)
        {
            string json = HttpHelper.Post(string.Format("/templates/{0}.json", clientID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Name", name }, { "HtmlPageURL", htmlPageUrl }, { "ZipFileUrl", zipUrl }, { "ScreenshotUrl", screenshotUrl } })
                );
            return JavaScriptConvert.DeserializeObject<string>(json);
        }

        public void Update(string name, string htmlPageUrl, bool zipUrl, string screenshotUrl)
        {
            HttpHelper.Put(string.Format("/templates/{0}.json", _templateID), null, JavaScriptConvert.SerializeObject(
                new Dictionary<string, object>() { { "Name", name }, { "HtmlPageURL", htmlPageUrl }, { "ZipFileUrl", zipUrl }, { "ScreenshotUrl", screenshotUrl } })
                );
        }

        public BasicTemplate Details()
        {
            string json = HttpHelper.Get(string.Format("/templates/{0}.json", _templateID), null);
            return JavaScriptConvert.DeserializeObject<BasicTemplate>(json);
        }

        public void Delete()
        {
            HttpHelper.Delete(string.Format("/templates/{0}.json", _templateID), null);
        }
    }
}
