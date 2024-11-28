using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.WebApp.Connection;

namespace IHSDC.WebApp.Models
{
    public class AA7Common
    {
        public string AviatorName { get; set; }
        public string PrefixLetter { get; set; }
        public string ANumber { get; set; }
        public string SuffixLetter { get; set; }
        public string ArmyNumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int32 UserId { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsExist { get; set; }
        public string Msg { get; set; }
        public string MsgStatus { get; set; }
        public string MidMsg { get; set; }
        public string Validation { get; set; }
        public string ValidationDate { get; set; }
        public string CounterValidation { get; set; }
        public string CounterValidationDate { get; set; }
        public string IsEdit { get; set; }
        public int UpdatedByUserId { get; set; }
        public int Sqn { get; set; }
        public int BdeCat { get; set; }
        public int Corps { get; set; }
        public int Command { get; set; }
        public int IsPermission { get; set; }
        public string Remarks { get; set; }
        public string DateTimeOfUpdate { get; set; }
        public string LastUpdatedBy { get; set; }
        public int IsHistory { get; set; }
      
        public string RoleName { get; set; }
        public string UnitName { get; set; }
        public int Unit_ID { get; set; }
        public int Aviator_ID { get; set; }

        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }

        public string ColContains1 { get; set; }
        public string ColContains2 { get; set; }
        public string ColContains3 { get; set; }
        public string ColContains4 { get; set; }

        public string ColVal1 { get; set; }
        public string ColVal2 { get; set; }
        public string ColVal3 { get; set; }
        public string ColVal4 { get; set; }
        public string DateOfCom { get; set; }
        public int IsDivEdit { get; set; }
    }

    public class ImportResult
    {
        public bool IsSuccess { get; set; }
        public bool IsExist { get; set; }
        public string Msg { get; set; }
        public string MsgStatus { get; set; }
        public string MidMsg { get; set; }
        public string UnitName { get; set; }
    }

    public class AviatorCRUD : AA7Common
    {
       // public string ArmyNumber { get; set; }
        public string AviatorRank { get; set; }
        public int RankId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public string Appt { get; set; }
        public int ApptId { get; set; }
        public string CivEdn { get; set; }
        public string CivEdnId { get; set; }
        public int ArmService_ID { get; set; }
        public string ArmServiceName { get; set; }
        //public int Aircraft_ID { get; set; }
        public string AircraftName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfSeniority { get; set; }
        public string DateOfWings { get; set; }
        public string DateOfRank { get; set; }
        public string CatCardNo { get; set; }
        public string DateofIssueCatCard { get; set; }
        public bool IsGEB { get; set; }
        public int FinalAppxId { get; set; }
        public int LetterForwardedId { get; set; }
        public string PresentCatIR { get; set; }
        public string DateOfCommision { get; set; }
       // public int IsPermission { get; set; }
        public int GEBLetterID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int FlyingStatusId { get; set; }
        public string ReasonUnFit { get; set; }
        public string GEBRefNoDt { get; set; }
        public int IsAppxE { get; set; }
        public int IsAppxF { get; set; }
        public int IsAppxG { get; set; }
        public string UnitType { get; set; }
        public Boolean RHandler { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public IList<AviatorCRUD> ILAviatorCRUD { get; set; }
        
        public IList<RankHistory> IRankHistory { get; set; }
        public IList<ApptHistory> IApptHistory { get; set; }
        public IList<AviatorLog> IAviatorLog { get; set; }
       
    }

    public class AviatorLog : AA7Common
    {
        public string RecordType { get; set; }
      //  public string ArmyNumber { get; set; }
        public string AviatorRank { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Appt { get; set; }
        public string CivEdn { get; set; }
        public string ArmServiceName { get; set; }
        //public int Aircraft_ID { get; set; }
        public string AircraftName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfSeniority { get; set; }
        public string DateOfWings { get; set; }
        public string DateOfRank { get; set; }
        public string DateOfCommision { get; set; }
        public string FlyingStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MirrorOn { get; set; }
        public string MirrorByUser { get; set; }
        public string MirrorIP { get; set; }
        public string UserName { get; set; }
        public string AircraftType { get; set; }
        public string CatCardNo { get; set; }
        public string CatCardDate { get; set; }
        public string CatIR { get; set; }
        public string CatIRDate { get; set; }
        public string InstrCAT { get; set; }
        public string InstrCatDate { get; set; }
        public string RPAdowDate { get; set; }
        public string RPA_Substream { get; set; }
        public string RPA_CatCardNo { get; set; }
        public string RPA_CatCardDate { get; set; }
        public string RPACat { get; set; }
        public string RPA_CatDate { get; set; }
        public string RPAInstrCat { get; set; }
        public string RPA_Instr_CatDate { get; set; }
        public string StatusAircraft { get; set; }
        public string Year { get; set; }
        public decimal DayDualHrs { get; set; }
        public decimal DaySoloHrs { get; set; }
        public decimal DayCopilotHrs { get; set; }
        public decimal NightDualHrs { get; set; }
        public decimal NightSoloHrs { get; set; }
        public decimal NightCopilotHrs { get; set; }
        public decimal NVGDualExp { get; set; }
        public decimal NVGSoloExp { get; set; }
        public decimal NVGCopilotExp { get; set; }
        public decimal WSOExp { get; set; }
        public decimal IF_Actual { get; set; }
        public decimal InstrDayHrs { get; set; }
        public decimal InstrNightHrs { get; set; }
        public decimal IMCHrs { get; set; }
        public decimal SIFHrs { get; set; }
        public decimal ALHSmlHrs { get; set; }
        public string Month { get; set; }

    }

    public class UserLog : AA7Common
    {
        public int IntId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public string Logindate { get; set; }
        public string IpAddress { get; set; }
        public string Remarks { get; set; }
        public IList<UserLog> IUserLog { get; set; }
    }

   
    public class Complaints : AA7Common
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }
        public int ComplaintDetailId { get; set; }
        public string TicketNo { get; set; }
        public string Date { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public string SubModuleName { get; set; }
        public int SubModuleId { get; set; }
        public string ComplaintStatus { get; set; }
        public int TotalCount { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Others { get; set; }
        public string Solution { get; set; }
        public string RecType { get; set; }
        public int Status { get; set; }
        public bool CloseComplaint { get; set; }

        public string Documents { get; set; }
        public IList<Complaints> IComplaints { get; set; }

    }

    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }

