using MarketingApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarketingApp
{
    public class CsvWriter
    {
        //private StreamWriter _writer;
        private string _fileName;
        public CsvWriter(string fileName)
        {
            _fileName = fileName;
            //_writer = new StreamWriter(fileName, true, Encoding.UTF8);
        }

        public void WriteCampaigns(IEnumerable<Campaign> campaigns)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("name,id,budget amount,creation time,amount spent,section ids");
                foreach (var campaign in campaigns)
                {
                    sb.Append($"{campaign.Name},{campaign.Id},{campaign.Budget.Amount},{campaign.CreationTime},{campaign.LiveStatus.AmountSpent}");
                    // Check if there are bids for current campaign
                    if (campaign.Bids != null)
                    {
                        sb.Append(',');
                        var sectionIds = campaign.Bids.BySection.Select((bid) => bid.SectionId);

                        // Section ids list is represented as a list seperated by semicolons. For example: value1;value2;value3.
                        sb.AppendJoin(';', sectionIds);
                    }
                    sb.AppendLine();
                }
                File.WriteAllText(_fileName, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
