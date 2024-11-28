using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Models
{
    public class ReportsModels
    {

        public int TypeOfConfId { get; set; }
        public int ScheduleId { get; set; }
        public int Type { get; set; }
        public string ConfRefNo { get; set; }
        public List<ScheduleLetter> ILScheduleLetter { get; set; }

    }



    public class StrengthReturn: AA7Common
    {
        public int StrId { get; set; }
        public string SUSNo { get; set; }
        public string PosnAs { get; set; }
        public string PresentReturnSerNo { get; set; }
        public string PresentReturnDate { get; set; }
        public string LastReturnSerNo { get; set; }
        public string LastReturnDate { get; set; }
        public string LastIAFF { get; set; }
        public string LastIAFFDate { get; set; }
        public string FmnOfUnit { get; set; }
        public string Location { get; set; }
        public string EstNo { get; set; }
        public string EstDate { get; set; }
        public string CoRemarks { get; set; }
        public string CoCertified { get; set; }
        public string OffrsPostedExcessToEst { get; set; }
        public string OffrsSOSAndHeldOnSupernumeraryStr { get; set; }
        public string DetailsOfPostingDuesInOffrs { get; set; }
        public string DetailsOfPostingDuesOutOffrs { get; set; }
        public string PSCQualified { get; set; }
        public string ReEmpOffrs { get; set; }
        public string OffrsOnSuperStr { get; set; }

        public string Month { get; set; }
        public string Year { get; set; }

    }

    public class TransationStrengthReturn : StrengthReturn
    {
        public int  TransationId { get; set; }       
        public string CDAcctNo { get; set; }
        public string PresentAppt { get; set; }
        public string SoSOffrs { get; set; }
       
        public string AviatorRank { get; set; }

        public string AvitorName { get; set; }

        public string ArmServiceName { get; set; }
        public string MyProperty { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfSeniority { get; set; }
        public string MedicalCat { get; set; }
        public string TOS { get; set; }
        public string AuthApp { get; set; }


        public List<TransationStrengthReturn> ILTransationStrengthReturn { get; set; }


    }





}