namespace voice.models
{
    public class FolderPathConsts
    {
        // Registration
        public const string CustomerRegistrationFromUpload = @"Registration\Input";

        public const string CustomerRegistrationReadSuccess = @"Registration\Success";

        public const string CustomerRegistrationReadErrors = @"Registration\Error";

        public const string CustomerRegistrationReadServerErrors = @"Registration\Archive\Error";

        public const string CustomerRegistrationArchive = @"Registration\Archive";

        // Payment
        public const string PaymentUpload = @"Payment\Input";

        public const string PaymentSuccess = @"Payment\Success";

        public const string PaymentErrors = @"Payment\Error";

        public const string PaymentServerErrors = @"Payment\Archive\Error";

        public const string PaymentArchive = @"Payment\Archive";

        // Stemp Duty
        public const string StempDutyUpload = @"Stampduty\Input";

        public const string StempDutyErrors = @"Stampduty\Error";

        public const string StempDutyServerErrors = @"Stampduty\Archive\Error";

        public const string StempDutySuccess = @"Stampduty\Success";

        public const string StempDutyArchive = @"Stampduty\Archive";

        // DMS
        public const string DmsDocumentUpload = @"DMS\Document";

        public const string DmsInputUpload = @"DMS\Input";

        public const string DmsError = @"DMS\Error";

        public const string DmsSuccess = @"DMS\Success";

        public const string DmsServerError = @"DMS\Archive\Error";

        public const string DmsArchive = @"DMS\Archive";

        // City and Pincode Master
        public const string CityMasterInput = @"SAP_Master\City_Master\Input";

        public const string CityMasterArchive = @"SAP_Master\City_Master\Archive";
     
        public const string PincodeMasterInput = @"SAP_Master\Pincode_Master\Input";
     
        public const string PincodeMasterArchive = @"SAP_Master\Pincode_Master\Archive";
    }
}