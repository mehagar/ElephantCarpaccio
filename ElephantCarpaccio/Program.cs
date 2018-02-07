using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElephantCarpaccio
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("$1");
            System.Diagnostics.Debug.WriteLine("this should be in output window");

            var app = new ElephantApp();

            System.Diagnostics.Debug.Fail("failed");

            app.PromptForItems();
            app.PromptForPricePerItem();
            if (app.PromptForStateCode())
            {
                app.PrintTotal();
            }

            // TODO:  do this thing

            Console.ReadKey();
        }
    }

    enum StateCode
    {
        CA,
        NV,
        UT,
        TX,
        AL
    }

    public class ElephantApp
    {
        Decimal _totalPriceInDollars;
        Decimal _pricePerItemInDollars;
        StateCode _stateCode;
        int _numItems;

        public void PromptForItems()
        {
            Console.WriteLine("Enter number of items: ");
            string numItems = Console.ReadLine();

            int.TryParse(numItems, out _numItems);
        }

        public void PrintTotal()
        {
            _totalPriceInDollars = CalculateTotal();

            Console.WriteLine($"The total price is : {_totalPriceInDollars}");
        }

        private Decimal CalculateTotal()
        {
            Decimal taxRate = TranslateStateCodeToTaxRate();

            return (_numItems * _pricePerItemInDollars) * ((1 + (taxRate / 100)));
        }

        private decimal TranslateStateCodeToTaxRate()
        {
            switch (_stateCode)
            {
                case StateCode.CA:
                    return 8.25M;
                case StateCode.NV:
                    return 8M;
                case StateCode.UT:
                    return 6.85M;
                case StateCode.TX:
                    return 6.25M;
                case StateCode.AL:
                    return 4M;
                default:
                    return 8.25M;
            }
        }

        internal void PromptForPricePerItem()
        {
            Console.WriteLine("Enter price per item: ");
            string pricePerItem = Console.ReadLine();

            Decimal.TryParse(pricePerItem, out _pricePerItemInDollars);
        }

        internal bool PromptForStateCode()
        {
            Console.WriteLine("Enter two letter state code: ");
            string stateCode = Console.ReadLine();

            bool succeeded = Enum.TryParse(stateCode, out _stateCode);
            if (!succeeded)
            {
                Console.WriteLine("Invalid state code: only CA, NV, TX, AL, UT is supported");
                return false;
            }

            return true;
        }
    }
}
