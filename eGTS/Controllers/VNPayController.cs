using eGTS_Backend.Data.VNPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> CreatePayment()
        {
            //Get Config Info
            string vnp_Returnurl = "https://egts.azurewebsites.net/api/PaymentConfirm"; //URL nhan ket qua tra ve 
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = "4S638AXZ"; //Ma website
            string vnp_HashSecret = "MJDZLNLYFKAXDYBAKINTSTMPYYNSCMCA"; //Chuoi bi mat

            VnPayLibrary pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", vnp_TmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", "1000000"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return paymentUrl;
        }
        // vui lòng tham khảo thêm tại code demo

        /*[System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> PaymentConfirm()
        {
            if (!Request.GetQueryNameValuePairs().IsNullOrEmpty())
            {
                string hashSecret = "MJDZLNLYFKAXDYBAKINTSTMPYYNSCMCA"; //Chuỗi bí mật
                var vnpayData = Request.GetQueryNameValuePairs();
                VnPayLibrary pay = new VnPayLibrary();

                //lấy toàn bộ dữ liệu được trả về
                foreach (var s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s.Key) && s.Value.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s.Key, s.Value);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.GetQueryNameValuePairs().FirstOrDefault(q => q.Key == "vnp_SecureHash").Value; //hash của dữ liệu trả về

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

            return Ok();
        }*/
    }
}

