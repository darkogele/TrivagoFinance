using AutoMapper;
using TrivagoFinance.Ui.Data.DomainModels;
using TrivagoFinance.Ui.ViewModels;

namespace TrivagoFinance.Ui.Controllers.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserVIewModel>();
            CreateMap<UserVIewModel, User>();
        }
    }
}