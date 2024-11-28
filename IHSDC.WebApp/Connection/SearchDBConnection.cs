using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IHSDC.WebApp.Models;
using IHSDC.WebApp.Connection;

namespace IHSDC.WebApp.Connection
{
    public class SearchDBConnection : DbContext 
    {
        public SearchDBConnection() : base("IHSDCAA7DBConnectionString")
        {

        }

        #region  Get Data in DB 
        public IList<AviatorCRUD> AviatorSearch(int id, string str)
        {
            var data = GetDataInList<AviatorCRUD>(" proc_SearchingProcess @procId ='" + id + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }

        public IList<AviatorHeptrRPACRUD> AviatorHeptrRPAList(int id)
        {
            var data = GetDataInList<AviatorHeptrRPACRUD>("proc_Search @procId ='" + 31 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }

        public IList<AviatorCoursesCRUD> AviatorCourseList(int id)
        {
            var data = GetDataInList<AviatorCoursesCRUD>("proc_Search @procId ='" + id + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorMedicalCRUD> AviatorMedicalList(int id)
        {
            var data = GetDataInList<AviatorMedicalCRUD>(" proc_Search @procId ='" + id + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }

        public IList<AviatorForeignVisitCRUD> AviatorForeignVisitList(int id)
        {
            var data = GetDataInList<AviatorForeignVisitCRUD>(" proc_Search @procId ='" + 32 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }

        public IList<AviatorCoursesCRUD> AviatorCourseSearch(int id, string str)
        {
            var data = GetDataInList<AviatorCoursesCRUD>(" proc_SearchingProcess @procId ='" + id + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }

        #region Heptr RPA Search
        public IList<AviatorHeptrRPACRUD> AviatorHeptrRPASearch(int id, string str)
        {
            var data = GetDataInList<AviatorHeptrRPACRUD>(" proc_SearchingProcess @procId ='" + 31 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }
        #endregion
        public IList<AviatorMedicalCRUD> AviatorMedicalSearch(int id, string str)
        {
            var data = GetDataInList<AviatorMedicalCRUD>(" proc_SearchingProcess @procId ='" + id + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }

        public IList<AviatorForeignVisitCRUD> AviatorForeignVisitSearch(int id, string str)
        {
            var data = GetDataInList<AviatorForeignVisitCRUD>(" proc_SearchingProcess @procId ='" + 32 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }


        public IList<AviatorHonourAwardsCRUD> AviatorHonourAwardsSearchList()
        {
            var data = GetDataInList<AviatorHonourAwardsCRUD>(" proc_Search @procId ='" + 3 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorHonourAwardsCRUD> AviatorHonourAwardsSearch(string str)
        {
            var data = GetDataInList<AviatorHonourAwardsCRUD>(" proc_SearchingProcess @procId ='" + 4 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }        
      
        public IList<AviatorAccidentCRUD> AviatorAccidentSearchList()
        {
            var data = GetDataInList<AviatorAccidentCRUD>(" proc_Search @procId ='" + 5 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorAccidentCRUD> AviatorAccidentSearch(string str)
        {
            var data = GetDataInList<AviatorAccidentCRUD>(" proc_SearchingProcess @procId ='" + 6 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }        
       
        public IList<AviatorRankCRUD> AviatorRankSearchList()
        {
            var data = GetDataInList<AviatorRankCRUD>(" proc_Search @procId ='" + 7 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorRankCRUD> AviatorRankSearch(string str)
        {
            var data = GetDataInList<AviatorRankCRUD>(" proc_SearchingProcess @procId ='" + 8 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' ,@Aid='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }     
        public IList<AviatorApptCRUD> AviatorAPPSearchList()
        {
            var data = GetDataInList<AviatorApptCRUD>(" proc_Search @procId ='" + 8 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorApptCRUD> AviatorAPPSearch(string str)
        {
            var data = GetDataInList<AviatorApptCRUD>(" proc_SearchingProcess @procId ='" + 9 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' ,@Aid='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorContactDetailsCRUD> AviatorContactDetailsSearchList()
        {
            var data = GetDataInList<AviatorContactDetailsCRUD>(" proc_Search @procId ='" + 9 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorContactDetailsCRUD> AviatorContactDetailsSearch(string str)
        {
            var data = GetDataInList<AviatorContactDetailsCRUD>(" proc_SearchingProcess @procId ='" + 10 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorFlyingHrsCRUD> AviatorFlyingSearchList()
        {
            var data = GetDataInList<AviatorFlyingHrsCRUD>(" proc_Search @procId ='" + 10 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<AviatorFlyingHrsCRUD> AviatorFlyingSearch(string str)
        {
            var data = GetDataInList<AviatorFlyingHrsCRUD>(" proc_SearchingProcess @procId ='" + 11 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }



        public IList<PostingAviator> AviatorPostingSearchList()
        {
            var data = GetDataInList<PostingAviator>(" proc_Search @procId ='" + 11 + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "' , @str ='" + SessionManager.AviatorIds.ToString() + "'").ToList();
            return data;
        }
        public IList<PostingAviator> AviatorPostingSearch(string str)
        {
            var data = GetDataInList<PostingAviator>(" proc_SearchingProcess @procId ='" + 12 + "', @str ='" + str + "',@UserId='" + Convert.ToInt16(SessionManager.UserIntId) + "',@UnitName='" + SessionManager.Unit_ID.ToString() + "'").ToList();
            return data;
        }


        #endregion
        #region Execute Query In DB  
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
        public IList<DashAvi> ErrorLogDashboard2(int procid, string Message, string ErrorInfo, string LogType)
        {
            var data = GetDataInList<DashAvi>("Dashboard @procId='" + procid + "',@Message='" + Message + "',@ErrorInfo='" + ErrorInfo + "',@LogType='" + LogType + "'").ToList();
            return data;
        }
    }
}