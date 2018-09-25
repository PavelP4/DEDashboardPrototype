using System.Web.Http;


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
        
        //[HttpGet]
        //[Route("tblAdata")]
        //public IEnumerable<TblA> GetTblAData()
        //{
        //    using (IDbConnection db = new SqlConnection(DataContext.Connection_String))
        //    {
        //        return db.Query<TblA>
        //            ("Select ID, Name From Table_A").ToList();
        //    }
        //}
    }
}