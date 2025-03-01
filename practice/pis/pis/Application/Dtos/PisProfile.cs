using AutoMapper;
using pis.Domain;

namespace pis.Application.Dtos;

public class PisProfile : Profile
{
    public PisProfile()
    {
        CreateMap<Drug, DrugDto>();
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Client,ClientRequestDto>().ReverseMap();
        CreateMap<MedicationRequest, MedicationRequestDto>().ReverseMap();
        CreateMap<MedicationRequest, MedicationDto>().ReverseMap();
    }
}