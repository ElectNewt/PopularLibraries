using Riok.Mapperly.Abstractions;

namespace PopularLibraries.Mappers;

[Mapper]
public partial class AccountInformationMapper
{   
    public partial AccountInformationDetailsEntity ToEntityMapperly(AccountInformationDto dto);
    
    [MapProperty(nameof(@UserDetailsDto.ContactDetails.Email), nameof(@UserDetailsEntity.Email))]
    public partial UserDetailsEntity ToEntity(UserDetailsDto dto);
}

public class MapperlyExample
{
    private readonly AccountInformationMapper _mapper = new();
    
    public AccountInformationDetailsEntity Execute(AccountInformationDto accountInfoDto)
        => _mapper.ToEntityMapperly(accountInfoDto);
}