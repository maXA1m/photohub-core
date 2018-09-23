using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;
using System.Collections.Generic;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// Contains methods with tags processing logic.
    /// Realization of ITagsService.
    /// </summary>
    public class TagsService : ITagsService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsService"/>.
        /// </summary>
        public TagsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Loads all tags and returns collection of tag DTOs.
        /// </summary>
        public IEnumerable<TagDTO> GetAll()
        {
            return TagsMapper.MapRange(_unitOfWork.Tags.GetAll());
        }

        /// <summary>
        /// Loads tag by name and returns tag DTO.
        /// </summary>
        public TagDTO Get(string name)
        {
            Tag tag = _unitOfWork.Tags.Find(t => t.Name == name).FirstOrDefault();

            if (tag != null)
            {
                return TagsMapper.Map(tag);
            }

            return null;
        }

        #endregion

        #region Disposing

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}