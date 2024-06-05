using AutoMapper;
using EmployeeManagement.Core.Entity;
using EmployeeManagement.Models;

namespace EmployeeManagement.Profiles
{
	public class CustomProfiles : Profile
	{
		public CustomProfiles()
		{
			CreateMap<LoginViewModel, Users>().ReverseMap();
			CreateMap<EmployeeViewModel, Employee>().ReverseMap();
/*            CreateMap<Employee, EmployeeViewModel>()
           .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds((long)src.DateOfJoining).UtcDateTime));

            CreateMap<Employee, EmployeeViewModel>()
            .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds((long)src.DateOfJoining).UtcDateTime));

            CreateMap<EmployeeViewModel, Employee>()
                .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => ((DateTimeOffset)src.DateOfJoining).ToUnixTimeSeconds()));*/
        }
	}
}
