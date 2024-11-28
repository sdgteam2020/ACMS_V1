using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Models
{
    public class Dashboard
    {

    }
    public class DashAvi
    {
        public int Aviator_ID { get; set; }
        public string ArmyNumber { get; set; }
        public string RankName { get; set; }
        public string AviatorName { get; set; }
        public string UnitName { get; set; }
        public int HeptrRPA { get; set; }
        public int AviatorContactDetails { get; set; }

        public int AviatorCourses { get; set; }

        public int AviatorHonourAwards { get; set; }
        public int AviatorFlyingHrs { get; set; }
        public int AviatorSpecialEqpt { get; set; }
        public int AviatorForeignVisit { get; set; }
        public int AviatorSpecialQual { get; set; }
        public int AviatorMedical { get; set; }
        public int AccidentIncident { get; set; }
        public int AviatorGoodShow { get; set; }
        
        public string EstablishmentAbbreviation { get; set; }
    }

    public class DashCourse
    {
        public int CourseQFI { get; set; }

        public int Unit_ID { get; set; }

        //public IList<DashCourse> ILDashCourse { get; set; }

    }
    public class DashCourseAHIC
    {
        public int CourseAHIC { get; set; }

        public int Unit_ID { get; set; }
    }

    public class DashCourseIP
    {
        public int CourseQFIIP { get; set; }

        public int Unit_ID { get; set; }
    }

    public class DashCourseQFIEP
    {
        public int CourseQFIEP { get; set; }

        public int Unit_ID { get; set; }
    }
    public class DashCourseQFIObP
    {
        public int CourseQFIObP { get; set; }

        public int Unit_ID { get; set; }
    }

    public class DashAircraftHEPTR
    {
        public int HEPTR { get; set; }

    }
    public class DashAircraftRPAS
    {
        public int RPAS { get; set; }

    }
}