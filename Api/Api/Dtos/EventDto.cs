using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        [Range(1, double.MaxValue)]
        public double OddsForFirstTeam { get; set; }

        [Range(1, double.MaxValue)]
        public double OddsForDraw { get; set; }

        [Range(1, double.MaxValue)]
        public double OddsForSecondTeam { get; set; }

        
        public DateTimeOffset EventStartDate { get; set; }
    }
}