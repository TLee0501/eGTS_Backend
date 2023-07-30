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

        public async Task<int> CreateRequest(RequestCreateViewModel request)
        {
            var id = Guid.NewGuid();
            var requestService = new Request(id, request.GymerId, request.ReceiverId, request.PackageGymerId, request.IsPt, null, false);
            var checkExist = await _context.Requests.SingleOrDefaultAsync(a => a.ReceiverId == request.ReceiverId && a.PackageGymerId == request.PackageGymerId);
            if (checkExist != null) return 2;
            try
            {
                _context.Requests.Add(requestService);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
                return 0;
            }
            return 1;
        }

        public async Task<List<RequestViewModel>> GetAllRequestForPTNE(Guid id)
        {
            var requests = await _context.Requests.Where(a => a.ReceiverId == id && a.IsAccepted == null).ToListAsync();
            List<RequestViewModel> result = new List<RequestViewModel>();
            foreach (var item in requests)
            {
                var account = await _context.Accounts.FindAsync(item.GymerId);
                var pg = await _context.PackageGymers.FindAsync(item.PackageGymerId);

                var temp = new RequestViewModel();
                temp.Id = item.Id;
                temp.ReceiverId = item.ReceiverId;
                temp.GymerName = account.Fullname;
                temp.GymerId = item.GymerId;
                temp.PackageGymerId = item.PackageGymerId;
                temp.PackageGymerName = pg.Name;
                temp.NumberOfSession = pg.NumberOfSession;
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
            Request newRequest = new Request(request.Id, request.GymerId, request.ReceiverId, request.PackageGymerId, request.IsPt, request.IsAccepted, false);

            //Accepct
            if(request.IsAccepted != null)
            {
                var packagegymer = await _context.PackageGymers.FindAsync(request.PackageGymerId);
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
