using CavuTechTest.DAL;
using CavuTechTest.DataAccess.IReadOnlyRepository;
using System.Data;
using System.Data.SqlClient;

namespace CavuTechTest.DataAccess.ReadOnlyRepository
{
    /// <summary>
    /// <see cref="IAvailableSpacesRepository"/>
    /// </summary>
    public class AvailableSpacesRepository : IAvailableSpacesRepository
    {
        public int GetAvailableSpaces(DateTime date)
        {
            using (var con = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB; Database=CavuCarParking; Trusted_Connection=True; MultipleActiveResultSets=true"))
            {
                using (var cmd = new SqlCommand("dbo.GetAvailableSpaces", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter retVal = cmd.CreateParameter();
                    retVal.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retVal);

                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    con.Open();

                    var result = (int)cmd.ExecuteScalar();

                    return result;
                }
            }
        }
    }
}