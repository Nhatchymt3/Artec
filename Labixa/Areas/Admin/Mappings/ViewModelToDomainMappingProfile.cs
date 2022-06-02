using System.Security.Claims;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Areas.Admin.ViewModel.WebsiteAtribute;
using Outsourcing.Data.Models;

namespace SocialGoal.Mappings
{

    public class ViewModelToDomainMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {

            //Mapper.CreateMap<UserFormModel, User>();
            //Mapper.CreateMap<UserFormViewModel, User>().ForMember(x => x.Id, opt => opt.MapFrom(source => source.UserId));
            //Mapper.CreateMap<XViewModel, X()
            //    .ForMember(x => x.PropertyXYZ, opt => opt.MapFrom(source => source.Property1));     
            Mapper.CreateMap<BlogFormModel, Blog>();
            Mapper.CreateMap<ProductFormModel, Product>();
            Mapper.CreateMap<ProductAttributeFormModel, ProductAttribute>();
            Mapper.CreateMap<OrderFormModel, Order>();
            Mapper.CreateMap<WebsiteAttributeFormModel, WebsiteAttribute>();
            Mapper.CreateMap<ProfileFormModel, Outsourcing.Data.Models.Profile>();
            Mapper.CreateMap<PetProfileFormModel, PetProfile>();
            Mapper.CreateMap<CommentFormModel, Comment>();
            Mapper.CreateMap<FileFormModel, File>();
            Mapper.CreateMap<DepartmentFormModel, Department>();
            Mapper.CreateMap<LocationFormModel, Location>();
            Mapper.CreateMap<RecruitFormModel, Recruit>();

            //LongT
            Mapper.CreateMap<BlogCategoryFormModel, BlogCategory>();
            Mapper.CreateMap<StaffFormModel, Staff>();
            Mapper.CreateMap<ProductCategoryFormModel, ProductCategory>();
            Mapper.CreateMap<AlbumPhotoFormModel, Product>();
        }
    }
}