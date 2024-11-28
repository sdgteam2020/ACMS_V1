using System.Net;

namespace IHSDC.WebApp.RepositryManager
{
    public class GetIpAddress
    {

        public string getAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  

            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
    }
}