using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using WebAppCode.Contexts;
using WebAppCode.Models;
using WebAppCode.Models.Db;
using WebAppCode.Models.Dto;

namespace WebAppCode.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/ok")]
        public string GetOk()
        {
            return "Ok";
        }
        
        [HttpGet]
        [Route("tblAdata")]
        public IEnumerable<TblA> GetTblAData()
        {
            using (IDbConnection db = new SqlConnection(DataContext.Connection_String))
            {
                return db.Query<TblA>
                    ("Select ID, Name From Table_A").ToList();
            }
        }
    }
}