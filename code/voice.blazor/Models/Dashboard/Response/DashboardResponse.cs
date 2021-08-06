using System.Collections.Generic;

namespace voice.blazor.Models.Dashboard.Response
{
    public class DetailDashboard
    {
        public int TotalUsers { get; set; }
        public int TotalBonus { get; set; }
        public int TotalCountry { get; set; }
        public int TotalLanguage { get; set; }
        public int TotalCurrency { get; set; }
    }

    public class DashboardResponse
    {
        public List<DetailDashboard> details { get; set; }
    }
}