    public class AviatorCoursesCRUD : AA7Common
    {
        public int AviatorCourses_ID { get; set; }

        public int Course_ID { get; set; }
        public string CourseName { get; set; }
        public string CourseSerialNumber { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public int CourseGradingId { get; set; }
        public string Grading { get; set; }
        public string CourseGrading { get; set; }
        public string InstructorGrading { get; set; }
        public string Special_Award { get; set; }
        public string Notes { get; set; }
        public string CourseInstitute { get; set; }
        public string CoursePlace { get; set; }

        

        public IList<AviatorCoursesCRUD> ILAviatorCoursesCRUD { get; set; }
    }

    public class AviatorHeptrRPACRUD : AA7Common
    {
        public int AviatorHeptrRPA_ID { get; set; }
        public int AircraftTypeId { get; set; }
        public string AircraftType { get; set; }
        [Required(ErrorMessage = "required!")]
        public int HeptrRPAMasterId { get; set; }
        public string AircraftName { get; set; }
        public string CatCardNo { get; set; }
        public string CatCardDate { get; set; }
       
        public int CatIRId { get; set; }
        public string CatIR { get; set; }
        public string CatIRDate { get; set; }
        public int InstrCATId { get; set; }
        public string InstrCAT { get; set; } 
        public string InstrCatDate { get; set; }
        public string RPAdowDate { get; set; }
        public int RPA_SubstreamId { get; set; }
        public string RPA_Substream { get; set; }
        public string RPA_CatCardNo { get; set; }
        public string RPA_CatCardDate { get; set; }
        public int RPACatId { get; set; }
        public string RPACat { get; set; }
        public string RPA_CatDate { get; set; }
        public int RPAInstrCatId { get; set; }
        public string RPAInstrCat { get; set; }
        public string RPA_Instr_CatDate { get; set; }
        public int StatusAircraftId { get; set; }
        public string StatusAircraft { get; set; }

        public Boolean IsCurrent { get; set; }
     
        public IList<AviatorHeptrRPACRUD> ILAviatorHeptrRPACRUD { get; set; }
        public IList<AviatorHeptrRPACRUD> ILAviatorHeptrRPACRUDHis { get; set; }

        public IList<AviatorLog> IAviatorLog { get; set; }
    }



    public class AviatorHonourAwardsCRUD : AA7Common
    {
        public int AviatorHonourAwards_ID { get; set; }
        public int HonourAward_ID { get; set; }
        public string HonourAwardsDate { get; set; }
        public string HonourAwardsPlace { get; set; }
        public string HonourAwardName { get; set; }
      //  public string Remarks { get; set; }
        public IList<AviatorHonourAwardsCRUD> ILAviatorHonourAwardsCRUD { get; set; }
    }
    public class AviatorFlyingHrsCRUD : AA7Common
    {
        public int AviatorFlyingHrs_ID { get; set; }
        public int AircraftTypeId { get; set; }
        public string AircraftType { get; set; }
        public int HeptrRPAMasterId { get; set; }
        public string AircraftName { get; set; }
        public string Year { get; set; }
        public decimal DayDualHrs { get; set; }
        public decimal DaySoloHrs { get; set; }
        public decimal DayCopilotHrs { get; set; }
        public decimal NightDualHrs { get; set; }
        public decimal NightSoloHrs { get; set; }
        public decimal NightCopilotHrs { get; set; }
        public decimal NVGDualExp { get; set; }
        public decimal NVGSoloExp { get; set; }
        public decimal NVGCopilotExp { get; set; }
        public decimal WSOExp { get; set; }
        public decimal IF_Actual { get; set; }
        public decimal InstrDayHrs { get; set; }
        public decimal InstrNightHrs { get; set; }
        public decimal IMCHrs { get; set; }
        public decimal SIFHrs { get; set; }
        public decimal ALHSmlHrs { get; set; }
        public Boolean IsCurrent { get; set; }
        public int AviatorHeptrRPA_ID { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string Month { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM/yyyy}")]
        public DateTime Month1 { get; set; }
        public string ListYear { get; set; }
        public IList<AviatorLog> IAviatorLog { get; set; }
        public IList<AviatorFlyingHrsCRUD> ILAviatorFlyingHrsCRUD { get; set; }

        public IList<AviatorFlyingHrsCRUD> IAviatorMonthWiseFlyingHrs { get; set; }
        public IList<AviatorFlyingHrsCRUD> IAviatorFlyingHrsSummary { get; set; }

        public string ConvertFlyingHrs(decimal hrs)
        {
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();
            }
            return parts[0] + ":" + parts[1];
        }



        public string ConvertDecimalToHrs(decimal hrs)
        {
            decimal FinalValue = 0;
            decimal NewHrsVal = 0;
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();
                
            }
            if (Convert.ToDecimal(parts[1]) >= 60)
            {
                int hours = (int)(Convert.ToDecimal(parts[1]) / 60);
                int minutes = (int)(Convert.ToDecimal(parts[1]) % 60);
                String HrsMin = Convert.ToString(hours) + "." + Convert.ToString(minutes);
                NewHrsVal = Convert.ToDecimal(HrsMin);
                FinalValue = Convert.ToDecimal(Convert.ToDecimal(parts[0]) + Convert.ToDecimal(NewHrsVal.ToString("00.00")));
            }
            else
            {
                NewHrsVal = Convert.ToDecimal(parts[1]);
                FinalValue = hrs;
            }
           
            string formattedValue = FinalValue.ToString("00.00");
            string Finaldata = formattedValue.ToString();
            string[] FinalPart = Finaldata.Split('.');
            return FinalPart[0] + ":" + FinalPart[1];
        }


    }




