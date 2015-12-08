using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;
using UserEntity = DataEntity.UserInfoEntity;
using DataEntity;

namespace DapperConsole
{
    public class UserInfoBusiness
    {
        #region Dapper原始用法
        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <returns></returns>
        public UserEntity GetUserEntityByKey(int key)
        {
            UserEntity entity = new UserEntity();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                entity = dbCon.Query<UserEntity>(string.Format("{0} where Id=@Id", UserEntity.Sql.selectSql), new { Id = key }).FirstOrDefault();
            }
            return entity ?? new UserEntity();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public List<UserEntity> GetListUserEntity(int age)
        {
            List<UserEntity> lstEntity = new List<UserEntity>();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                lstEntity = dbCon.Query<UserEntity>(string.Format("{0} where age=@age", UserEntity.Sql.selectSql), new { age = age }).ToList();
            }
            return lstEntity;
        }

        /// <summary>
        /// 新增/更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>返回执行影响了几行数据（同Ado.Net）</returns>
        public int SaveEntity(UserEntity entity)
        {
            int index = 0;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {

                if (entity.Id > 0)
                {
                    //更新
                    index =
                        dbCon.Execute(@"Update a Set UserName=@UserName,Age=@Age,JobNo=@JobNo From TCInterVacationOVDResource.dbo.UserInfo a WHERE Id=@Id ", entity);
                }
                else
                {
                    //新增   
                    index =
                        dbCon.Execute(@"Insert into TCInterVacationOVDResource.dbo.UserInfo(UserName,Age,JobNo) Values(@UserName,@Age,@JobNo)", entity);
                }
            }
            return index;
        }

        public int TransExcute(int key)
        {
            int index = 0;
            //取数据库中第1个UserInfo数据,删除,然后回滚
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                DbTransaction trans= dbCon.BeginTransaction();
                UserEntity entity = dbCon.Query<UserEntity>(string.Format("{0} where Id=@Id", UserEntity.Sql.selectSql), new { Id = key }, trans).FirstOrDefault();
                index =
                       dbCon.Execute(@"Delete From TCInterVacationOVDResource.dbo.UserInfo where id=@id", entity);
                if (index > 0)
                {
                    trans.Rollback();
                }
                trans.Commit();
            }
            return index;
        }

        public List<MultiUser> GetListEntityBySql(string sql, params object[] parms)
        {
            List<MultiUser> lstEntity = new List<MultiUser>();
            //取数据库中第1个UserInfo数据,删除,然后回滚
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                lstEntity = dbCon.Query<MultiUser>(sql).ToList();
            }
            return lstEntity;
        }


        #endregion
    }

    #region dapper extension
    public class UserInfoExtensionBusiness
    {
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public UserEntity GetUserEntityByKey(int key)
        {
            UserEntity entity = new UserEntity();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                dbCon.Open();
                entity = dbCon.Get<UserEntity>(key);
                dbCon.Close();
            }
            return entity;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<UserEntity> GetListUserEntityByKey()
        {
            List<UserEntity> lstEntity = new List<UserEntity>();
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {
                lstEntity = dbCon.GetAll<UserEntity>().ToList();
            }
            return lstEntity;
        }

        /// <summary>
        /// 新增/更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>返回执行影响了几行数据</returns>
        public object SaveEntity(UserEntity entity)
        {
            object index = null;
            using (DbConnection dbCon = DbCon.GetSqlCon())
            {

                if (entity.Id > 0)
                {
                    //更新
                    index =
                      dbCon.Update<UserEntity>(entity);
                }
                else
                {
                    //新增   
                    index =
                       dbCon.Insert<UserEntity>(entity);
                }
            }
            return index;
        }

    }
    #endregion
}
