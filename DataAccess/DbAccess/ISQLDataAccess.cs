namespace DataAccess.DbAccess
{
    public interface ISQLDataAccess
    {
        Task<IEnumerable<T>> LoadAll<T>(string sqlString, string connectionString = "Default");
        Task<IEnumerable<T>> LoadOne<T, U>(string sqlString, U parameter, string connectionString = "Default");
        Task SaveOne<T>(string sqlString, T parameter, string connectionString = "Default");
    }
}