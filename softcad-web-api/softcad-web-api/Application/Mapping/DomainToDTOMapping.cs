using AutoMapper;
using WebApiOperacaoCuriosidade.Domain.DTOs;
using WebApiOperacaoCuriosidade.Domain.Models;

namespace WebApiOperacaoCuriosidade.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Administrador, AdminDTO>()
                .ForMember(dest => dest.Foto, opt => opt.Ignore());

            CreateMap<AdminDTO, Administrador>()
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => ConvertIFormFileToString(src.Foto)));

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<UsuarioDTO, Usuario>();

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
