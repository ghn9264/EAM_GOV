using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EAM.Data.Comm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco;
using NPoco.Linq;

namespace EAM.Data.Test
{
    [TestClass]
    public class RepTest
    {
        private static string ConnStr =
            @"Server=127.0.0.1;Database=testdb;User=root;Password=;pooling=true;charset=utf8";

        private UserInfoRepository _userInfoRep;
        private Database Database;
        [TestInitialize]
        public void Init()
        {
            Database = new Database(ConnStr, DatabaseType.MySQL);
            _userInfoRep = new UserInfoRepository(Database);
        }

        [TestMethod]
        public void InsertTest()
        {
           /* var u = new UserInfo {UserName = "U", CreatedTime = DateTime.Now};
            _userInfoRep.Add(u);*/

            var list = new List<UserInfo>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new UserInfo {UserName = string.Format("U_{0}", i), CreatedTime = DateTime.Now.AddMinutes(i)});
            }
            _userInfoRep.Add(list);
        }

        [TestMethod]
        public void FindUpdateTest()
        {
            var u = _userInfoRep.Find(1);
            u.UserName = "DDDDDD";
            _userInfoRep.Update(u);
            u = _userInfoRep.Find(1);
            Console.WriteLine(u.UserName);
        }

        [TestMethod]
        public void FirstOrDefaultTest()
        {
            var u = _userInfoRep.FirstOrDefault(x => x.UserName.StartsWith("U"));
            u.UserName = "DDDDDD";
            _userInfoRep.Update(u);
            u = _userInfoRep.Find(1);
            Console.WriteLine(u.UserName);
        }

        [TestMethod]
        public void FindTest()
        {
            var users = _userInfoRep.Find(new int[] {1, 2, 3});
            Console.WriteLine(users.Count);
        }

        [TestMethod]
        public void QueryTest()
        {
            var users = _userInfoRep.Query(u => u.EntityId > 50).ToList();
            Console.WriteLine(users.Count);
        }

        [TestMethod]
        public void PagedListTest()
        {
            PagedList<UserInfo> users = _userInfoRep.PagedList(2, 10, u => u.EntityId > 50);
            Console.WriteLine(users.Count);
        }
        [TestMethod]
        public void RemoveAllTest()
        {
            int total = _userInfoRep.RemoveAll();
            Console.WriteLine(total);
        }
        [TestMethod]
        public void CountTest()
        {
            var list = _userInfoRep.Query(x => x.EntityId > 180, x => x.EntityId, true);
            list = list.Where(x => x.EntityId%3 == 0);
            Console.WriteLine("--{0}",list.Count());
               

            //QueryProvider<UserInfo> q = new QueryProvider<UserInfo>(Database, u => u.EntityId % 5 == 0);
            QueryProvider<UserInfo> q = new QueryProvider<UserInfo>(Database);
            //q.Where(u => u.EntityId%5 == 0);
             


            var result = q.Count(); 
            Console.WriteLine(result);


            int total = _userInfoRep.Count();
            Console.WriteLine(total);
        }
        
       
    }
}