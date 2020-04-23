using System.Data.SqlClient;

namespace Persistencia.Extensions
{
    public static class SQLDataReaderExtension
    {
        public static string GetString(this SqlDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }

        public static int GetInt(this SqlDataReader reader, string fieldName)
        {
            var index = reader.GetOrdinal(fieldName);
            return reader.IsDBNull(index) ? 0 : reader.GetInt32(index);
        }
    }
}
