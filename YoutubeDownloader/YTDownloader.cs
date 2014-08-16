using System;
using System.IO;
using System.Net;
using System.Web;

namespace YoutubeDownloader
{
    class YTDownloader
    {
        public string ParseResponse(string a, string b, string c)
        {
            try
            {
                var a1 = a.Split(new string[] { b }, StringSplitOptions.RemoveEmptyEntries);
                var a2 = a1[1].Split(new string[] { c }, StringSplitOptions.RemoveEmptyEntries);
                return a2[0];
            }
            catch (Exception)
            {
                return " ";
            }
        }

        public string ParseResponse2(string a, string b, string c)
        {
            try
            {
                var a1 = a.Split(new string[] { b }, StringSplitOptions.RemoveEmptyEntries);
                var a2 = a1[2].Split(new string[] { c }, StringSplitOptions.RemoveEmptyEntries);
                return a2[0];
            }
            catch (Exception)
            {
                return " ";
            }
        }

    }
}