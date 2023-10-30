namespace PopularLibraries.Mappers;

public class ManualMapperExample
{
    public AccountInformationDetailsEntity Execute(AccountInformationDto accountInfoDto)
        => accountInfoDto.ToEntity();
}

public static class UserManualMapper
{
    public static AccountInformationDetailsEntity ToEntity(this AccountInformationDto dto)
    {
        return new AccountInformationDetailsEntity()
        {
            AccountId = dto.AccountId,
            Users = dto.Users.Select(ToEntity).ToList()
        };
    }

    public static UserDetailsEntity ToEntity(this UserDetailsDto dto)
        => new UserDetailsEntity()
        {
            UserName = dto.UserName,
            Email = dto.ContactDetails.Email
        };
}