using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AA7.Models;

public class LibraryAA
{
    
      //public static IEnumerable<dbo_SearchAviator> Aviatorlist (int SessionUserID)
      //  {
      //        IHSDCAA7DBDBContext db = new IHSDCAA7DBDBContext();
      //        string unit = db.dbo_FullHierarchy.Where(x => x.ChildId == SessionUserID).FirstOrDefault().EstablishmentFull;
             
      //        List<string>mychild = db.dbo_FullHierarchy.Where(x => x.UserId == SessionUserID).Select(x => x.EstablishmentFull).ToList();
      //       // List <int>aviator = db.dbo_tbl_Aviator.Where(x => mychild.Contains(x.Unit_ID)).Select(x => x.Aviator_ID).ToList();
      //       // IEnumerable<dbo_SearchAviator> stands = db.dbo_SearchAviator.Where(x => aviator.Contains(x.Aviator_ID));
      //      //    return stands;                                
      //  }



         


    public static List<SelectListItem> GetRank(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();

        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = ""};
        SelectListItem Item1 = new SelectListItem { Text = "LT", Value = "LT" };
        SelectListItem Item2 = new SelectListItem { Text = "CAPT", Value = "CAPT" };
        SelectListItem Item3 = new SelectListItem { Text = "MAJ", Value = "MAJ" };
        SelectListItem Item4 = new SelectListItem { Text = "LT COL", Value = "LT COL" };
        SelectListItem Item5 = new SelectListItem { Text = "COL", Value = "COL" };
        SelectListItem Item6 = new SelectListItem { Text = "BRIG", Value = "BRIG" };
        SelectListItem Item7 = new SelectListItem { Text = "MAJ GEN", Value = "MAJ GEN" };
        SelectListItem Item8 = new SelectListItem { Text = "LT GEN", Value = "LT GEN" };
        SelectListItem Item9 = new SelectListItem { Text = "GEN", Value = "GEN" };

        if (select == "LT") { Item1.Selected = true; }
        else if (select == "CAPT") { Item2.Selected = true; }
        else if (select == "MAJ") { Item3.Selected = true; }
        else if (select == "LT COL") { Item4.Selected = true; }
        else if (select == "COL") { Item5.Selected = true; }
        else if (select == "BRIG") { Item6.Selected = true; }
        else if (select == "MAJ GEN") { Item7.Selected = true; }
        else if (select == "LT GEN") { Item8.Selected = true; }
        else if (select == "GEN") { Item9.Selected = true; }
        else { Item0.Selected = true; }
        //else { Item1.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        list.Add(Item6);
        list.Add(Item7);
        list.Add(Item8);
        list.Add(Item9);

