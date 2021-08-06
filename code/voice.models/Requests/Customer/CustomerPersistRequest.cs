using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace voice.models
{
    public class CustomerPersistRequest : BaseIdRequest
    {
        public string ApplicationNumber { get; set; }

        [JsonIgnore]
        public Guid? DmaStaffId { get; set; }

        [Required(ErrorMessage = "bad_response_applyingparty_required")]
        public string ApplyingParty { get; set; }

        [Required(ErrorMessage = "bad_response_title_required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "bad_response_language_required")]
        public string Language { get; set; }

        [Required(ErrorMessage = "bad_response_surname_required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "bad_response_firstname_required")]
        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "bad_response_primarymobile_required")]
        public string PrimaryMobileNumber { get; set; }

        //[Required(ErrorMessage = "bad_response_secondarymobile_required")]
        public string SecondaryMobileNumber { get; set; }

        //[Required(ErrorMessage = "bad_response_email_required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "bad_response_cityid_required")]
        public Guid? CityId { get; set; }

        [Required(ErrorMessage = "bad_response_streetid_required")]
        public Guid? StreetId { get; set; }

        [Required(ErrorMessage = "bad_response_housenumber_required")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "bad_response_housename_required")]
        public string HouseName { get; set; }

        [Required(ErrorMessage = "bad_response_addresslinetwo_required")]
        public string AddressLineTwo { get; set; }

        public string AddressLineThree { get; set; }

        public string AddressLineFour { get; set; }

        [Required(ErrorMessage = "bad_response_pincode_required")]
        public string Pincode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Required(ErrorMessage = "bad_response_segmentid_required")]
        public Guid? SegmentId { get; set; }

        [Required(ErrorMessage = "bad_response_premiseid_required")]
        public Guid? PremiseId { get; set; }

        public int ExtraKitchenPointCount { get; set; }

        public int ExtraGyserPointCount { get; set; }

        [Required(ErrorMessage = "bad_response_islpg_required")]
        public bool IsHavingLpgConnection { get; set; }

        public string LpgCompanyName { get; set; }

        public string LpguasDistributorsName { get; set; }

        public string LpgConsumerName { get; set; }

        public string LpgUniqueId { get; set; }

        public string Status { get; set; }

        public string StatusByDma { get; set; }

        public string StatusByGa { get; set; }

        [Required(ErrorMessage = "bad_response_proofs_required")]
        public ICollection<ProofRequest> ProofList { get; set; }

        [Required(ErrorMessage = "bad_response_digitalsignature_required")]
        public string DigitalSignature { get; set; }

        public string DigitalSignatureImage { get; set; }

        [Required(ErrorMessage = "bad_response_paymenttype_required")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "bad_response_paymentmode_required")]
        public string PaymentMode { get; set; }

        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        public string MicrNumber { get; set; }

        public string BankName { get; set; }

        [Required(ErrorMessage = "bad_response_amount_required")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "bad_response_rateid_required")]
        public Guid? RateId { get; set; }

        public string ChequePhoto { get; set; }

        public string CorrectionRemark { get; set; }

        public decimal? ChequeAmount { get; set; }

        [JsonIgnore]
        public Guid? PerformedById { get; set; }

        public string Remarks { get; set; }

        public bool IsFreshEntry { get; set; }   // This field required to ensure this is insert API From Mobile only

        public int FamilyMemberCount { get; set; }
        
        public string OwnerName { get; set; }
        
        public string OwnerSignature { get; set; }
        
        public string OwnerContactNumber { get; set; }
    }

    public class ProofRequest : BaseIdRequest
    {
        public ICollection<string> Url { get; set; }

        public Guid DocumentId { get; set; }

        public string DocumentName { get; set; }

        public string IdentificationNumber { get; set; }

        public bool IsCorrectionMark { get; set; }

        public string Remarks { get; set; }
        
        public string DocumentType { get; set; }

        public ProofRequest()
        {
            Id = Guid.NewGuid();
        }
    }

    public class CustomerPersistDbRequest : BaseIdRequest
    {
        public string ApplicationNumber { get; set; }

        public Guid? DmaStaffId { get; set; }

        public string ApplyingParty { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        public string PrimaryMobileNumber { get; set; }

        public string SecondaryMobileNumber { get; set; }

        public string EmailId { get; set; }

        public Guid? CityId { get; set; }

        public Guid? StreetId { get; set; }

        public string HouseNumber { get; set; }

        public string HouseName { get; set; }

        public string AddressLineTwo { get; set; }

        public string AddressLineThree { get; set; }

        public string AddressLineFour { get; set; }

        public string Pincode { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public Guid? PremiseId { get; set; }

        public Guid? SegmentId { get; set; }

        public int ExtraKitchenPointCount { get; set; }

        public int ExtraGyserPointCount { get; set; }

        public bool IsHavingLpgConnection { get; set; }

        public string LpgCompanyName { get; set; }

        public string LpguasDistributorsName { get; set; }

        public string LpgConsumerName { get; set; }

        public string LpgUniqueId { get; set; }

        public string Status { get; set; }

        public string StatusByDma { get; set; }

        public string StatusByGa { get; set; }

        public string ProofSerialized { get; set; }

        public string CustomerSerialized { get; set; }

        public string DigitalSignature { get; set; }

        public string DigitalSignatureImage { get; set; }

        public string PaymentType { get; set; }

        public string PaymentMode { get; set; }

        public string ChequeNumber { get; set; }

        public DateTime? ChequeDate { get; set; }

        public string MicrNumber { get; set; }

        public string BankName { get; set; }

        public decimal? Amount { get; set; }

        public string ChequePhoto { get; set; }

        public string CorrectionRemark { get; set; }

        public Guid? RateId { get; set; }

        public decimal? ChequeAmount { get; set; }

        public Guid? PerformedById { get; set; }

        public string Remarks { get; set; }

        public int FamilyMemberCount { get; set; }

        public string OwnerName { get; set; }

        public string OwnerSignature { get; set; }

        public string OwnerContactNumber { get; set; }
    }
}