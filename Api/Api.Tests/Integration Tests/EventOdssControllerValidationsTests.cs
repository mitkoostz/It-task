using Api.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Integration_Tests
{
    public class EventOdssControllerValidationsTests
    {
        private readonly IConfiguration config;
        private readonly string controllerUrl;
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public EventOdssControllerValidationsTests()
        {
            this.config = Config.InitConfiguration();
            this.controllerUrl = config["ApiUrl"] + "api/EventOdds/";
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
        }

        [Fact]
        public async void UpdatingEeventWithNullBadRequestTest()
        {
            HttpClient client = webApplicationFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(new Event()), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(controllerUrl + "updateevent", content);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void UpdateEventWithValueBelowOneBadRequestTest()
        {
            Event evToupdate = new Event();
            evToupdate.OddsForDraw = 0.50;
            evToupdate.OddsForSecondTeam = 0.24;
            evToupdate.OddsForFirstTeam = 2.53;

            HttpClient client = webApplicationFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(evToupdate), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(controllerUrl + "updateevent", content);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void DeletingEventWithNonNumerableParameterTest()
        {
            string eventId = "eventId123123";
            HttpClient client = webApplicationFactory.CreateClient();
            var response = await client.DeleteAsync(controllerUrl + "deleteevent" + $"?id={eventId}");
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }





    }
}
