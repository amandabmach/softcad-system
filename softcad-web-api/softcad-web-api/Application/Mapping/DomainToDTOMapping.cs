using AutoMapper;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Administrator, AdminDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<AdminDTO, Administrator>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => ConvertIFormFileToString(src.Photo)));

        }
        private string ConvertIFormFileToString(IFormFile file)
        {
            if (file != null)
            {
                var filePath = Path.Combine("Storage", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return filePath;
            }

            return null;
        }
    }
}
