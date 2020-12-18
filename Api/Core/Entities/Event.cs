using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Event : BaseEntity
    {
        public Event()
        {
            this.EventStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        }
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