using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EAM.Data.Comm;
using EAM.Data.Domain;
using EAM.Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPoco;
using NPoco.Linq;

namespace EAM.Data.Test
{
    [TestClass]
    public class RepTest2
    {
         

        [TestMethod]
        public void QueryTest()
        {
            var item = new AssetsMain
            {
              GoodsNo="SB3838438"
            };
            AssetsMainService.AddAssetsMain(item);

        }

       
    }
}