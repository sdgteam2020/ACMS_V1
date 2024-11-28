using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.WebApp.Connection;

namespace IHSDC.WebApp.Models
{
    public class Common
    {
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int32 UserId { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsExist { get; set; }
        public string Msg { get; set; }
        public string MsgStatus { get; set; }
        public string MidMsg { get; set; }
        public int Orderby { get; set; }
        public int NewOrderby { get; set; }
    }

    public class Rank
    {
        public int RankId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string RankName { get; set; }

        public string RankAbbreviation { get; set; }

        public IList<Rank> ILRankName { get; set; }
    }

    public class SearchData : Common
    {
        public int Id { get; set; }
        public string paragraph { get; set; }
        public string FileName { get; set; }

    }

    public class PolicyCRUD : Common
    {
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string PolicyName { get; set; }
        public string PolicyRemarks { get; set; }
        public string Upload { get; set; }
        public int TypeOfDocu { get; set; }
        public string TypeOfDocuName { get; set; }
        public string PolicyFolder { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public int PUserId { get; set; }
        public int UserPolicyControlId { get; set; }
        public int UpdatedByUserId { get; set; }
        public IList<PolicyCRUD> ILPolicyCRUD { get; set; }
        public IList<SearchableItem> ISearchableItem { get; set; }
    }

    public class ComdCRUD : Common
    {
        public int ComdId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string ComdName { get; set; }
        public IList<ComdCRUD> ILComdCRUD { get; set; }
    }


    #region Folder Control Permission [04-07-2024] 

    public class UserCRUD : Common
    {
        public int PolicyId { get; set; }
        public int PUserId { get; set; }
        public string UserName { get; set; }
        public int FolderControlId { get; set; }
        public string PolicyName { get; set; }
        public int UpdatedByUserId { get; set; }
        public IList<UserCRUD> ILUserCRUD { get; set; }
        // public IList<SearchableItem> ISearchableItem { get; set; }
    }

    public class PolicyDropdownItem
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public int UpdatedByUserId { get; set; }
        public string Msg { get; set; }
    }

    public class FolderControlPermissionCRUD : Common
    {
        public int FolderControlId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }

        public string RefLetter { get; set; }

        public string Users { get; set; }

        public string UsersName { get; set; }

        public int UpdatedByUserId { get; set; }

        public IList<FolderControlPermissionCRUD> ILFolderControlPermissionCRUD { get; set; }
        public IList<UserCRUD> ILFolderUserCRUD { get; set; }
    }


    #endregion

    public class CategoryCRUD : Common
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string CategoryName { get; set; }

