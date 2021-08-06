namespace voice.models
{
    public class PaymentConfigs
    {
        public string MerchantId { get; set; }

        public string SecurityId { get; set; }

        public string ReturnUrl { get; set; }

        public string ChecksumKey { get; set; }

        public string CurrencyType { get; set; }

        public string TypeField1 { get; set; }

        public string TypeField2 { get; set; }

        public string HostUrl { get; set; }
    }
}