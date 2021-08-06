using System;

namespace voice.blazor.Models.General.HowToPlay.Request
{
    public class EditHowToPlayRequest
    {
        public Guid Id { get; set; }

        public string howToPlay { get; set; }
    }
}