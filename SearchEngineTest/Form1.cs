using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using System.Globalization;
using Common;


namespace SearchEngineTest
{
    public partial class frmSearchForm : Form
    {
        public frmSearchForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string results = " results.";

            try
            {

                if (txtFirstWord.Text.Trim() == "" && txtSecondWord.Text.Trim() == "")
                    MessageBox.Show("You must enter at least one key word.");
                else
                {
                    if (txtFirstWord.Text.Trim() != "")
                    {
                        lblGoogleFirst.Text = GoogleSearch(txtFirstWord.Text) + results;
                        lblMSNFirst.Text = MSNSearch(txtFirstWord.Text) + results;
                    }

                    if (txtSecondWord.Text.Trim() != "")
                    {
                        lblGoogleSecond.Text = GoogleSearch(txtSecondWord.Text) + results;
                        lblMSNSecond.Text = MSNSearch(txtSecondWord.Text) + results;
                    }
                }
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected string GoogleSearch(string strKeyWord)
        {
            string engineUrl = Constant.EngineUrl.GoogleUrl;

            string WebResult = String.Empty;
            string resultNumber = String.Empty;

            WebClient webClient = new WebClient();
            webClient.Headers["Accept-Language"] = Constant.Culture.US;

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", strKeyWord);

            webClient.QueryString.Add(nameValueCollection);
            WebResult = webClient.DownloadString(engineUrl);
            resultNumber = getResultNumber(WebResult, Constant.LimitWord.GoogleStart
                , Constant.LimitWord.GoogleEnd, Constant.SearchEngine.Google);

            //txtResult.Text = WebResult;

            return resultNumber;
        }

        protected string MSNSearch(string strKeyWord)
        {
            string engineUrl = Constant.EngineUrl.MSNUrl;

            string WebResult = String.Empty;
            string resultNumber = String.Empty;

            WebClient webClient = new WebClient();
            webClient.Headers["Accept-Language"] = Constant.Culture.US;

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("q", strKeyWord);

            webClient.QueryString.Add(nameValueCollection);
            WebResult = webClient.DownloadString(engineUrl);
            resultNumber = getResultNumber(WebResult, Constant.LimitWord.MSNStart, Constant.LimitWord.MSNEnd, Constant.SearchEngine.MSN);

            //txtResult.Text = WebResult;

            return resultNumber;
        }

        public static string getResultNumber(string strSource, string strStart, string strEnd, string strEngine)
        {
            int start, end;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                start = strSource.IndexOf(strStart, 0) + strStart.Length;
                end = strSource.IndexOf(strEnd, start);
                if (end < 0 && strEngine == "Google")
                {
                    start = strSource.IndexOf("resultStats\">", 0) + 13;
                    end = strSource.IndexOf(strEnd, start);
                }
                return strSource.Substring(start, end - start).Trim();
            }
            else
            {
                return "";
            }
        }       

    }
}