        return list.ToList();
    }

    public static List<SelectListItem> GetNOKRelationship(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();

        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "SPOUSE", Value = "SPOUSE" };
        SelectListItem Item2 = new SelectListItem { Text = "FATHER", Value = "FATHER" };
        SelectListItem Item3 = new SelectListItem { Text = "MOTHER", Value = "MOTHER" };
        SelectListItem Item4 = new SelectListItem { Text = "BROTHER", Value = "BROTHER" };
        SelectListItem Item5 = new SelectListItem { Text = "SISTER", Value = "SISTER" };
        SelectListItem Item6 = new SelectListItem { Text = "SON", Value = "SON" };
        SelectListItem Item7 = new SelectListItem { Text = "DAUGHTER", Value = "DAUGHTER" };

        if (select == "SPOUSE") { Item1.Selected = true; }
        else if (select == "FATHER") { Item2.Selected = true; }
        else if (select == "MOTHER") { Item3.Selected = true; }
        else if (select == "BROTHER") { Item4.Selected = true; }
        else if (select == "SISTER") { Item5.Selected = true; }
        else if (select == "SON") { Item6.Selected = true; }
        else if (select == "DAUGHTER") { Item7.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        list.Add(Item6);
        list.Add(Item7);

        return list.ToList();
    }


    public static List<SelectListItem> GetMaritalStatus(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();

        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "SINGLE", Value = "SINGLE" };
        SelectListItem Item2 = new SelectListItem { Text = "MARRIED", Value = "MARRIED" };

 if (select == "SINGLE") { Item1.Selected = true; }
        else if (select == "MARRIED") { Item2.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);

        return list.ToList();
    }

   
    public static List<SelectListItem> GetHillFlyingHeightClearance(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "UPTO 5000", Value = "UPTO 5000" };
        SelectListItem Item2 = new SelectListItem { Text = "5000 TO 10000", Value = "5000 TO 10000" };
        SelectListItem Item3 = new SelectListItem { Text = "10000 TO 15000", Value = "10000 TO 15000" };
        SelectListItem Item4 = new SelectListItem { Text = "15000 TO 17000", Value = "15000 TO 17000" };
        SelectListItem Item5 = new SelectListItem { Text = "ABOVE 17000", Value = "ABOVE 17000" };

        if (select == null) { Item0.Selected = true; }

        else if (select == "UPTO 5000") { Item1.Selected = true; }
        else if (select == "5000 TO 10000") { Item2.Selected = true; }
        else if (select == "10000 TO 15000") { Item3.Selected = true; }
        else if (select == "15000 TO 17000") { Item4.Selected = true; }
        else if (select == "ABOVE 17000") { Item4.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);

        return list.ToList();
    }

    public static List<SelectListItem> GetCategory(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "A", Value = "A" };
        SelectListItem Item2 = new SelectListItem { Text = "B", Value = "B" };
        SelectListItem Item3 = new SelectListItem { Text = "C REC", Value = "C REC" };
        SelectListItem Item4 = new SelectListItem { Text = "C", Value = "C" };
        SelectListItem Item5 = new SelectListItem { Text = "D REC C", Value = "D REC C" };
        SelectListItem Item6 = new SelectListItem { Text = "D", Value = "D" };
        SelectListItem Item7 = new SelectListItem { Text = "CR", Value = "CR" };
        SelectListItem Item8 = new SelectListItem { Text = "CT", Value = "CT" };
        SelectListItem Item9 = new SelectListItem { Text = "UT", Value = "UT" };


        if (select == null) { Item0.Selected = true; }
        else if (select == "A") { Item1.Selected = true; }
        else if (select == "B") { Item2.Selected = true; }
        else if (select == "C REC") { Item3.Selected = true; }
        else if (select == "C") { Item4.Selected = true; }
        else if (select == "D REC C") { Item5.Selected = true; }
        else if (select == "D") { Item6.Selected = true; }
        else if (select == "CR") { Item7.Selected = true; }
        else if (select == "CT") { Item8.Selected = true; }
        else if (select == "UT") { Item9.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        list.Add(Item6);
        list.Add(Item7);
        list.Add(Item8);
        list.Add(Item9);

        return list.ToList();
    }
    public static List<SelectListItem> GetResult(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "NA", Value = "NA" };
        SelectListItem Item2 = new SelectListItem { Text = "INITIAL CAT", Value = "INITIAL CAT" };
        SelectListItem Item3 = new SelectListItem { Text = "UPGRADED", Value = "UPGRADED" };
        SelectListItem Item4 = new SelectListItem { Text = "RENEWED", Value = "RENEWED" };
        SelectListItem Item5 = new SelectListItem { Text = "DOWNGRADED", Value = "DOWNGRADED" };

        if (select == null) { Item0.Selected = true; }
        else if (select == "NA") { Item1.Selected = true; }
        else if (select == "INITIAL CAT") { Item2.Selected = true; }
        else if (select == "UPGRADED") { Item3.Selected = true; }
        else if (select == "RENEWED") { Item4.Selected = true; }
        else if (select == "DOWNGRADED") { Item5.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        return list.ToList();
    }

    public static List<SelectListItem> GetInstrumentRating(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = ""};
        SelectListItem Item1 = new SelectListItem { Text = "MASTER GREEN", Value = "MG" };
        SelectListItem Item2 = new SelectListItem { Text = "GREEN", Value = "G" };
        SelectListItem Item3 = new SelectListItem { Text = "WHITE", Value = "W" };
        SelectListItem Item4 = new SelectListItem { Text = "UNRATED", Value = "UR" };


        if (select == null) { Item0.Selected = true; }
        else if (select == "MASTER GREEN") { Item1.Selected = true; }
        else if (select == "GREEN") { Item2.Selected = true; }
        else if (select == "WHITE") { Item3.Selected = true; }
        else if (select == "UNRATED") { Item4.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);

        return list.ToList();
    }

    public static List<SelectListItem> GetInstructorCat(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "MAI", Value = "MAI" };
        SelectListItem Item2 = new SelectListItem { Text = "SAI I", Value = "SAI I" };
        SelectListItem Item3 = new SelectListItem { Text = "SAI II", Value = "SAI II" };
        SelectListItem Item4 = new SelectListItem { Text = "JAI I", Value = "JAI I" };
        SelectListItem Item5 = new SelectListItem { Text = "JAI II", Value = "JAI II" };


        if (select == null) { Item0.Selected = true; }
        else if (select == "MAI") { Item1.Selected = true; }
        else if (select == "SAI I") { Item2.Selected = true; }
        else if (select == "SAI II") { Item3.Selected = true; }
        else if (select == "JAI I") { Item4.Selected = true; }
        else if (select == "JAI II") { Item5.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);

        return list.ToList();
    }

    public static List<SelectListItem> GetGoodShow(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "PART I", Value = "PART I" };
        SelectListItem Item2 = new SelectListItem { Text = "PART II", Value = "PART II" };
        SelectListItem Item3 = new SelectListItem { Text = "PART I & II", Value = "PART I & II" };
        SelectListItem Item4 = new SelectListItem { Text = "FLYING", Value = "FLYING" };
        SelectListItem Item5 = new SelectListItem { Text = "PART I & FLYING", Value = "PART I & FLYING" };
        SelectListItem Item6 = new SelectListItem { Text = "PART II & FLYING", Value = "PART II & FLYIN" };
        SelectListItem Item7 = new SelectListItem { Text = "PART I, II & FLYING", Value = "PART I, II & FLYING" };
        SelectListItem Item8 = new SelectListItem { Text = "PRESENTATION", Value = "PRESENTATION" };

        if (select == null) { Item0.Selected = true; }
        else if (select == "PART I") { Item1.Selected = true; }
        else if (select == "PART II") { Item2.Selected = true; }
        else if (select == "PART I & II") { Item3.Selected = true; }
        else if (select == "FLYING") { Item4.Selected = true; }
        else if (select == "PART I & FLYING") { Item5.Selected = true; }
        else if (select == "PART II & FLYING") { Item6.Selected = true; }
        else if (select == "PART I, II & FLYING") { Item7.Selected = true; }
        else if (select == "PRESENTATION") { Item8.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        list.Add(Item6);
        list.Add(Item7);
        list.Add(Item8);
        return list.ToList();

    }

    public static List<SelectListItem> GetWarnedFor(String select)
    {
        List<SelectListItem> list = new List<SelectListItem>();
        SelectListItem Item0 = new SelectListItem { Text = "--SELECT--", Value = "" };
        SelectListItem Item1 = new SelectListItem { Text = "PART I", Value = "PART I" };
        SelectListItem Item2 = new SelectListItem { Text = "PART II", Value = "PART II" };
        SelectListItem Item3 = new SelectListItem { Text = "PART I & II", Value = "PART I & II" };
        SelectListItem Item4 = new SelectListItem { Text = "FLYING", Value = "FLYING" };
        SelectListItem Item5 = new SelectListItem { Text = "PART I & FLYING", Value = "PART I & FLYING" };
        SelectListItem Item6 = new SelectListItem { Text = "PART II & FLYING", Value = "PART II & FLYIN" };
        SelectListItem Item7 = new SelectListItem { Text = "PART I, II & FLYING", Value = "PART I, II & FLYING" };

        if (select == null) { Item0.Selected = true; }
        else if (select == "PART I") { Item1.Selected = true; }
        else if (select == "PART II") { Item2.Selected = true; }
        else if (select == "PART I & II") { Item3.Selected = true; }
        else if (select == "FLYING") { Item4.Selected = true; }
        else if (select == "PART I & FLYING") { Item5.Selected = true; }
        else if (select == "PART II & FLYING") { Item6.Selected = true; }
        else if (select == "PART I, II & FLYING") { Item7.Selected = true; }
        else { Item0.Selected = true; }

        list.Add(Item0);
        list.Add(Item1);
        list.Add(Item2);
        list.Add(Item3);
        list.Add(Item4);
        list.Add(Item5);
        list.Add(Item6);
        list.Add(Item7);

        return list.ToList();

    }

   



}
