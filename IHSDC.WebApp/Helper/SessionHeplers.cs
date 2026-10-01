using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace IHSDC.WebApp.Helper
{
    public static class SessionHeplers
    {
        public static T GetObject<T>(
            HttpSessionState session,
            string key)
        {
            if (session == null || session[key] == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(
                session[key].ToString());
        }

        public static T GetObject<T>(
            HttpSessionStateBase session,
            string key)
        {
            if (session == null || session[key] == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(
                session[key].ToString());
        }

        public static void SetObject<T>(
            HttpSessionState session,
            string key,
            T value)
        {
            session[key] = JsonConvert.SerializeObject(value);
        }

        public static void SetObject<T>(
            HttpSessionStateBase session,
            string key,
            T value)
        {
            session[key] = JsonConvert.SerializeObject(value);
        }
    }
}