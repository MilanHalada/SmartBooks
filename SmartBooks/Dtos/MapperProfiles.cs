using AutoMapper;
using SmartBooks.Domain.Entities;

namespace SmartBooks.Dtos;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<Subject, SubjectDto>();
        CreateMap<Lecture, LectureDto>();
    }
}