        public int ScheduleId { get; set; }
        public IList<CategoryCRUD> ILCategoryCRUD { get; set; }
    }


    public class BranchCRUD : Common
    {
        public int BranchId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BranchName { get; set; }
        public int ScheduleId { get; set; }
        public IList<BranchCRUD> ILBranchCRUD { get; set; }
    }


    public class Appointment
    {
        public int ApptId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Appt { get; set; }
        public string ApptAbbreviation { get; set; }
        public IList<Appointment> IAppt { get; set; }
    }
    public class SqnCRUD : Common
    {
        public int SqnId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string SqnName { get; set; }

        public int ComdId { get; set; }
        public string ComdName { get; set; }
        public int CorpsId { get; set; }
        public string CorpsName { get; set; }
        public int BdeCatId { get; set; }
        public string BdeCatName { get; set; }
        public IList<SqnCRUD> ILSqnCRUD { get; set; }
    }


    public class BdeCatCRUD : Common
    {
        public int BdeCatId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BdeCatName { get; set; }

        public int ComdId { get; set; }
        public string ComdName { get; set; }
        public int CorpsId { get; set; }
        public string CorpsName { get; set; }
        public IList<BdeCatCRUD> ILBdeCatCRUD { get; set; }
    }

    public class RankCRUD : Common
    {
        public int RankId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string RankName { get; set; }

        public string RankAbbreviation { get; set; }
        public IList<RankCRUD> ILRankCRUD { get; set; }
    }

    public class CorpsCRUD : Common
    {
        public int CorpsId { get; set; }
        // [Required(ErrorMessage = "required!")]
        // [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string CorpsName { get; set; }
        // [Required(ErrorMessage = "required!")]
        // [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public int ComdId { get; set; }
        public string ComdName { get; set; }
        public IList<CorpsCRUD> ILCorpsCRUD { get; set; }
    }


    public class HitCRUD 
    {
        public string ComdName { get; set; }
        public int Today { get; set; }
        public int CurrentMonth { get; set; }
        public int Total { get; set; }
    }

    public class UnitCRUD : Common
    {
        public int Unit_ID { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string UnitName { get; set; }

        //[Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BdeCat { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Corps { get; set; }

        public int SqnId { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Sqn { get; set; }


        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string TypeOfUnit { get; set; }

        //[Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Command { get; set; }
        public int ComdId { get; set; }

        public int ComdIdUnit { get; set; }
        public int BdeCatId { get; set; }
        public int CorpsId { get; set; }
        public int CorpId { get; set; }


        public int IsFlight { get; set; }
        public int SentId { get; set; }
        public string SentName { get; set; }
        public IList<UnitCRUD> ILUnitCRUD { get; set; }
        public string id { get; set; }
        public int ScheduleId { get; set; }
        public int InboxID { get; set; }
        public string ComdName { get; set; }
        public int Today { get; set; }
        public int CurrentMonth { get; set; }
        public int Total { get; set; }
    }


    public class UserListCRUD : Common
    {
        public int PUserId { get; set; }
        public int Unit_ID { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string UnitName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BdeCat { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Corps { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string TypeOfUnit { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Command { get; set; }

        public IList<UserListCRUD> ILUserListCRUD { get; set; }
    }



    public class user
    {
        public int IntId { get; set; }
        public string UserName { get; set; }
        public IList<user> GetUser { get; set; }
    }


    public class TypeofConfCRUD : Common
    {
        public int TypeOfConfId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string TypeOfConf { get; set; }
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public IList<TypeofConfCRUD> ILTypeofConfCRUD { get; set; }
    }


    public class UserPolicyControlCRUD : Common
    {
        public int UserPolicyControlId { get; set; }
        public string Policies { get; set; }
        public int PUserId { get; set; }
        public string UserName { get; set; }
        public string PolicyName { get; set; }
        public IList<UserPolicyControlCRUD> ILUserPolicyControlCRUD { get; set; }
        public IList<PolicyCRUD> ILUserPolicyCRUD { get; set; }
    }

    public class downloadLog : Common
    {
        public int downloadby { get; set; }
        public int PolicyId { get; set; }
        public string FileName { get; set; }
        public string ICNumber { get; set; }
        public string IpAddress { get; set; }
        public string downloadUser { get; set; }
        public string downloadDate { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public string AccessDownload { get; set; }
        public IList<downloadLog> ILdownloadLogCRUD { get; set; }

    }

    public class GenderCRUD : Common
    {
        public int GenderId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string Gender { get; set; }
        public IList<GenderCRUD> ILGenderCRUD { get; set; }
    }


    public class StagesGroundAccidentCRUD : Common
    {
        public int StagesGroundAccidentId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string StagesGroundAccident { get; set; }
        public IList<StagesGroundAccidentCRUD> ILStagesGroundAccidentCRUD { get; set; }
    }

    public class PeriodOperationCRUD : Common
    {
        public int PeriodOperationId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string PeriodOperation { get; set; }
        public IList<PeriodOperationCRUD> ILPeriodOperationCRUD { get; set; }
    }




    public class PostingTypeCRUD : Common
    {
        public int PostingTypeId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string PostingType { get; set; }
        public IList<PostingTypeCRUD> ILPostingTypeCRUD { get; set; }
    }


    public class PlaceMedicalCRUD : Common
    {
        public int PlaceMedicalId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string PlaceMedical { get; set; }
        public IList<PlaceMedicalCRUD> ILPlaceMedicalCRUD { get; set; }
    }

    public class TypeMedicalCRUD : Common
    {
        public int TypeMedicalId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string TypeMedical { get; set; }
        public IList<TypeMedicalCRUD> ILTypeMedicalCRUD { get; set; }
    }

    public class MedicalCatCRUD : Common
    {
        public int MedicalCatId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string MedicalCat { get; set; }
        public IList<MedicalCatCRUD> ILMedicalCatCRUD { get; set; }
    }

    public class ApptCRUD : Common
    {
        public int ApptId { get; set; }
        [Required(ErrorMessage = "required!")]
        // [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string Appt { get; set; }
        public string ApptAbbreviation { get; set; }
        public IList<ApptCRUD> ILApptCRUD { get; set; }
    }


    public class FlyingStatusCRUD : Common
    {
        public int FlyingStatusId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string FlyingStatus { get; set; }
        public IList<FlyingStatusCRUD> ILFlyingStatusCRUD { get; set; }
    }

    public class RankHistory : Common
    {
        public int Aviator_Id { get; set; }
        public int SRNo { get; set; }
        public string RankName { get; set; }
        public string RankDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }

    public class ApptHistory : Common
    {
        public int Aviator_Id { get; set; }
        public int SRNo { get; set; }
        public string Appt { get; set; }
        public string ApptDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string LastUpdateBy { get; set; }
    }



    public class SpecialQualStatusCRUD : Common
    {
        public int SpecialQualStatusId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]
        public string SpecialQualStatus { get; set; }
        public IList<SpecialQualStatusCRUD> ILSpecialQualStatusCRUD { get; set; }
    }


    public class InstrCATCRUD : Common
    {
        public int InstrCATId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string InstrCAT { get; set; }
        public IList<InstrCATCRUD> ILInstrCATCRUD { get; set; }
    }




    public class AircraftTypeCRUD : Common
    {
        public int AircraftTypeId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]

        public string AircraftType { get; set; }
        public string AircraftTypeGet { get; set; }
        public IList<AircraftTypeCRUD> ILAircraftTypeCRUD { get; set; }
    }

    public class FlgWithApptCRUD : Common
    {
        public int FlgWithApptId { get; set; }
        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9-,)(>< ]*$", ErrorMessage = "Only Alphabets, Numbers and brackets allowed.")]
        public string ApptName { get; set; }
        public IList<FlgWithApptCRUD> ILFlgWithApptCRUD { get; set; }
    }

    public class PolicyFolderCRUD : Common
    {
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "required!")]
        [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string PolicyName { get; set; }
        public string PolicyRemarks { get; set; }
        public string Upload { get; set; }
        public int TypeOfDocu { get; set; }
        public string TypeOfDocuName { get; set; }
        public string PolicyFolder { get; set; }
        public string LogFromDate { get; set; }
        public string LogToDate { get; set; }
        public int PUserId { get; set; }
        public int UserPolicyControlId { get; set; }
        public int UpdatedByUserId { get; set; }
        public IList<PolicyFolderCRUD> ILPolicyFolderCRUD { get; set; }
        //   public IList<SearchableItem> ISearchableItem { get; set; }
    }



    public class CommandId
    {
        public string ComdId { get; set; }
        public string CommandName { get; set; }

    }


    public class MasterModels
    {
        public SelectList LoadCommandId()
        {
            DBConnection con = new DBConnection();
            CommandId model = new CommandId();
            return new SelectList(con.CommandId(), "ComdId", "CommandName");
        }

        //Folder Control Permission [04-07-2024]       
        public SelectList LoadFolders()
        {
            DBConnection con = new DBConnection();
            user model = new user();
            var data = con.GetDataInList<UserCRUD>(@"proc_dbuser @PUserId='" + Convert.ToInt32(SessionManager.UserIntId) + "'  ").ToList();
            return new SelectList(data, "PolicyId", "PolicyName");
        }

        public SelectList LoadRanks()
        {
            DBConnection con = new DBConnection();
            Rank model = new Rank();
            var data = con.GetDataInList<Rank>("SELECT [RankId],[RankName] FROM [dbo].[RankMaster] WHERE  IsActive = 1  AND IsDeleted = 0 ORDER BY [RankOder]  asc").ToList();

            return new SelectList(data, "RankId", "RankName");
        }

        public SelectList LoadCorpsAll()
        {
            DBConnection con = new DBConnection();
            CorpsCRUD model = new CorpsCRUD();

            return new SelectList(con.CorpsCRUD(6, model), "CorpsId", "CorpsName");
        }
        public SelectList LoadBdeCATAll()
        {
            DBConnection con = new DBConnection();
            BdeCatCRUD model = new BdeCatCRUD();

            return new SelectList(con.BdeCatCRUD(6, model), "BdeCatId", "BdeCatName");
        }
        public SelectList LoadSqnAll()
        {
            DBConnection con = new DBConnection();
            SqnCRUD model = new SqnCRUD();
            return new SelectList(con.SqnCRUD(6, model), "SqnId", "SqnName");
        }

        public class UserType
        {
            public int UserTypeId { get; set; }
            [Required(ErrorMessage = "required!")]
            [RegularExpression("^[a-zA-Z0-9 ()-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
            public string Name { get; set; }
            public IList<UserType> IUserType { get; set; }
        }

        public SelectList LoadAppointment()
        {
            DBConnection con = new DBConnection();
            Appointment model = new Appointment();
            //var data = con.GetDataInList<Appointment>("SELECT [ApptId],[Appt] FROM [dbo].[Tbl_Appt] WHERE  IsActive = 1  AND IsDeleted = 0 order by Appt").ToList();
            var data = con.GetDataInList<Appointment>("SELECT [ApptId],[Appt] FROM [dbo].[Tbl_Appt] WHERE  IsActive = 1  AND IsDeleted = 0 order by orderby ASC").ToList();

            return new SelectList(data, "ApptId", "Appt");
        }
        public SelectList LoadUserType()
        {
            DBConnection con = new DBConnection();
            UserType model = new UserType();
            var data = con.GetDataInList<UserType>("SELECT [UserTypeId],[Name] FROM [dbo].[tbl_UserType] WHERE  IsActive = 1  AND IsDeleted = 0 ORDER BY [UserOder]  asc").ToList();

            return new SelectList(data, "UserTypeId", "Name");
        }
        public SelectList LoadAllUsers()
        {
            DBConnection con = new DBConnection();
            user model = new user();
            var data = con.GetDataInList<UserListCRUD>("select IntId as PUserId,UserName from AspNetUsers AU where id in (select UserId from AspNetUserRoles where RoleId in (select id from AspNetRoles where Name !='Administrator'))").ToList();
            return new SelectList(data, "PUserId", "UserName");
        }



        public SelectList LoadYesNo()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="YES",Value="YES"},
                new SelectListItem{Text="NO",Value="NO"}

            }, "Value", "Text");
        }


        public SelectList LoadAircraftClassMaster()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Single Engine",Value="S"},
                new SelectListItem{Text="Twin Engine",Value="T"}
            }, "Value", "Text");
        }

        public SelectList LoadAppearedWith()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="CEB",Value="C"},
                new SelectListItem{Text="GEB",Value="G"}
            }, "Value", "Text");
        }
        

        public SelectList LoadUnitName()
        {
            DBConnection con = new DBConnection();
            UnitCRUD model = new UnitCRUD();
            return new SelectList(con.UnitCRUD(2, model), "Unit_ID", "UnitName");
        }
        public SelectList LoadUnit(int ComdId, int CorpsId, int BdeId)
        {
            DBConnection con = new DBConnection();
            UnitCRUD model = new UnitCRUD();
            model.ComdId = ComdId;
            model.CorpsId = CorpsId;
            model.BdeCatId = BdeId;
            return new SelectList(con.GetAllUnitCRUD(15, model), "Unit_ID", "UnitName");
        }


        public SelectList LoadUnitUsersList()
        {
            DBConnection con = new DBConnection();
            UserListCRUD model = new UserListCRUD();

            return new SelectList(con.UserListCRUD(5, model), "UnitName", "UnitName");
        }


        public SelectList LoadUserList(String UnitName)
        {
            DBConnection con = new DBConnection();
            UserListCRUD model = new UserListCRUD();
            model.UnitName = Convert.ToString(UnitName);
            return new SelectList(con.UserListCRUD(6, model), "UserName", "UserName");
        }




        public SelectList LoadGender()
        {
            DBConnection con = new DBConnection();
            GenderCRUD model = new GenderCRUD();
            return new SelectList(con.GenderCRUD(2, model), "GenderId", "Gender");
        }


        public SelectList LoadStagesGroundAccident()
        {
            DBConnection con = new DBConnection();
            StagesGroundAccidentCRUD model = new StagesGroundAccidentCRUD();
            return new SelectList(con.StagesGroundAccidentCRUD(2, model), "StagesGroundAccidentId", "StagesGroundAccident");
        }


        public SelectList LoadPeriodOperation()
        {
            DBConnection con = new DBConnection();
            PeriodOperationCRUD model = new PeriodOperationCRUD();
            return new SelectList(con.PeriodOperationCRUD(2, model), "PeriodOperationId", "PeriodOperation");
        }




        public SelectList LoadFlyingStatus()
        {
            DBConnection con = new DBConnection();
            FlyingStatusCRUD model = new FlyingStatusCRUD();
            return new SelectList(con.FlyingStatusCRUD(2, model), "FlyingStatusId", "FlyingStatus");
        }



        public SelectList LoadCorpsName()
        {
            DBConnection con = new DBConnection();
            CorpsCRUD model = new CorpsCRUD();
            return new SelectList(con.CorpsCRUD(2, model), "CorpsId", "CorpsName");
        }
        public SelectList LoadCorpsNameByCommandId(int ComdId)
        {
            DBConnection con = new DBConnection();
            CorpsCRUD model = new CorpsCRUD();
            model.ComdId = ComdId;
            return new SelectList(con.CorpsCRUD(5, model), "CorpsId", "CorpsName");
        }

        public SelectList LoadBdeCAT(int ComdId, int CorpsId)
        {
            DBConnection con = new DBConnection();
            BdeCatCRUD model = new BdeCatCRUD();
            model.ComdId = ComdId;
            model.CorpsId = CorpsId;
            return new SelectList(con.BdeCatCRUD(5, model), "BdeCatId", "BdeCatName");
        }
        public SelectList LoadSqnName()
        {
            DBConnection con = new DBConnection();
            SqnCRUD model = new SqnCRUD();
            return new SelectList(con.SqnCRUD(2, model), "SqnId", "SqnName");
        }
        public SelectList LoadGetSQNId(int ComdId, int CorpsId, int BdeCatId)
        {
            DBConnection con = new DBConnection();
            SqnCRUD model = new SqnCRUD();
            model.ComdId = ComdId;
            model.CorpsId = CorpsId;
            model.BdeCatId = BdeCatId;
            return new SelectList(con.SqnCRUD(5, model), "SqnId", "SqnName");
        }

        public SelectList LoadBdeCatName()
        {
            DBConnection con = new DBConnection();
            BdeCatCRUD model = new BdeCatCRUD();
            return new SelectList(con.BdeCatCRUD(2, model), "BdeCatId", "BdeCatName");
        }

        public SelectList CommadOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            ComdCRUD model = new ComdCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.ComdCRUD(5, model), "ComdId", "ComdName");
        }
        public SelectList PolicyOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            PolicyCRUD model = new PolicyCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.PolicyCRUD(5, model), "PolicyId", "PolicyName");
        }

        public SelectList LoadPolicy()
        {
            DBConnection con = new DBConnection();
            PolicyCRUD model = new PolicyCRUD();
            return new SelectList(con.PolicyCRUD(2, model), "PolicyId", "PolicyName");
        }

        public SelectList LoadResPolicy()
        {
            DBConnection con = new DBConnection();
            PolicyCRUD model = new PolicyCRUD();
            return new SelectList(con.PolicyCRUD(8, model), "PolicyId", "PolicyName");
        }

        public SelectList CorpsOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            CorpsCRUD model = new CorpsCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.CorpsCRUD(7, model), "ComdId", "Name");
        }
        public SelectList bdeOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            BdeCatCRUD model = new BdeCatCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.BdeCatCRUD(7, model), "ComdId", "Name");
        }
        public SelectList sqnOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            SqnCRUD model = new SqnCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.SqnCRUD(7, model), "ComdId", "Name");
        }


        public SelectList BranchOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            BranchCRUD model = new BranchCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.BranchCRUD(6, model), "BranchId", "BranchName");
        }

        public SelectList ApptOrderby(int OrderbyId,int NewOrderbyId)
        {
            DBConnection con = new DBConnection();
            ApptCRUD model = new ApptCRUD();
            model.Orderby = OrderbyId;
            model.NewOrderby = NewOrderbyId;
            return new SelectList(con.ApptCRUD(5, model), "ApptId", "Appt");
        }

        public SelectList CATOrderby(int OrderbyId)
        {
            DBConnection con = new DBConnection();
            CategoryCRUD model = new CategoryCRUD();
            model.Orderby = OrderbyId;
            return new SelectList(con.CategoryCRUD(10, model), "CategoryId", "CategoryName");
        }
        

        public SelectList LoadCommandName()
        {
            DBConnection con = new DBConnection();
            ComdCRUD model = new ComdCRUD();
            return new SelectList(con.ComdCRUD(2, model), "ComdId", "ComdName");
        }

        public SelectList LoadSchedule(int ClosedYN)
        {
            DBConnection con = new DBConnection();
            ScheduleLetter model = new ScheduleLetter();
            model.isClosed = ClosedYN;
            return new SelectList(con.ScheduleLetter_CRUD(3, model), "ScheduleId", "Subject");
        }


        //public SelectList LoadScheduleSearch(int ClosedYN)
        //{
        //    DBConnection con = new DBConnection();
        //    ScheduleLetter model = new ScheduleLetter();
        //    model.isClosed = ClosedYN;
        //    return new SelectList(con.CreateAPCRUD(4, model), "ScheduleId", "Subject");
        //}

        public SelectList LoadTypeOfConf()
        {
            DBConnection con = new DBConnection();
            TypeofConfCRUD model = new TypeofConfCRUD();
            return new SelectList(con.TypeofConfCRUD(6, model), "TypeOfConfId", "TypeOfConf");
        }

        public SelectList LoadNodal(int ScheduleId, int InboxID)
        {
            DBConnection con = new DBConnection();
            UnitCRUD model = new UnitCRUD();
            model.ScheduleId = ScheduleId;
            model.InboxID = InboxID;
            return new SelectList(con.UnitCRUD(12, model), "SentID", "SentName");
        }

        public SelectList LoadTypeOfDocu()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Value="1",Text="Restricted"},
                new SelectListItem{Value="2",Text="Confidential"},
            }, "Value", "Text");
        }

        public SelectList LoadCategory()
        {
            DBConnection con = new DBConnection();
            CategoryCRUD model = new CategoryCRUD();
            return new SelectList(con.CategoryCRUD(1, model), "CategoryId", "CategoryName");
        }

        public SelectList LoadBranch()
        {
            DBConnection con = new DBConnection();
            BranchCRUD model = new BranchCRUD();
            return new SelectList(con.BranchCRUD(1, model), "BranchId", "BranchName");
        }
        public SelectList LoadReportType()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Value="1",Text="Appx A"},
                new SelectListItem{Value="2",Text="Appx B"},
            }, "Value", "Text");
        }
        //public SelectList LoadCategory()
        //{
        //    return new SelectList(new SelectListItem[]{
        //        new SelectListItem{Value="1",Text="Command Session"},
        //        new SelectListItem{Value="2",Text="MS Day"},
        //        new SelectListItem{Value="3",Text="Strat Day"},
        //        new SelectListItem{Value="4",Text="PSO Session / Update"},
        //        new SelectListItem{Value="5",Text="On File / Mutually Resolved"},
        //        new SelectListItem{Value="6",Text="Closed / Resolved"},
        //        new SelectListItem{Value="7",Text="Dropped"},
        //    }, "Value", "Text");
        //}


        //public SelectList LoadRanks()
        //{
        //    return new SelectList(new SelectListItem[]{

        //        new SelectListItem{Text="LT",Value="Lt"},
        //        new SelectListItem{Text="CAPT",Value="Capt"},
        //        new SelectListItem{Text="MAJ",Value="Maj"},
        //         new SelectListItem{Text="LT COL",Value="Lt Col"},
        //        new SelectListItem{Text="COL",Value="Col"},
        //        new SelectListItem{Text="BRIG",Value="Brig"},
        //        new SelectListItem{Text="MAJ GEN",Value="Maj Gen"},
        //        new SelectListItem{Text="LT GEN",Value="Lt Gen"},
        //         new SelectListItem{Text="GEN",Value="Gen"}

        //    }, "Value", "Text");
        //}

        public SelectList LoadResults()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Upgraded",Value="Upgraded"},
                new SelectListItem{Text="Downgraded",Value="Downgraded"},
                new SelectListItem{Text="Standardised",Value="Standardised"},
                 new SelectListItem{Text="Not Awarded",Value="Not Awarded"},
                new SelectListItem{Text="To reappear",Value="To reappear"},


            }, "Value", "Text");
        }

        public SelectList LoadAppxType()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Value="Appx C",Text="Appx C"},
                new SelectListItem{Value="Appx D",Text="Appx D"},
                new SelectListItem{Value="Appx E",Text="Appx E"},
                 new SelectListItem{Value="Appx F",Text="Appx F"},
                new SelectListItem{Value="Appx G",Text="Appx G"}

            }, "Value", "Text");
        }
        public SelectList LoadLocation()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Value="Peace",Text="Peace"},
                new SelectListItem{Value="Field",Text="Field"},


            }, "Value", "Text");
        }




        public SelectList LoadSuffixLetterNumber()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Value="A",Text="A"},
                new SelectListItem{Value="F",Text="F"},
                new SelectListItem{Value="H",Text="H"},
                new SelectListItem{Value="K",Text="K"},
                new SelectListItem{Value="L",Text="L"},
                new SelectListItem{Value="M",Text="M"},
                new SelectListItem{Value="N",Text="N"},
                new SelectListItem{Value="P",Text="P"},
                  new SelectListItem{Value="W",Text="W"},
                new SelectListItem{Value="X",Text="X"},
               new SelectListItem{Value="Y",Text="Y"}

            }, "Value", "Text");
        }

        public SelectList LoadANumber()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Value="IC",Text="IC"},
                new SelectListItem{Value="SS",Text="SS"},
                new SelectListItem{Value="SL",Text="SL"},
                new SelectListItem{Value="MS",Text="MS"},
                  new SelectListItem{Value="SC",Text="SC"},
                    new SelectListItem{Value="WS",Text="WS"},

            }, "Value", "Text");
        }

        //public SelectList LoadMedicalStatus()
        //{
        //    return new SelectList(new SelectListItem[]{
        //        new SelectListItem{Value="",Text="--Select Medical Status"},
        //        new SelectListItem { Text = "NA", Value = "NA" },
        //        new SelectListItem{Value="Flying Fit",Text="Flying Fit"},
        //        new SelectListItem{Value="Flying Unfit",Text="Flying Unfit"},
        //        new SelectListItem{Value="Flying Temporarily Fit",Text="Flying Temporarily Fit"},
        //        new SelectListItem{Value="Flying Temporarily UnFit",Text="Flying Permanently UnFit"},
        //        new SelectListItem{Value="Fit for CoPilot Duties",Text="Fit for CoPilot Duties"}
        //    }, "Value", "Text");
        //}

        //public SelectList LoadTypeofMedical()
        //{
        //    return new SelectList(new SelectListItem[]{
        //      new SelectListItem() {Text = "--Select--", Value=""},
        //       new SelectListItem { Text = "NA", Value = "NA" },
        //       new SelectListItem() {Text = "AME", Value="AME"},
        //       new SelectListItem() {Text = "PME", Value="PME"},
        //        new SelectListItem() {Text = "SME", Value="SME"},

        //    }, "Value", "Text");
        //}



        public SelectList LoadAircraft()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Value="1",Text="CTH"},
                new SelectListItem{Value="2",Text="CTK"},
                new SelectListItem{Value="3",Text="CTL"},
                 new SelectListItem{Value="4",Text="ALH MK1"},
                new SelectListItem{ Value="5",Text="ALH MK2"},
                 new SelectListItem{Value="6",Text="ALH MK3"},
                new SelectListItem{Value="7",Text="ALH MK4"},
                new SelectListItem{Value="8",Text="ALH - WSI"},
                 new SelectListItem{Value="9",Text="CTHH"},
                new SelectListItem{Value="10",Text="KA226T"}

            }, "Value", "Text");
        }




        public SelectList LoadNokRelationship()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Text="SPOUSE",Value="SPOUSE"},
                new SelectListItem{Text="FATHER",Value="FATHER"},
                new SelectListItem{Text="MOTHER",Value="MOTHER"},
                new SelectListItem{Text="BROTHER",Value="BROTHER"},
                new SelectListItem{Text="SISTER",Value="SISTER"},
                new SelectListItem{Text="SON",Value="SON"},
                new SelectListItem{Text="DAUGHTER",Value="DAUGHTER"},
                  new SelectListItem{Text="GRAND FATHER",Value="GRAND FATHER"},
                new SelectListItem{Text="GRAND MOTHER",Value="GRAND MOTHER"}

            }, "Value", "Text");
        }

        public SelectList LoadMartialStatus()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Text="SINGLE",Value="SINGLE"},
                new SelectListItem{Text="MARRIED",Value="MARRIED"},
                  new SelectListItem{Text="SEPARATED",Value="SEPARATED"},
                   new SelectListItem{Text="DIVORCED",Value="DIVORCED"},
                    new SelectListItem{Text="WIDOWER",Value="WIDOWER"}


            }, "Value", "Text");
        }


        public SelectList LoadApptName()
        {
            return new SelectList(new SelectListItem[]{
               new SelectListItem{Text="Flt Cdr",Value="Flt Cdr"},
                new SelectListItem{Text="2IC",Value="2IC"},
                  new SelectListItem{Text="Adjut",Value="Adjut"},
                    new SelectListItem{Text="QM",Value="QM"},
                      new SelectListItem{Text="Sec Cdr",Value="Sec Cdr"},
                        new SelectListItem{Text="SATCO",Value="SATCO"}


            }, "Value", "Text");
        }
        public SelectList LoadHillflyingQualification()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="YES",Value="YES"},
                new SelectListItem{Text="NO",Value="NO"}

            }, "Value", "Text");
        }
        public SelectList LoadPermission()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Text="Edit/View",Value="1"},
                new SelectListItem{Text="View",Value="2"},
                new SelectListItem{Text="Unit Level Edit",Value="3"},
                new SelectListItem{Text="Unit Level View",Value="4"},
                 new SelectListItem{Text="Avn Dte",Value="5"},
            }, "Value", "Text");
        }

        public SelectList LoadAttackHelicopter()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="YES",Value="YES"},
                new SelectListItem{Text="NO",Value="NO"}

            }, "Value", "Text");
        }
        public SelectList LoadHillflyingHeightClearance()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="UPTO 5000",Value="UPTO 5000"},
                new SelectListItem{Text="5000 TO 10000",Value="5000 TO 10000"},
                new SelectListItem{Text="10000 TO 15000",Value="10000 TO 15000"},
                new SelectListItem{Text="15000 TO 17000",Value="15000 TO 17000"},
                new SelectListItem{Text="ABOVE 17000",Value="ABOVE 17000"}

            }, "Value", "Text");
        }

        public SelectList LoadTypeOfFlight()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="NORMAL",Value="NORMAL"},
                new SelectListItem{Text="INDEPENDENT",Value="INDEPENDENT"}

            }, "Value", "Text");
        }
        public SelectList LoadDeckLanding()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="YES",Value="YES"},
                new SelectListItem{Text="NO",Value="NO"}

            }, "Value", "Text");
        }
        public SelectList LoadMonth()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Text="JAN",Value="1"},
                new SelectListItem{Text="FEB",Value="2"},
                new SelectListItem{Text="MAR",Value="3"},
                new SelectListItem{Text="APR",Value="4"},
                new SelectListItem{Text="MAY",Value="5"},
                new SelectListItem{Text="JUN",Value="6"},
                new SelectListItem{Text="JUL",Value="7"},
                new SelectListItem{Text="AUG",Value="8"},
                new SelectListItem{Text="SEP",Value="9"},
                new SelectListItem{Text="OCT",Value="10"},
                new SelectListItem{Text="NOV",Value="11"},
                 new SelectListItem{Text="DEC",Value="12"}

            }, "Value", "Text");
        }
        public SelectList LoadYear()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="2000",Value="2000"},
                new SelectListItem{Text="2001",Value="2001"}

            }, "Value", "Text");
        }


      

        public SelectList LoadBlameWorthy()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
                new SelectListItem{Text="YES",Value="YES"},
                new SelectListItem{Text="NO",Value="NO"}

            }, "Value", "Text");
        }

        public SelectList LoadAppearingFor()
        {
            return new SelectList(new SelectListItem[]{

            new SelectListItem { Text = "Not Appeared", Value = "Not Appeared" },
            new SelectListItem { Text = "AMG", Value = "A MASTER GREEN" },
            new SelectListItem { Text = "BMG", Value = "B MASTER GREEN" },
            new SelectListItem { Text = "BG", Value = "B GREEN" },
            new SelectListItem { Text = "CG", Value = "C GREEN" },
            new SelectListItem { Text = "CW Rec G", Value = "CW REC G" },
             new SelectListItem { Text = "CW", Value = "CW" },
             new SelectListItem { Text = "D Rec CW", Value = "D REC CW" },
            new SelectListItem { Text = "DW", Value = "D WHITE" },
            new SelectListItem { Text = "CR", Value = "CR" },
             new SelectListItem { Text = "CT", Value = "CT" },


        }
        , "Value", "Text");
        }

        public SelectList LoadCatResult()
        {
            return new SelectList(new SelectListItem[]{
            new SelectListItem { Text = "AMG", Value = "A MASTER GREEN" },
            new SelectListItem { Text = "BMG", Value = "B MASTER GREEN" },
            new SelectListItem { Text = "CG", Value = "C GREEN" },
            new SelectListItem { Text = "BG", Value = "B GREEN" },
            new SelectListItem { Text = "DW", Value = "D WHITE" },
            new SelectListItem { Text = "D Rec CW", Value = "D REC CW" },
            new SelectListItem { Text = "CW", Value = "CW" },
            new SelectListItem { Text = "CW Rec G", Value = "CW REC G" },
            new SelectListItem { Text = "CR", Value = "CR" },
             new SelectListItem { Text = "CT", Value = "CT" },
            new SelectListItem { Text = "INITIAL CAT", Value = "INITIAL CAT" },

        }
        , "Value", "Text");
        }

        public SelectList LoadQFI()
        {
            return new SelectList(new SelectListItem[]{

            new SelectListItem { Text = "JAI I", Value = "JAI I" },
      new SelectListItem { Text = "SAI I", Value = "SAI I" },
        new SelectListItem { Text = "SAI II", Value = "SAI II" },
        new SelectListItem { Text = "MAI", Value = "MAI" },


          }
        , "Value", "Text");
        }



        public SelectList LoadAviatorCol()
        {
            return new SelectList(new SelectListItem[]{
          new SelectListItem { Text = "Army Number", Value = "ArmyNumber" },
          new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
          new SelectListItem { Text = "Rank", Value = "R.RankName" },
           new SelectListItem { Text = "Arm Service", Value = "d.ArmServiceName" },
          new SelectListItem { Text = "Gender", Value = "G.Gender" },
           //new SelectListItem { Text = "Civ Edn", Value = "CE.CivEdn" },
           // new SelectListItem { Text = "Date of Seniority", Value = "DateOfSeniority" },
             new SelectListItem { Text = "Date of Rank", Value = "DateOfRank" },

          //new SelectListItem { Text = "Last Name", Value = "LastName" },
           //new SelectListItem { Text = "Gender_ID", Value = "Gender_ID" },
             //new SelectListItem { Text = "First Name", Value = "FirstName" },
            //new SelectListItem { Text = "Appt_ID", Value = "Appt_ID" },
          //new SelectListItem { Text = " Aircraft Name  ", Value = "AircraftName" },
          //new SelectListItem { Text = "DateOfBirth", Value = "DateOfBirth" },
          //new SelectListItem { Text = "DateOfWings", Value = "DateOfWings" },
          //new SelectListItem { Text = "CatCardNo", Value = "CatCardNo" },
          //new SelectListItem { Text = "DateofIssueCatCard", Value = "DateofIssueCatCard" },

          }
        , "Value", "Text");
        }
        public SelectList LoadContainsCol()
        {
            return new SelectList(new SelectListItem[]{
              new SelectListItem { Text = "Contains", Value = "Contains" },
               new SelectListItem { Text = "Equal", Value = "=" },
              new SelectListItem { Text = "Greater Than", Value = ">" },
              new SelectListItem { Text = "Less Than", Value = "<" },
              new SelectListItem { Text = "Greater Than Equal", Value = ">=" },
              new SelectListItem { Text = "Less Than Equal", Value = "<=" },
          }
        , "Value", "Text");
        }

        public SelectList LoadAViatorCourseCol()
        {
            return new SelectList(new SelectListItem[]{
                 new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Course Name", Value = "a.CourseName" },
               new SelectListItem { Text = "Course Serial Number", Value = "c.CourseSerialNumber" },
              new SelectListItem { Text = "Course Star tDate", Value = "CourseStartDate" },
              new SelectListItem { Text = "Course End Date", Value = "CourseEndDate" },
             
              //new SelectListItem { Text = "Course Grading", Value = "CourseGrading" },
              //new SelectListItem { Text = "Instructor Grading", Value = "InstructorGrading" },
              //new SelectListItem { Text = "Special Award", Value = "Special_Award" },
              //new SelectListItem { Text = "Notes", Value = "Notes" },
          }
        , "Value", "Text");
        }

        public SelectList LoadAviatorHeptrRPACol()
        {
            return new SelectList(new SelectListItem[]{
               new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Aircraft Type", Value = "a.AircraftType" },
              new SelectListItem { Text = "Aircraft", Value = "HeptrRPAMaster.AircraftName" },
               new SelectListItem { Text = "Status Aircraft", Value = "StatusAircraft.StatusAircraft" },
              //new SelectListItem { Text = "CAT Card Date", Value = "CatCardDate" },
              //new SelectListItem { Text = "CAT IR", Value = "CatIR" },
              //new SelectListItem { Text = "CAT IR Date", Value = "CatIRDate" },
        
              //new SelectListItem { Text = "Instr CAT", Value = "InstrCat" },
              //new SelectListItem { Text = "RPA DOW Date", Value = "RPAdowDate" },
              //new SelectListItem { Text = "RPA Substream", Value = "RPASubstream" },
              //new SelectListItem { Text = "RPA CAT Card No", Value = "RPA_CatCardNo" },
              //new SelectListItem { Text = "RPA CAT Card Date", Value = "RPA_CatCardDate" },
              //new SelectListItem { Text = "RPA CAT", Value = "RPA_Cat" },
              //new SelectListItem { Text = "RPA CAT Date", Value = "RPA_CatDate" },
              //new SelectListItem { Text = "RPA Instr CAT", Value = "RPA_Instr_Cat" },
              //new SelectListItem { Text = "RPA Instr CAT Date", Value = "RPA_Instr_CatDate" },
             
          }
        , "Value", "Text");
        }



        public SelectList LoadAViatorMedicalCol()
        {
            return new SelectList(new SelectListItem[]{
                 new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Type of Medical", Value = "TM.TypeMedical" },
               new SelectListItem { Text = "Medical Start Date", Value = "MedicalStartDate" },
              new SelectListItem { Text = "Medical End Date", Value = "MedicalEndDate" },
              new SelectListItem { Text = "Medical Cat", Value = "MC.MedicalCat" },
              //new SelectListItem { Text = "Cope Code", Value = "CC.CopeCode" },
             
              //new SelectListItem { Text = "Medical Cat Date", Value = "MedicalCatDate" },
              //new SelectListItem { Text = "Duration In Weeks", Value = "c.DurationInWeeks" },
              //new SelectListItem { Text = "ReCat Due Date", Value = "ReCatDueDate" },
              //new SelectListItem { Text = "Medical Status", Value = "SM.StatusMedical" },
          }
        , "Value", "Text");
        }



        public SelectList LoadCA()
        {
            return new SelectList(new SelectListItem[]{
              new SelectListItem { Text = "Supervisory", Value = "Supervisory" },
               new SelectListItem { Text = "Instructional", Value = "Instructional" },

          }
        , "Value", "Text");
        }


        public SelectList LoadAViatorHounerCol()
        {
            return new SelectList(new SelectListItem[]{
               new SelectListItem { Text = "Award Name", Value = "h.HonourAwardName" },
               new SelectListItem { Text = "Award Date", Value = "HonourAwardsDate" },
               new SelectListItem { Text = "Place of Award", Value = "c.HonourAwardsPlace" },
          

          }
        , "Value", "Text");
        }
        public SelectList LoadAViatorAccidentCol()
        {
            return new SelectList(new SelectListItem[]{
                 new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Accident Cat", Value = "AC.AccidentCatName" },
               new SelectListItem { Text = "Date of Accident", Value = "DateOfAccidentIncident" },
               new SelectListItem { Text = "Place of Accident", Value = "PlaceOfAccidentIncident" },
               //new SelectListItem { Text = "Details Of Accident Incident", Value = "DetailsOfAccidentIncident" },
               //new SelectListItem { Text = "Blameworthy", Value = "Blameworthy" },
               new SelectListItem { Text = "Aircraft Name", Value = "HeptrRPAMaster.AircraftName" },
              
          }
        , "Value", "Text");
        }



        public SelectList LoadAViatorAPPCol()
        {
            return new SelectList(new SelectListItem[]{

               new SelectListItem { Text = "Appt Name", Value = "ApptName" },
                new SelectListItem { Text = "Appt Date", Value = "ApptDate" },

          }
        , "Value", "Text");
        }

        public SelectList LoadAViatorRankCol()
        {
            return new SelectList(new SelectListItem[]{
              new SelectListItem { Text = "Rank", Value = "Rank" },
               new SelectListItem { Text = "Rank Date", Value = "RankDate" },

          }
        , "Value", "Text");
        }


        public SelectList LoadContactContainsCol()
        {
            return new SelectList(new SelectListItem[]{
              new SelectListItem { Text = "Contains", Value = "Contains" },
               new SelectListItem { Text = "Equal", Value = "=" },

          }
        , "Value", "Text");
        }

        public SelectList LoadAviatorContactDetailsCol()
        {
            return new SelectList(new SelectListItem[]{
                 new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Mobile No", Value = "MobileNo" },
               //new SelectListItem { Text = "LandLine No", Value = "LandLineNo" },
               new SelectListItem { Text = "NOK", Value = "NOK" },
               //new SelectListItem { Text = "Relation With Nok", Value = "RelationWithNok" },
               //new SelectListItem { Text = "Marital Status", Value = "MaritalStatus" },
               new SelectListItem { Text = "Name of Spouse", Value = "NameOfSpouse" },
               //new SelectListItem { Text = "Contact No of Spouse", Value = "ContactNoOfSpouse" },
               //new SelectListItem { Text = "Email of Spouse", Value = "EmailOfSpouse" },
               //new SelectListItem { Text = "Residental HouseNo ", Value = "ResidentalHouseNo" },
               //new SelectListItem { Text = "Residental Village Street", Value = "ResidentalVillage_Street" },
               //new SelectListItem { Text = "Residental Post Office", Value = "ResidentalPostOffice" },
               //new SelectListItem { Text = "Residental Tehsil", Value = "ResidentalTehsil" },
               //new SelectListItem { Text = "City", Value = "City" },
               //new SelectListItem { Text = "Residental District", Value = "ResidentalDistrict" },
               //new SelectListItem { Text = "Residental State", Value = "ResidentalState" },
               //new SelectListItem { Text = "Residental Pincode", Value = "ResidentalPincode" },
               //new SelectListItem { Text = "Permanent House No", Value = "PermanentHouseNo" },
               //new SelectListItem { Text = "Permanent Village Street", Value = "PermanentVillage_Street" },
               //new SelectListItem { Text = "Permanent Post Office", Value = "PermanentPostOffice" },
               //new SelectListItem { Text = "Permanent Tehsil", Value = "PermanentTehsil" },
               //new SelectListItem { Text = "Permanent District", Value = "PermanentDistrict" },
               //new SelectListItem { Text = "Permanent State", Value = "PermanentState" },
               //new SelectListItem { Text = "Permanent Pincode", Value = "PermanentPincode" },

          }
      , "Value", "Text");
        }


        public SelectList LoadAviatorFlyingCol()
        {
            return new SelectList(new SelectListItem[]{
                 new SelectListItem { Text = "Unit Name", Value = "f.UnitName" },
              new SelectListItem { Text = "Aircraft Name", Value = "HeptrRPAMaster.AircraftName" },
              //new SelectListItem { Text = "Day Dual Hrs", Value = "DayDualHrs" },
              // new SelectListItem { Text = "Day Solo Hrs", Value = "DaySoloHrs" },
              // new SelectListItem { Text = "Day Copilot Hrs", Value = "DayCopilotHrs" },
              // new SelectListItem { Text = "Night Dual Hrs", Value = "NightDualHrs" },
              // new SelectListItem { Text = "Night Solo Hrs", Value = "NightSoloHrs" },
              // new SelectListItem { Text = "Night Copilot Hrs", Value = "NightCopilotHrs" },
              // new SelectListItem { Text = "NVG Exp", Value = "NVGExp" },
              // new SelectListItem { Text = "WSO Exp", Value = "WSOExp" },
              // new SelectListItem { Text = "IF Actual", Value = "IF_Actual" },
              // new SelectListItem { Text = "Instr Day Hrs", Value = "InstrDayHrs" },
              // new SelectListItem { Text = "IMC Hrs", Value = "IMCHrs" },
              // new SelectListItem { Text = "SIF Hrs", Value = "SIFHrs" },
              // new SelectListItem { Text = "ALH Sml Hrs", Value = "ALHSmlHrs" },
               new SelectListItem { Text = "Month", Value = "Month" },
               new SelectListItem { Text = "Year", Value = "Year" },


          }
, "Value", "Text");
        }

        public SelectList LoadPostingCol()
        {
            return new SelectList(new SelectListItem[]{
              new SelectListItem { Text = "Posting Auth", Value = "PostingAuth" },
               new SelectListItem { Text = "Posting From Unit", Value = "ui.UnitName" },
               new SelectListItem { Text = "SOS", Value = "SOS" },
               new SelectListItem { Text = "SORS", Value = "SORS" },
               new SelectListItem { Text = "Posting To Unit", Value = "u.UnitName" },
               new SelectListItem { Text = "TOS", Value = "TOS" },
               new SelectListItem { Text = "TORS", Value = "TORS" },
               new SelectListItem { Text = "Posting Type", Value = "PostingType" },
               new SelectListItem { Text = "Posting In Date", Value = "PostingInDate" },
          }
        , "Value", "Text");
        }
        public SelectList LoadPostingTypeCol()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem { Text = "NA", Value = "NA" },
              new SelectListItem { Text = "Flying Duties", Value = "Flying Duties" },

               new SelectListItem { Text = "Graded Staff", Value = "Graded Staff" },
               new SelectListItem { Text = "Line Staff", Value = "Line Staff" },
               new SelectListItem { Text = "Deputation", Value = "Deputation" },
               new SelectListItem { Text = "Cross Attachment", Value = "Cross Attachment" },
               new SelectListItem { Text = "Instr Appt", Value = "Instr Appt" },
               new SelectListItem { Text = "Foreign assignment Appt", Value = "Foreign assignment" },
               new SelectListItem { Text = "Other", Value = "Other" },
                        }
        , "Value", "Text");
        }

        // else if (select == "NA") { Item1.Selected = true; }
        //else if (select == "INITIAL CAT") { Item2.Selected = true; }
        //else if (select == "UPGRADED") { Item3.Selected = true; }
        //else if (select == "RENEWED") { Item4.Selected = true; }
        //else if (select == "DOWNGRADED") { Item5.Selected = true; }

        public SelectList LoadAppearingRemarks()
        {
            return new SelectList(new SelectListItem[]{
               new SelectListItem { Text = "NA", Value = "NA" },
               new SelectListItem { Text = "OnLeave", Value = "OnLeave" },
               new SelectListItem { Text = "OnTD", Value = "OnTD" },
               new SelectListItem { Text = "OnCourse", Value = "OnCourse" },



          }
        , "Value", "Text");
        }



        public SelectList LoadYesOrNO()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Yes",Value="True"},
                new SelectListItem{Text="No",Value="False"}

            }, "Value", "Text");
        }

        public SelectList LoadYesNOVal()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Yes",Value="1"},
                new SelectListItem{Text="No",Value="0"}

            }, "Value", "Text");
        }


        //LoadModule
        //LoadSubModule
        public SelectList LoadModule()
        {
            DBConnection con = new DBConnection();
            Complaints model = new Complaints();
            var data = con.GetDataInList<Complaints>("SELECT [ModuleId],[ModuleName] FROM [dbo].[tbl_Module] ORDER BY [ModuleId]  asc").ToList();

            return new SelectList(data, "ModuleId", "ModuleName");
        }

        public SelectList LoadSubModule(int ModuleId)
        {
            DBConnection con = new DBConnection();
            Complaints model = new Complaints();
            var data = con.GetDataInList<Complaints>("SELECT [SubModuleId],[SubModuleName] FROM [dbo].[tbl_SubModule] where ModuleId='" + ModuleId + "' ORDER BY [SubModuleId]  asc").ToList();

            return new SelectList(data, "SubModuleId", "SubModuleName");
        }


        public SelectList LoadRecommededBrigAvn()
        {
            return new SelectList(new SelectListItem[]{

                new SelectListItem{Text="Recommeded",Value="True"},
                new SelectListItem{Text="Not Recommeded",Value="False"}

            }, "Value", "Text");
        }
        public SelectList LoadApprovedAvndte()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Approved",Value="True"},
                new SelectListItem{Text="Not Approved",Value="False"}

            }, "Value", "Text");
        }

        public SelectList LoadActive()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Active",Value="1"},
                new SelectListItem{Text="InActive",Value="0"}

            }, "Value", "Text");
        }

        public SelectList LoadCourseType()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="AHIC",Value="AHIC-"},
                new SelectListItem{Text="QFIC",Value="QFIC-"}

            }, "Value", "Text");
        }
        public SelectList LoadGoodShow()
        {
            return new SelectList(new SelectListItem[]{
                new SelectListItem{Text="Part I",Value="Part I"},
                new SelectListItem{Text="Part II",Value="Part II"},
                  new SelectListItem{Text="Lecture",Value="Lecture"},
                    new SelectListItem{Text="Ground Subjects",Value="Ground Subjects"},
                    new SelectListItem{Text="Flying Subjects",Value="Flying Subjects"},
                       new SelectListItem{Text="Flying & Ground Subjects",Value="Flying & Ground Subjects"},
            }, "Value", "Text");
        }


        public SelectList LoadPercentage()
        {
            return new SelectList(new SelectListItem[]{
  new SelectListItem{Text="1%",Value="1"},
 new SelectListItem{Text="2%",Value="2"},
 new SelectListItem{Text="3%",Value="3"},
 new SelectListItem{Text="4%",Value="4"},
 new SelectListItem{Text="5%",Value="5"},
 new SelectListItem{Text="6%",Value="6"},
 new SelectListItem{Text="7%",Value="7"},
 new SelectListItem{Text="8%",Value="8"},
 new SelectListItem{Text="9%",Value="9"},
 new SelectListItem{Text="10%",Value="10"},
 new SelectListItem{Text="11%",Value="11"},
 new SelectListItem{Text="12%",Value="12"},
 new SelectListItem{Text="13%",Value="13"},
 new SelectListItem{Text="14%",Value="14"},
 new SelectListItem{Text="15%",Value="15"},
 new SelectListItem{Text="16%",Value="16"},
 new SelectListItem{Text="17%",Value="17"},
 new SelectListItem{Text="18%",Value="18"},
 new SelectListItem{Text="19%",Value="19"},
 new SelectListItem{Text="20%",Value="20"},
 new SelectListItem{Text="21%",Value="21"},
 new SelectListItem{Text="22%",Value="22"},
 new SelectListItem{Text="23%",Value="23"},
 new SelectListItem{Text="24%",Value="24"},
 new SelectListItem{Text="25%",Value="25"},
 new SelectListItem{Text="26%",Value="26"},
 new SelectListItem{Text="27%",Value="27"},
 new SelectListItem{Text="28%",Value="28"},
 new SelectListItem{Text="29%",Value="29"},
 new SelectListItem{Text="30%",Value="30"},
 new SelectListItem{Text="31%",Value="31"},
 new SelectListItem{Text="32%",Value="32"},
 new SelectListItem{Text="33%",Value="33"},
 new SelectListItem{Text="34%",Value="34"},
 new SelectListItem{Text="35%",Value="35"},
 new SelectListItem{Text="36%",Value="36"},
 new SelectListItem{Text="37%",Value="37"},
 new SelectListItem{Text="38%",Value="38"},
 new SelectListItem{Text="39%",Value="39"},
 new SelectListItem{Text="40%",Value="40"},
 new SelectListItem{Text="41%",Value="41"},
 new SelectListItem{Text="42%",Value="42"},
 new SelectListItem{Text="43%",Value="43"},
 new SelectListItem{Text="44%",Value="44"},
 new SelectListItem{Text="45%",Value="45"},
 new SelectListItem{Text="46%",Value="46"},
 new SelectListItem{Text="47%",Value="47"},
 new SelectListItem{Text="48%",Value="48"},
 new SelectListItem{Text="49%",Value="49"},
 new SelectListItem{Text="50%",Value="50"},
 new SelectListItem{Text="51%",Value="51"},
 new SelectListItem{Text="52%",Value="52"},
 new SelectListItem{Text="53%",Value="53"},
 new SelectListItem{Text="54%",Value="54"},
 new SelectListItem{Text="55%",Value="55"},
 new SelectListItem{Text="56%",Value="56"},
 new SelectListItem{Text="57%",Value="57"},
 new SelectListItem{Text="58%",Value="58"},
 new SelectListItem{Text="59%",Value="59"},
 new SelectListItem{Text="60%",Value="60"},
 new SelectListItem{Text="61%",Value="61"},
 new SelectListItem{Text="62%",Value="62"},
 new SelectListItem{Text="63%",Value="63"},
 new SelectListItem{Text="64%",Value="64"},
 new SelectListItem{Text="65%",Value="65"},
 new SelectListItem{Text="66%",Value="66"},
 new SelectListItem{Text="67%",Value="67"},
 new SelectListItem{Text="68%",Value="68"},
 new SelectListItem{Text="69%",Value="69"},
 new SelectListItem{Text="70%",Value="70"},
 new SelectListItem{Text="71%",Value="71"},
 new SelectListItem{Text="72%",Value="72"},
 new SelectListItem{Text="73%",Value="73"},
 new SelectListItem{Text="74%",Value="74"},
 new SelectListItem{Text="75%",Value="75"},
 new SelectListItem{Text="76%",Value="76"},
 new SelectListItem{Text="77%",Value="77"},
 new SelectListItem{Text="78%",Value="78"},
 new SelectListItem{Text="79%",Value="79"},
 new SelectListItem{Text="80%",Value="80"},
 new SelectListItem{Text="81%",Value="81"},
 new SelectListItem{Text="82%",Value="82"},
 new SelectListItem{Text="83%",Value="83"},
 new SelectListItem{Text="84%",Value="84"},
 new SelectListItem{Text="85%",Value="85"},
 new SelectListItem{Text="86%",Value="86"},
 new SelectListItem{Text="87%",Value="87"},
 new SelectListItem{Text="88%",Value="88"},
 new SelectListItem{Text="89%",Value="89"},
 new SelectListItem{Text="90%",Value="90"},
 new SelectListItem{Text="91%",Value="91"},
 new SelectListItem{Text="92%",Value="92"},
 new SelectListItem{Text="93%",Value="93"},
 new SelectListItem{Text="94%",Value="94"},
 new SelectListItem{Text="95%",Value="95"},
 new SelectListItem{Text="96%",Value="96"},
 new SelectListItem{Text="97%",Value="97"},
 new SelectListItem{Text="98%",Value="98"},
 new SelectListItem{Text="99%",Value="99"},
 new SelectListItem{Text="100%",Value="100"},


            }, "Value", "Text");
        }





    }
}