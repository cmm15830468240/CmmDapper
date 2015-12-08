using DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace DapperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dapper();
            //DapperExtension();
            //DapperMulti();
            MyDapper();
        }

        #region Dapper原始用法
        public static void Dapper()
        {
            string code = Console.ReadLine();
            UserInfoBusiness bll = new UserInfoBusiness();
            if (code == "selkey")
            {
                UserInfoEntity entity = new UserInfoEntity();
                entity = bll.GetUserEntityByKey(1);
                Console.WriteLine("Id:{0},UserName:{1},Age:{2},JobNo:{3};", entity.Id, entity.UserName, entity.Age, entity.JobNo);
                Console.ReadKey();
            }
            else if (code == "sellist")
            {
                List<UserInfoEntity> lstEntity = new List<UserInfoEntity>();
                lstEntity = bll.GetListUserEntity(2);
                lstEntity.ForEach(a =>
                Console.WriteLine("Id:{0},UserName:{1},Age:{2},JobNo:{3};", a.Id, a.UserName, a.Age, a.JobNo));
                Console.ReadKey();
            }
            else if (code == "add")
            {
                UserInfoEntity entity = new UserInfoEntity { Age = 18, JobNo = "110", UserName = "我叫18岁" };
                int excuteRes = bll.SaveEntity(entity);
                Console.WriteLine("新增数据主键:{0}", excuteRes);
                Console.ReadKey();
            }
            else if (code == "mod")
            {
                UserInfoEntity entity = bll.GetUserEntityByKey(1);
                entity.JobNo = "更新工号";
                int excuteRes = bll.SaveEntity(entity);
                Console.WriteLine("更新数据主键:{0}", excuteRes);
                Console.ReadKey();
            }
            //Dapper(); 
            

        }

        public static void DapperMulti()
        {
            UserInfoBusiness bll = new UserInfoBusiness();
            string sql = @"select UserName,CompanyName from TCInterVacationOVDResource.dbo.UserInfo a
		                    inner join TCInterVacationOVDResource.dbo.UserSex b
			                    on a.Id=b.UserId ";
            var lst = bll.GetListEntityBySql(sql);

        }
        #endregion

        #region DapperExtension 原始用法
        public static void DapperExtension()
        {
            string code = Console.ReadLine();
            UserInfoExtensionBusiness bll = new UserInfoExtensionBusiness();
            if (code == "selkey")
            {
                UserInfoEntity entity = new UserInfoEntity();
                entity = bll.GetUserEntityByKey(1);
                Console.WriteLine("Id:{0},UserName:{1},Age:{2},JobNo:{3};", entity.Id, entity.UserName, entity.Age, entity.JobNo);
                Console.ReadKey();
            }
            else if (code == "sellist")
            {
                List<UserInfoEntity> lstEntity = new List<UserInfoEntity>();
                lstEntity = bll.GetListUserEntityByKey();
                lstEntity.ForEach(a =>
                Console.WriteLine("Id:{0},UserName:{1},Age:{2},JobNo:{3};", a.Id, a.UserName, a.Age, a.JobNo));
                Console.ReadKey();
            }
            else if (code == "add")
            {
                UserInfoEntity entity = new UserInfoEntity { Age = 18, JobNo = "110", UserName = "我叫18岁" };
                object excuteRes = bll.SaveEntity(entity);
                Console.WriteLine("Extension新增数据主键:{0}", excuteRes);
                Console.ReadKey();
            }
            else if (code == "mod")
            {
                UserInfoEntity entity = bll.GetUserEntityByKey(1);
                entity.JobNo = "Extension更新工号";
                object excuteRes = bll.SaveEntity(entity);
                Console.WriteLine("Extension更新数据主键:{0}", excuteRes);
                Console.ReadKey();
            }

            DapperExtension();
        }
        #endregion

        #region 封装
        public static void MyDapper()
        {
            int res = 0;
            string code = Console.ReadLine();
            Imp imp = new Imp();
            if (code == "add")
            {
                string jobNo = Console.ReadLine();
                // string addSql = @"insert into TCInterVacationOVDResource.dbo.UserInfo(UserName,Age,JobNo,CreatedTime) Values(@UserName,@Age,@JobNo,@CreatedTime)";
                //string addSql = @"insert into TCInterVacationOVDResource.dbo.UserInfo(UserName,Age,JobNo,CreatedTime) Values(1,1,1,'1900-01-01')";
                string addSql = @"delete from TCInterVacationOVDResource.dbo.UserInfo where id=6";
                res = imp.ExcuteSql(addSql, new { UserName = "test", Age = 12, jobNo = jobNo, CreatedTime = DateTime.Now });
            }
            else if (code == "addt")
            {
                UserInfoEntity entity = new UserInfoEntity
                {
                    CreatedTime = DateTime.Now,
                };
                entity.SetDefaultValue();
                res = imp.Insert<UserInfoEntity>(entity);
            }
            else if (code == "list")
            {
                var aa = imp.GetList<UserInfoEntity>("Select * from TCInterVacationOVDResource.dbo.UserInfo where id=100");
            }  
            else if (code == "del")
            {
                UserInfoEntity entity = new UserInfoEntity();
                entity.UserName = "test";
                var bb = imp.Delete<UserInfoEntity>(entity);
            }

            MyDapper();
        }

        #endregion
    }
}
