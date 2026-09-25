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
            var data = GetDataInList<PolicyCRUD>("proc_Policy_CRUD @procId='" + procid + "',@PolicyId='" + model.PolicyId + "',@PolicyName='" + model.PolicyName + "' ,@PolicyRemarks='" + model.PolicyRemarks + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Orderby='" + model.Orderby + "',@TypeofDocu='"+ model.TypeOfDocu + "',@UserPolicyControlId='"+model.UserPolicyControlId +"',@PUserId='" + model.PUserId + "'").ToList();
            return data;
        }

        public IList<downloadLog> downloadLogCRUD(int procid, downloadLog model)
        {
            var data = GetDataInList<downloadLog>("proc_DownloadPolicy_CRUD @procId='" + procid + "',@downloadby='" + Convert.ToInt16(SessionManager.UserIntId) + "',@PolicyId='" + model.PolicyId + "',@FileName='" + model.FileName + "',@ICNumber='" + model.ICNumber + "',@IpAddress='" + model.IpAddress + "',@AccessDownload='"+ model.AccessDownload + "'").ToList();
            return data;
        }

        #endregion



        #region Comd
        public IList<ComdCRUD> ComdCRUD(int procid, ComdCRUD model)
        {
            var data = GetDataInList<ComdCRUD>("proc_Comd_CRUD @procId='" + procid + "',@ComdId='" + model.ComdId + "',@ComdName='" + model.ComdName + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Orderby='"+ model.Orderby +"'").ToList();
            return data;
        }
        #endregion






        #region Sqn
        public IList<SqnCRUD> SqnCRUD(int procid, SqnCRUD model)
        {
            var data = GetDataInList<SqnCRUD>("proc_Sqn_CRUD @procId='" + procid + "',@SqnId='" + model.SqnId + "',@SqnName='" + model.SqnName + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ComdId='" + model.ComdId + "',@CorpsId='" + model.CorpsId + "',@BdeCatId='"+model.BdeCatId+ "',@Orderby='" + model.Orderby + "'").ToList();
            return data;
        }
        #endregion



        #region BdeCat
        public IList<BdeCatCRUD> BdeCatCRUD(int procid, BdeCatCRUD model)
        {
            var data = GetDataInList<BdeCatCRUD>("proc_BdeCat_CRUD @procId='" + procid + "',@BdeCatId='" + model.BdeCatId + "',@BdeCatName='" + model.BdeCatName + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ComdId='" + model.ComdId+ "',@CorpsId='"+model.CorpsId+ "',@Orderby='" + model.Orderby + "'").ToList();
            return data;
        }
        #endregion


        #region Rank
        public IList<RankCRUD> RankCRUD(int procid, RankCRUD model)
        {
            var data = GetDataInList<RankCRUD>("proc_Rank_CRUD @procId='" + procid + "',@RankId='" + model.RankId + "',@RankName='" + model.RankName + "' ,@RankAbbreviation='" + model.RankAbbreviation + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion

        #region Corps

        public IList<CorpsCRUD> CorpsCRUD(int procid, CorpsCRUD model)
        {
            var data = GetDataInList<CorpsCRUD>("proc_Corps_CRUD @procId='" + procid + "',@CorpsId='" + model.CorpsId + "',@CorpsName='" + model.CorpsName + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ComdId='" + model.ComdId + "',@Orderby='" + model.Orderby + "'").ToList();
            return data;
        }
        #endregion

        #region User Policy Control
        public IList<UserPolicyControlCRUD> UserPolicyControlCRUD(int procid, UserPolicyControlCRUD model)
        {
            var data = GetDataInList<UserPolicyControlCRUD>("proc_UserPolicyControl_CRUD @UserPolicyControlId='"+ model.UserPolicyControlId +"', @procId='" + procid + "',@PUserId='" + model.PUserId + "',@Policies='" + model.Policies + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion


        #region Unit
        public IList<UnitCRUD> UnitCRUD(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId='" + procid + "',@Unit_ID='" + model.Unit_ID + "',@UnitName='" + model.UnitName + "',@Command='" + model.ComdId + "' ,@Corps='" + model.CorpsId + "' ,@Sqn='" + model.SqnId + "',@BdeCat='" + model.BdeCatId + "'  ,@TypeOfUnit='" + model.TypeOfUnit + "'  ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Isflight='"+ model.IsFlight + "',@ScheduleId='"+ model.ScheduleId + "',@Inbox_Id = '" + model.InboxID +"'").ToList();
            return data;
        }


        public IList<UnitCRUD> UnitCRUDNew(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId='" + procid + "',@Unit_ID='" + model.Unit_ID + "',@UnitName='" + model.UnitName + "',@Command='" + model.ComdId + "' ,@Corps='" + model.CorpId + "' ,@Sqn='" + model.SqnId + "',@BdeCat='" + model.BdeCatId + "'  ,@TypeOfUnit='" + model.TypeOfUnit + "'  ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Isflight='" + model.IsFlight + "',@ScheduleId='" + model.ScheduleId + "',@Inbox_Id = '" + model.InboxID + "'").ToList();
            return data;
        }



        public IList<HitCRUD> HitCRUD(int procid, HitCRUD model)
        {
            var data = GetDataInList<HitCRUD>("proc_Unit_CRUD @procId='" + procid + "'").ToList();
            return data;
        }

        

        public IList<UnitCRUD> GetAllUnitCRUD(int procid, UnitCRUD model)
        {
            var data = GetDataInList<UnitCRUD>("proc_Unit_CRUD @procId='" + procid + "',@Unit_ID='" + model.Unit_ID + "',@UnitName='" + model.UnitName + "',@Command='" + model.ComdId + "' ,@Corps='" + model.CorpsId + "' ,@Sqn='" + model.Sqn + "',@BdeCat='" + model.BdeCatId + "'  ,@TypeOfUnit='" + model.TypeOfUnit + "'  ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Isflight='" + model.IsFlight + "',@ScheduleId='" + model.ScheduleId + "'").ToList();
            return data;
        }
        #endregion

        #region Category
        public IList<CategoryCRUD> CategoryCRUD(int procid, CategoryCRUD model)
        {
            var data = GetDataInList<CategoryCRUD>("proc_Category_CRUD @procId='" + procid + "',@CategoryId='" + model.CategoryId + "',@CategoryName='" + model.CategoryName + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ScheduleId='"+ model.ScheduleId +"',@Orderby='"+ model.Orderby +"'").ToList();
            return data;
        }
        #endregion

        #region Branch
        public IList<BranchCRUD> BranchCRUD(int procid, BranchCRUD model)
        {
            var data = GetDataInList<BranchCRUD>("proc_Branch_CRUD @procId='" + procid + "',@BranchId='" + model.BranchId + "',@BranchName='" + model.BranchName + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ScheduleId='" + model.ScheduleId + "',@Orderby='" + model.Orderby +"'").ToList();
            return data;
        }
        #endregion


        #region Type of Conf
        public IList<TypeofConfCRUD> TypeofConfCRUD(int procid, TypeofConfCRUD model)
        {
            var data = GetDataInList<TypeofConfCRUD>("proc_TypeofConf_CRUD @procId='" + procid + "',@TypeOfConfId='" + model.TypeOfConfId + "',@TypeOfConf='" + model.TypeOfConf + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@PolicyId='" + model.PolicyId + "'").ToList();
            return data;
        }

        public IList<SearchData> GetPolicyTitleData(int procid, SearchData model)
        {
            var data = GetDataInList<SearchData>("proc_TypeofConf_CRUD @procId='" + procid + "',@TypeOfConfId='" + model.Id + "'").ToList();
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
            var data = GetDataInList<DashAvi>("Dashboard @procId='" + procid + "',@UserId  = '" + UserId + "', @Unit_id=" + Unit_ID + ",@Ip='" + Ip + "',@Remarks='" + Remarks + "'").ToList();



            return data;
        }
        #region UserList
        public IList<UserListCRUD> UserListCRUD(int procid, UserListCRUD model)
        {
            var data = GetDataInList<UserListCRUD>("proc_Unit_CRUD @procId='" + procid + "',@Unit_ID='" + model.Unit_ID + "',@UnitName='" + model.UnitName + "' ,@UserName='" + model.UserName + "' ,@Command='" + model.Command + "' ,@Corps='" + model.Corps + "'  ,@BdeCat='" + model.BdeCat + "'  ,@TypeOfUnit='" + model.TypeOfUnit + "'  ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion



        #region GenderCRUD
        public IList<GenderCRUD> GenderCRUD(int procid, GenderCRUD model)
        {
            var data = GetDataInList<GenderCRUD>("proc_Gender_CRUD @procId='" + procid + "',@GenderId='" + model.GenderId + "',@Gender='" + model.Gender + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion


   

        #region StagesGroundAccidentCRUD
        public IList<StagesGroundAccidentCRUD> StagesGroundAccidentCRUD(int procid, StagesGroundAccidentCRUD model)
        {
            var data = GetDataInList<StagesGroundAccidentCRUD>("proc_StagesGroundAccident_CRUD @procId='" + procid + "',@StagesGroundAccidentId='" + model.StagesGroundAccidentId + "',@StagesGroundAccident='" + model.StagesGroundAccident + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion


        #region PeriodOperationCRUD
        public IList<PeriodOperationCRUD> PeriodOperationCRUD(int procid, PeriodOperationCRUD model)
        {
            var data = GetDataInList<PeriodOperationCRUD>("proc_PeriodOperation_CRUD @procId='" + procid + "',@PeriodOperationId='" + model.PeriodOperationId + "',@PeriodOperation='" + model.PeriodOperation + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion







        #region FlyingStatusCRUD
        public IList<FlyingStatusCRUD> FlyingStatusCRUD(int procid, FlyingStatusCRUD model)
        {
            var data = GetDataInList<FlyingStatusCRUD>("proc_FlyingStatus_CRUD @procId='" + procid + "',@FlyingStatusId='" + model.FlyingStatusId + "',@FlyingStatus='" + model.FlyingStatus + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion






        #endregion Master


        #region RankHistory
        public IList<RankHistory> RankHistory(int procid, RankHistory model)
        {
            var data = GetDataInList<RankHistory>("Proc_AviatorRankApptHistory @procId='" + procid + "',@Aviator_Id='" + Convert.ToInt16(model.Aviator_Id) + "'").ToList();
            return data;
        }
        #endregion

        #region Folder Control Permission [04-07-2024] 
        public IList<FolderControlPermissionCRUD> FolderControlPermissionCRUD(int procid, FolderControlPermissionCRUD model)
        {
            var data = GetDataInList<FolderControlPermissionCRUD>("proc_FolderControl_CRUD @FolderControlId='" + model.FolderControlId + "', @procId='" + procid + "',@PolicyID='" + model.PolicyId + "',@Useres='" + model.Users + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }


        public IList<UserCRUD> UserCRUD(int procid, UserCRUD model)
        {
            var data = GetDataInList<UserCRUD>("proc_FolderPermission_CRUD @procId='" + procid + "',@PolicyId='" + model.PolicyId + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Orderby='" + model.Orderby + "',@FolderControlId='" + model.FolderControlId + "',@PUserId='" + model.PUserId + "'").ToList();
            return data;
        }

        #endregion


        #region Policy Folder Permission [08-07-2024]
        public IList<PolicyFolderCRUD> proc_PolicyFolder_CRUD(int procid, PolicyFolderCRUD model)
        {
            var data = GetDataInList<PolicyFolderCRUD>("proc_PolicyFolder_CRUD @procId='" + procid + "',@PolicyId='" + model.PolicyId + "',@PolicyName='" + model.PolicyName + "' ,@PolicyRemarks='" + model.PolicyRemarks + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Orderby='" + model.Orderby + "',@TypeofDocu='" + model.TypeOfDocu + "',@UserPolicyControlId='" + model.UserPolicyControlId + "',@PUserId='" + model.PUserId + "'").ToList();
            return data;
        }

        #endregion

        public IList<DropdownList> LoadUnitAll(int procid, int UserId,int ComndId,int CorpsId,int BdeId,int SqnId)
        {
            var data = GetDataInList<DropdownList>("FilterDropDown @procId='"+ procid + "',@UserId ='" + UserId + "',@ComndId ='" + ComndId + "',@CorpsId ='" + CorpsId + "',@BdeId ='" + BdeId + "',@SqnId ='" + SqnId + "'").ToList();



            return data;
        }
        #region ApptCRUD
        public IList<ApptCRUD> ApptCRUD(int procid, ApptCRUD model)
        {
            var data = GetDataInList<ApptCRUD>("proc_Appt_CRUD @procId='" + procid + "',@ApptId='" + model.ApptId + "',@Appt='" + model.Appt + "',@ApptAbbreviation='" + model.ApptAbbreviation + "'  ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Orderby='" + model.Orderby + "',@NewOrderby='" + model.NewOrderby + "'").ToList();
            return data;
        }
        #endregion

        #region Aviator Appt
        public IList<AviatorApptCRUD> AviatorApptCRUD(int procid, AviatorApptCRUD model)
        {
            var data = GetDataInList<AviatorApptCRUD>("proc_AviatorAppt_CRUD @procId='" + procid + "',@AviatorAppointment_ID='" + model.AviatorAppointment_ID + "',@Aviator_ID='" + model.Aviator_ID + "',@ApptDate='" + model.ApptDate + "' ,@ApptName='" + model.ApptName + "' ,@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.UnitName + "'").ToList();
            return data;

        }

        #endregion

        #region Aviator Rank
        public IList<AviatorRankCRUD> AviatorRankCRUD(int procid, AviatorRankCRUD model)
        {
            var data = GetDataInList<AviatorRankCRUD>("proc_AviatorRank_CRUD @procId='" + procid + "',@AviatorRank_ID='" + model.AviatorRank_ID + "',@Aviator_ID='" + model.Aviator_ID + "',@RankDate='" + model.RankDate + "' ,@Rank='" + model.Rank + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.UnitName + "'").ToList();
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
            var data = GetDataInList<ScheduleLetter>("proc_ScheduleLetter_CRUD @procId='" + procid + "',@ScheduleId='" + model.ScheduleId + "', @FromDate='" + model.FromDate + "',@ToDate='" + model.ToDate + "',@Comd='" + model.Comd + "',@UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Unit_ID=" + Convert.ToInt16(SessionManager.Unit_ID) + ",@StatusName ='" + model.StatusName + "' ,@Remark ='" + model.Remark + "',@Subject ='" + model.Subject + "',@ScheduleRefNoDt ='" + model.ScheduleRefNoDt + "',@IsClosed='" + model.isClosed + "',@OrganizerCat='" + model.OrganizerCatId + "',@SponsorCat='" + model.SponsorCatId + "',@NodalCat='" + model.NodalCatId + "',@BranchIds='" + model.BranchIds + "',@TypeOfConfId='" + model.TypeOfConfId + "',@ParticipateUserId='" + model.ParticipateUsers + "',@ParticipateNewUserId='" + model.ParticipateNewUsers + "' ").ToList();
            return data;

        }






        #region Inbox
        public IList<InboxCRUD> InboxCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Inbox_CRUD @procId='" + procid + "',@Inbox_ID='" + model.Inbox_ID + "',@Schedule_ID='" + model.Schedule_ID + "',@Comd_ID='" + model.ComdId + "',@Branch_ID='" + model.Branch + "',@Title='" + model.Title + "',@Summary='" + model.Summary + "',@Upload='" + model.Upload + "',@DateTimeOfUpdate='" + model.DateTimeOfUpdate + "',@LastUpdatedBy='" + model.LastUpdatedBy + "' , @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Unit_ID  = '" + model.Unit_ID + "',@UnitName  = '" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }

        public IList<InboxCRUD> SubmitInbox(string procid, string Query)

        {

            var data = GetDataInList<InboxCRUD>("proc_Submit @procId='" + procid + "',@Query='" + Query + "'").ToList();
            return data;
        }
        #endregion

        public IList<InboxCRUD> CreateAPCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_AP_CRUD @procId='" + procid + "',@id='" + model.Inbox_ID + "',@Inbox_ID='" + model.Inbox_ID + "',@RefId='" + model.Schedule_ID + "',@ComdId='" + model.Unit_ID + "',@Branch='" + model.Branch + "',@Title='" + model.Title + "',@Upload='" + model.Upload + "',@UploadPath='" + model.UploadPath + "',@Summary='" + model.Summary + "',@Status='" + model.Status + "',@DateTimeOfUpdate='" + model.DateTimeOfUpdate + "',@LastUpdatedBy='" + model.LastUpdatedBy + "',@IsVisibleToAll='" + model.IsVisibleToAll + "' ,@AllowEdit='" + model.AllowEdit + "' , @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Unit_ID  = '" + model.Unit_ID + "',@CategoryId='" + model.CategoryId + "',@SponsorCategoryId='" + model.SponsorCategoryId + "',@NodalCategoryId='" + model.NodalCategoryId + "',@WordFile='" + model.WordFile + "',@WordFilePath='" + model.WordFilePath + "'").ToList();
            return data;
        }

        public IList<AgendaPointCRUD> CreateAgendaPoint(int procid, AgendaPointCRUD model)
        {
            var data = GetDataInList<AgendaPointCRUD>("proc_AP_CRUD @procId='" + procid + "',@id='" + model.Inbox_ID + "',@Inbox_ID='" + model.Inbox_ID + "',@RefId='" + model.Schedule_ID + "',@ComdId='" + model.Unit_ID + "',@Branch='" + model.Branch + "',@Title='" + model.Title + "',@Upload='" + model.Upload + "',@UploadPath='" + model.UploadPath + "',@Summary='" + model.Summary + "',@Status='" + model.Status + "',@DateTimeOfUpdate='" + model.DateTimeOfUpdate + "',@LastUpdatedBy='" + model.LastUpdatedBy + "',@IsVisibleToAll='" + model.IsVisibleToAll + "' ,@AllowEdit='" + model.AllowEdit + "' , @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Unit_ID  = '" + model.Unit_ID + "',@CategoryId='" + model.CategoryId + "',@SponsorCategoryId='" + model.SponsorCategoryId + "',@NodalCategoryId='" + model.NodalCategoryId + "',@WordFile='" + model.WordFile + "',@WordFilePath='" + model.WordFilePath + "'").ToList();
            return data;
        }

        public IList<APSupportingDocu> GetSupportingData(int procid, APSupportingDocu model)


        {
            var data = GetDataInList<APSupportingDocu>("proc_SupportingData_CRUD @procId='" + procid + "',@SuppFileName='" + model.SuppFileName + "',@SuppFilePath='" + model.SuppFilePath + "',@UploadBy='" + model.UploadBy + "'").ToList();
            return data;
        }



        public IList<AgendaPointLog> AgendaPointLogCRUD(int procid, AgendaPointLog model)
        {
            var data = GetDataInList<AgendaPointLog>("proc_AP_CRUD @procId='" + procid + "',@Inbox_ID='" + model.Inbox_ID + "'").ToList();
            return data;
        }

        public IList<NotingDetail> CreateNotingDetailCRUD(int procid, NotingDetail model)
        {
            var data = GetDataInList<NotingDetail>("proc_NotingDetail_CRUD @procId='" + procid + "',@InboxId  ='" + model.InboxId + "',@CommentFrom ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Comment='" + model.Comment + "',@UpdatedByUserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Attachment='" + model.Attachment + "'").ToList();
            return data;
        }

        public IList<InboxCRUD> ForwardCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Forward_CRUD @procId='" + procid + "',@InboxID='" + model.Inbox_ID + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@ForwardFrom='"+ Convert.ToInt16(SessionManager.UserIntId) + "',@ForwardTo='"+ model.SentID + "',@ForwardText='"+ model.FwdComment +"',@Nodal='"+ model.NodalId + "',@NodalComment='" + model.NodalComment + "',@ForwordId='" + model.ForwardID + "'").ToList();
            return data;
        }

        public IList<InboxCRUD> RevertMailCRUD(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_Forward_CRUD @procId='" + procid + "',@Ord='"+ model.Ord + "',@FwdId='"+ model.FwdId + "'").ToList();
            return data;
        }
        

        public IList<InboxCRUD> OpenCloseAgendaPoint(int procid, InboxCRUD model)
        {
            var data = GetDataInList<InboxCRUD>("proc_OpenCloseAgendaPoint_CRUD @procId='" + procid + "',@ApId='" + model.Inbox_ID + "',@CloseBy='" + Convert.ToInt16(SessionManager.UserIntId) + "',@OpenBy='" + Convert.ToInt16(SessionManager.UserIntId) + "',@CloseReason='" + model.CloseReason + "',@OpenReason='" + model.OpenReason + "',@CloseIds='"+ model.CloseIds +"'").ToList();
            return data;
        }


        #region Inbox Noting Fwd
        public IList<InboxNotingFwdCRUD> InboxNotingFwdCRUD(int procid, InboxNotingFwdCRUD model)
        {
            var data = GetDataInList<InboxNotingFwdCRUD>("proc_InboxNotingFwd_CRUD @procId='" + procid + "',@InboxNoting_ID='" + model.InboxNoting_ID + "',@Inbox_ID='" + model.Inbox_ID + "',@Comd_ID='" + model.Comd_ID + "',@Branch_ID='" + model.Branch_ID + "',@Title='" + model.Title + "',@Summary='" + model.Summary + "',@DateTimeOfUpdate='" + model.DateTimeOfUpdate + "',@LastUpdatedBy='" + model.LastUpdatedBy + "' , @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@Unit_ID  = '" + model.Unit_ID + "',@UnitName  = '" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }

        public IList<InboxNotingFwdCRUD> SubmitInboxNotingFwd(string procid, string Query)

        {

            var data = GetDataInList<InboxNotingFwdCRUD>("proc_Submit @procId='" + procid + "',@Query='" + Query + "'").ToList();
            return data;
        }
        #endregion





        #endregion

        #region

        public IList<ImportResult> CheckXML(int procid, string xmldata)
        {
            var data = GetDataInList<ImportResult>("proc_CheckXML @procid='" + procid + "',@xml='" + xmldata + "' ").ToList();
            return data;

        }

        public IList<ImportResult> ReadXML(int procid, string xmldata)
        {
            var data = GetDataInList<ImportResult>("proc_GetXMLData @procid='" + procid + "',@xml='" + xmldata + "' ").ToList();
            return data;
        }

        #endregion


        public IList<DashAvi> ErrorLogDashboard2(int procid, string Message, string ErrorInfo, string LogType)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId='" + procid + "',@Message='" + Message + "',@ErrorInfo='" + ErrorInfo + "',@LogType='" + LogType + "'").ToList();
            return data;
        }

        public IList<AppxG> UpdateAppxG(int procid, AppxG model)
        {
            var data = GetDataInList<AppxG>("AppxG_CRUD @procid='" + procid + "' ,@FinalAppxId ='" + model.FinalAppxId + "',@AppxGForYear ='" + model.AppxGForYear + "',@CheckAviatorEndorsementExp ='" + model.CheckAviatorEndorsementExp + "',@CheckAviatorEndorsementOnAirCraftType ='" + model.CheckAviatorEndorsementOnAirCraftType + "',@CheckAviatorEndorsementExpUnit ='" + model.CheckAviatorEndorsementExpUnit + "',@TotalInstrDayExp ='" + model.TotalInstrDayExp + "',@TotalInstrNightExp ='" + model.TotalInstrNightExp + "',@Syllabus9ACompletedOn ='" + model.Syllabus9ACompletedOn + "',@CompletedByICNo ='" + model.CompletedByICNo + "',@CompletedByRank ='" + model.CompletedByRank + "',@CompletedByName ='" + model.CompletedByName + "',@NoQFIInTheUnit ='" + model.NoQFIInTheUnit + "',@RecomeForCheckAviatorUnit ='" + model.RecomeForCheckAviatorUnit + "',@RecomeForCheckAviatorOnType ='" + model.RecomeForCheckAviatorOnType + "', @UserId='" + Convert.ToString(SessionManager.UserIntId) + "',  @ISRecommendByBrigAvn='" + Convert.ToInt16(model.ISRecommendByBrigAvn) + "', @IsApprovedByGSO  ='" + Convert.ToInt16(model.IsApprovedByGSO) + "'").ToList();
            return data;
        }


        public IList<user> insertUserCert(user user, byte[] cert)
        {
            string st = Encoding.ASCII.GetString(cert);


            // string st = ByteArrayToString(cert);
            var data = GetDataInList<user>("insertCert @userId='" + user.IntId + "',@publicKey='" + st + "'").ToList();
            return data;
        }
        public IList<UserKeyMapping> findPublicKey(UserKeyMapping userKeyMapping)
        {
            var data = GetDataInList<UserKeyMapping>("findPublicKey @userId='" + userKeyMapping.UserId + "'").ToList();
            return data;

        }
        public IList<user> getAllUsers()
        {
            var data = GetDataInList<user>("getAllUsers").ToList();
            return data;
        }


    

        #region GetAviatorForDashboard
        public IList<DashAvi> GetAviatorForDashboard(int procid,int Unit_ID)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId='" + procid + "',@UserId  = '" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Unit_ID + "").ToList();



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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="procid"></param>
        /// <returns></returns>
        /// 



        //public VieViewAviatorDetails VieViewAviatorDetails(int AviatorId)
        //{
        //    VieViewAviatorDetails model = new VieViewAviatorDetails();
        //    ///Aviatore
        //    var data1 = GetDataInList<AviatorCRUD>(" proc_VieViewAviatorDetails @procid ='" + 1 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorCRUD = data1;

        //    var data2 = GetDataInList<AviatorContactDetailsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 2 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorContactDetailsCRUD = data2;


        //    var data3 = GetDataInList<AviatorCoursesCRUD>(" proc_VieViewAviatorDetails @procid ='" + 3 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorCoursesCRUD = data3;


        //    var data4 = GetDataInList<AviatorHonourAwardsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 4 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorHonourAwardsCRUD = data4;

        //    var data5 = GetDataInList<AviatorFlyingHrsCRUD>("proc_VieViewAviatorDetails @procid ='" + 5 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorFlyingHrsCRUD = data5;






        //    var data8 = GetDataInList<AviatorMedicalCRUD>(" proc_VieViewAviatorDetails @procid ='" + 8 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorMedicalCRUD = data8;

        //    var data9 = GetDataInList<AviatorHeptrRPAViewCRUD>(" proc_VieViewAviatorDetails @procid ='" + 9 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorHeptrRPAViewCRUD = data9;

        //    var data10 = GetDataInList<AviatorGoodShowCRUD>(" proc_VieViewAviatorDetails @procid ='" + 10 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorGoodShowCRUD = data10;



        //    var data11 = GetDataInList<AviatorForeignVisitCRUD>(" proc_VieViewAviatorDetails @procid ='" + 11 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorForeignVisitCRUD = data11;


        //    var data12 = GetDataInList<AviatorAccidentCRUD>(" proc_VieViewAviatorDetails @procid ='" + 12 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorAccidentCRUD = data12;



        //    return model;

        //}




        //public HistoryAviatorDetails HistoryAviatorDetails(int AviatorId)
        //{
        //    HistoryAviatorDetails model = new HistoryAviatorDetails();
        //    ///Aviatore
        //    var data1 = GetDataInList<AviatorCRUD>(" proc_VieViewAviatorDetails @procid ='" + 1 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorCRUD = data1;

        //    //var data2 = GetDataInList<AviatorContactDetailsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 2 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorContactDetailsCRUD = data2;


        //    //var data3 = GetDataInList<AviatorCoursesCRUD>(" proc_VieViewAviatorDetails @procid ='" + 3 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorCoursesCRUD = data3;


        //    //var data4 = GetDataInList<AviatorHonourAwardsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 4 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorHonourAwardsCRUD = data4;

        //    //var data5 = GetDataInList<AviatorFlyingHrsCRUD>("proc_VieViewAviatorDetails @procid ='" + 5 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorFlyingHrsCRUD = data5;



        //    //var data6 = GetDataInList<AviatorSpecialEqptCRUD>(" proc_VieViewAviatorDetails @procid ='" + 6 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorSpecialEqptCRUD = data6;

        //    //var data7 = GetDataInList<AviatorSpecialQualCRUD>(" proc_VieViewAviatorDetails @procid ='" + 7 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorSpecialQualCRUD = data7;

        //    //var data8 = GetDataInList<AviatorMedicalCRUD>(" proc_VieViewAviatorDetails @procid ='" + 8 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorMedicalCRUD = data8;

        //    //var data9 = GetDataInList<AviatorHeptrRPAViewCRUD>(" proc_VieViewAviatorDetails @procid ='" + 9 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorHeptrRPAViewCRUD = data9;

        //    //var data10 = GetDataInList<AviatorGoodShowCRUD>(" proc_VieViewAviatorDetails @procid ='" + 10 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorGoodShowCRUD = data10;

        //    return model;

        //}



        //public HistoryHeptrRPADetails HistoryHeptrRPADetails(int AviatorId)
        //{
        //    HistoryHeptrRPADetails model = new HistoryHeptrRPADetails();



        //    var data9 = GetDataInList<AviatorHeptrRPAViewCRUD>(" proc_VieViewAviatorDetails @procid ='" + 11 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorHeptrRPAViewCRUD = data9;


        //    ///Aviator
        //    var data1 = GetDataInList<AviatorCRUD>(" proc_VieViewAviatorDetails @procid ='" + 1 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    model.ILAviatorCRUD = data1;

        //    //var data2 = GetDataInList<AviatorContactDetailsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 2 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorContactDetailsCRUD = data2;


        //    //var data3 = GetDataInList<AviatorCoursesCRUD>(" proc_VieViewAviatorDetails @procid ='" + 3 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorCoursesCRUD = data3;


        //    //var data4 = GetDataInList<AviatorHonourAwardsCRUD>(" proc_VieViewAviatorDetails @procid ='" + 4 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorHonourAwardsCRUD = data4;

        //    //var data5 = GetDataInList<AviatorFlyingHrsCRUD>("proc_VieViewAviatorDetails @procid ='" + 5 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorFlyingHrsCRUD = data5;



        //    //var data6 = GetDataInList<AviatorSpecialEqptCRUD>(" proc_VieViewAviatorDetails @procid ='" + 6 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorSpecialEqptCRUD = data6;

        //    //var data7 = GetDataInList<AviatorSpecialQualCRUD>(" proc_VieViewAviatorDetails @procid ='" + 7 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorSpecialQualCRUD = data7;

        //    //var data8 = GetDataInList<AviatorMedicalCRUD>(" proc_VieViewAviatorDetails @procid ='" + 8 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorMedicalCRUD = data8;


        //    //var data10 = GetDataInList<AviatorGoodShowCRUD>(" proc_VieViewAviatorDetails @procid ='" + 10 + "', @Aviator_ID ='" + AviatorId + "', @UserId ='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
        //    //model.ILAviatorGoodShowCRUD = data10;

        //    return model;

        //}



        #region

        public IList<TransationStrengthReturn> IUDUnitStrReturn(TransationStrengthReturn model, int procid)
        {
            var data = GetDataInList<TransationStrengthReturn>("proc_UnitStrReturn @ProcId='" + procid + "',  @StrId = '" + model.StrId + "', @SUSNo =   '" + model.SUSNo + "' ,@PosnAs ='" + model.PosnAs + "' , @PresentReturnSerNo =    '" + model.PresentReturnSerNo + "' ,@PresentReturnDate =   '" + model.PresentReturnDate + "' , @LastReturnSerNo =    '" + model.LastReturnSerNo + "' ,@LastReturnDate =   '" + model.LastReturnDate + "'  ,@LastIAFF =   '" + model.LastIAFF + "'  ,@LastIAFFDate =   '" + model.LastIAFFDate + "'  ,@FmnOfUnit =  '" + model.FmnOfUnit + "'   ,@Location =  '" + model.Location + "'   ,@EstNo =   '" + model.EstNo + "'  ,@EstDate =  '" + model.EstDate + "'   ,@CoRemarks =  '" + model.SUSNo + "'   ,@CoCertified =   '" + model.SUSNo + "'  ,@OffrsPostedExcessToEst =  '" + model.CoRemarks + "'   ,@OffrsSOSAndHeldOnSupernumeraryStr =   '" + model.OffrsSOSAndHeldOnSupernumeraryStr + "'  ,@DetailsOfPostingDuesInOffrs =   '" + model.DetailsOfPostingDuesInOffrs + "'  ,@DetailsOfPostingDuesOutOffrs =   '" + model.DetailsOfPostingDuesOutOffrs + "'  ,@PSCQualified =   '" + model.PSCQualified + "'  ,@ReEmpOffrs =     '" + model.ReEmpOffrs + "',@OffrsOnSuperStr =   '" + model.OffrsOnSuperStr + "'  ,@Remarks =   '" + model.Remarks + "'  ,@Month =  '" + model.Month + "'   ,@Year = '" + model.Year + "'    ,@userId = '" + SessionManager.UserId + "',@UnitName='" + SessionManager.UnitName + "' ").ToList();
            return data;

        }

        #endregion
        // GEBDetailsForSqnCdr





        #region CheckLoggedIn
        public IList<CheckLoggedIn> CheckLoggedIn(int procid, int IntId, string UserName, string id)
        {
            var data = GetDataInList<CheckLoggedIn>("proc_UpdateLogin @procId='" + procid + "',@intId='" + IntId + "' ,@UserName='" + UserName + "'  ,@id='" + id + "'").ToList();
            return data;
        }
        #endregion





        public IList<AddUpdateStatus> AddUpdateStatus(AddUpdateStatus Model)
        {
            var data = GetDataInList<AddUpdateStatus>("Proc_AddUpdateStatus @FormName='" + Model.FormName + "',@Aviator_ID ='" + Convert.ToInt16(Model.Aviator_Id) + "'," +
                                                      " @CourseId ='" + Convert.ToInt16(Model.CourseID) + "',@AircraftType_ID='"+ Convert.ToInt16(Model.AircraftType_ID) + "'," +
                                                      "@HeptrRPAMasterId='" + Convert.ToInt16(Model.HeptrRPAMasterId) + "',@CatIRId='" + Convert.ToInt16(Model.CatIRId) + "'," +
                                                      "@RPACatId='" + Convert.ToInt16(Model.RPACatId) + "',@HonourAwardsDate='" + Model.HonourAwardsDate + "', " +
                                                      "@Month='"+ Model.Month + "',@SpecialEqpt_ID='"+ Model.SpecialEqpt_ID + "',@ForeignPostingTypeId='" + Model.ForeignPostingTypeId + "', " +
                                                      "@SpecialQual_ID='"+ Model.SpecialQual_ID + "',@MedicalStartDate='"+ Model.MedicalStartDate + "',@DateOfAccidentIncident='"+ Model.DateOfAccidentIncident + "',@DateGoodShow='" + Model.DateGoodShow + "'").ToList();
            return data;
        }


        public IList<AviatorLog> GetAviatorLog(int Procid, AviatorLog Model)
        {
            var data = GetDataInList<AviatorLog>("Proc_AviatorLog @ProcId='" + Procid + "',@Aviator_ID ='" + Convert.ToInt16(Model.Aviator_ID) + "',@FromDate='" + Model.FromDate + "',@ToDate='" + Model.ToDate + "'").ToList();
            return data;
        }


        public int SendForgot(ForgotPasswordViewModel model)
        {

            var data = GetDataInList<getId>("INSERT INTO [dbo].[forgotPassword]([username],[userId],[superiorId],[createTime])VALUES('" + model.UserName + "','" + model.UserId + "','" + model.superiorId + "',getDate())SELECT @@IDENTITY    AS 'Identity' ").FirstOrDefault();
            return Convert.ToInt32(data.Identity);
        }

        public int UpdatePassword(int id, string haspassword)
        {
            var data = GetDataInList<updateId>(" UPDATE AspNetUsers SET PasswordHash = '" + haspassword + "'OUTPUT INSERTED.IntId as 'Identity' WHERE IntId = '" + id + "' ").FirstOrDefault();
            return Convert.ToInt32(data.Identity);
        }

        public int UpdateForgotPasswordTable(int id)
        {
            var data = GetDataInList<updateId>(" UPDATE [forgotPassword] SET [ApprovedBy] = '" + SessionManager.UserId + "', [ApprovedDate]=getDate() OUTPUT INSERTED.Id as 'Identity' WHERE Id = '" + id + "' ").FirstOrDefault();
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
            var data = GetDataInList<ForgotPasswordViewModel>("SELECT fe.id,c.[UserId], c.[ChildId] , c.[UserName], c.[RoleName], c.[EstablishmentFull],fe.[superiorId], fe.[createTime],fe.ApprovedDate FROM [aa7data].[dbo].[Child] c inner join [dbo].[forgotPassword] fe on c.ChildId = fe.userid  where c.UserId = '" + SessionManager.UserIntId + "' and c.ChildId Not In('" + SessionManager.UserIntId + "')").ToList();
            return data;
        }
        #region  for connection DB 
        public List<T> GetDataInList<T>(String Query)
        {
            try
            {
                IList<T> sqlList = this.Database.SqlQuery<T>(Query).ToList();
                return sqlList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region QFIC
        public IList<DashCourse> QficCount(int procid, DashCourse model)
        {
            var data = GetDataInList<DashCourse>("proc_Course_Dashboard @procId='" + procid + "',@CourseQFI='" + model.CourseQFI + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Convert.ToInt16(model.Unit_ID) + "").ToList();
            return data;
        }
        #endregion

        #region AHICCount
        public IList<DashCourseAHIC> AHICCount(int procid, DashCourseAHIC model)
        {
            var data = GetDataInList<DashCourseAHIC>("proc_Course_Dashboard @procId='" + procid + "',@CourseAHIC='" + model.CourseAHIC + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Convert.ToInt16(model.Unit_ID) + "").ToList();
            return data;
        }
        #endregion

        #region IPCount
        public IList<DashCourseIP> IPCount(int procid, DashCourseIP model)
        {
            var data = GetDataInList<DashCourseIP>("proc_Course_Dashboard @procId='" + procid + "',@CourseQFIIP='" + model.CourseQFIIP + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Convert.ToInt16(model.Unit_ID) + "").ToList();
            return data;
        }
        #endregion


        #region QFIEPCount
        public IList<DashCourseQFIEP> QFIEPCount(int procid, DashCourseQFIEP model)
        {
            var data = GetDataInList<DashCourseQFIEP>("proc_Course_Dashboard @procId='" + procid + "',@CourseQFIEP ='" + model.CourseQFIEP + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Convert.ToInt16(model.Unit_ID) + "").ToList();
            return data;
        }
        #endregion

        #region QFIObPCount
        public IList<DashCourseQFIObP> QFIObPCount(int procid, DashCourseQFIObP model)
        {
            var data = GetDataInList<DashCourseQFIObP>("proc_Course_Dashboard @procId='" + procid + "',@CourseQFIObP ='" + model.CourseQFIObP + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "', @Unit_id=" + Convert.ToInt16(model.Unit_ID) + "").ToList();
            return data;
        }
        #endregion


        #region HEPTRCount
        public IList<DashAircraftHEPTR> HEPTRCount(int procid, DashAircraftHEPTR model)
        {
            var data = GetDataInList<DashAircraftHEPTR>("proc_Course_Dashboard @procId='" + procid + "',@HEPTR ='" + model.HEPTR + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion


        #region RPASCount
        public IList<DashAircraftRPAS> RPASCount(int procid, DashAircraftRPAS model)
        {
            var data = GetDataInList<DashAircraftRPAS>("proc_Course_Dashboard @procId='" + procid + "',@RPAS ='" + model.RPAS + "', @UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "'").ToList();
            return data;
        }
        #endregion


        #region UserLog
        public IList<UserLog> UserLog(int procid, UserLog model)
        {
            var data = GetDataInList<UserLog>("proc_UserLog @procId='" + procid + "',@Unit_ID ='" + Convert.ToInt16(model.Unit_ID) + "', @UserId='" + Convert.ToInt16(model.UserId) + "',@FromDate='"+ model.LogFromDate + "',@ToDate='" + model.LogToDate + "'").ToList();
            return data;
        }
        #endregion

        #region  AP User Log 
        public IList<APUserLog> APUserLog(int procid, APUserLog model)
        {
            var data = GetDataInList<APUserLog>("proc_UserLog @procId='" + procid + "',@Unit_ID ='" + Convert.ToInt16(model.Unit_ID) + "', @UserId='" + Convert.ToInt16(model.UserId) + "',@FromDate='" + model.LogFromDate + "',@ToDate='" + model.LogToDate + "'").ToList();
            return data;
        }

        #endregion


        #region  Counter Log 
        public IList<CounterLog> CounterLog(int procid, CounterLog model)
        {
            var data = GetDataInList<CounterLog>("proc_Unit_CRUD @procId='" + procid + "', @Command = '" + model.ComdName + "'").ToList();
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