using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
    interface DapperInterFace
    {
        #region Dapper原生
        int ExcuteSql(string sql, object parms = null, DbTransaction trans = null);

        //int DapperUpdate();

        //int DapperDelete();

        //T DapperQuerySingleOrDefault<T>();

        //T DapperQueryList<T>();

        //List<T> DapperGetList<T>(string sql);

        //List<T> DapperGetList<T>(string sql, object parms);
        //#endregion

        //#region Dapper Extension 增删改查

        ///// <summary>
        ///// 新增实体
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="model"></param>
        ///// <returns>返回新增行主键</returns>
        //int DapperAdd<T>(T model);




        #endregion

    }
}
