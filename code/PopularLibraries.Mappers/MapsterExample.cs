using Mapster;

namespace PopularLibraries.Mappers;

public class MapsterExample
{
    public MapsterExample()
    {
        TypeAdapterConfig<UserDetailsDto, UserDetailsEntity>
            .NewConfig()
            .Map(dest => dest.Email, src => src.ContactDetails.Email);
    }

    public AccountInformationDetailsEntity Execute(AccountInformationDto accountInfoDto)
        => accountInfoDto.Adapt<AccountInformationDetailsEntity>();
}