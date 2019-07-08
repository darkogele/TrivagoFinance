using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
