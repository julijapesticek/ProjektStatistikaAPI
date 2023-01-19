using DataAccess.Model;
using ProjektStatistikaAPI.DataAccess.Model;

namespace DataAccess.Data
{
    public interface IStatistikaData
    {
        Task<StatistikaModelCounter> NaloziNajpogostejeKlicanEP();
        Task<StatistikaModel> NaloziZadjeKlicanEP();
        Task ShraniNoviKlicEP(StatistikaModel parameter);
        Task<IEnumerable<StatistikaModelCounter>> SteviloEPKlicev();
    }
}