using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Models
{
    public class UserKeyMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PublicKey { get; set; }
    }
}