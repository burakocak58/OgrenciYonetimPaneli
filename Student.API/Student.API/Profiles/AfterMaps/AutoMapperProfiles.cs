using AutoMapper;
using Student.API.DomainModels;

namespace Student.API.Profiles.AfterMaps
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<DataModels.Student, DomainModels.Student>().ReverseMap();
			CreateMap<DataModels.Gender, Gender>().ReverseMap();
			CreateMap<DataModels.Address, Address>().ReverseMap();
			CreateMap<UpdateStudentRequest, DataModels.Student>()
				.AfterMap<UpdateStudentRequestAfterMap>();
			CreateMap<AddStudentRequest, DataModels.Student>()
				.AfterMap<AddStudentRequestAfterMap>();
		}
	}
}
