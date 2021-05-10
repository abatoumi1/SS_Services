using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions.ExecuteStoreProc
{
    public static class StoreProc
    {
        //public getData()
        //{
        //    SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Prac\TestAssembly\ForumsProjectCSharp\App_Data\Studetnt.mdf;Integrated Security=True;User Instance=True");
        //    SqlCommand cmd = new SqlCommand("select * from Student", conn);
        //    SqlDataReader dr;
        //    try
        //    {
        //        conn.Open();
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            student.Add(new Student()
        //            {
        //                ID = dr.GetInt32(dr.GetOrdinal("ID")),
        //                Name = dr.GetString(dr.GetOrdinal("Name")),
        //                DateOfBirth = dr.GetDateTime(dr.GetOrdinal("DateOfBirth"))
        //            });

        //        }
        //        dr.Close();
        //    }
        //    catch (Exception exp)
        //    {

        //        throw;
        //    }
        //    finally
        //    {

        //        conn.Close();
        //    }
        //}

        public static DbCommand LoadStoredProc(
          this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
        public static DbCommand WithSqlParam(
                        this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException(
                  "Call LoadStoredProc before using this method");
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }
        private static List<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
              .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
              .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val =
                          dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }
        public static async Task<List<T>> ExecuteStoredProc<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.MapToList<T>();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
        //public static execute()
        //{
        //    List<MyType> myTypeList = new List<MyType>();
        //    using (var context = new MyDbContext())
        //    {
        //        DbCommand cmd = context.LoadStoredProc("StoredProcedureName")
        //        .WithSqlParam("firstparamname", firstParamValue)
        //        .WithSqlParam("secondparamname", secondParamValue);
        //        myTypeList = await ExecuteStoredProc<MyType>(cmd);
        //    }
        //}

    }
}