    public class AviatorAccidentCRUD : AA7Common
    {
        public int AviatorAccidentIncident_ID { get; set; }
        public string TypeAircraft { get; set; }
        public int AircraftTypeId { get; set; }
        public string AircraftType { get; set; }
        [Required(ErrorMessage = "required!")]
        public int HeptrRPAMasterId { get; set; }
        public string AircraftName { get; set; }
        public int AccidentCat_ID { get; set; }
        public string AccidentCatName { get; set; }
        public int AccidentCatSubId { get; set; }
        public int AccidentCatDamage_ID { get; set; }
        public string AccidentCatDamage { get; set; }
        public string AccidentCatSub { get; set; }
        public int StagesGroundAccidentId { get; set; }
        public string StagesGroundAccident { get; set; }
        public int PeriodOperationId { get; set; }
        public string PeriodOperation { get; set; }
        public int ClassInjuryId { get; set; }
        public string ClassInjury { get; set; }
        public string DateOfAccidentIncident { get; set; }
        public string PlaceOfAccidentIncident { get; set; }
        public string DetailsOfAccidentIncident { get; set; }
        public string Blameworthy { get; set; }
      //  public string Remarks { get; set; }
        public IList<AviatorAccidentCRUD> ILAviatorAccidentCRUD { get; set; }
    }

    public class AviatorGoodShowCRUD : AA7Common
    {
        public int AviatorGoodShow_ID { get; set; }
        public int AircraftTypeId { get; set; }
        public string AircraftType { get; set; }
        public int HeptrRPAMasterId { get; set; }
        public string AircraftName { get; set; }
        public string DateGoodShow { get; set; }
        public string PlaceGoodShow { get; set; }
        public string DetailGoodShow { get; set; }
      //  public string Remarks { get; set; }
        public IList<AviatorGoodShowCRUD> ILAviatorGoodShowCRUD { get; set; }
    }


    public class AviatorDashPendingCRUD : AA7Common
    {
        public string AviatorApprovalCounterValidation { get; set; }
        public string AviatorApprovalPending { get; set; }
        public IList<AviatorDashPendingCRUD> ILAviatorDashPendingCRUD { get; set; }
    }



   


    public class AviatorApptCRUD : AA7Common
    {
        public int AviatorAppointment_ID { get; set; }
        public string ApptDate { get; set; }
        public string ApptName { get; set; }
        public IList<AviatorApptCRUD> ILAviatorApptCRUD { get; set; }
    }

    public class AviatorRankCRUD : AA7Common
    {
        public int AviatorRank_ID { get; set; }
        public string RankDate { get; set; }
        public string Rank { get; set; }
        public IList<AviatorRankCRUD> ILAviatorRankCRUD { get; set; }
    }

    public class AviatorContactDetailsCRUD : AA7Common
    {
        public int AviatorDetailID { get; set; }
        public string MobileNo { get; set; }
        public string LandLineNo { get; set; }
        public string Email { get; set; }
        public string NOK { get; set; }
        public string RelationWithNok { get; set; }
        public string MaritalStatus { get; set; }
        public string NameOfSpouse { get; set; }
        public string ContactNoOfSpouse { get; set; }
        public string EmailOfSpouse { get; set; }
        public string ResidentalHouseNo { get; set; }
        public string ResidentalVillage_Street { get; set; }
        public string ResidentalPostOffice { get; set; }
        public string ResidentalTehsil { get; set; }
        public string City { get; set; }
        public string ResidentalDistrict { get; set; }
        public string ResidentalState { get; set; }
        public string ResidentalPincode { get; set; }
        public string PermanentHouseNo { get; set; }
        public string PermanentVillage_Street { get; set; }
        public string PermanentPostOffice { get; set; }
        public string PermanentTehsil { get; set; }
        public string PermanentDistrict { get; set; }
        public string PermanentState { get; set; }
        public string PermanentPincode { get; set; }
        public IList<AviatorContactDetailsCRUD> ILAviatorContactDetailsCRUD { get; set; }
    }

    public class AviatorMedicalCRUD : AA7Common
    {
        public int AviatorMedical_ID { get; set; }
        public int TypeMedicalId { get; set; }
        public string TypeMedical { get; set; }
        public string MedicalStartDate { get; set; }
        public string MedicalEndDate { get; set; }
        public int PlaceMedicalId { get; set; }
        public string Place { get; set; }
        public string PlaceMedical { get; set; }
        public int MedicalCatId { get; set; }
        public string OtherMedicalRemarks { get; set; }
        public string MedicalCat { get; set; }
        public int CopeCodeId { get; set; }
        public string OtherCopeRemarks { get; set; }
        public string CopeCode { get; set; }
        public string MedicalCatDate { get; set; }
        public int DurationInWeeks { get; set; }
        public string ReCatDueDate { get; set; }
        public int StatusMedicalId { get; set; }
        public string StatusMedical { get; set; }
      //  public string Remarks { get; set; }

        public IList<AviatorMedicalCRUD> ILAviatorMedicalCRUD { get; set; }
    }


    public class AviatorForeignVisitCRUD : AA7Common
    {
        public int AviatorForeignVisit_ID { get; set; }
        public string ForeignPostingTypeId { get; set; }
        public string ForeignPostingType { get; set; }
        public string ForeignAppt { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Country { get; set; }
        //public string Remarks { get; set; }

        public IList<AviatorForeignVisitCRUD> ILAviatorForeignVisitCRUD { get; set; }
    }


    public class AviatorHeptrRPAViewCRUD : AA7Common
    {
        public int AviatorHeptrRPA_ID { get; set; }
        public string AircraftType_ID { get; set; }
        public string Aircraft { get; set; }
        public string CatCardNo { get; set; }
        public string CatCardDate { get; set; }
        public string CatIR { get; set; }
        public string CatIRDate { get; set; }
        public string InstrCat { get; set; }
        public string RPAdowDate { get; set; }
        public string RPASubstream { get; set; }
        public string RPA_CatCardNo { get; set; }
        public string RPA_CatCardDate { get; set; }
        public string RPA_Cat { get; set; }
        public string RPA_CatDate { get; set; }
        public string RPA_Instr_Cat { get; set; }
        public string RPA_Instr_CatDate { get; set; }
        public string Status_Aircraft { get; set; }
        public string AircraftType { get; set; }
        public string AircraftName { get; set; }
        public string RPACat { get; set; }
        public string StatusAircraft { get; set; }
        public string InstrCatDate { get; set; }
        public Boolean IsCurrent { get; set; }
        //public int AviatorHeptrRPA_ID { get; set; }
        //public int AircraftType_ID { get; set; }
        //public string AircraftType { get; set; }

