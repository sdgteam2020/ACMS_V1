using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Models
{
    public class AddAgendaRequest
    {
        public int Id { get; set; }

        public string View { get; set; }

        public string Edit { get; set; }
    }
}