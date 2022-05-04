using Credit_Suisse;
using System;
using System.Globalization;

class Program
{
    private static Portfolio portfolio;

    public static void Main()
    { 
        do { Run(); } while (RunAgain());
        Environment.Exit(0);
    }

    private static void Run()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("------------ BRUNO MAXIMILIANO VIGNONE TABAREZ -----------");
        Console.WriteLine(" __   __    ___  __     ___     __          __    __   ___");
        Console.WriteLine(@"/  ` | __) |__  |   \ |  |     /__` |  | | /__`  /__` |__");
        Console.WriteLine(@"\__, |  \  |___ |__ / |  |     .__/ \__/ | .__/ .__/  |___");
        Console.WriteLine("__________________________________________________________");
        Console.WriteLine("");

        portfolio = new Portfolio();
        ReferenceDate();
        NumberOfTrades();
        Trades();
        portfolio.CategorizeTrades();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("                 CATEGORY RESULT");
        Console.WriteLine("__________________________________________________________");
        ListCategoryTrades();
    }

    private static bool RunAgain()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("RUN AGAIN (y/n)...");
        string exit = Console.ReadLine();
        if (exit.ToUpper() == "Y")
            return true;
        else
            return false;
    }

    #region ReferenceDate
    private static void ReferenceDate()
    {
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INSERT REFERENCE DATE");

        } while (!ValidateDate(Console.ReadLine()));
    }

    private static bool ValidateDate(string? sDate)
    {
        try
        {
            portfolio.ReferenceDate = DateTime.Parse(sDate, GetCultureInfo());
            SetOK();
            return true;
        }
        catch (Exception)
        {
            SetError("REFERENCE DATE NOT VALID");
            return false;
        }
    }
    #endregion ReferenceDate

    #region NumberOfTrades
    private static void NumberOfTrades()
    {
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("INSERT NUMBER OF TRADES");

        } while (!ValidateNTrades(Console.ReadLine()));
    }

    private static bool ValidateNTrades(string? nTrades)
    {
        try
        {
            portfolio.NTrades = Convert.ToInt32(nTrades);
            Console.ForegroundColor = ConsoleColor.Green;
            SetOK();
            return true;
        }
        catch (Exception)
        {
            SetError("NUMBER OF TRADES NOT VALID");
            return false;
        }
    }
    #endregion NumberOfTrades

    #region Trades
    private static void Trades()
    {
        for (int i = 0; i < portfolio.NTrades; i++)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("INSERT TRADE " + (i + 1).ToString());

            } while (!ValidateTrades(Console.ReadLine()));
        }
    }

    private static bool ValidateTrades(string? sTrade)
    {
        try
        {
            Trade trade = new Trade();
            string[] tradeContent = sTrade.Split(' ');
            trade.Value = Convert.ToInt32(tradeContent[(int)Trade.TradeContent.Value]);
            if ((
                (tradeContent[(int)Trade.TradeContent.ClientSector]).Equals("Private") ||
                (tradeContent[(int)Trade.TradeContent.ClientSector]).Equals("Public")
                ))
            { trade.ClientSector = tradeContent[(int)Trade.TradeContent.ClientSector]; }
            else
            {
                SetError("TRADE NOT VALID");
                return false;
            }
            trade.NextPaymentDate = DateTime.Parse(tradeContent[((int)Trade.TradeContent.NextPaymentDate)], GetCultureInfo());

            portfolio.Trades.Add(trade);
            Console.ForegroundColor = ConsoleColor.Green;
            SetOK();
            return true;
        }
        catch (Exception)
        {
            SetError("TRADE NOT VALID");
            return false;
        }
    }
    #endregion Trades

    #region Extra Method
    private static void ListCategoryTrades()
    {
        foreach (Trade item in portfolio.Trades)
            Console.WriteLine(item.Categoria);

        Console.WriteLine("__________________________________________________________");
    }

    private static CultureInfo GetCultureInfo()
    {
        CultureInfo cultureInfo = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        cultureInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
        cultureInfo.DateTimeFormat.DateSeparator = "/";
        return cultureInfo;
    }

    private static void SetError(string description)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("     " + description);
    }

    private static void SetOK()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("     OK");
    }
    #endregion 
}