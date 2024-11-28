using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHSDC.WebApp.Connection;

namespace IHSDC.WebApp.Models
{
    public class ACCCommon
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
        public string ReadOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public int IsHistory { get; set; }

        public string RoleName { get; set; }
        public string UnitName { get; set; }
        public string SponsorName { get; set; }
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
        public string AmendWordFileName { get; set; }
        public int ScheduleCreatedById { get; set; }
        public int CloseBy { get; set; }
        public string CloseByName { get; set; }
        public string CloseDate { get; set; }
        public string CloseReason { get; set; }
        public int OpenBy { get; set; }
        public string OpenDate { get; set; }
        public string OpenReason { get; set; }

    }


    public class ScheduleLetter : ACCCommon
    {
        public int TypeOfConfId { get; set; }
        public string TypeOfConf { get; set; }
        public int ScheduleId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Subject { get; set; }
        public int Comd { get; set; }
        public string ComdName { get; set; }
        public string SubmitByComd { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsNotification { get; set; }
        public string Remark { get; set; }
        public string ScheduleRefNoDt { get; set; }
        public int isClosed { get; set; }
        public IList<ScheduleLetter> ILScheduleLetter { get; set; }
        public string StatusName { get; set; }
        public string OrganizerCatId { get; set; }
        public string SponsorCatId { get; set; }
        public string NodalCatId { get; set; }
        public string BranchIds { get; set; }
        public string ParticipateUsers { get; set; }
        public string ParticipateNewUsers { get; set; }
        public IList<CategoryCRUD> IOrganizerCatCRUD { get; set; }
        public IList<CategoryCRUD> ISponsorCatCRUD { get; set; }
        public IList<CategoryCRUD> INodalCatCRUD { get; set; }
        public IList<BranchCRUD> IBranchCRUD { get; set; }
        public IList<UnitCRUD> IParticipateUsersCRUD { get; set; }

        public IList<UnitCRUD> IParticipateNewUsersCRUD { get; set; }
        public int LetterCreatedBy { get; set; }
    }


    public class NotingDetail : ACCCommon
    {
        public int Id { get; set; }
        public int InboxId { get; set; }
        public int CommentFrom { get; set; }
        public string Comment { get; set; }
        public string Upload { get; set; }
        public string UploadPath { get; set; }
        public string UserUnitName { get; set; }
        public string UserName { get; set; }
        public string ForwardedToUsers { get; set; }
        public string CCToUsers { get; set; }
        public string CCText { get; set; }
        public string Attachment { get; set; }
    }

    public class AgendaPointCRUD : ACCCommon
    {
        public int ConfId { get; set; }
        public string Conf { get; set; }
        public int Inbox_ID { get; set; }
        public int ComdId { get; set; }
        public int Branch { get; set; }
        public string BranchName { get; set; }
        public int Schedule_ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
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
        public string FileName { get; set; }
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
        public IList<InboxCRUD> ILInboxCRUD { get; set; }
    }

    public class InboxCRUD : ACCCommon
    {
        public int ConfId { get; set; }
        public string Conf { get; set; }
        public int Inbox_ID { get; set; }
        public int ComdId { get; set; }
        public int Branch { get; set; }
        public string BranchName { get; set; }
        public int Schedule_ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
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
        public IList<InboxCRUD> ILInboxCRUD { get; set; }
        public IList<UnitCRUD> ILUnitCRUD { get; set; }
        public IList<NotingDetail> ILNotingCRUD { get; set; }
        public IList<AgendaPointLog> ILAgendaPointLog { get; set; }

        public IList<APSupportingDocu> ILAPSupportingDocu { get; set; }
    }


    public class APSupportingDocu : ACCCommon
    {
        public string SuppFileName { get; set; }
        public string SuppFilePath { get; set; }
    }

    public class InboxNotingFwdCRUD : ACCCommon
    {
        public int InboxNoting_ID { get; set; }
        public int Inbox_ID { get; set; }
        public int Comd_ID { get; set; }
        public int Branch_ID { get; set; }
        public int Schedule_ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Upload { get; set; }
        //   public string DateTimeOfUpdate { get; set; }

        public IList<InboxNotingFwdCRUD> ILInboxNotingFwdCRUD { get; set; }
    }


    public class shortdata
    {
        public int value { get; set; }
        public string Name { get; set; }
    }

    public class AgendaPointLog : ACCCommon
    {
        public int Inbox_ID { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string CCId { get; set; }
        public string Description { get; set; }
        public string OtherDescription { get; set; }
    }

    public class SearchableItem
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
        public string MatchedParagraph { get; set; }
        public string ParagraphBefore { get; set; }
        public string ParagraphAfter { get; set; }
    }

    public class CertificateData
    {
        public string API { get; set; }
        public bool CRL_OCSPCheck { get; set; }
        public String CRL_OCSPMsg { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Thumbprint { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string issuer { get; set; }
        public string subject { get; set; }
        public Boolean TokenValid { get; set; }
    }
    public class ResponseList
    {
        public bool ValidatePersID2FAResult { get; set; }
    }

    public class FileList
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int FileType { get; set; }
    }
    public class SendData
    {
        public string inputPersID { get; set; }
    }

    public class RequestData
    {
        public String Thumbprint { get; set; }
        public string pdfpath { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
    }
}