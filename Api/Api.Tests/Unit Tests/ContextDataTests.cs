using Api.Dtos;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Unit_Tests
{
    public class ContextDataTests
    {

        OddContext oddContext;
        public ContextDataTests()
        {
            oddContext = new OddContext(new DbContextOptionsBuilder<OddContext>()
               .UseInMemoryDatabase("test").Options);
        }

        [Fact]
        public void EventCreateTestDateToday2399()
        {
            Event newEvent = new Event();
            var ev = oddContext.Events.Add(newEvent);
            oddContext.SaveChanges();

            Event eventCreated = oddContext.Events.FirstOrDefault();
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59),
                eventCreated.EventStartDate);
        }

        [Fact]
        public void EventCreatedSuccessfullyTest()
        {
            Event newEvent = new Event()
            {
                EventName = "Liverpool - Barcelona",
                OddsForDraw = 1.53,
                OddsForFirstTeam = 2.31,
                OddsForSecondTeam = 253.24
            };
            var ev = oddContext.Events.Add(newEvent);
            oddContext.SaveChanges();

            Event eventCreated = oddContext.Events.FirstOrDefault();
            Assert.Equal(1.53,eventCreated.OddsForDraw);
            Assert.Equal(2.31, eventCreated.OddsForFirstTeam);
            Assert.Equal(253.24, eventCreated.OddsForSecondTeam);
        }

        [Fact]
        public void UpdateAllSuccessfullyTest()
        {
            Event newEvent = new Event()
            {
                EventName = "Liverpool - Barcelona",
                OddsForDraw = 1.53,
                OddsForFirstTeam = 2.31,
                OddsForSecondTeam = 253.24
            };
            var ev = oddContext.Events.Add(newEvent);
            oddContext.SaveChanges();

            var nowDate = DateTime.Now;
            Event eventCreated = oddContext.Events.FirstOrDefault();
            eventCreated.EventName = "Real - Manchester";
            eventCreated.OddsForDraw = 2.12;
            eventCreated.OddsForFirstTeam = 1.57;
            eventCreated.OddsForSecondTeam = 51.75;
            eventCreated.EventStartDate = nowDate;
            oddContext.Update(eventCreated);
            oddContext.SaveChanges();

            Assert.Equal("Real - Manchester", eventCreated.EventName);
            Assert.Equal(2.12, eventCreated.OddsForDraw);
            Assert.Equal(1.57, eventCreated.OddsForFirstTeam);
            Assert.Equal(51.75, eventCreated.OddsForSecondTeam);
            Assert.Equal(nowDate, eventCreated.EventStartDate);
        }


        private void SeedOdds()
        {
            for (int i = 0; i < 26; i++)
            {
                Event ev = new Event();
                
            }

        }

    }
}
