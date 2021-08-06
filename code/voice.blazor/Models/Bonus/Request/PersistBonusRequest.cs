namespace voice.blazor.Models.Bonus.Request
{
    public class PersistBonusRequest
    {
        public string id { get; set; }
        public string bonusName { get; set; }
        public string bonusDescription { get; set; }
        public int bonusAmount { get; set; }
    }
}