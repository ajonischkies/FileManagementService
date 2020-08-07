using AutoMapper;
using RedSky.FileManagement.Contracts.DataTransferObjects;
using RedSky.FileManagement.Contracts.Entities;

namespace RedSky.FileManagement.Api.Profiles
{
    public class FileDataProfile : Profile
    {
        public FileDataProfile()
        {
            CreateMap<FileData, FileSummaryDto>()
                .ForMember(
                    dest => dest.downloadLink,
                    options => options.MapFrom(source => "/files/download/" + source.Id.ToString())
                ).ReverseMap();
        }
    }
}
