using Dapper;
using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = "server=104.155.208.131; database=OLYMPICENGLISH.VN.2023;User Id=HSSV_Intern;Password=ames@123456;Trusted_Connection=True; Integrated Security=false; TrustServerCertificate=True";
       

        public ActionResult Index(int? page)
        {
            //int pageSize = 20;
            //int pageNumber = (page ?? 1);
            using (var connection = new SqlConnection(connectionString))
            {
                var proc = "select top (100) FullName, Email, Phone, SchoolName, District, Lop, RoleName from v_OlympicCBT_Admin_Users";
                var lisuser = connection.Query<Users>(proc).ToList();
               // IPagedList<Users> pagedListUsers = lisuser.ToPagedList(pageNumber, pageSize);
                return View(lisuser);
            }
        }

        public ActionResult Search(String keyword, int? page)  
        {
            //int pageSize = 20;
           // int pageNumber = (page ?? 1);
            using (var connection = new SqlConnection(connectionString))
            {
                var proc = "p_OlympicHSV_Admin_UserRole_Find_By_Keyword";
                var parameters = new { Keyword = keyword };
                var listuser = connection.Query<Users>(proc, parameters, commandType: CommandType.StoredProcedure).ToList();
               // IPagedList<Users> pagedListUsers = listuser.ToPagedList(pageNumber, pageSize);
                return View("Index", listuser);

            }
        }
    }
}