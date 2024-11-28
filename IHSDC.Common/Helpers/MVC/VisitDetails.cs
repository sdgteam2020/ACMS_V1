using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace IHSDC.Common.Helpers.MVC
{
    public static class VisitDetails
    {
        public static string GetIPAddress(bool OnlyIP = false)
        {
            string IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IP))
            {
                IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                return IP;
            }
            else
            {
                string RemoteIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                return string.IsNullOrEmpty(RemoteIP) && OnlyIP ? IP : string.Format("{0} ({1})", IP, RemoteIP);
            }            
        }

        public static string GetMACAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                String sMacAddress = string.Empty;
                foreach (NetworkInterface adapter in nics)
                {
                    if (sMacAddress == String.Empty)// will only return MAC Address from first card  
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        sMacAddress = adapter.GetPhysicalAddress().ToString();
                    }
                }
                return sMacAddress;
            }
            catch (Exception)
            {
                return "MAC NA";    //Custom string when MAC is Not Available
            }            
        }

        public static string GetMachineName()
        {
            //IPAddress myIP = IPAddress.Parse(GetIPAddress(true));
            //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            //List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();

            return "";
        }

        public static _Browser GetBrowser()
        {
            _Browser rBrowser = new _Browser()
            {
                Name = HttpContext.Current.Request.Browser.Browser,
                Version = HttpContext.Current.Request.Browser.Version,
                Platform = HttpContext.Current.Request.Browser.Platform
            };
            return rBrowser;
        }
    }
}