        //public int HeptrRPAMasterId { get; set; }
        //public string AircraftName { get; set; }
        //public string CatCardNo { get; set; }
        //public string CatCardDate { get; set; }

        //public int CatIRId { get; set; }
        //public string CatIR { get; set; }
        //public string CatIRDate { get; set; }
        //public int InstrCATId { get; set; }
        //public string InstrCAT { get; set; }
        //public string InstrCatDate { get; set; }
        //public string RPAdowDate { get; set; }
        //public int RPA_SubstreamId { get; set; }
        //public string RPA_Substream { get; set; }
        //public string RPA_CatCardNo { get; set; }
        //public string RPA_CatCardDate { get; set; }
        //public int RPACatId { get; set; }
        //public string RPACat { get; set; }
        //public string RPA_CatDate { get; set; }
        //public int RPAInstrCatId { get; set; }
        //public string RPAInstrCat { get; set; }
        //public string RPA_Instr_CatDate { get; set; }
        //public int StatusAircraftId { get; set; }
        //public string StatusAircraft { get; set; }


        public IList<AviatorHeptrRPAViewCRUD> ILAviatorHeptrRPAViewCRUD { get; set; }
    }


    public class AppxCCRUD : AA7Common
    {
        public int AppxCId { get; set; }
        public int AppxListId { get; set; }
        public string PresentCat { get; set; }
        public string PresentIR { get; set; }
        public string PresentCatIR { get; set; }
        public string RecomCat { get; set; }
        public string RecomIR { get; set; }
        public string RecomOfCO { get; set; }
        public int TotalCaptHrs { get; set; }
        public int OnTypeCaptHrs { get; set; }
        public IList<AppxCCRUD> ILAppxCCRUD { get; set; }
    }
    public class AppxListCRUD : AA7Common
    {
        public int AppxListId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AppxType { get; set; }
        public string UpToFlyingHrsDate { get; set; }
        public IList<AppxListCRUD> ILAppxListCRUD { get; set; }
    }

    public class GEBLetterForwardedForUnit
    {
        public int ChildId { get; set; }
        public string UserName { get; set; }
        public bool checkbox { get; set; }
        public string RoleName { get; set; }
        public string TypeOfUnit { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsNotification { get; set; }

    }

    public class GEBLetter : AA7Common
    {
        public int GEBLetterId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Comd { get; set; }
        public string ComdName { get; set; }
        public string BrigAvnName { get; set; }
        public string SubmitSqnCdr { get; set; }
        public string Status { get; set; }
        public string TypeOfUnit { get; set; }
        public string LetterForwarded { get; set; }
        public string LetterForwardedDate { get; set; }
        public string SubmitByComd { get; set; }
        public string ComdSubmitDate { get; set; }
        public int LetterFwdSqnId { get; set; }
        public bool IsSubmit { get; set; }
        public int CountChildBrig { get; set; }
        public bool IsNotification { get; set; }
        public IList<GEBLetter> ILGEBLetter { get; set; }
        public IList<GEBLetterForwardedForUnit> IlGEBLetterForwardedForUnit { get; set; }
        public string StatusName { get; set; }
        public string GEBExamCentre { get; set; }
        public string GEBRefNoDt { get; set; }
        public int Aviators { get; set; }
        public int TotalAviators { get; set; }
    }

    //public class ScheduleLetter : AA7Common
    //{
    //    public int ScheduleId { get; set; }
    //    public string FromDate { get; set; }
    //    public string ToDate { get; set; }        
    //    public string Remark { get; set; }
    //    public string ScheduleRefNoDt { get; set; }
  
    //}



    public class LetterForwardedGEB : GEBLetter
    {

        public int CountOfGEBList { get; set; }
        public int AvCount { get; set; }
        //public int GEBLetterId { get; set; }
        public int ChildId { get; set; }
        public string SubmitByFltCdr { get; set; }
        public string SubmitByFltCdrDate { get; set; }
        public string SubmitSqnCdrDate { get; set; }
        public string SubmitBy2IC { get; set; }
        public string SubmitBy2ICDate { get; set; }
        public int FinalAppxId { get; set; }
        public string RankName { get; set; }
        public int RankId { get; set; }
        public string PresentCat { get; set; }
        public string PresentIR { get; set; }
        public string PresentCatIRDate { get; set; }
        public string PresentCatIRType { get; set; }
        public string TypesIfAny { get; set; }
        public int SpecialQual_ID { get; set; }
        public string SplQul { get; set; }
        public int AppearingFor { get; set; }

        public string AppearedWith { get; set; }
        public decimal DayHrsLastThreeMonth { get; set; }
        public decimal NighHrsLastSixMonth { get; set; }
        public decimal SIFHrs { get; set; }
        public int AircraftType { get; set; }
        public string QfiCat { get; set; }
        public string QfiDate { get; set; }
        public decimal TotalInstHrs { get; set; }
        public string CheckAviatorEndorsmentAwardedOn { get; set; }
        public string AwardByComd { get; set; }
        public decimal TotalCptHrsPresentTypeDay { get; set; }
        public decimal TotalCptHrsPresentTypeNight { get; set; }
        public decimal OnHelicoptorDay { get; set; }
        public decimal OnHelicoptornight { get; set; }
        public string RemarksDay { get; set; }
        public string RemarksNight { get; set; }
        public string RecomForCat { get; set; }
        public string RecomForIR { get; set; }
        public string RemarksAppearing { get; set; }
        public string ArmServiceName { get; set; }
        public string AircraftName { get; set; }
        public decimal IMCDayHrs { get; set; }
        public decimal IMCNightHrs { get; set; }
        public decimal CaptHrsDayOnAllTypes { get; set; }
        public decimal CaptHrsNightOnAllTypes { get; set; }
        public string PresentCatIR { get; set; }
        public string RecomForCatIR { get; set; }
        public int IsAppxE { get; set; }
        public int IsAppxF { get; set; }
        public int IsAppxG { get; set; }
        public bool IsRejectBrig { get; set; }
        public bool IsRejectGSO { get; set; }
        public bool? IsApprovedByGSO { get; set; }
        public string GEBType { get; set; }
        public int CatIRId { get; set; }

