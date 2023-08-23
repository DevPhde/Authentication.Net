using Authentication.Net.Application.DTOs;
using Authentication.Net.Domain.Entities;
using AutoMapper;

namespace Authentication.Net.Application.Mappings
{
	public class DomainToDTOMapping : Profile
	{
		public DomainToDTOMapping()
		{
			CreateMap<User, UserRegisterDTO>().ReverseMap();
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<User, UserLoginDTO>().ReverseMap();
		}
	}
}
