using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Countries
{
    class Program
    {
        // each country is made up of name, capital and population.
        struct country
        {
            public string Name;
            public string Capital;
            public int Population;
        }

        //maximum possible number of countries
        const int MaxCountries = 10;

        // random number generator
        static Random RNG = new Random();

        static int LoadCountryData(ref country[] countrylist)
        {

            // Read the file with country data.
            // place each county into the array of countries
            // starting at position 1 in the array (not 0!)
            // this is returned by reference parameter to the calling routine
            System.IO.StreamReader file = new System.IO.StreamReader("countries.txt");

            int CountryCount=0;

            while (file.EndOfStream ==false)
            {
                country ThisCountry;
                ThisCountry.Name = file.ReadLine();
                ThisCountry.Capital = file.ReadLine();
                ThisCountry.Population = Convert.ToInt32(file.ReadLine());
                CountryCount += 1;
                countrylist[CountryCount] = ThisCountry;
            }
            
            file.Close();
            return CountryCount;

        }

        static void StoreCountryData( country[] countrylist, int CountryCount)
        {

            //Stores the entire array of country data in the array
            //back to the textfile countries.txt
            //which is completly overwriiten each time
            System.IO.StreamWriter outfile = new System.IO.StreamWriter("countries.txt",false);

          

            for (int i = 0; i <= CountryCount; i++)
			{
		        outfile.WriteLine(countrylist[i].Name);
                outfile.WriteLine(countrylist[i].Capital);
                outfile.WriteLine(countrylist[i].Population.ToString());
            
            }

            outfile.Close();
    

        }

        static void DisplayAllCountries(country[] countrylist, int NumberOfCountries)
        {
            for (int i = 0; i<= NumberOfCountries; i++)
            {
                Console.WriteLine(countrylist[i].Name);
            }
        }

        static void AddCountry(ref country[] countrylist, ref int NumberOfCountries )
        {
            for (int i = NumberOfCountries; i>= NumberOfCountries;)
            {
                Console.WriteLine("Please enter the country name: ");
                countrylist[i].Name = Console.ReadLine();
                Console.WriteLine($"Please enter {countrylist[i].Name}'s captial: ");
                countrylist[i].Capital = Console.ReadLine();
                Console.WriteLine("Please enter the countries population: ");
                countrylist[i].Population = int.Parse( Console.ReadLine() );
                break;
            }
        }



        static void FindCountry(country[] CountryList, int NumberOfCountries)
        {
            // Locates a country in the array enterd by the user
            // if not found displays 'not found'
            // Case sensitive!

            int position = 1;//was 1
            bool found=false;
            string CountryToFind;

            Console.Write("Enter Country Name: ");
            CountryToFind = (Console.ReadLine().ToUpper());

            while (position <= NumberOfCountries & found == false)
            {
                if (CountryList[position].Name == CountryToFind) 
                {
                    found=true;
                }
                else
                {
                    position=position+1;
                }
            }
            if (found==true)
            {
                Console.WriteLine("country: {0}, capital: {1}, population: {2}", CountryList[position].Name, CountryList[position].Capital, CountryList[position].Population);
            }
            else
            {
                Console.WriteLine("Country not found");
            }

        }


        static void DisplayMenu()
        {
            //clears the screen and displays the menu
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MENU");
                Console.WriteLine("1. Display Countries");
                Console.WriteLine("2. Find Country");
                Console.WriteLine("3. Add Country");
                Console.WriteLine("5. Quit");
        }


        static void Main(string[] args)
        {
            //we are not using position 0 of the array so we need to add one extra location
            // this will create locations upto and including maxcountries
            country[] CountryList = new country[MaxCountries]; 
            //current number of countries stored
            int NumberOfCountries = 0;
           
            // load country data from file
            //sets number of countries to the number of records on file.
            NumberOfCountries=LoadCountryData(ref CountryList);

            Console.WriteLine("{0} countries loaded from file - hit any key",NumberOfCountries);

            Console.ReadKey();
            
            string choice = "";

            // accepts user choice and runs appropriate routine
            // until user enters 5 - quit.
            while (choice != "5")
            {
                DisplayMenu();
                Console.Write("Enter Selection: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllCountries(CountryList, NumberOfCountries);
                        break;

                    case "2":
                        FindCountry(CountryList, NumberOfCountries);                    
                        break;
                   
                    case "3":
                        AddCountry(ref CountryList, ref NumberOfCountries);   
                        break;
                    case "5":
                        break;

                    default:
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("Invalid Selection");
                        
                        break;



                }

                Console.WriteLine("Hit any key");
                Console.ReadKey();
            }

            //save country data back to file
            StoreCountryData(CountryList, NumberOfCountries);
        }
    }
}
