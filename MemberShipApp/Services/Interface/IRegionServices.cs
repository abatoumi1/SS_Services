using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Services
{
    public interface IRegionServices
    {
       
        Task<IEnumerable<RegionDto>> GetAllWithCountrID(int country);
        Task<IEnumerable<RegionDto>> GetAllRegions();
        Task<RegionDto> GetRegionById(int id);
        Task<RegionDto> GetRegionWithStatesById(int id);
        Task<ReturnResponse> CreateRegion(RegionDto newRegion);
        Task<ReturnResponse> UpdateRegion(int id, RegionDto region);
        Task<ReturnResponse> DeleteRegion(int id);
        Task<IEnumerable<RegionDto>> GetWithStatesAsync();
    }
}
