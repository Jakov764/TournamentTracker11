using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@osvojenoMjesto", model.PlaceNumber);
                p.Add("@nazivNagrade", model.PlaceName);
                p.Add("@iznosNagrade", model.PrizeAmount);
                p.Add("@postotakNagrade", model.PrizePercentage);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spNAGRADA_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                return model;

            }
        }
    }
}
