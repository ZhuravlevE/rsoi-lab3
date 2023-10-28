﻿using System.Net.Http;
using System;
using Gateway.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Gateway.Controllers;
using Microsoft.Extensions.Logging;
using Gateway.Utils;
using System.Net;

namespace Gateway.Services
{
    public class LoyaltyService
    {
        private readonly RequestQueueService _requestQueueService;
        private readonly HttpClient _httpClient;
        
        public LoyaltyService(RequestQueueService requestQueueService)
        {
            _requestQueueService = new RequestQueueService();
            _requestQueueService.StartWorker();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://loyalty:8050/");
        }

        public async Task<bool> HealthCheckAsync()
        {
            using var req = new HttpRequestMessage(HttpMethod.Get,
                "manage/health");
            using var res = await _httpClient.SendAsync(req);
            return res.StatusCode == HttpStatusCode.OK;
        }

        public async Task<Loyalty?> GetLoyaltyByUsernameAsync(string username)
        {
            using var req = new HttpRequestMessage(HttpMethod.Get, "api/v1/loyalty");
            req.Headers.Add("X-User-Name", username);
            using var res = await _httpClient.SendAsync(req);
            var response = await res.Content.ReadFromJsonAsync<Loyalty>();
            return response;
        }

        public async Task<Loyalty?> PutLoyaltyByUsernameAsync(string username)
        {
            using var req = new HttpRequestMessage(HttpMethod.Put, "api/v1/loyalty");
            req.Headers.Add("X-User-Name", username);
            using var res = await _httpClient.SendAsync(req);
            var response = await res.Content.ReadFromJsonAsync<Loyalty>();
            return response;
        }

        public async Task<Loyalty?> DeleteLoyaltyByUsernameAsync(string username)
        {
            using var req = new HttpRequestMessage(HttpMethod.Delete, "api/v1/loyalty");
            req.Headers.Add("X-User-Name", username);

            try
            {
                using var res = await _httpClient.SendAsync(req);
                if (!res.IsSuccessStatusCode)
                {
                    var reqClone = await HttpRequestMessageHelper.CloneHttpRequestMessageAsync(req);
                    _requestQueueService.AddRequestToQueue(reqClone);
                    return null;
                }

                var response = await res.Content.ReadFromJsonAsync<Loyalty>();

                return response;
            }
            catch (HttpRequestException e)
            {
                var reqClone = await HttpRequestMessageHelper.CloneHttpRequestMessageAsync(req);
                _requestQueueService.AddRequestToQueue(reqClone);
                return null;
            }
        }
    }
}
