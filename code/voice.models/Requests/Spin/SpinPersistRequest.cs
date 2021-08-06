namespace voice.models.Requests.Spin
{
    public class SpinPersistRequest : BaseIdRequest
    {
        public string SpinValue { get; set; }

        public string SpinFrequency { get; set; }
    }
}