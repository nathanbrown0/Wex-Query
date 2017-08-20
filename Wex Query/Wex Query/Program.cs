using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wex_Query
{
    class TC
    {
        public int total;
        public int packages;
        public int PackagesTotal;
        public int CardTotal;

        public void setTotal(int totel)
        {
            total = totel;
        }
        public int getTotal()
        {
            return total;
        }
        public void setPackages(int package)
        {
            packages = package;
        }
        public int getPackages()
        {
            return packages;
        }
        public void setPT(int totel)
        {
            PackagesTotal = PackagesTotal + totel;
        }
        public int getPT()
        {
            return PackagesTotal;
        }
        public void setCT(int totel)
        {
            CardTotal = CardTotal + totel;
        }
        public int getCT()
        {
            return CardTotal;
        }
        public void ProcessTCs(string file)
        {
            DateTime creation = File.GetCreationTime(file);
            DateTime d1 = DateTime.Today.AddMonths(-1);
            if (file.Contains("TC") & (creation > d1))
            {
                int counter = 0;
                setTotal(0);
                setPackages(0);
                String[] myFile = File.ReadAllLines(file);
                foreach (var line in myFile)
                {
                    if (counter != 0)
                    {
                        string[] strArray = line.Split('|');
                        int what = 0;
                        int.TryParse(strArray[10], out what);
                        if (what < 5)
                        {
                            total = total + what;
                            packages++;
                        }
                    }
                    counter++;
                }
            }
        }
    }
    class Expedite
    {
        public int total;
        public int packages;
        public int PackagesTotal;
        public int CardTotal;

        public void setTotal(int totel)
        {
            total = totel;
        }
        public int getTotal()
        {
            return total;
        }
        public void setPackages(int package)
        {
            packages = package;
        }
        public int getPackages()
        {
            return packages;
        }
        public void setPT(int totel)
        {
            PackagesTotal = PackagesTotal + totel;
        }
        public int getPT()
        {
            return PackagesTotal;
        }
        public void setCT(int totel)
        {
            CardTotal = CardTotal + totel;
        }
        public int getCT()
        {
            return CardTotal;
        } 
        public void PE(string file)
        {
            DateTime creation = File.GetCreationTime(file);
            DateTime d1 = DateTime.Today.AddMonths(-1);
            if (file.Contains("Expedite") & (creation > d1))
            {
                setTotal(0);
                setPackages(0);
                String[] myFile = File.ReadAllLines(file);
                foreach(var line in myFile)
                {
                    if (line.Substring(0, 2) == "04")
                    {
                        string[] strArray = line.Split('|');
                        string FPL = strArray[19];
                        string containMulti = strArray[4];
                        string shipMethod = strArray[7];
                        int cpp = 0;
                        int.TryParse(strArray[12], out cpp);
                        if ((FPL != "Florida Power and Light") & (containMulti != "Multi") & (cpp < 5) & (containMulti != "BIN"))
                        {
                            total = total + cpp;
                            packages++;
                        }
                    }
                }
            }
        }
    }
    class Renewal
    {

    }
    class Daily
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            string TCtargetDirectory = @"\\cospfp1\customer_data\Active\Cipher\WrightExpress\WEXExport";
            string ERDdirectory = @"\\cospfp1\customer_data\Cipher\Wex\Processed";
            TCgo(TCtargetDirectory);
            ERDgo(ERDdirectory);
        }

        public static void TCgo(string directory)
        {
            TC test = new TC();
            string[] TCfileEntries = Directory.GetFiles(directory);
            foreach (string fileName in TCfileEntries)
            {
                test.ProcessTCs(fileName);
                test.setPT(test.getPackages());
                test.setCT(test.getTotal());
            }
            Console.WriteLine("Packages - " + test.getPT() + "   Cards - " + test.getCT());
            Console.ReadKey();
        }
        public static void ERDgo(string directory)
        {
            Expedite ERD = new Expedite();
            string[] ERDfileEntries = Directory.GetFiles(directory);
            foreach (string fileName in ERDfileEntries)
            {
                ERD.PE(fileName);
                ERD.setPT(ERD.getPackages());
                ERD.setCT(ERD.getTotal());
            }
            Console.WriteLine("Packages - " + ERD.getPT() + "   Cards - " + ERD.getCT());
            Console.ReadKey();
        }
    }
}
