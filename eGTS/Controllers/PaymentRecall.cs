using eGTS_Backend.Data.VNPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace eGTS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaymentRecall : ControllerBase
    {
        [HttpGet]
        [Route("PaymentConfirm")]
        protected async Task<IHttpActionResult> PaymentConfirm()
        {
            var queryParameters = HttpContext.Request.Query;
            if (!queryParameters.IsNullOrEmpty())
            {
                string hashSecret = "MJDZLNLYFKAXDYBAKINTSTMPYYNSCMCA"; //Chuỗi bí mật
                var vnpayData = queryParameters;
                VnPayLibrary pay = new VnPayLibrary();

                //lấy toàn bộ dữ liệu được trả về
                foreach (var s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s.Key) && s.Value.ToString().StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s.Key, s.Value);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = queryParameters.FirstOrDefault(q => q.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        Console.WriteLine("Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId);
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        Console.WriteLine("Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode);
                    }
                }
                else
                {
                    Console.WriteLine("Có lỗi xảy ra trong quá trình xử lý");
                }
            }

            return (IHttpActionResult)Ok();
        }
    }
}
