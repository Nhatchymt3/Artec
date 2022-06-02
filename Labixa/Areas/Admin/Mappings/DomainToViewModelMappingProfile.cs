using AutoMapper;
using Labixa.Areas.Admin.ViewModel;
using Labixa.Areas.Admin.ViewModel.WebsiteAtribute;
using Outsourcing.Data.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialGoal.Mappings
{
    public class DomainToViewModelMappingProfile : AutoMapper.Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {

            //Mapper.CreateMap<UserProfile, UserProfileFormModel>();

            //Mapper.CreateMap<X, XViewModel>()
            //    .ForMember(x => x.Property1, opt => opt.MapFrom(source => source.PropertyXYZ));
            //Mapper.CreateMap<Goal, GoalListViewModel>().ForMember(x => x.SupportsCount, opt => opt.MapFrom(source => source.Supports.Count))
            //                                          .ForMember(x => x.UserName, opt => opt.MapFrom(source => source.User.UserName))
            //                                          .ForMember(x => x.StartDate, opt => opt.MapFrom(source => source.StartDate.ToString("dd MMM yyyy")))
            //                                          .ForMember(x => x.EndDate, opt => opt.MapFrom(source => source.EndDate.ToString("dd MMM yyyy")));
            //Mapper.CreateMap<Group, GroupsItemViewModel>().ForMember(x => x.CreatedDate, opt => opt.MapFrom(source => source.CreatedDate.ToString("dd MMM yyyy")));

            //Mapper.CreateMap<IPagedList<Group>, IPagedList<GroupsItemViewModel>>().ConvertUsing<PagedListConverter<Group, GroupsItemViewModel>>();

            Mapper.CreateMap<Blog, BlogFormModel>();
            Mapper.CreateMap<Product, ProductFormModel>();
            Mapper.CreateMap<ProductAttribute, ProductAttributeFormModel>();
            Mapper.CreateMap<Order, OrderFormModel>();
            Mapper.CreateMap<WebsiteAttribute, WebsiteAttributeFormModel>();
            Mapper.CreateMap<Outsourcing.Data.Models.Profile, ProfileFormModel>();
            Mapper.CreateMap<PetProfile, PetProfileFormModel>();
            Mapper.CreateMap<Comment, CommentFormModel>();
            Mapper.CreateMap<File, FileFormModel>();
            Mapper.CreateMap<Department, DepartmentFormModel>();
            Mapper.CreateMap <Location, LocationFormModel>();
            Mapper.CreateMap <Recruit, RecruitFormModel>();

            //LongT
            Mapper.CreateMap<BlogCategory, BlogCategoryFormModel>();
            Mapper.CreateMap<Staff, StaffFormModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryFormModel>();
            Mapper.CreateMap<Product, AlbumPhotoFormModel>();
        }
    }
}