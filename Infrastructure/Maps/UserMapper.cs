using Domain.DTO;
using Domain.Entities;
using Mapster;

namespace Infrastructure.Maps
{
    public class UserMapper
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<UserDTO, User>
                .NewConfig();

            TypeAdapterConfig<UserUpdaetDTO, User>
                .NewConfig();
        }
    }
}
