using System.Data;

namespace OgarCommon.DB.Core
{
    public interface IDatabase
    {
        IDbConnection Connection { get; }
        string ConnectionString { get; set; }

        void Open();
        void Close();
        void Dispose();

        void BeginTrans();
        void CommitTrans();
        void RollbackTrans();

        int ExcuteSql(string strSql);
        int ExcuteSql(string strSql, string[] strParams, object[] objValues);
        object ExcuteScalarSql(string strSql);
        object ExcuteScalarSql(string strSql, string[] strParams, object[] strValues);
        DataSet ExcuteSqlForDataSet(string queryString);
    }
}
