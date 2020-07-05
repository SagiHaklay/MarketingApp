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
                Task.Run(async () => {
                    await api.GetCampaignsByMarketer("0004f6d7c482ee7b6c5b5d40dc6823c5c5");
                    var campaign = api.GetCampaignById("00260216138447a76be5e8f50b3701920f");
                    PrintCampaign(campaign);
                    var lastYear = api.GetCampaignsFromLastYear();
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
                Console.WriteLine($"Name: {campaign.Name}");
                Console.WriteLine($"Creation Time: {campaign.CreationTime}");
            }
            else
            {
                Console.WriteLine("Campaign does not exist");
            }
        }
    }
}
