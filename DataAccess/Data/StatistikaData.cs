using DataAccess.DbAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using Dapper;
using System.Data;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using System.Data.Common;
using ProjektStatistikaAPI.DataAccess.Model;
using System.Text.RegularExpressions;

namespace DataAccess.Data
{
    public class StatistikaData : IStatistikaData
    {
        private readonly ISQLDataAccess _db;

        public StatistikaData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<StatistikaModel> NaloziZadjeKlicanEP()
        {
            string sqlString = "SELECT * FROM tbStatistikaKlicevEP WHERE DatumVnosaStatistike = (SELECT MAX(DatumVnosaStatistike) " +
                "FROM tbStatistikaKlicevEP)";
            var resalt = await _db.LoadOne<StatistikaModel, dynamic>(sqlString, new { });
            return resalt.FirstOrDefault();
        }

        public async Task<StatistikaModelCounter> NaloziNajpogostejeKlicanEP()
        {
            string sqlString = "SELECT TOP 1 ImeKlicaneStoritve, SteviloKlicev  FROM (SELECT ImeKlicaneStoritve, Count(*) AS SteviloKlicev "+
                "FROM tbStatistikaKlicevEP GROUP BY ImeKlicaneStoritve) AS tabela WHERE ImeKlicaneStoritve> '' ORDER BY SteviloKlicev DESC";
            var resalt = await _db.LoadOne<StatistikaModelCounter, dynamic>(sqlString, new { });
            return resalt.FirstOrDefault();
        }

        public async Task<IEnumerable<StatistikaModelCounter>> SteviloEPKlicev()
        {
            string sqlString = "SELECT ImeKlicaneStoritve, Count(*) AS SteviloKlicev FROM tbStatistikaKlicevEP GROUP BY ImeKlicaneStoritve";
            var resalt = await _db.LoadAll<StatistikaModelCounter>(sqlString);
            return resalt;
        }

        public async Task ShraniNoviKlicEP(StatistikaModel parameter)
        {

            string sqlString = "INSERT INTO tbStatistikaKlicevEP (DatumVnosaStatistike, ImeKlicaneStoritve)" +
               " VALUES(@DatumVnosaStatistike, @ImeKlicaneStoritve)";

            var _parameter = new DynamicParameters();
            // _parameter.Add("IdNarocila", parameter.IdNarocila, DbType.Int32);
            _parameter.Add("DatumVnosaStatistike", parameter.DatumVnosaStatistike, DbType.Date);
            _parameter.Add("ImeKlicaneStoritve", parameter.ImeKlicaneStoritve, DbType.String);

            await _db.SaveOne<DynamicParameters>(sqlString, _parameter);
        }


    }
}
