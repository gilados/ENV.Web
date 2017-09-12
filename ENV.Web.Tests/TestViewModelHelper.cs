﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Firefly.Box.Testing;
using Firefly.Box;

namespace ENV.Web.Tests
{
    [TestClass]
    public class TestViewModelHelper
    {
        internal class TestVMH : ViewModelHelper
        {
            public MockTable mt = new MockTable();
            public TestVMH()
            {
                From = mt;
            }
            protected override void OnInsert()
            {
                mt.a.Value = mt.Max(mt.a) + 1;
            }
        }
        [TestMethod]
        public void TestViewModelHelper_0()
        {
            var mt = new MockTable();
            mt.Truncate();
            mt.InsertRow(1, 1, "noam");
            mt.InsertRow(2, 2, "yael");
            var vmc = new TestVMH();
            var dl = vmc.GetRows();
            dl.Count.ShouldBe(2);
            dl[0]["id"].Number.ShouldBe(1);
            dl[1]["id"].Number.ShouldBe(2);
            dl[0]["c2"].Number.ShouldBe(1);
            dl[0]["c3"].Text.ShouldBe("noam");
        }
        [TestMethod]
        public void TestViewModelHelper_1()
        {
            var mt = new MockTable();
            mt.Truncate();
            mt.InsertRow(1, 1, "noam");
            mt.InsertRow(2, 2, "yael");
            var vmc = new TestVMH();
            var item = vmc.GetRow("1");

            item["id"].Number.ShouldBe(1);
            item["c2"].Number.ShouldBe(1);
            item["c3"].Text.ShouldBe("noam");
            item.Set("c3", "yoni");
            item = vmc.Update("1", item);
            item["c3"].Text.ShouldBe("yoni");
            mt.GetValue(mt.c, mt.a.IsEqualTo(1)).ShouldBe("yoni");
        }
        [TestMethod]
        public void TestViewModelHelper_2()
        {
            var mt = new MockTable();
            mt.Truncate();
            mt.InsertRow(1, 1, "noam");
            mt.InsertRow(2, 2, "yael");
            var vmc = new TestVMH();
            var item = vmc.GetRows();
            vmc.Delete(item[0]["id"].Text);
            mt.CountRows().ShouldBe(1);
            new BusinessProcess { From = mt }.ForFirstRow(() => mt.c.ShouldBe("yael"));

        }
        [TestMethod]
        public void TestViewModelHelper_post()
        {
            var mt = new MockTable();
            mt.Truncate();
            mt.InsertRow(1, 1, "noam");
            mt.InsertRow(2, 2, "yael");
            var vmc = new TestVMH();
            var i = new DataItem();
            i.Set("c2", 2);
            i.Set("c3", "yael");
            i = vmc.Insert(i);
            mt.CountRows().ShouldBe(3);
            i["id"].Number.ShouldBe(3);



        }
        [TestInitialize]
        public void TestInitialize()
        {
            ViewModelHelper.HttpContext.Value = new MockHttpContext();
        }
    }


}