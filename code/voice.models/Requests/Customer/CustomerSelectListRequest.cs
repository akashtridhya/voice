using System;

namespace voice.models
{
    public class CustomerSelectListRequest : SearchParamRequest
    {
        public string PaymentMode { get; set; }

        public Guid? UserId { get; set; }
        public string DmaStaffUserIdCsv { get; set; }
        public string DmaUserIdCsv { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string GaUserIdCsv { get; set; }
        public string CompanyUserIdCsv { get; set; }
        public string CityIdCsv { get; set; }
        public string AreaIdCsv { get; set; }
        public string PincodeIdCsv { get; set; }
        public string PremiseTypeIdCsv { get; set; }
        public string KichenPointIdCsv { get; set; }
        public string GyserPointIdCsv { get; set; }
        public bool? IsLpguas { get; set; }
        public string PaymentTypeCsv { get; set; }
        public string PaymentModeCsv { get; set; }
        public string StatusCsv { get; set; }
    }
}