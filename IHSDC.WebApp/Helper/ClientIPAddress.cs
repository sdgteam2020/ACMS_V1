using System.Web;


namespace IHSDC.WebApp.Helper
{
    public static class ClientIPAddress
    {
        public static string GetIPAddress()
        {
            var context = HttpContext.Current;

            if (context == null)
                return "Unknown";

            string ip = context.Request.Headers["X-Forwarded-For"];

            if (string.IsNullOrWhiteSpace(ip))
            {

                ip = context.Request.UserHostAddress;
            }
            else
            {
                ip = ip.Split(',')[0].Trim();
            }

            return ip;
        }
    }
}