using Bogus;
using Bogus.Extensions.UnitedStates;
using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;


namespace fakeData
{
    class Program
    {
        static readonly string outputFile = @"peopleData.csv";
        static void Main(string[] args)
        {
            // Define variables
            string emailDomain = "";
            int numEntries = 0;

            // Manage command line options
            var commandOptions = new CMDOptions();
            Parser.Default.ParseArguments<CMDOptions>(args).WithParsed<CMDOptions>(o => {
                if (o.numEntries == null)
                {
                    Console.WriteLine(o.GetUsage());
                    System.Environment.Exit(1);
                }
                else if (o.emailDomain == null)
                {
                    Console.WriteLine(o.GetUsage());
                    System.Environment.Exit(1);
                }
                else
                { }
            });

            // Set command line options
            var result = Parser.Default.ParseArguments<CMDOptions>(args);
            result.WithParsed<CMDOptions>(o => {
                try
                {
                    emailDomain = o.emailDomain;
                }
                catch
                {
                    Console.WriteLine("");
                    Console.WriteLine("[!] Error: Failed to parse command line option: -d, --domain");
                    Console.WriteLine("Please check provided options. Input should be a string.");
                    Console.WriteLine(o.GetUsage());
                    System.Environment.Exit(1);
                }
            });
            result.WithParsed<CMDOptions>(o => {
                try
                {
                    numEntries = Int32.Parse(o.numEntries);
                }
                catch
                {
                    Console.WriteLine("");
                    Console.WriteLine("[!] Error: Failed to parse command line option: -n, --num");
                    Console.WriteLine("Please check provided options. Input should be an integer.");
                    Console.WriteLine(o.GetUsage());
                    System.Environment.Exit(1);
                } 
            });

            // Create Faker rules
            var modelFaker = new Faker<MyModel>()
               .RuleFor(o => o.fName, f => f.Name.FirstName())
               .RuleFor(o => o.lName, f => f.Name.LastName())
               .RuleFor(o => o.email, (f, o) => o.fName + "." + o.lName + "@" + emailDomain)
               .RuleFor(o => o.ssn, f => f.Person.Ssn())
               .RuleFor(o => o.phoneNum, f => f.Phone.PhoneNumber())
               .RuleFor(o => o.address, f => f.Address.BuildingNumber() + " " + f.Address.StreetName() + " " + f.Address.StreetSuffix())
               .RuleFor(o => o.city, f => f.Address.City())
               .RuleFor(o => o.state, f => f.Address.State())
               .RuleFor(o => o.country, f => f.Address.Country());

            var users = modelFaker.Generate(numEntries);

            // Create file headers based on users object
            var fileHeaders = "First Name,Last Name,Email,SSN (Social Security Number),Phone Number,Address,City,State,Country";
            byte[] fileHeaders_bytes = new UTF8Encoding(true).GetBytes(fileHeaders);

            // Create output file if it does not exist and write file headers
            if (!System.IO.File.Exists(outputFile))
            {
                System.IO.FileStream fs = System.IO.File.Create(outputFile);
                fs.Write(fileHeaders_bytes, 0, fileHeaders_bytes.Length);
                fs.Close();
            }
            else
            {
                Console.WriteLine($"Error: File '{outputFile}' already exists. \nQuitting...");
                System.Environment.Exit(1);
            }
            
            // Write each generated user out using the StreamWriter
            using (StreamWriter outFileHandle = new StreamWriter(outputFile, true))
                foreach (MyModel val in users)
                {
                    var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", val.fName, val.lName, val.email, val.ssn, val.phoneNum, val.address, val.city, val.state, val.country);

                    outFileHandle.WriteLine(newLine);

                    //Debug
                    //Console.WriteLine(newLine);
                }
                
            Console.WriteLine($"File written to: {outputFile}\n");
        }
    }
}
