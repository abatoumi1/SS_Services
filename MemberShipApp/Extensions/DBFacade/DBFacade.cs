using MemberShipApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberShipApp.Extensions.DBFacade
{
    public class DBFacade
    {
        public static RegionDto RegionDto(Region data)
        {
            return new RegionDto
            {
                RegionID = data.RegionID,
                CountryID = data.CountryID,
                Description = data.Description,
                Name = data.Name,
                StateIDs = data.States!=null ? data.States.ToList().Select(s => new TupleData {ID=s.StateID, Name=s.Name }).ToList() : new List<TupleData>()
            };


        }

        public static CountryDto CountryDto(Country data)
        {
            return new CountryDto
            {                
                CountryID = data.CountryID,
                Code = data.Code,
                Name = data.Name,
                RegionIDs = data.Regions != null ? data.Regions.ToList().Select(s => s.RegionID).ToList() : new List<int>()
            };

        }

        public static StateDto StateDto(State data)
        {
            return new StateDto
            {
                StateID = data.StateID,
                RegionID = data.RegionID,
                Code = data.Code,
                Name = data.Name
                
            };

        }

    }
}
