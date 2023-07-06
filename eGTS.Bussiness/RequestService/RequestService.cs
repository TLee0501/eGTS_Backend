using eGTS_Backend.Data.Models;
using eGTS_Backend.Data.ViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eGTS.Bussiness.RequestService
{
    public class RequestService : IRequestService
    {
        private readonly EGtsContext _context;

        public RequestService(EGtsContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRequest(RequestCreateViewModel request)
        {
            var id = Guid.NewGuid();
            var requestService = new Request(id, request.GymerId, request.ReceiverId, request.PackageGymerId, request.IsPt, null);
            _context.Requests.Add(requestService);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
            return false;
        }

        public async Task<List<RequestViewModel>> GetAllRequestForPTNE(Guid id, bool isPT)
        {
            var requests = await _context.Requests.Where(a => a.ReceiverId == id && a.IsPt == isPT && a.IsAccepted != null).ToListAsync();
            List<RequestViewModel> result = new List<RequestViewModel>();
            foreach (var item in requests)
            {
                var temp = new RequestViewModel();
                temp.Id = item.Id;
                temp.ReceiverId = item.ReceiverId;
                temp.GymerId = item.GymerId;
                temp.PackageGymerId = item.PackageGymerId;
                temp.IsPt = item.IsPt;
                if (item.IsAccepted != null)
                {
                    temp.IsAccepted = item.IsAccepted;
                }
                result.Add(temp);
            }
            return result;
        }

        public async Task<Request> GetRequest(Guid id)
        {
            var result = await _context.Requests.FindAsync(id);
            return result;
        }

        public async Task<bool> UpdateRequest(RequestViewModel request)
        {
            Request newRequest = new Request(request.Id, request.GymerId, request.ReceiverId, request.PackageGymerId, request.IsPt, request.IsAccepted);

            //Accepct
            if(request.IsAccepted != null)
            {
                var packagegymer = await _context.PackageGymers.SingleOrDefaultAsync(a => a.GymerId == request.GymerId);
                if (request.IsPt == true)
                {
                    packagegymer.Ptid = request.ReceiverId;
                }
                else packagegymer.Neid = request.ReceiverId;

                //Update status PackageGymer
                if(packagegymer.Ptid != null && packagegymer.Neid != null)
                {
                    packagegymer.Status = "Active";
                }
                var packageType = await _context.Packages.FindAsync(packagegymer.PackageId);
                if(packageType.HasPt == false && packagegymer.Neid != null)
                {
                    packagegymer.Status = "Active";
                }
                if (packageType.HasNe == false && packagegymer.Ptid != null)
                {
                    packagegymer.Status = "Active";
                }
            }

            _context.Entry(newRequest).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return false;
            }
        }
    }
}
