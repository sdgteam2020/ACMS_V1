using System;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Helper
{
    public static class CommonHelper
    {
        public static bool HasAgendaAccess(int inboxId)
        {
            var allowedIds = HttpContext.Current?
                .Session?["AllowedAgendaIds"] as int[];

            if (allowedIds == null)
            {
                return false;
            }

            return allowedIds.Contains(inboxId);
        }

        public static bool HasScheduleAccess(int ScheduleId)
        {
            var allowedIds = HttpContext.Current?
                .Session?["AllowScheduleId"] as int[];

            if (allowedIds == null)
            {
                return false;
            }

            return allowedIds.Contains(ScheduleId);
        }
    }
}