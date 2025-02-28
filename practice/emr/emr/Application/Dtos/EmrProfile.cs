using AutoMapper;
using emr.Domain;

namespace emr.Application.Dtos;

public class EmrProfile : Profile
{
    public EmrProfile()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<Prescription, PrescriptionDto>();
        CreateMap<DrugDispense, DrugDispenseDto>();
    }
}