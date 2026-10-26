using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace IHSDC.WebApp.Connection
{
    public class DBConnection : DbContext
    {
        public DBConnection() : base("IHSDCAA7DBConnectionString")
        {

        }

        #region Master

        #region Policy
        public IList<PolicyCRUD> PolicyCRUD(int procid, PolicyCRUD model)
        {
            var data = GetDataInList<PolicyCRUD>("proc_Policy_CRUD @procId=@p0,@PolicyId=@p1,@PolicyName=@p2 ,@PolicyRemarks=@p3 ,@UserId=@p4,@Orderby=@p5,@TypeofDocu=@p6,@UserPolicyControlId=@p7,@PUserId=@p8", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.PolicyId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PolicyName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.PolicyRemarks) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.TypeOfDocu) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.UserPolicyControlId) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.PUserId) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<downloadLog> downloadLogCRUD(int procid, downloadLog model)
        {
            var data = GetDataInList<downloadLog>("proc_DownloadPolicy_CRUD @procId=@p0,@downloadby=@p1,@PolicyId=@p2,@FileName=@p3,@ICNumber=@p4,@IpAddress=@p5,@AccessDownload=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PolicyId) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.FileName) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ICNumber) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.IpAddress) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.AccessDownload) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion



        #region Comd
        public IList<ComdCRUD> ComdCRUD(int procid, ComdCRUD model)
        {
            var data = GetDataInList<ComdCRUD>("proc_Comd_CRUD @procId=@p0,@ComdId=@p1,@ComdName=@p2 ,@UserId=@p3,@Orderby=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.ComdName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion






        #region Sqn
        public IList<SqnCRUD> SqnCRUD(int procid, SqnCRUD model)
        {
            var data = GetDataInList<SqnCRUD>("proc_Sqn_CRUD @procId=@p0,@SqnId=@p1,@SqnName=@p2 ,@UserId=@p3,@ComdId=@p4,@CorpsId=@p5,@BdeCatId=@p6,@Orderby=@p7", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.SqnId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.SqnName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.CorpsId) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.BdeCatId) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion



        #region BdeCat
        public IList<BdeCatCRUD> BdeCatCRUD(int procid, BdeCatCRUD model)
        {
            var data = GetDataInList<BdeCatCRUD>("proc_BdeCat_CRUD @procId=@p0,@BdeCatId=@p1,@BdeCatName=@p2 ,@UserId=@p3,@ComdId=@p4,@CorpsId=@p5,@Orderby=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.BdeCatId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.BdeCatName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.CorpsId) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region Rank
        public IList<RankCRUD> RankCRUD(int procid, RankCRUD model)
        {
            var data = GetDataInList<RankCRUD>("proc_Rank_CRUD @procId=@p0,@RankId=@p1,@RankName=@p2 ,@RankAbbreviation=@p3,@UserId=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.RankId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.RankName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.RankAbbreviation) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region Corps

        public IList<CorpsCRUD> CorpsCRUD(int procid, CorpsCRUD model)
        {
            var data = GetDataInList<CorpsCRUD>("proc_Corps_CRUD @procId=@p0,@CorpsId=@p1,@CorpsName=@p2 ,@UserId=@p3,@ComdId=@p4,@Orderby=@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CorpsId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.CorpsName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region User Policy Control
        public IList<UserPolicyControlCRUD> UserPolicyControlCRUD(int procid, UserPolicyControlCRUD model)
        {
            var data = GetDataInList<UserPolicyControlCRUD>("proc_UserPolicyControl_CRUD @UserPolicyControlId=@p0, @procId=@p1,@PUserId=@p2,@Policies=@p3 ,@UserId=@p4", new SqlParameter("@p0", (object)(model.UserPolicyControlId) ?? DBNull.Value), new SqlParameter("@p1", (object)(procid) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PUserId) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Policies) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region Unit
        public IList<UnitCRUD> UnitCRUD(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId=@p0,@Unit_ID=@p1,@UnitName=@p2,@Command=@p3 ,@Corps=@p4 ,@Sqn=@p5,@BdeCat=@p6  ,@TypeOfUnit=@p7  ,@UserId=@p8,@Isflight=@p9,@ScheduleId=@p10,@Inbox_Id = @p11", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.UnitName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.CorpsId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.SqnId) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.BdeCatId) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.TypeOfUnit) ?? DBNull.Value), new SqlParameter("@p8", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.IsFlight) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.ScheduleId) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.InboxID) ?? DBNull.Value)).ToList();
            return data;
        }


        public IList<UnitCRUD> UnitCRUDNew(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId=@p0,@Unit_ID=@p1,@UnitName=@p2,@Command=@p3 ,@Corps=@p4 ,@Sqn=@p5,@BdeCat=@p6  ,@TypeOfUnit=@p7  ,@UserId=@p8,@Isflight=@p9,@ScheduleId=@p10,@Inbox_Id = @p11", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.UnitName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.CorpId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.SqnId) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.BdeCatId) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.TypeOfUnit) ?? DBNull.Value), new SqlParameter("@p8", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.IsFlight) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.ScheduleId) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.InboxID) ?? DBNull.Value)).ToList();
            return data;
        }



        public IList<HitCRUD> HitCRUD(int procid, HitCRUD model)
        {
            var data = GetDataInList<HitCRUD>("proc_Unit_CRUD @procId=@p0", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value)).ToList();
            return data;
        }



        public IList<UnitCRUD> GetAllUnitCRUD(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId=@p0,@Unit_ID=@p1,@UnitName=@p2,@Command=@p3 ,@Corps=@p4 ,@Sqn=@p5,@BdeCat=@p6  ,@TypeOfUnit=@p7  ,@UserId=@p8,@Isflight=@p9,@ScheduleId=@p10", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.UnitName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.CorpsId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Sqn) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.BdeCatId) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.TypeOfUnit) ?? DBNull.Value), new SqlParameter("@p8", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.IsFlight) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.ScheduleId) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region Category
        public IList<CategoryCRUD> CategoryCRUD(int procid, CategoryCRUD model)
        {
            var data = GetDataInList<CategoryCRUD>("proc_Category_CRUD @procId=@p0,@CategoryId=@p1,@CategoryName=@p2,@UserId=@p3,@ScheduleId=@p4,@Orderby=@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CategoryId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.CategoryName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ScheduleId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region Branch
        public IList<BranchCRUD> BranchCRUD(int procid, BranchCRUD model)
        {
            var data = GetDataInList<BranchCRUD>("proc_Branch_CRUD @procId=@p0,@BranchId=@p1,@BranchName=@p2,@UserId=@p3,@ScheduleId=@p4,@Orderby=@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.BranchId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.BranchName) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ScheduleId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region Type of Conf
        public IList<TypeofConfCRUD> TypeofConfCRUD(int procid, TypeofConfCRUD model)
        {
            var data = GetDataInList<TypeofConfCRUD>("proc_TypeofConf_CRUD @procId=@p0,@TypeOfConfId=@p1,@TypeOfConf=@p2,@UserId=@p3,@PolicyId=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.TypeOfConfId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.TypeOfConf) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.PolicyId) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<SearchData> GetPolicyTitleData(int procid, SearchData model)
        {
            var data = GetDataInList<SearchData>("proc_TypeofConf_CRUD @procId=@p0,@TypeOfConfId=@p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Id) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion


        public SelectList GetUnitDropdown(int procid, UnitCRUD model)
        {
            DBConnection con = new DBConnection();

            return new SelectList(con.UnitCRUD(procid, model), "Unit_ID", "UnitName");
        }
        public IList<DashAvi> GetAviatorForDashboard2(int procid, int Unit_ID, int UserId, string Ip, string Remarks)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId=@p0,@UserId  = @p1, @Unit_id=@p2,@Ip=@p3,@Remarks=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(UserId) ?? DBNull.Value), new SqlParameter("@p2", (object)(Unit_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(Ip) ?? DBNull.Value), new SqlParameter("@p4", (object)(Remarks) ?? DBNull.Value)).ToList();



            return data;
        }
        #region UserList
        public IList<UserListCRUD> UserListCRUD(int procid, UserListCRUD model)
        {
            var data = GetDataInList<UserListCRUD>("proc_Unit_CRUD @procId=@p0,@Unit_ID=@p1,@UnitName=@p2 ,@UserName=@p3 ,@Command=@p4 ,@Corps=@p5  ,@BdeCat=@p6  ,@TypeOfUnit=@p7  ,@UserId=@p8", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.UnitName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.UserName) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Command) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Corps) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.BdeCat) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.TypeOfUnit) ?? DBNull.Value), new SqlParameter("@p8", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion



        #region GenderCRUD
        public IList<GenderCRUD> GenderCRUD(int procid, GenderCRUD model)
        {
            var data = GetDataInList<GenderCRUD>("proc_Gender_CRUD @procId=@p0,@GenderId=@p1,@Gender=@p2 ,@UserId=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.GenderId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Gender) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion




        #region StagesGroundAccidentCRUD
        public IList<StagesGroundAccidentCRUD> StagesGroundAccidentCRUD(int procid, StagesGroundAccidentCRUD model)
        {
            var data = GetDataInList<StagesGroundAccidentCRUD>("proc_StagesGroundAccident_CRUD @procId=@p0,@StagesGroundAccidentId=@p1,@StagesGroundAccident=@p2 ,@UserId=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.StagesGroundAccidentId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.StagesGroundAccident) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region PeriodOperationCRUD
        public IList<PeriodOperationCRUD> PeriodOperationCRUD(int procid, PeriodOperationCRUD model)
        {
            var data = GetDataInList<PeriodOperationCRUD>("proc_PeriodOperation_CRUD @procId=@p0,@PeriodOperationId=@p1,@PeriodOperation=@p2 ,@UserId=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.PeriodOperationId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PeriodOperation) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion







        #region FlyingStatusCRUD
        public IList<FlyingStatusCRUD> FlyingStatusCRUD(int procid, FlyingStatusCRUD model)
        {
            var data = GetDataInList<FlyingStatusCRUD>("proc_FlyingStatus_CRUD @procId=@p0,@FlyingStatusId=@p1,@FlyingStatus=@p2 ,@UserId=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.FlyingStatusId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.FlyingStatus) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion






        #endregion Master


        #region RankHistory
        public IList<RankHistory> RankHistory(int procid, RankHistory model)
        {
            var data = GetDataInList<RankHistory>("Proc_AviatorRankApptHistory @procId=@p0,@Aviator_Id=@p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(model.Aviator_Id)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region Folder Control Permission [04-07-2024] 
        public IList<FolderControlPermissionCRUD> FolderControlPermissionCRUD(int procid, FolderControlPermissionCRUD model)
        {
            var data = GetDataInList<FolderControlPermissionCRUD>("proc_FolderControl_CRUD @FolderControlId=@p0, @procId=@p1,@PolicyID=@p2,@Useres=@p3 ,@UserId=@p4", new SqlParameter("@p0", (object)(model.FolderControlId) ?? DBNull.Value), new SqlParameter("@p1", (object)(procid) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PolicyId) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Users) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }


        public IList<UserCRUD> UserCRUD(int procid, UserCRUD model)
        {
            var data = GetDataInList<UserCRUD>("proc_FolderPermission_CRUD @procId=@p0,@PolicyId=@p1,@UserId=@p2,@Orderby=@p3,@FolderControlId=@p4,@PUserId=@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.PolicyId) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Orderby) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.FolderControlId) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.PUserId) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion


        #region Policy Folder Permission [08-07-2024]
        public IList<PolicyFolderCRUD> proc_PolicyFolder_CRUD(int procid, PolicyFolderCRUD model)
        {
            var data = GetDataInList<PolicyFolderCRUD>("proc_PolicyFolder_CRUD @procId=@p0,@PolicyId=@p1,@PolicyName=@p2 ,@PolicyRemarks=@p3 ,@UserId=@p4,@Orderby=@p5,@TypeofDocu=@p6,@UserPolicyControlId=@p7,@PUserId=@p8", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.PolicyId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.PolicyName) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.PolicyRemarks) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.TypeOfDocu) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.UserPolicyControlId) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.PUserId) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion

        public IList<DropdownList> LoadUnitAll(int procid, int UserId, int ComndId, int CorpsId, int BdeId, int SqnId)
        {
            var data = GetDataInList<DropdownList>("FilterDropDown @procId=@p0,@UserId =@p1,@ComndId =@p2,@CorpsId =@p3,@BdeId =@p4,@SqnId =@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(UserId) ?? DBNull.Value), new SqlParameter("@p2", (object)(ComndId) ?? DBNull.Value), new SqlParameter("@p3", (object)(CorpsId) ?? DBNull.Value), new SqlParameter("@p4", (object)(BdeId) ?? DBNull.Value), new SqlParameter("@p5", (object)(SqnId) ?? DBNull.Value)).ToList();



            return data;
        }
        #region ApptCRUD
        public IList<ApptCRUD> ApptCRUD(int procid, ApptCRUD model)
        {
            var data = GetDataInList<ApptCRUD>("proc_Appt_CRUD @procId=@p0,@ApptId=@p1,@Appt=@p2,@ApptAbbreviation=@p3  ,@UserId=@p4,@Orderby=@p5,@NewOrderby=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.ApptId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Appt) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ApptAbbreviation) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Orderby) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.NewOrderby) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region Aviator Appt
        public IList<AviatorApptCRUD> AviatorApptCRUD(int procid, AviatorApptCRUD model)
        {
            var data = GetDataInList<AviatorApptCRUD>("proc_AviatorAppt_CRUD @procId=@p0,@AviatorAppointment_ID=@p1,@Aviator_ID=@p2,@ApptDate=@p3 ,@ApptName=@p4 ,@UserId=@p5,@UnitName=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.AviatorAppointment_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Aviator_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ApptDate) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.ApptName) ?? DBNull.Value), new SqlParameter("@p5", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p6", (object)(SessionManager.UnitName) ?? DBNull.Value)).ToList();
            return data;

        }

        #endregion

        #region Aviator Rank
        public IList<AviatorRankCRUD> AviatorRankCRUD(int procid, AviatorRankCRUD model)
        {
            var data = GetDataInList<AviatorRankCRUD>("proc_AviatorRank_CRUD @procId=@p0,@AviatorRank_ID=@p1,@Aviator_ID=@p2,@RankDate=@p3 ,@Rank=@p4,@UserId=@p5,@UnitName=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.AviatorRank_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Aviator_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.RankDate) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Rank) ?? DBNull.Value), new SqlParameter("@p5", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p6", (object)(SessionManager.UnitName) ?? DBNull.Value)).ToList();
            return data;

        }



        #endregion


        #region     ScheduleLetter


        public IList<CommandId> CommandId()
        {
            var data = GetDataInList<CommandId>("proc_BrigAvnId").ToList();
            return data;

        }



        public IList<ScheduleLetter> ScheduleLetter_CRUD(int procid, ScheduleLetter model)
        {
            var data = GetDataInList<ScheduleLetter>("proc_ScheduleLetter_CRUD @procId=@p0,@ScheduleId=@p1, @FromDate=@p2,@ToDate=@p3,@Comd=@p4,@UserId =@p5,@Unit_ID=@p6,@StatusName =@p7 ,@Remark =@p8,@Subject =@p9,@ScheduleRefNoDt =@p10,@IsClosed=@p11,@OrganizerCat=@p12,@SponsorCat=@p13,@NodalCat=@p14,@BranchIds=@p15,@TypeOfConfId=@p16,@ParticipateUserId=@p17,@ParticipateNewUserId=@p18 ", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.ScheduleId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.FromDate) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ToDate) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Comd) ?? DBNull.Value), new SqlParameter("@p5", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p6", (object)(Convert.ToInt16(SessionManager.Unit_ID)) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.StatusName) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.Remark) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.Subject) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.ScheduleRefNoDt) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.isClosed) ?? DBNull.Value), new SqlParameter("@p12", (object)(model.OrganizerCatId) ?? DBNull.Value), new SqlParameter("@p13", (object)(model.SponsorCatId) ?? DBNull.Value), new SqlParameter("@p14", (object)(model.NodalCatId) ?? DBNull.Value), new SqlParameter("@p15", (object)(model.BranchIds) ?? DBNull.Value), new SqlParameter("@p16", (object)(model.TypeOfConfId) ?? DBNull.Value), new SqlParameter("@p17", (object)(model.ParticipateUsers) ?? DBNull.Value), new SqlParameter("@p18", (object)(model.ParticipateNewUsers) ?? DBNull.Value)).ToList();
            return data;

        }






        #region Inbox
        public IList<InboxCRUD> InboxCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Inbox_CRUD @procId=@p0,@Inbox_ID=@p1,@Schedule_ID=@p2,@Comd_ID=@p3,@Branch_ID=@p4,@Title=@p5,@Summary=@p6,@Upload=@p7,@DateTimeOfUpdate=@p8,@LastUpdatedBy=@p9 , @UserId=@p10,@Unit_ID  = @p11,@UnitName  = @p12", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Schedule_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.ComdId) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Branch) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Title) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.Summary) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.Upload) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.DateTimeOfUpdate) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.LastUpdatedBy) ?? DBNull.Value), new SqlParameter("@p10", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p12", (object)(SessionManager.Unit_ID.ToString()) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<InboxCRUD> SubmitInbox(string procid, string Query)

        {

            var data = GetDataInList<InboxCRUD>("proc_Submit @procId=@p0,@Query=@p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Query) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        public IList<InboxCRUD> CreateAPCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_AP_CRUD @procId=@p0,@id=@p1,@Inbox_ID=@p2,@RefId=@p3,@ComdId=@p4,@Branch=@p5,@Title=@p6,@Upload=@p7,@UploadPath=@p8,@Summary=@p9,@Status=@p10,@DateTimeOfUpdate=@p11,@LastUpdatedBy=@p12,@IsVisibleToAll=@p13 ,@AllowEdit=@p14 , @UserId=@p15,@Unit_ID  = @p16,@CategoryId=@p17,@SponsorCategoryId=@p18,@NodalCategoryId=@p19,@WordFile=@p20,@WordFilePath=@p21", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Schedule_ID) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Branch) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.Title) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.Upload) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.UploadPath) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.Summary) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.Status) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.DateTimeOfUpdate) ?? DBNull.Value), new SqlParameter("@p12", (object)(model.LastUpdatedBy) ?? DBNull.Value), new SqlParameter("@p13", (object)(model.IsVisibleToAll) ?? DBNull.Value), new SqlParameter("@p14", (object)(model.AllowEdit) ?? DBNull.Value), new SqlParameter("@p15", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p16", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p17", (object)(model.CategoryId) ?? DBNull.Value), new SqlParameter("@p18", (object)(model.SponsorCategoryId) ?? DBNull.Value), new SqlParameter("@p19", (object)(model.NodalCategoryId) ?? DBNull.Value), new SqlParameter("@p20", (object)(model.WordFile) ?? DBNull.Value), new SqlParameter("@p21", (object)(model.WordFilePath) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<AgendaPointCRUD> CreateAgendaPoint(int procid, AgendaPointCRUD model)
        {
            var data = GetDataInList<AgendaPointCRUD>(
                @"proc_AP_CRUD
            @procId=@p0,
            @id=@p1,
            @Inbox_ID=@p2,
            @RefId=@p3,
            @ComdId=@p4,
            @Branch=@p5,
            @Title=@p6,
            @Upload=@p7,
            @UploadPath=@p8,
            @Summary=@p9,
            @Status=@p10,
            @DateTimeOfUpdate=@p11,
            @LastUpdatedBy=@p12,
            @IsVisibleToAll=@p13,
            @AllowEdit=@p14,
            @UserId=@p15,
            @Unit_ID=@p16,
            @CategoryId=@p17,
            @SponsorCategoryId=@p18,
            @NodalCategoryId=@p19,
            @WordFile=@p20,
            @WordFilePath=@p21",

                new SqlParameter("@p0", (object)procid ?? DBNull.Value),
                new SqlParameter("@p1", (object)model.Inbox_ID ?? DBNull.Value),
                new SqlParameter("@p2", (object)model.Inbox_ID ?? DBNull.Value),
                new SqlParameter("@p3", (object)model.Schedule_ID ?? DBNull.Value),

                // ComdId: 0 -> NULL
                new SqlParameter("@p4", SqlDbType.Int)
                {
                    Value = NullIfZero(model.Unit_ID)
                },

                // Branch: 0 -> NULL
                new SqlParameter("@p5", SqlDbType.Int)
                {
                    Value = NullIfZero(model.Branch)
                },

                new SqlParameter("@p6", (object)model.Title ?? DBNull.Value),
                new SqlParameter("@p7", (object)model.Upload ?? DBNull.Value),
                new SqlParameter("@p8", (object)model.UploadPath ?? DBNull.Value),
                new SqlParameter("@p9", (object)model.Summary ?? DBNull.Value),
                new SqlParameter("@p10", (object)model.Status ?? DBNull.Value),
                new SqlParameter("@p11", (object)model.DateTimeOfUpdate ?? DBNull.Value),
                new SqlParameter("@p12", (object)model.LastUpdatedBy ?? DBNull.Value),
                new SqlParameter("@p13", (object)model.IsVisibleToAll ?? DBNull.Value),
                new SqlParameter("@p14", (object)model.AllowEdit ?? DBNull.Value),
                new SqlParameter("@p15", (object)Convert.ToInt16(SessionManager.UserIntId)),
                new SqlParameter("@p16", (object)model.Unit_ID ?? DBNull.Value),

                // CategoryId: 0 -> NULL
                new SqlParameter("@p17", SqlDbType.Int)
                {
                    Value = NullIfZero(model.CategoryId)
                },

                // SponsorCategoryId: 0 -> NULL
                new SqlParameter("@p18", SqlDbType.Int)
                {
                    Value = NullIfZero(model.SponsorCategoryId)
                },

                // NodalCategoryId: 0 -> NULL
                new SqlParameter("@p19", SqlDbType.Int)
                {
                    Value = NullIfZero(model.NodalCategoryId)
                },

                new SqlParameter("@p20", (object)model.WordFile ?? DBNull.Value),
                new SqlParameter("@p21", (object)model.WordFilePath ?? DBNull.Value)
            ).ToList();

            return data;
        }
        private static object NullIfZero(int? value)
        {
            return !value.HasValue || value.Value == 0
                ? (object)DBNull.Value
                : value.Value;
        }
        public IList<APSupportingDocu> GetSupportingData(int procid, APSupportingDocu model)


        {
            var data = GetDataInList<APSupportingDocu>("proc_SupportingData_CRUD @procId=@p0,@SuppFileName=@p1,@SuppFilePath=@p2,@UploadBy=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.SuppFileName) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.SuppFilePath) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.UploadBy) ?? DBNull.Value)).ToList();
            return data;
        }



        public IList<AgendaPointLog> AgendaPointLogCRUD(int procid, AgendaPointLog model)
        {
            var data = GetDataInList<AgendaPointLog>("proc_AP_CRUD @procId=@p0,@Inbox_ID=@p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Inbox_ID) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<NotingDetail> CreateNotingDetailCRUD(int procid, NotingDetail model)
        {
            var data = GetDataInList<NotingDetail>("proc_NotingDetail_CRUD @procId=@p0,@InboxId  =@p1,@CommentFrom =@p2,@Comment=@p3,@UpdatedByUserId =@p4,@Attachment=@p5", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.InboxId) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Comment) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Attachment) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<InboxCRUD> ForwardCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Forward_CRUD @procId=@p0,@InboxID=@p1,@UserId=@p2,@ForwardFrom=@p3,@ForwardTo=@p4,@ForwardText=@p5,@Nodal=@p6,@NodalComment=@p7,@ForwordId=@p8", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.SentID) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.FwdComment) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.NodalId) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.NodalComment) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.ForwardID) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<InboxCRUD> RevertMailCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Forward_CRUD @procId=@p0,@Ord=@p1,@FwdId=@p2", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Ord) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.FwdId) ?? DBNull.Value)).ToList();
            return data;
        }


        public IList<InboxCRUD> OpenCloseAgendaPoint(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_OpenCloseAgendaPoint_CRUD @procId=@p0,@ApId=@p1,@CloseBy=@p2,@OpenBy=@p3,@CloseReason=@p4,@OpenReason=@p5,@CloseIds=@p6", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.CloseReason) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.OpenReason) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.CloseIds) ?? DBNull.Value)).ToList();
            return data;
        }


        #region Inbox Noting Fwd
        public IList<InboxNotingFwdCRUD> InboxNotingFwdCRUD(int procid, InboxNotingFwdCRUD model)
        {
            var data = GetDataInList<InboxNotingFwdCRUD>("proc_InboxNotingFwd_CRUD @procId=@p0,@InboxNoting_ID=@p1,@Inbox_ID=@p2,@Comd_ID=@p3,@Branch_ID=@p4,@Title=@p5,@Summary=@p6,@DateTimeOfUpdate=@p7,@LastUpdatedBy=@p8 , @UserId=@p9,@Unit_ID  = @p10,@UnitName  = @p11", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.InboxNoting_ID) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.Inbox_ID) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.Comd_ID) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.Branch_ID) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.Title) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.Summary) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.DateTimeOfUpdate) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.LastUpdatedBy) ?? DBNull.Value), new SqlParameter("@p9", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.Unit_ID) ?? DBNull.Value), new SqlParameter("@p11", (object)(SessionManager.Unit_ID.ToString()) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<InboxNotingFwdCRUD> SubmitInboxNotingFwd(string procid, string Query)

        {

            var data = GetDataInList<InboxNotingFwdCRUD>("proc_Submit @procId=@p0,@Query=@p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Query) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion





        #endregion

        #region

        public IList<ImportResult> CheckXML(int procid, string xmldata)
        {
            var data = GetDataInList<ImportResult>("proc_CheckXML @procid=@p0,@xml=@p1 ", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(xmldata) ?? DBNull.Value)).ToList();
            return data;

        }

        public IList<ImportResult> ReadXML(int procid, string xmldata)
        {
            var data = GetDataInList<ImportResult>("proc_GetXMLData @procid=@p0,@xml=@p1 ", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(xmldata) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion


        public IList<DashAvi> ErrorLogDashboard2(int procid, string Message, string ErrorInfo, string LogType)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId=@p0,@Message=@p1,@ErrorInfo=@p2,@LogType=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Message) ?? DBNull.Value), new SqlParameter("@p2", (object)(ErrorInfo) ?? DBNull.Value), new SqlParameter("@p3", (object)(LogType) ?? DBNull.Value)).ToList();
            return data;
        }

        public IList<AppxG> UpdateAppxG(int procid, AppxG model)
        {
            var data = GetDataInList<AppxG>("AppxG_CRUD @procid=@p0 ,@FinalAppxId =@p1,@AppxGForYear =@p2,@CheckAviatorEndorsementExp =@p3,@CheckAviatorEndorsementOnAirCraftType =@p4,@CheckAviatorEndorsementExpUnit =@p5,@TotalInstrDayExp =@p6,@TotalInstrNightExp =@p7,@Syllabus9ACompletedOn =@p8,@CompletedByICNo =@p9,@CompletedByRank =@p10,@CompletedByName =@p11,@NoQFIInTheUnit =@p12,@RecomeForCheckAviatorUnit =@p13,@RecomeForCheckAviatorOnType =@p14, @UserId=@p15,  @ISRecommendByBrigAvn=@p16, @IsApprovedByGSO  =@p17", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.FinalAppxId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.AppxGForYear) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.CheckAviatorEndorsementExp) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.CheckAviatorEndorsementOnAirCraftType) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.CheckAviatorEndorsementExpUnit) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.TotalInstrDayExp) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.TotalInstrNightExp) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.Syllabus9ACompletedOn) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.CompletedByICNo) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.CompletedByRank) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.CompletedByName) ?? DBNull.Value), new SqlParameter("@p12", (object)(model.NoQFIInTheUnit) ?? DBNull.Value), new SqlParameter("@p13", (object)(model.RecomeForCheckAviatorUnit) ?? DBNull.Value), new SqlParameter("@p14", (object)(model.RecomeForCheckAviatorOnType) ?? DBNull.Value), new SqlParameter("@p15", (object)(Convert.ToString(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p16", (object)(Convert.ToInt16(model.ISRecommendByBrigAvn)) ?? DBNull.Value), new SqlParameter("@p17", (object)(Convert.ToInt16(model.IsApprovedByGSO)) ?? DBNull.Value)).ToList();
            return data;
        }


        public IList<user> insertUserCert(user user, byte[] cert)
        {
            string st = Encoding.ASCII.GetString(cert);


            // string st = ByteArrayToString(cert);
            var data = GetDataInList<user>("insertCert @userId=@p0,@publicKey=@p1", new SqlParameter("@p0", (object)(user.IntId) ?? DBNull.Value), new SqlParameter("@p1", (object)(st) ?? DBNull.Value)).ToList();
            return data;
        }
        public IList<UserKeyMapping> findPublicKey(UserKeyMapping userKeyMapping)
        {
            var data = GetDataInList<UserKeyMapping>("findPublicKey @userId=@p0", new SqlParameter("@p0", (object)(userKeyMapping.UserId) ?? DBNull.Value)).ToList();
            return data;

        }
        public IList<user> getAllUsers()
        {
            var data = GetDataInList<user>("getAllUsers").ToList();
            return data;
        }




        #region GetAviatorForDashboard
        public IList<DashAvi> GetAviatorForDashboard(int procid, int Unit_ID)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId=@p0,@UserId  = @p1, @Unit_id=@p2", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p2", (object)(Unit_ID) ?? DBNull.Value)).ToList();



            return data;
        }
        #endregion

        public DataSet GetAppxReport(int procid, string procName, int Param1)
        {
            DataSet ds = new DataSet();
            DBConnection con = new DBConnection();
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["IHSDCAA7DBConnectionString"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand(procName, conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.Add(new SqlParameter("@procid", procid));
                sqlComm.Parameters.Add(new SqlParameter("@ScheduleId", Param1));
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);
            }
            return ds;
        }
        #region

        public IList<TransationStrengthReturn> IUDUnitStrReturn(TransationStrengthReturn model, int procid)
        {
            var data = GetDataInList<TransationStrengthReturn>("proc_UnitStrReturn @ProcId=@p0,  @StrId = @p1, @SUSNo =   @p2 ,@PosnAs =@p3 , @PresentReturnSerNo =    @p4 ,@PresentReturnDate =   @p5 , @LastReturnSerNo =    @p6 ,@LastReturnDate =   @p7  ,@LastIAFF =   @p8  ,@LastIAFFDate =   @p9  ,@FmnOfUnit =  @p10   ,@Location =  @p11   ,@EstNo =   @p12  ,@EstDate =  @p13   ,@CoRemarks =  @p14   ,@CoCertified =   @p15  ,@OffrsPostedExcessToEst =  @p16   ,@OffrsSOSAndHeldOnSupernumeraryStr =   @p17  ,@DetailsOfPostingDuesInOffrs =   @p18  ,@DetailsOfPostingDuesOutOffrs =   @p19  ,@PSCQualified =   @p20  ,@ReEmpOffrs =     @p21,@OffrsOnSuperStr =   @p22  ,@Remarks =   @p23  ,@Month =  @p24   ,@Year = @p25    ,@userId = @p26,@UnitName=@p27 ", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.StrId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.SUSNo) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.PosnAs) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.PresentReturnSerNo) ?? DBNull.Value), new SqlParameter("@p5", (object)(model.PresentReturnDate) ?? DBNull.Value), new SqlParameter("@p6", (object)(model.LastReturnSerNo) ?? DBNull.Value), new SqlParameter("@p7", (object)(model.LastReturnDate) ?? DBNull.Value), new SqlParameter("@p8", (object)(model.LastIAFF) ?? DBNull.Value), new SqlParameter("@p9", (object)(model.LastIAFFDate) ?? DBNull.Value), new SqlParameter("@p10", (object)(model.FmnOfUnit) ?? DBNull.Value), new SqlParameter("@p11", (object)(model.Location) ?? DBNull.Value), new SqlParameter("@p12", (object)(model.EstNo) ?? DBNull.Value), new SqlParameter("@p13", (object)(model.EstDate) ?? DBNull.Value), new SqlParameter("@p14", (object)(model.SUSNo) ?? DBNull.Value), new SqlParameter("@p15", (object)(model.SUSNo) ?? DBNull.Value), new SqlParameter("@p16", (object)(model.CoRemarks) ?? DBNull.Value), new SqlParameter("@p17", (object)(model.OffrsSOSAndHeldOnSupernumeraryStr) ?? DBNull.Value), new SqlParameter("@p18", (object)(model.DetailsOfPostingDuesInOffrs) ?? DBNull.Value), new SqlParameter("@p19", (object)(model.DetailsOfPostingDuesOutOffrs) ?? DBNull.Value), new SqlParameter("@p20", (object)(model.PSCQualified) ?? DBNull.Value), new SqlParameter("@p21", (object)(model.ReEmpOffrs) ?? DBNull.Value), new SqlParameter("@p22", (object)(model.OffrsOnSuperStr) ?? DBNull.Value), new SqlParameter("@p23", (object)(model.Remarks) ?? DBNull.Value), new SqlParameter("@p24", (object)(model.Month) ?? DBNull.Value), new SqlParameter("@p25", (object)(model.Year) ?? DBNull.Value), new SqlParameter("@p26", (object)(SessionManager.UserId) ?? DBNull.Value), new SqlParameter("@p27", (object)(SessionManager.UnitName) ?? DBNull.Value)).ToList();
            return data;

        }

        #endregion
       
        #region CheckLoggedIn
        public IList<CheckLoggedIn> CheckLoggedIn(int procid, int IntId, string UserName, string id)
        {
            var data = GetDataInList<CheckLoggedIn>("proc_UpdateLogin @procId=@p0,@intId=@p1 ,@UserName=@p2  ,@id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(IntId) ?? DBNull.Value), new SqlParameter("@p2", (object)(UserName) ?? DBNull.Value), new SqlParameter("@p3", (object)(id) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion





        public IList<AddUpdateStatus> AddUpdateStatus(AddUpdateStatus Model)
        {
            var data = GetDataInList<AddUpdateStatus>("Proc_AddUpdateStatus @FormName=@p0,@Aviator_ID =@p1, @CourseId =@p2,@AircraftType_ID=@p3,@HeptrRPAMasterId=@p4,@CatIRId=@p5,@RPACatId=@p6,@HonourAwardsDate=@p7, @Month=@p8,@SpecialEqpt_ID=@p9,@ForeignPostingTypeId=@p10, @SpecialQual_ID=@p11,@MedicalStartDate=@p12,@DateOfAccidentIncident=@p13,@DateGoodShow=@p14", new SqlParameter("@p0", (object)(Model.FormName) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(Model.Aviator_Id)) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(Model.CourseID)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(Model.AircraftType_ID)) ?? DBNull.Value), new SqlParameter("@p4", (object)(Convert.ToInt16(Model.HeptrRPAMasterId)) ?? DBNull.Value), new SqlParameter("@p5", (object)(Convert.ToInt16(Model.CatIRId)) ?? DBNull.Value), new SqlParameter("@p6", (object)(Convert.ToInt16(Model.RPACatId)) ?? DBNull.Value), new SqlParameter("@p7", (object)(Model.HonourAwardsDate) ?? DBNull.Value), new SqlParameter("@p8", (object)(Model.Month) ?? DBNull.Value), new SqlParameter("@p9", (object)(Model.SpecialEqpt_ID) ?? DBNull.Value), new SqlParameter("@p10", (object)(Model.ForeignPostingTypeId) ?? DBNull.Value), new SqlParameter("@p11", (object)(Model.SpecialQual_ID) ?? DBNull.Value), new SqlParameter("@p12", (object)(Model.MedicalStartDate) ?? DBNull.Value), new SqlParameter("@p13", (object)(Model.DateOfAccidentIncident) ?? DBNull.Value), new SqlParameter("@p14", (object)(Model.DateGoodShow) ?? DBNull.Value)).ToList();
            return data;
        }


        public IList<AviatorLog> GetAviatorLog(int Procid, AviatorLog Model)
        {
            var data = GetDataInList<AviatorLog>("Proc_AviatorLog @ProcId=@p0,@Aviator_ID =@p1,@FromDate=@p2,@ToDate=@p3", new SqlParameter("@p0", (object)(Procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(Model.Aviator_ID)) ?? DBNull.Value), new SqlParameter("@p2", (object)(Model.FromDate) ?? DBNull.Value), new SqlParameter("@p3", (object)(Model.ToDate) ?? DBNull.Value)).ToList();
            return data;
        }


        public int SendForgot(ForgotPasswordViewModel model)
        {

            var data = GetDataInList<getId>("INSERT INTO [dbo].[forgotPassword]([username],[userId],[superiorId],[createTime])VALUES(@p0,@p1,@p2,getDate())SELECT @@IDENTITY    AS 'Identity' ", new SqlParameter("@p0", (object)(model.UserName) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.UserId) ?? DBNull.Value), new SqlParameter("@p2", (object)(model.superiorId) ?? DBNull.Value)).FirstOrDefault();
            return Convert.ToInt32(data.Identity);
        }

        public int UpdatePassword(int id, string haspassword)
        {
            var data = GetDataInList<updateId>(" UPDATE AspNetUsers SET PasswordHash = @p0 OUTPUT INSERTED.IntId as 'Identity' WHERE IntId = @p1 ", new SqlParameter("@p0", (object)(haspassword) ?? DBNull.Value), new SqlParameter("@p1", (object)(id) ?? DBNull.Value)).FirstOrDefault();
            return Convert.ToInt32(data.Identity);
        }

        public int UpdateForgotPasswordTable(int id)
        {
            var data = GetDataInList<updateId>(" UPDATE [forgotPassword] SET [ApprovedBy] = @p0, [ApprovedDate]=getDate() OUTPUT INSERTED.Id as 'Identity' WHERE Id = @p1 ", new SqlParameter("@p0", (object)(SessionManager.UserId) ?? DBNull.Value), new SqlParameter("@p1", (object)(id) ?? DBNull.Value)).FirstOrDefault();
            return Convert.ToInt32(data.Identity);
        }

        class updateId
        {
            public int Identity { get; set; }
        }
        class getId
        {
            public Decimal Identity { get; set; }
        }

        public List<ForgotPasswordViewModel> GetListResetPassword()
        {
            var data = GetDataInList<ForgotPasswordViewModel>("SELECT fe.id,c.[UserId], c.[ChildId] , c.[UserName], c.[RoleName], c.[EstablishmentFull],fe.[superiorId], fe.[createTime],fe.ApprovedDate FROM [aa7data].[dbo].[Child] c inner join [dbo].[forgotPassword] fe on c.ChildId = fe.userid  where c.UserId = @p0 and c.ChildId Not In(@p1)", new SqlParameter("@p0", (object)(SessionManager.UserIntId) ?? DBNull.Value), new SqlParameter("@p1", (object)(SessionManager.UserIntId) ?? DBNull.Value)).ToList();
            return data;
        }
        #region  for connection DB 
        public List<T> GetDataInList<T>(string query, params SqlParameter[] parameters)
        {
            return this.Database.SqlQuery<T>(query, parameters.Cast<object>().ToArray()).ToList();
        }

        #endregion

        #region QFIC
        public IList<DashCourse> QficCount(int procid, DashCourse model)
        {
            var data = GetDataInList<DashCourse>("proc_Course_Dashboard @procId=@p0,@CourseQFI=@p1, @UserId=@p2, @Unit_id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CourseQFI) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region AHICCount
        public IList<DashCourseAHIC> AHICCount(int procid, DashCourseAHIC model)
        {
            var data = GetDataInList<DashCourseAHIC>("proc_Course_Dashboard @procId=@p0,@CourseAHIC=@p1, @UserId=@p2, @Unit_id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CourseAHIC) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region IPCount
        public IList<DashCourseIP> IPCount(int procid, DashCourseIP model)
        {
            var data = GetDataInList<DashCourseIP>("proc_Course_Dashboard @procId=@p0,@CourseQFIIP=@p1, @UserId=@p2, @Unit_id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CourseQFIIP) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region QFIEPCount
        public IList<DashCourseQFIEP> QFIEPCount(int procid, DashCourseQFIEP model)
        {
            var data = GetDataInList<DashCourseQFIEP>("proc_Course_Dashboard @procId=@p0,@CourseQFIEP =@p1, @UserId=@p2, @Unit_id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CourseQFIEP) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region QFIObPCount
        public IList<DashCourseQFIObP> QFIObPCount(int procid, DashCourseQFIObP model)
        {
            var data = GetDataInList<DashCourseQFIObP>("proc_Course_Dashboard @procId=@p0,@CourseQFIObP =@p1, @UserId=@p2, @Unit_id=@p3", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.CourseQFIObP) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region HEPTRCount
        public IList<DashAircraftHEPTR> HEPTRCount(int procid, DashAircraftHEPTR model)
        {
            var data = GetDataInList<DashAircraftHEPTR>("proc_Course_Dashboard @procId=@p0,@HEPTR =@p1, @UserId=@p2", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.HEPTR) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region RPASCount
        public IList<DashAircraftRPAS> RPASCount(int procid, DashAircraftRPAS model)
        {
            var data = GetDataInList<DashAircraftRPAS>("proc_Course_Dashboard @procId=@p0,@RPAS =@p1, @UserId=@p2", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.RPAS) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(SessionManager.UserIntId)) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion


        #region UserLog
        public IList<UserLog> UserLog(int procid, UserLog model)
        {
            var data = GetDataInList<UserLog>("proc_UserLog @procId=@p0,@Unit_ID =@p1, @UserId=@p2,@FromDate=@p3,@ToDate=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(model.UserId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.LogFromDate) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.LogToDate) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        #region  AP User Log 
        public IList<APUserLog> APUserLog(int procid, APUserLog model)
        {
            var data = GetDataInList<APUserLog>("proc_UserLog @procId=@p0,@Unit_ID =@p1, @UserId=@p2,@FromDate=@p3,@ToDate=@p4", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(Convert.ToInt16(model.Unit_ID)) ?? DBNull.Value), new SqlParameter("@p2", (object)(Convert.ToInt16(model.UserId)) ?? DBNull.Value), new SqlParameter("@p3", (object)(model.LogFromDate) ?? DBNull.Value), new SqlParameter("@p4", (object)(model.LogToDate) ?? DBNull.Value)).ToList();
            return data;
        }

        #endregion


        #region  Counter Log 
        public IList<CounterLog> CounterLog(int procid, CounterLog model)
        {
            var data = GetDataInList<CounterLog>("proc_Unit_CRUD @procId=@p0, @Command = @p1", new SqlParameter("@p0", (object)(procid) ?? DBNull.Value), new SqlParameter("@p1", (object)(model.ComdName) ?? DBNull.Value)).ToList();
            return data;
        }
        #endregion

        public IList<UserWiseRole> GetUserRole(UserWiseRole model)
        {
            var data = GetDataInList<UserWiseRole>("proc_UserWiseRole").ToList();
            return data;
        }
    }
}