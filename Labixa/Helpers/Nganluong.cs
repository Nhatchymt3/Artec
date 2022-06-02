using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labixa.Helpers
{
    public static class Nganluong
    {
        public static string GetCheckout_URL(string option_payment, string bankcode,string merchantId,string merchantPassword, string Receive_Email, string cur_code, 
                                    string OrderCode, string TotalAmount, string feeShipping, string discount, string order_desc, string returnUrl, string cancelUrl,
                                    string BuyerName, string BuyerEmail, string BuyerMobile)
        {
            string payment_method = option_payment;
            string str_bankcode = bankcode;
            RequestInfo info = new RequestInfo();
            info.Merchant_id = merchantId;
            info.Merchant_password = merchantPassword;
            info.Receiver_email = Receive_Email;
            info.cur_code = cur_code;
            info.bank_code = bankcode;
            info.Order_code = OrderCode;
            info.Total_amount = TotalAmount;
            info.fee_shipping = feeShipping;
            info.Discount_amount = discount;
            info.order_description = order_desc;
            info.return_url = returnUrl;
            info.cancel_url = cancelUrl;
            info.Buyer_fullname = BuyerName;
            info.Buyer_email = BuyerEmail;
            info.Buyer_mobile = BuyerMobile;
            info.Payment_type = "1";
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);
            return result.Checkout_url;
        }
        public static ResponseCheckOrder GetTransaction(string token, string merchantId, string MerchantPass)
        {
            String Token = token;
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = merchantId;
            info.Merchant_password = MerchantPass;
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            return result;
        }
    }
}