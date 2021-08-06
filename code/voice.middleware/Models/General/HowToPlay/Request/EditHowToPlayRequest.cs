using System;

namespace voice.middleware.Models.General.HowToPlay.Request
{
    public class EditHowToPlayRequest
    {
        public Guid Id { get; set; }

        public string howToPlay { get; set; }
    }
}