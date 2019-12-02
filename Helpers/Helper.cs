using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace MyWebsite.Helpers
{
    public static class Helper
    {
        public static void cid(object o,HttpRequestBase r)
        {
            var ip = (r.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                r.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            o.GetType().GetField("ModIp").SetValue(o, ip);
            o.GetType().GetField("CreateIp").SetValue(o, ip);
            o.GetType().GetField("CreateDate").SetValue(o, DateTime.Now);
            o.GetType().GetField("ModDate").SetValue(o, DateTime.Now);
        }
        public static void mid(object o, HttpRequestBase r)
        {
            var ip = (r.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                r.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();
            o.GetType().GetField("ModIp").SetValue(o, ip);
            o.GetType().GetField("ModDate").SetValue(o, DateTime.Now);

        }
        public static String newsletter_template(string header, string text,string action,string action_text,bool action_visible,string text2)
        {
            String line = "";
            using (StreamReader sr = new StreamReader("newsletter_template.ntmp"))
            {
                line = sr.ReadToEnd();
                line.Replace("<HeadNewsletter />", header);
                line.Replace("<TextNewsletter />", text);
                line.Replace("<TextNewsletter2 />", text2);
                if (action_visible)
                {
                    line.Replace("<ActionVisibleNewsletter />", "");
                    line.Replace("<ActionNewsletter />", action);
                    line.Replace("<ActionTextNewsletter />", action_text);
                }
                else
                {
                    line.Replace("<ActionVisibleNewsletter />", "invisible");
                }
            }
            if (line == "")
            {
                line = text;
            }
            return line;
        }
    }
}