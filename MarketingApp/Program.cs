using MarketingApp.Models;
using System;
using System.Threading.Tasks;

namespace MarketingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var api = new AmplifyApiService();
                var csvWriter = new CsvWriter("C:/projects/MarketingApp/MarketingApp/file.csv");
                Task.Run(async () => {
                    await api.GetCampaignsByMarketer("0004f6d7c482ee7b6c5b5d40dc6823c5c5");
                    var campaign = api.GetCampaignById("00260216138447a76be5e8f50b3701920f");
                    PrintCampaign(campaign);
                    var lastYear = api.GetCampaignsFromLastYear();
                    csvWriter.WriteCampaigns(lastYear);
                    var above10 = api.GetCampaignAboveAmountSpent(-1);
                    foreach (var item in above10)
                    {
                        PrintCampaign(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        static void PrintCampaign(Campaign campaign)
        {
            if (campaign != null)
            {
                Console.WriteLine($"Campaign {campaign.Id}");
                Console.WriteLine($"    Name: {campaign.Name}");
                Console.WriteLine($"    Creation Time: {campaign.CreationTime}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Campaign does not exist");
            }
        }

        static void PrintInstructions()
        {
            Console.WriteLine("Choose action:");
            Console.WriteLine("Enter 1 to retrieve a campaign by ID.");
            Console.WriteLine("Enter 2 to save all campaigns created last year.");
            Console.WriteLine("Enter 3 to see all campaigns with spending above a certain amount.");
            Console.WriteLine("Enter 0 to exit");
        }
    }
}