        public int PresentCatIRTypeID { get; set; }
        public int TypesIfAnyID { get; set; }

        public string AppearingForName { get; set; }

        public string SpecialQualName { get; set; }

        public string DateandTypeAward { get; set; }

        public string DateofExpiry { get; set; }

        public string AircraftTypeName { get; set; }

        public string AwardByComdName { get; set; }

        //  public int LetterFwdSqnId { get; set; }
        //public bool IsSubmit { get; set; }
        //public bool IsNotification
        //{
        //    get; set;
        //}


        public IList<LetterForwardedGEB> ILLetterForwardedGEB { get; set; }
        public string ConvertFlyingHrs(decimal hrs)
        {
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();
            }
            return parts[0] + ":" + parts[1];
        }


        public string ConvertDecimalToHrs(decimal hrs)
        {
            decimal FinalValue = 0;
            decimal NewHrsVal = 0;
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();

            }
            if (Convert.ToDecimal(parts[1]) >= 60)
            {
                int hours = (int)(Convert.ToDecimal(parts[1]) / 60);
                int minutes = (int)(Convert.ToDecimal(parts[1]) % 60);
                String HrsMin = Convert.ToString(hours) + "." + Convert.ToString(minutes);
                NewHrsVal = Convert.ToDecimal(HrsMin);
                FinalValue = Convert.ToDecimal(Convert.ToDecimal(parts[0]) + Convert.ToDecimal(NewHrsVal.ToString("00.00")));
            }
            else
            {
                NewHrsVal = Convert.ToDecimal(parts[1]);
                FinalValue = hrs;
            }

            string formattedValue = FinalValue.ToString("00.00");
            string Finaldata = formattedValue.ToString();
            string[] FinalPart = Finaldata.Split('.');
            return FinalPart[0] + ":" + FinalPart[1];
        }
    }





    public class PostingAviator : AA7Common
    {
        public int AviatorPosting_ID { get; set; }
        public string PostingAuth { get; set; }
        public int? PostingFromUnit { get; set; }
        public string SOS { get; set; }
        public string SORS { get; set; }
        public string RecordHandler  { get; set; }
        public int PostingToUnit { get; set; }
        public string OtherUnit { get; set; }
        public string PostingToUnitName { get; set; }
        public string PostingFromUnitName { get; set; }
        public string TOS { get; set; }
        public string TORS { get; set; }
        public string PostingType { get; set; }
        public string PostingInDate { get; set; }
        public IList<PostingAviator> ILPostingAviator { get; set; }
    }


    public class ImportData : AA7Common
    {
        public HttpPostedFileWrapper HttpPostedFileWrapper { get; set; }
    }

    public class AppxGPerformance : LetterForwardedGEB
    {
        public int AppxGPerformance_ID { get; set; }
        public string ExaminingAuth { get; set; }
        public string ExaminedOnType { get; set; }
        public string ExaminedOnTypeName { get; set; }
        public string DateOfExam { get; set; }
        public string PlaceOfExam { get; set; }
        public int FlgAbilityPercentage { get; set; }
        public string FlgAbilityStandard { get; set; }
        public string FlgAbilityStandardName { get; set; }
        public int FlgInstrAbilityPercentage { get; set; }
        public string FlgInstrAbilityStandard { get; set; }
        public string FlgInstrAbilityStandardName { get; set; }
        public string GrdSubInstrAbility { get; set; }
        public string GrdSubInstrAbilityStandards { get; set; }
        public string GrdSubInstrAbilityStandardsName { get; set; }
        public string RecommendCheckAviator { get; set; }
        public string RecommendOnType { get; set; }
        public string RecommendOnTypeName { get; set; }
        public string RecommendDate { get; set; }
        public string NameOfExaminer { get; set; }
        public string RankOfExaminer { get; set; }
        public string RankOfExaminerName { get; set; }
        public string ApptOfExaminer { get; set; }
        public string ApptOfExaminerName { get; set; }
        public string ApprovedCheckAviator { get; set; }
        public string ApprovedOnType { get; set; }
        public string ApprovedOnTypeName { get; set; }
        public string ApprovedOnDate { get; set; }
        public string ApprovedNameOfDte { get; set; }
        public string ApprovedRankOfDte { get; set; }
        public string ApprovedRankOfDteName { get; set; }
        public string ApprovedApptOfDte { get; set; }
        public string ApprovedApptOfDteName { get; set; }
        public AppxG AppxG { get; set; }
        public AppxGFlgHrs AppxGFlgHrs { get; set; }
        public IList<AppxGFlgHrs> ILAppxGFlgHrs { get; set; }

    }
    public class AppxFPerformance : LetterForwardedGEB
    {
        public int AppxFPerformance_ID { get; set; }
        public int AppxFId { get; set; }
        public string ExaminingAuth { get; set; }
        public string ExaminedOnType { get; set; }
        public string ExaminedOnTypeName { get; set; }
        public string DateOfExam { get; set; }
        public string PlaceOfExam { get; set; }
        public int FlgAbilityPercentage { get; set; }
        public string FlgAbilityStandard { get; set; }
        public string FlgAbilityStandardName { get; set; }
        public int FlgInstrAbilityPercentage { get; set; }
        public string FlgInstrAbilityStandard { get; set; }
        public string FlgInstrAbilityStandardName { get; set; }
        public int GrdSubInstrAbilityPercentage { get; set; }
        public string GrdSubInstrAbilityStandard { get; set; }
        public string GrdSubInstrAbilityStandardName { get; set; }
        public int SplKnowledgePercentage { get; set; }
        public string SplKnowledgeStandard { get; set; }
        public string SplKnowledgeStandardName { get; set; }
        public int OverallPerformancePercentage { get; set; }
        public string OverallPerformanceStandard { get; set; }
        public string OverallPerformanceStandardName { get; set; }
        public string RecommendInstrCat { get; set; }
        public string RecommendInstrCatName { get; set; }
        public string RecommendOnType { get; set; }
        public string RecommendOnTypeName { get; set; }
        public string RecommendOnDate { get; set; }
        public string NameOfExaminer { get; set; }
        public string RankOfExaminer { get; set; }
        public string RankOfExaminerName { get; set; }
        public string ApptOfExaminer { get; set; }
        public string ApptOfExaminerName { get; set; }
        public string ApprovedInstrCat { get; set; }
        public string ApprovedInstrCatName { get; set; }
        public string ApprovedOnType { get; set; }
        public string ApprovedOnTypeName { get; set; }
        public string ApprovedOnDate { get; set; }
        public string ApprovedNameOfDte { get; set; }
        public string ApprovedRankOfDte { get; set; }
        public string ApprovedRankOfDteName { get; set; }
        public string ApprovedApptOfDte { get; set; }
        public string ApprovedApptOfDteName { get; set; }
        public AppxF appxF { get; set; }
        public AppxFFlgHrs appxFFlgHrs { get; set; }
        public AppxFIntsrExp appxFIntsrExp { get; set; }
        public IList<AppxFFlgHrs> ILAppxFFlgHrs { get; set; }
        public IList<AppxFIntsrExp> ILAppxFIntsrExp { get; set; }



    }


