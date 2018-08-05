using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;
namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void ListToStringArrayTest()
        {
            VendingMachine testVendingMachine = new VendingMachine();
            string[] result = testVendingMachine.InventoryListAsString();
            Assert.AreEqual("A1 | Name: Potato Crisps        | Price: 3.05 | Quantity: 5", result[0]);
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            VendingMachine test2VendingMachine = new VendingMachine();
            bool resultTrue = test2VendingMachine.FeedMoney(2);
            Assert.AreEqual(true, resultTrue);

            bool resultFalse = test2VendingMachine.FeedMoney(3);
            Assert.AreEqual(false, resultFalse);
        }

        [TestMethod]
        public void ChangeTest()
        {
            VendingMachine test3VendingMachine = new VendingMachine();
            test3VendingMachine.FeedMoney(1);
            Dictionary<string, int> change = test3VendingMachine.Change();
            Assert.AreEqual( 4, change["Quarters"]);
        }

        [TestMethod]
        public void PrintMessageTest()
        {
            VendingMachine test4VendingMachine = new VendingMachine();
            test4VendingMachine.FeedMoney(10);
            test4VendingMachine.MakePurchase("A1");
            string result = test4VendingMachine.PrintMessage("A1");
            Assert.AreEqual("Crunch Crunch, Yum!", result);
        }

        [TestMethod]
        public void ProductPrintsTest()
        {
            VendingMachine test5VendingMachine = new VendingMachine();
            test5VendingMachine.FeedMoney(10);
            test5VendingMachine.MakePurchase("A1");
            string result = test5VendingMachine.ProductPrints("A1");
            Assert.AreEqual("Name: Potato Crisps | Price: 3.05 | Remaining Balance: 6.95", result);
        }

        [TestMethod]
        public void MakePurchase()
        {
            VendingMachine test6VendingMachine = new VendingMachine();
            test6VendingMachine.FeedMoney(10);
            test6VendingMachine.MakePurchase("A1");
            int result = test6VendingMachine.inventory[0].Quantity;
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void MakePurchaseCurrentBalance()
        {
            VendingMachine test7VendingMachine = new VendingMachine();
            test7VendingMachine.FeedMoney(10);
            test7VendingMachine.MakePurchase("A1");
            decimal result = test7VendingMachine.CurrentBalance;
            Assert.AreEqual(6.95M, result);
        }

    }
}
