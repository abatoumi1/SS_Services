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

        public static CountryTuplate CountryTuplate(Country data)
        {
            return new CountryTuplate
            {
                CountryID = data.CountryID,
                Name = data.Name,
                Regions = data.Regions != null ? data.Regions.Select(s=>RegionTuplate(s)).ToList() : new List<RegionTuplate>()
            };

        }
        public static RegionTuplate RegionTuplate(Region data)
        {
            return new RegionTuplate
            {
               
                RegionID = data.RegionID,               
                Name = data.Name,
                States = data.States != null ? data.States.ToList().Select(s => new TupleData { ID = s.StateID, Name = s.Name }).ToList() : new List<TupleData>()
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
