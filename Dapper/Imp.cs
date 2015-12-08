using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Dapper
{
    public class Imp : DapperInterFace
    {
        /// <summary>
        /// 执行增删改Sql语句,返回影响行数;查询数据则返回-1;
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parms">参数</param>
        /// <returns>数据库影响行数</returns>
        /// 注： 1. parms参数中必须覆盖SQl语句中的存在的参数;
        ///      2. 原则上参数名称映射不区分大小写,但应尽量保持一致
        public int ExcuteSql(string sql, object parms = null, DbTransaction trans = null)
        {
            int excuteRes = 0;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                excuteRes = dbCon.Execute(sql, parms, trans);
            }
            return excuteRes;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <typeparam name="T">泛行</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>返回新增数据的主键</returns>
        /// 注:依赖PoCo对象,TableAttribute,KeyAttribute特性
        public int Insert<T>(T entity, DbTransaction trans = null) where T : class
        {
            int excuteRes = 0;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                excuteRes = (int)dbCon.Insert<T>(entity, trans);
            }
            return excuteRes;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">要更新的实体</param>
        /// <param name="trans">事务</param>
        /// <returns>true：更新成功;false:更新失败</returns>
        /// 注： 1. parms参数中必须覆盖SQl语句中的存在的参数;
        ///      2. 原则上参数名称映射不区分大小写,但应尽量保持一致
        public bool Edit<T>(T entity, DbTransaction trans = null) where T : class
        {
            bool editRes = false;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                editRes = dbCon.Update<T>(entity, trans);
            }
            return editRes;
        }

        /// <summary>
        /// 删除数据(只会根据主键值删除)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">要删除的对象(主键要有值)</param>
        /// <param name="trans">事务</param>
        /// <returns>删除是否成功</returns>
        public bool Delete<T>(T entity, DbTransaction trans = null) where T : class
        {
            bool delRes = false;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                delRes = dbCon.Delete<T>(entity, null);
            }
            return delRes;
        }

        /// <summary>
        /// 删除(泛型对象表)全部的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool DeleteAll<T>(T entity, DbTransaction trans = null) where T : class
        {
            bool delRes = false;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                delRes = dbCon.DeleteAll<T>(trans);
            }
            return delRes;
        }

        /// <summary>
        /// 根据Sql查询单个实体,没有则返回默认对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selSql"></param>
        /// <param name="parms"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public T GetDefaultEntity<T>(string selSql, object parms = null, DbTransaction trans = null) where T : class, new()
        {
            T entity = new T();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                entity = dbCon.Query(selSql, parms, trans).SingleOrDefault();
            }
            return entity ?? new T();
        }

        /// <summary>
        /// 根据Sql查询单表实体列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selSql">Sql语句</param>
        /// <param name="parms">参数对象</param>
        /// <param name="trans">事务</param>
        /// <returns></returns>
        public List<T> GetList<T>(string selSql, object parms = null, DbTransaction trans = null)
        {
            List<T> lstT = new List<T>();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                lstT = dbCon.Query<T>(selSql, parms, trans).ToList();
            }
            return lstT;
        }

        


    }
}
