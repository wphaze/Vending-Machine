using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine;

        public UserInterface(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                //Main Menu 

                Console.WriteLine("Main Menu");
                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Exit");
                Console.WriteLine();

                string mainMenuSelection = Console.ReadLine();

                //Display Inventory

                if (mainMenuSelection == "1")
                {
                    string[] inventory = vendingMachine.InventoryListAsString();
                    foreach (string item in inventory)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                }

                //Go to Purchase Menu

                else if (mainMenuSelection == "2")
                {
                    PurchaseMenu();
                }

                //Exit Vending Machine

                else if (mainMenuSelection == "3")
                {
                    done = true;
                }
            }
        }

        //Purchase Menu

        public void PurchaseMenu()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Purchase Menu");
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine($"Current Money Provided: {vendingMachine.CurrentBalance}");
                Console.WriteLine();

                string purchaseMenuSelection = Console.ReadLine();

                //Feed Money

                if (purchaseMenuSelection == "1")
                {
                    Console.WriteLine("Please enter valid bill ($1, $2, $5, $10)");
                    decimal billAmount = decimal.Parse(Console.ReadLine());
                    Console.WriteLine();

                    if (vendingMachine.FeedMoney(billAmount) == false)
                    {
                        Console.WriteLine("Invalid amount entered, Please enter valid bill ($1, $2, $5, $10)");
                    }
                }

                //Select Product

                else if (purchaseMenuSelection == "2")
                {
                    string[] inventory = vendingMachine.InventoryListAsString();
                    foreach (string item in inventory)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("Please enter item ID: ");

                    string selection = Console.ReadLine();
                    int answer = vendingMachine.ValidItem(selection);

                    if (answer == 1)
                    {
                        Console.WriteLine("Invalid ID entered");
                    }
                    else if (answer == 2)
                    {
                        Console.WriteLine("Out of stock"); ;
                    }
                    else if (answer == 3)
                    {
                        Console.WriteLine("Not enough $");
                    }
                    else
                    {
                        vendingMachine.MakePurchase(selection);
                        Console.WriteLine(vendingMachine.PrintMessage(selection));
                        Console.WriteLine(vendingMachine.ProductPrints(selection));                        
                    }
                }

                //Finish Transaction; Go back to Main Menu

                else if (purchaseMenuSelection == "3")
                {
                    Dictionary<string, int> result = new Dictionary<string, int>(vendingMachine.Change());
                    foreach (KeyValuePair<string, int> kvp in result)
                    {
                        Console.WriteLine(kvp);
                    }
                    Console.WriteLine();
                    done = true;
                }
            }   
        }
    }
}
