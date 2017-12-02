using System;
using System.Collections.Generic;
using System.Text;

namespace SpotHero.Services.BusObj.Models.Client
{
    /// <summary>
    /// Represents the POCO for client rate as read in by the JSON
    /// </summary>
    public class ClientRate
    {
        /// <summary>
        /// Days of week, comma-separated
        /// </summary>
        public string Days { get; set; }

        /// <summary>
        /// Dash-separated start and end time. eg. 0900-1200
        /// </summary>
        public string Times { get; set; }


        /// <summary>
        /// Price in Cents
        /// </summary>
        public int Price { get; set; }
    }
}
