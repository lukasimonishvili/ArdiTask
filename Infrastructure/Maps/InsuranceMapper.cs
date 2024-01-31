using Domain.DTO;
using Domain.Entities;
using Mapster;

namespace Infrastructure.Maps
{
    public class InsuranceMapper
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<InsuranceDTO, Insurance>
                .NewConfig();

            TypeAdapterConfig<Insurance, InsuranceGetDTO>
                .NewConfig();

            TypeAdapterConfig<InsuranceUpdateDTO, Insurance>
                .NewConfig();
        }
    }
}
