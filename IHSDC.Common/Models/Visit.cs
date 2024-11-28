using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDC.Common.Models
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string MachineName { get; set; }
        public _Browser Browser { get; set; }
        public string SessionId { get; set; }
    }

    public class _Browser
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
    }

    public class Login
    {
        public Guid Id { get; set; }
        public int UserIntId { get; set; }
        public string Username { get; set; }
        public DateTime LoggedInAt { get; set; }
        public string SessionId { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime LoggedOutAt { get; set; }
    }
}
