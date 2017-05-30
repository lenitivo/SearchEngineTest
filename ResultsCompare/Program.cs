using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Globalization;

namespace ResultsCompare
{
    class Program
    {
        # region "Search Engine Code"
        public static string GoogleSearch(string strKeyWord)
        {
            string engineUrl = Constant.EngineUrl.GoogleUrl;

            string WebResult = String.Empty;
            string resultNumber = String.Empty;

            try
            {

                WebClient webClient = new WebClient();
                webClient.Headers["Accept-Language"] = Constant.Culture.US;

                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add("q", strKeyWord);

                webClient.QueryString.Add(nameValueCollection);
                WebResult = webClient.DownloadString(engineUrl);
                resultNumber = getResultNumber(WebResult, Constant.LimitWord.GoogleStart
                    , Constant.LimitWord.GoogleEnd, Constant.SearchEngine.Google);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultNumber;
        }

        public static string MSNSearch(string strKeyWord)
        {
            string engineUrl = Constant.EngineUrl.MSNUrl;

            string WebResult = String.Empty;
            string resultNumber = String.Empty;

            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers["Accept-Language"] = Constant.Culture.US;

                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add("q", strKeyWord);

                webClient.QueryString.Add(nameValueCollection);
                WebResult = webClient.DownloadString(engineUrl);
                resultNumber = getResultNumber(WebResult, Constant.LimitWord.MSNStart, Constant.LimitWord.MSNEnd, Constant.SearchEngine.MSN);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultNumber;
        }

        public static string getResultNumber(string strSource, string strStart, string strEnd, string strEngine)
        {
            int start, end;
            
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                start = strSource.IndexOf(strStart, 0) + strStart.Length;
                end = strSource.IndexOf(strEnd, start);
                if (end < 0 && strEngine == Constant.SearchEngine.Google)
                {
                    start = strSource.IndexOf(Constant.LimitWord.GoogleStart2, 0) + 13;
                    end = strSource.IndexOf(strEnd, start);
                }
                return strSource.Substring(start, end - start).Trim();
            }
            else
            {
                return "";
            }
        }
        #endregion

        static void Main(string[] args)
        {
            int numKeyWords = -1;
            decimal googleResults = -1;
            decimal msnResults = -1;
            string strGoogleResult = String.Empty;  //Number of results found using Google
            string strMSNResult = String.Empty;     //Number of results found using MSN
            string keyWord = String.Empty;
            string googleWinner = String.Empty;
            string msnWinner = String.Empty;

            try
            {
                if (args.Length > 0)
                {
                    numKeyWords = args.Length;

                    Console.WriteLine("\nSEARCH ENGINE COMPARE APPLICATION\n");
                    for (int i = 0; i < numKeyWords; i++)
                    {
                        keyWord = args[i];

                        //GOOGLE
                        strGoogleResult = GoogleSearch(keyWord).Replace('.', ',');
                        if (googleResults < Decimal.Parse(strGoogleResult, CultureInfo.InvariantCulture))
                        {
                            googleResults = Decimal.Parse(strGoogleResult, CultureInfo.InvariantCulture);
                            googleWinner = keyWord.ToUpper();
                        }
                        Console.WriteLine("GOOGLE results for " + keyWord.ToUpper() + ": " + strGoogleResult + " results.");

                        //MSN
                        strMSNResult = MSNSearch(keyWord).Replace('.', ',');
                        if (msnResults < Decimal.Parse(strMSNResult, CultureInfo.InvariantCulture))
                        {
                            msnResults = Decimal.Parse(strMSNResult, CultureInfo.InvariantCulture);
                            msnWinner = keyWord.ToUpper();
                        }
                        Console.WriteLine("MSN results for " + keyWord.ToUpper() + ": " + strMSNResult + " results.");
                    }
                    Console.WriteLine("\nGOOGLE most results are for: " + googleWinner + ".");
                    Console.WriteLine("MSN most results are for: " + msnWinner + ".");
                    Console.Write("\nMost results in TOTAL are for: ");
                    if (googleResults > msnResults)
                        Console.WriteLine(googleWinner + ".\n");
                    else
                        Console.WriteLine(msnWinner + ".\n");
                }
                else
                    Console.WriteLine("You did not enter any keyword.\n");
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred, please try again.\n");
            }            
            //char ch = Console.ReadKey().KeyChar;
        }
        
    }
}