    public class AppxEPerformance : LetterForwardedGEB
    {
        public int AppxEPerformance_ID { get; set; }

        public string DateOfGEB { get; set; }
        public string Result { get; set; }
        public string Category { get; set; }
        public string InstrumentRating { get; set; }
        public string InstructorCat { get; set; }
        public string GoodShow { get; set; }
        public string WarnedFor { get; set; }
        public string SpecialRemarks { get; set; }
        public string FlgSubDate { get; set; }
        public int FlgSubMarks { get; set; }
        public string FlgSubRemarks { get; set; }
        public string TacandSplDate { get; set; }
        public int TacandSplMarks { get; set; }
        public string TacandSplRemarks { get; set; }
        public string SplBfgDate { get; set; }
        public int SplBfgMarks { get; set; }
        public string SplBfgRemarks { get; set; }
        public string DayFlgDate { get; set; }
        public int DayFlgMarks { get; set; }
        public string DayFlgRemarks { get; set; }
        public string SplExDate { get; set; }
        public int SplExMarks { get; set; }
        public string SplExRemarks { get; set; }
        public string OpADate { get; set; }
        public int OpAMarks { get; set; }
        public string OpARemarks { get; set; }
        public string NiFlgDate { get; set; }
        public int NiFlgMarks { get; set; }
        public string NiFlgRemarks { get; set; }
        public string DateOfAward { get; set; }


        public LetterForwardedGEB finalAppx { get; set; }



    }
    public class AppxF : LetterForwardedGEB
    {
        public int AppxFId { get; set; }
        public int AppxFForYear { get; set; }
        public int InstructoionalCatApplied { get; set; }
        public string InstructoionalCatAppliedName { get; set; }
        public int InstructoionalCatIRHeld { get; set; }
        public string InstructoionalCatIRHeldName { get; set; }
        public int InstructoionalCatIRHeldAircraft { get; set; }
        public string InstructoionalCatIRHeldAircraftName { get; set; }
        public string InstructoionalCatIRDate { get; set; }
        public string DateofCatAward { get; set; }
        public int AwardTypeAircraft { get; set; }

        public string AwardTypeAircraftName { get; set; }
        public string AHI_QFICourseNo { get; set; }
        public string DateofCompletion { get; set; }
        public string QFICatDate { get; set; }
        public string OpInstructionalSyllabusNo { get; set; }
        public decimal OpInstructionalHrs { get; set; }
        public string OpInstructionalSyllabusComletedOn { get; set; }
        public string OpInstructionalSyllabusComletedByArmyNo { get; set; }
        public int OpInstructionalSyllabusComletedByRank { get; set; }
        public string OpInstructionalSyllabusComletedByRankName { get; set; }
        public string OpInstructionalSyllabusComletedByName { get; set; }
        public int ReccommendInstructionalCatIR { get; set; }
        public int ReccommendInstructionalCatAircraft { get; set; }
        public string CourseType { get; set; }

    }

    public class AppxG : LetterForwardedGEB
    {
        public int AppxGId { get; set; }
        public int AppxGForYear { get; set; }
        public string CheckAviatorEndorsementExp { get; set; }
        public string CheckAviatorEndorsementOnAirCraftType { get; set; }
        public string CheckAviatorEndorsementOnAirCraftTypeName { get; set; }
        public string CheckAviatorEndorsementExpUnit { get; set; }
        public string CheckAviatorEndorsementExpUnitName { get; set; }
        public decimal TotalInstrDayExp { get; set; }
        public decimal TotalInstrNightExp { get; set; }
        public string Syllabus9ACompletedOn { get; set; }
        public string CompletedByICNo { get; set; }
        public string CompletedByRank { get; set; }
        public string CompletedByRankName { get; set; }
        public string CompletedByName { get; set; }
        public int NoQFIInTheUnit { get; set; }
        public string RecomeForCheckAviatorUnit { get; set; }
        public string RecomeForCheckAviatorOnType { get; set; }
        public string RecomeForCheckAviatorOnTypeName { get; set; }
        public bool? ISRecommendByBrigAvn { get; set; }
        ///public bool? IsApprovedByGSO { get; set; }


    }

    public class AppxFFlgHrs : Common
    {
        public int AppxF_FlyingDetailId { get; set; }
        public int AppxFId { get; set; }
        public int AircraftType { get; set; }
        public decimal DayDualHr { get; set; }
        public decimal DayCaptainHr { get; set; }
        public decimal NightDualHr { get; set; }
        public decimal NightCaptainHr { get; set; }

