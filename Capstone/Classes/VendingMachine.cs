using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        
        //Properties

        public decimal CurrentBalance { get; private set; }

        //List

        public List<VendingMachineItem> inventory = new List<VendingMachineItem>();

        //Constructors

        public VendingMachine()
        {
            ReadFile();
        }//no test neded

        //Methods

        public string[] InventoryListAsString()
        {
            int columnWidth = 20;
            string[] inventoryStringArray = new string[inventory.Count];
            for (int i = 0; i < inventory.Count; i++)
            {
                string itemFromInventory = inventory[i].SlotID + " | Name: " + inventory[i].Name.PadRight(columnWidth) + " | Price: " + inventory[i].Price + " | Quantity: " + inventory[i].Quantity;

                inventoryStringArray[i] = itemFromInventory;
            }
            return inventoryStringArray;
        }//test pass        

        public void ReadFile()
        {
            string filePath = @"C:\Users\wfaiz\team8-c-week4-pair-exercises\c#-mini-capstone\etc\vendingmachine.csv";

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string result = sr.ReadLine();
                        string[] values = result.Split('|');
                        VendingMachineItem thisItem = new VendingMachineItem();
                        thisItem.SlotID = values[0];
                        thisItem.Name = values[1];
                        thisItem.Price = decimal.Parse(values[2]);
                        thisItem.Type = values[3];
                        thisItem.Quantity = 5;

                        inventory.Add(thisItem);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading the file");
                Console.WriteLine(e.Message);
            }
        }//no test needed

        public void WriteToLogFile(string displayInformation, decimal firstDollarAmount, decimal secondDollarAmmount)
        {
            string directory = Environment.CurrentDirectory;
            string filename = "Log.txt";
            string fullPath = Path.Combine(directory, filename);
           


            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine($"{DateTime.UtcNow} {displayInformation} {firstDollarAmount} {secondDollarAmmount}");
            }
        }//test pass

        public bool FeedMoney(decimal billAmount)
        {
            if (billAmount == 1 || billAmount == 2 || billAmount == 5 || billAmount == 10)
            {
                CurrentBalance += billAmount;


                WriteToLogFile("FEED MONEY", billAmount, CurrentBalance);
                return true;
            }
            return false;
        }//test pass

        public Dictionary<string, int> Change()
        {
            decimal quarter = 0.25M;
            decimal dime = 0.10M;
            decimal nickel = 0.05M;
            int numQuarters = 0;
            int numDimes = 0;
            int numNickels = 0;

            WriteToLogFile("GIVE CHANGE", CurrentBalance, 0.00M);

            while (CurrentBalance > 0)
            {
                if (CurrentBalance >= quarter)
                {
                    numQuarters = (int)(CurrentBalance / quarter);
                    decimal cashQuarter = numQuarters * quarter;
                    CurrentBalance -= cashQuarter;
                }

                else if (CurrentBalance >= dime)
                {
                    numDimes = (int)(CurrentBalance / dime);
                    decimal cashDime = numDimes * dime;
                    CurrentBalance -= cashDime;
                }

                else if (CurrentBalance >= nickel)
                {
                    numNickels = (int)(CurrentBalance / nickel);
                    decimal cashNickel = numNickels * nickel;
                    CurrentBalance -= cashNickel;
                }                
            }

            Dictionary<string, int> change = new Dictionary<string, int>
            {
                { "Quarters", numQuarters},
                { "Dimes", numDimes},
                { "Nickels", numNickels},
            };

            return change;
        }//test pass

        public int ValidItem(string selction)
        {
            int result = -1;
            int answer = 0;

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].SlotID == selction)
                {
                    result = i;
                }
            }

            if (result == -1)
            {
                answer = 1;
            }

            else if (inventory[result].Quantity == 0)
            {
                answer = 2;
            }

            else if (inventory[result].Price > CurrentBalance)
            {
                answer = 3;
            }
            return answer;
        }

        public string ProductPrints(string selection)//test pass
        {
            string printedProductMessages = "";

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].SlotID == selection)
                {
                    printedProductMessages = "Name: " + inventory[i].Name + " | Price: " + inventory[i].Price + " | Remaining Balance: " + CurrentBalance;
                }
            }
            return printedProductMessages;
        }

        public void MakePurchase(string selection)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].SlotID == selection)
                {
                    decimal cost = inventory[i].Price;
                    int quantity = inventory[i].Quantity;

                    CurrentBalance -= cost;
                    inventory[i].Quantity--;

                    WriteToLogFile($"{inventory[i].Name} {inventory[i].SlotID}", CurrentBalance+cost, CurrentBalance);
                   
                }
            }
        }

        public string PrintMessage(string selection)
        {
            string message = "";

            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].SlotID == selection)
                {
                    string type = inventory[i].Type;

                    if (type == "Chip")
                    {
                        message = "Crunch Crunch, Yum!";
                    }
                    else if (type == "Candy")
                    {
                        message = "Munch Munch, Yum!";
                    }
                    else if (type == "Drink")
                    {
                        message = "Glug Glug, Yum!";
                    }
                    else if (type == "Gum")
                    {
                        message = "Chew Chew, Yum!";
                    }
                }

            }
            return message;
        }//test pass
    }
}
