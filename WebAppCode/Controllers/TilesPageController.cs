using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Dapper;
using WebAppCode.Contexts;
using WebAppCode.Models;
using WebAppCode.Models.Dto;

namespace WebAppCode.Controllers
{
    [RoutePrefix("api/tilespage")]
    public class TilesPageController : ApiController
    {
        [HttpGet]
        [Route("racunitiles")]
        public IEnumerable<TileItemData> GetRacuniTiles()
        {
            IList<TileItemData> result = new List<TileItemData>();
            IEnumerable<RacuniPerState> tilesData;

            using (var con = new SqlConnection(DataContext.Connection_String))
            {
                try
                {
                    tilesData = con.Query<RacuniPerState>(
                        @"select State, count(*) as Count
                            from pn.dokument d inner join pn.racun r on d.id = r.id
                            where state in ('NA_ODOBRENJU', 'NA_PREDOVJERI')
                            group by state
                            order by state")
                        .ToList();
                }
                catch (Exception ex)
                {
                    return result;
                }
            }

            foreach (var tileData in tilesData)
            {
                result.Add(new TileItemData()
                {
                    Id = tileData.State,
                    Caption = StateToCaption(tileData.State),
                    Amount = tileData.Count,
                    Href = HrefOfState(tileData.State)
                });
            }

            return result;
        }

        private string StateToCaption(string state)
        {
            switch (state)
            {
                case "NA_ODOBRENJU":
                    return "Na odobrenju";
                case "NA_PREDOVJERI":
                    return "Na predovjeri";
                default:
                    return "No name";
            }
        }

        private string HrefOfState(string state)
        {
            switch (state)
            {
                case "NA_ODOBRENJU":
                    return "http://test.dokument.hr/ePlanNabave4_1.DU.Test/Default.aspx?sys_m=Racun_Odobravanje";
                case "NA_PREDOVJERI":
                    return "http://test.dokument.hr/ePlanNabave4_1.DU.Test/Default.aspx?sys_m=Racun_Predovjeravanje";
                default:
                    return "No name";
            }
        }
    }
}