        public string AircraftTypeName { get; set; }
        public IList<AppxFFlgHrs> ILAppxFFlgHrs { get; set; }


        public string ConvertFlyingHrs(decimal hrs)
        {
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();
            }
            return parts[0] + ":" + parts[1];
        }
    }

    public class AppxGFlgHrs : Common
    {
        public int AppxG_FlyingDetailId { get; set; }
        public int AppxGId { get; set; }
        public string AircraftType { get; set; }
        public decimal DayCaptainHr { get; set; }
        public decimal NightCaptainHr { get; set; }
        public string AircraftTypeName { get; set; }

    }
    public class AppxFIntsrExp : Common
    {
        public int AppxF_InstrExpId { get; set; }
        public int AppxFId { get; set; }
        public string AircraftType { get; set; }
        public int CatHeld { get; set; }
        public string DateOfAward { get; set; }
        public decimal InstrDayHrs { get; set; }
        public decimal InstrNightHrs { get; set; }
        public string CatHeldName { get; set; }
        public string AircraftTypeName { get; set; }

    }

    public class FinalAppxF
    {
        public AppxF appxF { get; set; }
        public AppxFFlgHrs appxFFlgHrs { get; set; }
        public AppxFIntsrExp appxFIntsrExp { get; set; }
        public IList<AppxFFlgHrs> ILAppxFFlgHrs { get; set; }
        public IList<AppxFIntsrExp> ILAppxFIntsrExp { get; set; }
    }

    public class FinalAppxG
    {
        public AppxG AppxG { get; set; }
        public AppxGFlgHrs AppxGFlgHrs { get; set; }      
        public IList<AppxGFlgHrs> ILAppxGFlgHrs { get; set; }
      
    }

    public class NominalRollForGEB
    {
        public IList<GEBLetter> ILGEBLetter { get; set; }
        public IList<LetterForwardedGEB> ILLetterForwardedGEB { get; set; }
        public IList<GEBLetterForwardedForUnit> ILGEBLetterForwardedForUnit { get; set; }

        public AviatorCRUD aviator { get; set; }
    }

    public class VieViewAviatorDetails
    {

        public IList<AviatorCRUD> ILAviatorCRUD { get; set; }
        public IList<AviatorContactDetailsCRUD> ILAviatorContactDetailsCRUD { get; set; }
        public IList<AviatorCoursesCRUD> ILAviatorCoursesCRUD { get; set; }
        public IList<AviatorHonourAwardsCRUD> ILAviatorHonourAwardsCRUD { get; set; }
        public IList<AviatorFlyingHrsCRUD> ILAviatorFlyingHrsCRUD { get; set; }

        public IList<AviatorForeignVisitCRUD> ILAviatorForeignVisitCRUD { get; set; }

        public IList<AviatorMedicalCRUD> ILAviatorMedicalCRUD { get; set; }
        public IList<AviatorGoodShowCRUD> ILAviatorGoodShowCRUD { get; set; }
        public IList<AviatorHeptrRPAViewCRUD> ILAviatorHeptrRPAViewCRUD { get; set; }
        public IList<AviatorAccidentCRUD> ILAviatorAccidentCRUD { get; set; }
    }

    public class HistoryAviatorDetails
    {

        public IList<AviatorCRUD> ILAviatorCRUD { get; set; }
        //public IList<AviatorContactDetailsCRUD> ILAviatorContactDetailsCRUD { get; set; }
        //public IList<AviatorCoursesCRUD> ILAviatorCoursesCRUD { get; set; }
        //public IList<AviatorHonourAwardsCRUD> ILAviatorHonourAwardsCRUD { get; set; }
        //public IList<AviatorFlyingHrsCRUD> ILAviatorFlyingHrsCRUD { get; set; }
        //public IList<AviatorSpecialEqptCRUD> ILAviatorSpecialEqptCRUD { get; set; }
        //public IList<
        //CRUD> ILAviatorSpecialQualCRUD { get; set; }
        //public IList<AviatorMedicalCRUD> ILAviatorMedicalCRUD { get; set; }
        //public IList<AviatorGoodShowCRUD> ILAviatorGoodShowCRUD { get; set; }
        //public IList<AviatorHeptrRPAViewCRUD> ILAviatorHeptrRPAViewCRUD { get; set; }
    }

    public class HistoryHeptrRPADetails
    {

        public IList<AviatorCRUD> ILAviatorCRUD { get; set; }
        //public IList<AviatorContactDetailsCRUD> ILAviatorContactDetailsCRUD { get; set; }
        //public IList<AviatorCoursesCRUD> ILAviatorCoursesCRUD { get; set; }
        //public IList<AviatorHonourAwardsCRUD> ILAviatorHonourAwardsCRUD { get; set; }
        //public IList<AviatorFlyingHrsCRUD> ILAviatorFlyingHrsCRUD { get; set; }
        //public IList<AviatorSpecialEqptCRUD> ILAviatorSpecialEqptCRUD { get; set; }
        //public IList<AviatorSpecialQualCRUD> ILAviatorSpecialQualCRUD { get; set; }
        //public IList<AviatorMedicalCRUD> ILAviatorMedicalCRUD { get; set; }
        //public IList<AviatorGoodShowCRUD> ILAviatorGoodShowCRUD { get; set; }
        public IList<AviatorHeptrRPAViewCRUD> ILAviatorHeptrRPAViewCRUD { get; set; }
    }


    public class AASQN
    {
        public string AviatorName { get; set; }
        public string CivEdn { get; set; }
        public string Appt { get; set; }
        public string DateOfCommision { get; set; }
        public string DateOfSeniority { get; set; }
        public string ArmServiceName { get; set; }
        public string TOS { get; set; }
        public string CatIRDate { get; set; }
        public string CatIR { get; set; }
        public string InstrCAT { get; set; }
        public string InstrCATDate { get; set; }
        public Decimal captionHrs { get; set; }
        public string CatIRDate_rpas { get; set; }
        public string CatIR_rpas { get; set; }
        public string InstrCat_rpas { get; set; }
        public Decimal Instrs { get; set; }
        public Decimal ALHSmlHrs { get; set; }
        public Decimal NVGExp { get; set; }

