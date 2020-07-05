using MarketingApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string MARKETER_ID = "0004f6d7c482ee7b6c5b5d40dc6823c5c5";
                var api = new AmplifyApiService();
                var csvWriter = new CsvWriter("C:/projects/MarketingApp/MarketingApp/file.csv");
                Task.Run(async () =>
                {
                    Console.WriteLine("Loading campaigns...");
                    await api.GetCampaignsByMarketer(MARKETER_ID);// Loading all the campaign for the marketer ID given in the assignment.
                    Console.WriteLine("Finished loading.");
                    PrintInstructions();
                    string command = Console.ReadLine();
                    while (command != "0")
                    {
                        switch (command)
                        {
                            case "1":
                                Console.WriteLine("Enter campaign ID (default is c175ad51c42f2a7713e53dce5a12bc0088): ");
                                string campaignId = Console.ReadLine();
                                Campaign campaign;
                                if (campaignId.Length == 0)
                                {
                                    campaign = api.GetCampaignById();
                                }
                                else
                                {
                                    campaign = api.GetCampaignById(campaignId);
                                }
                                PrintCampaign(campaign);
                                break;
                            case "2":
                                var lastYear = api.GetCampaignsFromLastYear();
                                csvWriter.WriteCampaigns(lastYear);
                                Console.WriteLine("Campaigns saved!");
                                break;
                            case "3":
                                Console.WriteLine("Enter spend amount (default is 10$): ");
                                bool invalidInput = true;
                                while (invalidInput)
                                {
                                    string amountSpent = Console.ReadLine();

                                    IEnumerable<Campaign> campaigns;
                                    if (amountSpent.Length == 0)
                                    {
                                        campaigns = api.GetCampaignAboveAmountSpent();
                                        invalidInput = false;
                                        PrintCampaignList(campaigns);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            campaigns = api.GetCampaignAboveAmountSpent(float.Parse(amountSpent));
                                            invalidInput = false;
                                            PrintCampaignList(campaigns);
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Enter a number");
                                        }
                                    }
                                }

                                break;
                            case "0":
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                        if (command != "0")
                        {
                            PrintInstructions();
                            command = Console.ReadLine();
                        }
                    }
                }).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Console.ReadLine();
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

        static void PrintCampaignList(IEnumerable<Campaign> campaigns)
        {
            foreach (var campaign in campaigns)
            {
                PrintCampaign(campaign);
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
