using voice.api.Controllers;
using voice.models;
using Microsoft.Extensions.Localization;
using System;
using System.Security.Cryptography;
using System.Text;

namespace voice.api.Helpers
{
    public class PaymentHelpers
    {
        // DO NOT MODIFY THIS FUNCTION WITHOUT CONFIRMATION
        public string GetPaymentRequestString(
            PaymentConfigs paymentConfigs,
            string orderId,
            string amount,
            string applicationNumber)
        {
            // DO NOT MODIFY THIS STRING CONCATINATION WITHOUT ENSURE
            var request = $"{paymentConfigs.MerchantId}|{orderId}|NA|{amount}|NA|NA|NA|{paymentConfigs.CurrencyType}|NA|{paymentConfigs.TypeField1}|{paymentConfigs.SecurityId}|NA|NA|{paymentConfigs.TypeField2}|{applicationNumber}|NA|NA|NA|NA|NA|NA|{paymentConfigs.ReturnUrl}";

            var checksum = GetHMACSHA256(request, paymentConfigs.ChecksumKey);

            var response = $"{request}|{checksum.ToUpper()}";

            return response;
        }

        // DO NOT MODIFY THIS FUNCTION WITHOUT CONFIRMATION
        public string GetHMACSHA256(
            string text,
            string key)
        {
            UTF8Encoding encoder = new UTF8Encoding();

            byte[] hashValue;
            byte[] keybyt = encoder.GetBytes(key);
            byte[] message = encoder.GetBytes(text);

            HMACSHA256 hashString = new HMACSHA256(keybyt);
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }

            return hex;
        }

        // DO NOT MODIFY THIS FUNCTION WITHOUT CONFIRMATION
        public string GetStatusReason(
            IStringLocalizer<BaseController> Localizer,
            string value)
        {
            return value switch
            {
                "0399" => Localizer["payment_response_invalid_authentication"].Value,
                "NA" => Localizer["payment_response_invalid_request"].Value,
                "0002" => Localizer["payment_response_waiting_for_response"].Value,
                "0001" => Localizer["payment_response_error_at_billdesk"].Value,
                _ => null,
            };
        }

        // DO NOT MODIFY THIS FUNCTION WITHOUT CONFIRMATION
        public static string GetTransactionType(string value)
        {
            return value switch
            {
                "01" => "Net Banking",
                "02" => "Credit Card",
                "03" => "Debit Card",
                "04" => "Cash Card",
                "05" => "Mobile Wallet",
                "06" => "IMPS",
                "07" => "Reward Points",
                "08" => "Rupay",
                "09" => "Others",
                "10" => "UPI",
                _ => string.Empty,
            };
        }

        public static string ConvertWholeNumber(string number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX    
                bool isDone = false;//test if already translated    
                double dblAmt = (Convert.ToDouble(number));
                //if ((dblAmt > 0) && number.StartsWith("0"))    
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric    
                    beginsZero = number.StartsWith("0");

                    int numDigits = number.Length;
                    int pos = 0;//store digit grouping    
                    string place = "";//digit grouping name:hundres,thousand,etc...    
                    switch (numDigits)
                    {
                        case 1://ones' range    

                            word = Ones(number);
                            isDone = true;
                            break;
                        case 2://tens' range    
                            word = Tens(number);
                            isDone = true;
                            break;
                        case 3://hundreds' range    
                            pos = (numDigits % 3) + 1;
                            place = " Hundred ";
                            break;
                        case 4://thousands' range    
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " Thousand ";
                            break;
                        case 7://millions' range    
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " Million ";
                            break;
                        case 10://Billions's range    
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " Billion ";
                            break;
                        //add extra case options for anything above Billion...    
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)    
                        if (number.Substring(0, pos) != "0" && number.Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(number.Substring(0, pos)) + place + ConvertWholeNumber(number.Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(number.Substring(0, pos)) + ConvertWholeNumber(number.Substring(pos));
                        }

                        //check for trailing zeros    
                        //if (beginsZero) word = " and " + word.Trim();    
                    }
                    //ignore digit grouping names    
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }
        public static string Ones(string number)
        {
            int _Number = Convert.ToInt32(number);
            string name = "";
            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }
        public static string Tens(string number)
        {
            int _Number = Convert.ToInt32(number);
            string name = null;
            switch (_Number)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = Tens(number.Substring(0, 1) + "0") + " " + Ones(number.Substring(1));
                    }
                    break;
            }
            return name;
        }
    }
}