        public Decimal RPA_Instrs { get; set; }
        public Decimal RPA_Inl { get; set; }

        public string CT_CG_BA_TCO { get; set; }
        public string JC { get; set; }
        public string DSSC_TSOC_ISC_AL_MC { get; set; }
        public string QFIC_AHIC_LGSC { get; set; }
        public string TP_PTP { get; set; }
        public string FCC_ACTC_BCTC { get; set; }
        public string FS_AI { get; set; }
        public string Medcat { get; set; }
        public string Remarks { get; set; }

        public string ComdName { get; set; }
        public string CorpName { get; set; }
        public string BdeName { get; set; }
        public string SqnName { get; set; }
        public string UnitName { get; set; }
        public string GroupUnitName { get; set; }
        public int GroupUnitId { get; set; }
        public IList<AASQN> IAASQN { get; set; }

        public string ConvertDecimalToHrs(decimal hrs)
        {
            decimal FinalValue = 0;
            decimal NewHrsVal = 0;
            string data = hrs.ToString();
            string[] parts = data.Split('.');
            if (parts[0].Length == 1)
            {
                parts[0] = "0" + parts[0].ToString();

            }
            if (Convert.ToDecimal(parts[1]) >= 60)
            {
                int hours = (int)(Convert.ToDecimal(parts[1]) / 60);
                int minutes = (int)(Convert.ToDecimal(parts[1]) % 60);
                String HrsMin = Convert.ToString(hours) + "." + Convert.ToString(minutes);
                NewHrsVal = Convert.ToDecimal(HrsMin);
                FinalValue = Convert.ToDecimal(Convert.ToDecimal(parts[0]) + Convert.ToDecimal(NewHrsVal.ToString("00.00")));
            }
            else
            {
                NewHrsVal = Convert.ToDecimal(parts[1]);
                FinalValue = hrs;
            }

            string formattedValue = FinalValue.ToString("00.00");
            string Finaldata = formattedValue.ToString();
            string[] FinalPart = Finaldata.Split('.');
            return FinalPart[0] + ":" + FinalPart[1];
        }

    }
    public class AASQNSummary
    {
        public string UnitName { get; set; }
        public int AMG { get; set; }
        public int BMG { get; set; }
        public int BG { get; set; }
        public int CG { get; set; }
        public int CW { get; set; }
        public int DW { get; set; }
        public int CR { get; set; }
        public int CT { get; set; }
        public IList<AASQNSummary> IAASQNSummary { get; set; }
    }
    public class DropdownList
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
        public class AASQNlist
    {
        public List<AASQN> AASQNlst { get; set; }
        public List<AASQNSummary> AASQNSummarylst { get; set; }
    }

    public class AddUpdateStatus
    {
        public string FormName { get; set; }
        public int CourseID { get; set; }
        public int AircraftType_ID { get; set; }
        public int HeptrRPAMasterId { get; set; }
        public int CatIRId { get; set; }
        public int RPACatId { get; set; }
        public int SpecialEqpt_ID { get; set; }
        public int SpecialQual_ID { get; set; }
        public int ForeignPostingTypeId { get; set; }
        public string HonourAwardsDate { get; set; }
        public string MedicalStartDate { get; set; }
        public string DateOfAccidentIncident { get; set; }
        public string DateGoodShow { get; set; }
        public string Month { get; set; }
        public int Aviator_Id { get; set; }
        public string StatusName { get; set; }


    }


    public class APUserLog : AA7Common
    {
        public int IntId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public string Logindate { get; set; }
        public string IpAddress { get; set; }
        public string ConfTitle { get; set; }
        public string AgendaptTitle { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string SponsorName { get; set; }
        public Int64 SLNO { get; set; }

        public int ConfId { get; set; }
        public string Conf { get; set; }
        public int Inbox_ID { get; set; }
        public int ComdId { get; set; }
        public int Branch { get; set; }
        public string BranchName { get; set; }
        public int Schedule_ID { get; set; }
        public int CategoryId { get; set; }
        public int SponsorCategoryId { get; set; }
        public int NodalCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SentBy { get; set; }
        public string ReceivedOn { get; set; }
        public string FwdTo { get; set; }
        public string CCToText { get; set; }
        public string FwdToRemark { get; set; }
        public string CCToTextRemark { get; set; }
        public string SentOn { get; set; }
        public string SentTo { get; set; }
        public string SponsorCategoryName { get; set; }
        public int sponsorId { get; set; }
        public string NodalCategoryName { get; set; }
        public int Status { get; set; }
        public string Upload { get; set; }
        public string UploadPath { get; set; }
        public string WordFile { get; set; }
        public string WordFilePath { get; set; }
        public Boolean FinalSubmitYN { get; set; }
        public Boolean IsView { get; set; }
        public Boolean IsVisibleToAll { get; set; }
        public Boolean AllowEdit { get; set; }
        public int NodalViewId { get; set; }
        public string FromInbox { get; set; }
        public string Comment { get; set; }
        public string RefLetter { get; set; }
        public string SentID { get; set; }
        public int NodalId { get; set; }
        public string CloseIds { get; set; }
        public string NodalComment { get; set; }
        public string FwdComment { get; set; }
        public string Attachment { get; set; }
        public int CloseId { get; set; }
        public int OpenId { get; set; }
        public int ForwardID { get; set; }
        public string ShortValue { get; set; }
        public int Ord { get; set; }
        public int FwdId { get; set; }
        public int IsRead { get; set; }
        public Boolean RequestForUnfreeze { get; set; }
        public IList<APUserLog> IAPUserLog { get; set; }
    }


    public class CounterLog : AA7Common
    {

        public string ComdName { get; set; }
        public int Today { get; set; }
        public int CurrentMonth { get; set; }
        public int Total { get; set; }
        public string UserName { get; set; }
        public IList<CounterLog> ICounterLog { get; set; }
    }

    public class UserWiseRole
    {
        public string Id { get; set; }
        public string Role { get; set; }
    }

  

}