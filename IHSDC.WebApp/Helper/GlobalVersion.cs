using System;

namespace IHSDC.WebApp.Helper
{
    public static class GlobalVersion
    {
        public static long JSVersion
        {
            get
            {
                return DateTime.UtcNow.Ticks;
            }
        }
    }
}