using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Falquan.KeyRinger.Web.Transforms
{
    public class UrlReplaceTransform : IBundleTransform
    {
        private readonly string _key;
        private readonly string _serviceUrlFormat;
        
        public UrlReplaceTransform(string key, string serviceUrlFormat)
        {
            _key = key;
            _serviceUrlFormat = serviceUrlFormat;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            response.Content = response.Content.Replace(_key,
                                                        string.Format(_serviceUrlFormat,
                                                                      context.HttpContext.Request.Url.Scheme));
        }
    }
}