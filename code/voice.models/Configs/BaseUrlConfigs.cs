namespace voice.models
{
    public class BaseUrlConfigs
    {
        public string AdminInvite { get; set; }

        public string LoginUrl { get; set; }

        public string BaseUrl { get; set; }

        public string ResetPassword { get; set; }

        public string ImageBase { get; set; }

        public string ImageLocalPath { get; set; }

        public string ChangeEmail { get; set; }

        public string ConfirmEmail { get; set; }

        public string PaymentUrl { get; set; }

        public string UploadBaseLocalPath { get; set; }

        public string DmsDocumentStoragepath { get; set; }

        public string UploadBaseLocalPathServer { get; set; }

        /*
         * TODO: Remove below lines if all works fine. 
         */

        //public string SapBaseLocalPath { get; set; }

        //public string SapDfsCentralPath { get; set; }

        //public string SapSharedPath { get; set; }
    }
}