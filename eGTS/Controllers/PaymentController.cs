using eGTS.Bussiness.PackageGymersService;
using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.Responses;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ZaloPay.Helper;
using ZaloPay.Helper.Crypto;

namespace eGTS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private string appid = "553";
        private string key1 = "9phuAOYhan4urywHTh0ndEXiV3pKHr5Q";
        private string key2 = "eG4r0GcoNtRGbO8";
        private string createOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";
        static string queryOrderUrl = "https://sandbox.zalopay.com.vn/v001/tpe/getstatusbyapptransid";

        private PackageGymerCreateViewModel dataCreate { get; set; }
        private readonly IPackageGymersService _packageGymersService;

        public PaymentController(IPackageGymersService packageGymersService)
        {
            _packageGymersService = packageGymersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZaloPayResponse>>> CreareOrder(Guid GymerId, Guid PackageId)
        {
            //check if gymer already has NE
            var checkNE = await _packageGymersService.CheckAlreadyPackGymerHasNE(GymerId);
            if (checkNE == true) return BadRequest("Gymer đã mua gói tập có NE!");
            //check if gymer already has Center
            var checkCenter = await _packageGymersService.CheckAlreadyPackGymerHasCenter(GymerId);
            if (checkCenter == true) return BadRequest("Gymer đã mua gói tập có Center!");

            var package = new Package();
            using(var context = new EGtsContext())
            {
                package = await context.FindAsync<Package>(PackageId);
            }
            var transid = Guid.NewGuid().ToString();
            var embeddata = new { merchantinfo = "embeddata123" };
            var items = new[]{
                new { itemid = PackageId, itemname = GymerId, itemprice = package.Price.ToString(), itemquantity = 1 }
            };
            var param = new Dictionary<string, string>();

            param.Add("appid", appid);
            param.Add("appuser", GymerId.ToString());
            param.Add("apptime", Utils.GetTimeStamp().ToString());
            param.Add("amount", package.Price.ToString());
            param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + transid); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embeddata", JsonConvert.SerializeObject(embeddata));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "ZaloPay demo");
            param.Add("bankcode", "zalopayapp");

            var data = appid + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(createOrderUrl, param);
            var response = new List<ZaloPayResponse>();
            foreach (var entry in result)
            {
                var item = new ZaloPayResponse();
                item.property = entry.Key;
                item.value = $"{entry.Value}";
                //Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
                response.Add(item);
            }
            //add a vari
            var tmp = new PackageGymerCreateViewModel()
            {
                PackageID = PackageId,
                GymerID = GymerId
            };
            dataCreate = tmp;
            return response;
        }

        [HttpPost]
        private IActionResult Post([FromBody] dynamic cbdata)
        {
            var result = new Dictionary<string, object>();

            try
            {
                var dataStr = Convert.ToString(cbdata["data"]);
                var reqMac = Convert.ToString(cbdata["mac"]);

                var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, dataStr);

                Console.WriteLine("mac = {0}", mac);

                // kiểm tra callback hợp lệ (đến từ ZaloPay server)
                if (!reqMac.Equals(mac))
                {
                    // callback không hợp lệ
                    result["returncode"] = -1;
                    result["returnmessage"] = "mac not equal";
                }
                else
                {
                    // thanh toán thành công
                    // merchant cập nhật trạng thái cho đơn hàng
                    var dataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
                    //Console.WriteLine("update order's status = success where apptransid = {0}", dataJson["apptransid"]);
                    _packageGymersService.CreatePackageGymer(dataCreate);

                    result["returncode"] = 1;
                    result["returnmessage"] = "success";
                }
            }
            catch (Exception ex)
            {
                result["returncode"] = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
                result["returnmessage"] = ex.Message;
            }

            // thông báo kết quả cho ZaloPay server
            return Ok(result);
        }

        [HttpGet]
        private async Task<List<ZaloPayResponse>> GetStatus()
        {
            var apptransid = "<apptransid>";

            var param = new Dictionary<string, string>();
            param.Add("appid", appid);
            param.Add("apptransid", apptransid);
            var data = appid + "|" + apptransid + "|" + key1;

            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(queryOrderUrl, param);

            var response = new List<ZaloPayResponse>();
            foreach (var entry in result)
            {
                var item = new ZaloPayResponse();
                item.property = entry.Key;
                item.value = $"{entry.Value}";
                //Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
                response.Add(item);
            }
            return response;
        }
    }
}
