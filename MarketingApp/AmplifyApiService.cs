using MarketingApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MarketingApp
{
    public class AmplifyApiService
    {
        private const string TOKEN = "MTU5MzM3MTI4NjE4OTo4NzM2OTBiYTYzZTNmY2QxYWQxNjkyODRmNzQ4ZWYzOGVkMTMwMDkzMTYyYzBiNjNjNDcyMTlhZTRlODA1YzI4NjNlMDQxYzgzNWQ4M2ExMjUxOTdkMDg5YzY5NzdhOTY2MDg0ODMzNjYxNTNlODcyYjY0YjE4ZjM0MjI3NmRiMDdhZjhkZWYyNDEwNjNhNzI4ZDgyM2Q1ZmFjMmVjZGY5YTcxMWIxY2Y0MGUxNmMxYTU3YjRkM2ViMGI1MzQzMTdjYmYyOWNhZGYxZGU2ZTI4NjQ5MzNjMjk5NzcxNTdlZDBjZWFkMGY5MmE0NjIxM2ExOTI2MmZlMGQ1YTlmMzQzZDEyOWY3MmJkYzZjNWRkY2JmZjdkMGM0ZTk3YTM4NmM1MjFlYjA3NDAyNmU4MDc0ZDAwYmNlY2UxMGMzYzQyNDgyYTA4ZGEzMjBhYTgwZWM5YzU3MjJiOGM3NTg5ZWJiMTUxYTc4ZTUwNWM4YWU4MzM1ZGM4YzUyYWU4ZGEwZmFkMmQzMmNhNTBiYzYyYjY1ZWRiN2MwZmNhZDk2OGFjMjk1OWMxNjUzMDI4YzBiYWY3NmRmMTVlYTA4ZTU4NjYwNzcwZDRlYTg3Njk3MzVlYTMxN2UwZmY3OGE1NTAzZTNmYTNlNGEwZWFkYTY4YWY0ZTA4YzYyZjU0NGNkYjlmMjp7ImNhbGxlckFwcGxpY2F0aW9uIjoiQW1lbGlhIiwiaXBBZGRyZXNzIjoiNzcuMTI3Ljc5LjU1IiwiYnlwYXNzQXBpQXV0aCI6ImZhbHNlIiwidXNlck5hbWUiOiJhbWlyZ2F0MjEyQGdtYWlsLmNvbSIsInVzZXJJZCI6IjEwNjI0NjA3IiwiZGF0YVNvdXJjZVR5cGUiOiJNWV9PQl9DT00ifTpkYTRlMGRhMWJiYTRjMjMzMDIzZWViZmNhMjRjY2VlNGVmODM2MTAzMGM2ZTUxNjc5MDZjMmE5NjI5YTQ2YjhlMDU5OTZhOWI1YWY4OTc4NDViZTkwNGFhOGQyZTYyMzQ0YTUxMmMzY2FmYjBkZDJiMTdmY2E2YmZmZWIyYmI3Mg==";
        private const string BASE_URI = "https://api.outbrain.com/amplify/v0.1/";
        private const string MOCK_URI = "https://private-anon-18e4f51dfd-amplifyv01.apiary-mock.com";
        private HttpClient _httpClient;
        private CampaignCollection _campaigns;

        public AmplifyApiService()
        {
            var baseAddress = new Uri(BASE_URI);
            _httpClient = new HttpClient { BaseAddress = baseAddress };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("ob-token-v1", TOKEN);
            _campaigns = null;
        }

        public Campaign GetCampaignById(string id)
        {
            if (_campaigns != null)
            {
                try
                {
                    return _campaigns.Campaigns.First((campaign) => {
                        return campaign.Id == id;
                    });
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Campaign> GetCampaignsFromLastYear()
        {
            if (_campaigns != null)
            {
                try
                {
                    return _campaigns.Campaigns.Where((campaign) =>
                    {
                        var creationTime = DateTime.Parse(campaign.CreationTime);
                        return DateTime.Now.Subtract(creationTime).Days <= 365;
                    }).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Campaign> GetCampaignAboveAmountSpent(float amountSpent)
        {
            if (_campaigns != null)
            {
                try
                {
                    return _campaigns.Campaigns.Where((campaign) =>
                    {
                        return campaign.LiveStatus.AmountSpent > amountSpent;
                    }).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Campaign GetCampaignById()
        {
            return GetCampaignById("c175ad51c42f2a7713e53dce5a12bc0088");
        }

        public async Task GetCampaignsByMarketer(string id)
        {
            string json = await _httpClient.GetStringAsync($"marketers/{id}/campaigns?includeArchived=true&extraFields=BidBySections");
            try
            {
                _campaigns = JsonConvert.DeserializeObject<CampaignCollection>(json);
            }
            catch (Exception ex)
            {
                _campaigns = null;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
