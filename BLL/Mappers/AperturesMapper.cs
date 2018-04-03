﻿using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class AperturesMapper : IMapper<ApertureDTO, Aperture>
    {
        public ApertureDTO Map(Aperture item)
        {
            if (item == null)
                return null;

            return new ApertureDTO()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<ApertureDTO> MapRange(IEnumerable<Aperture> items)
        {
            if (items == null)
                return null;

            List<ApertureDTO> apertures = new List<ApertureDTO>();
            foreach (Aperture item in items)
            {
                apertures.Add(new ApertureDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return apertures;
        }
    }
}