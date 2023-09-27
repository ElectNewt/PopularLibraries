namespace PopularLibraries.Tests.Mappers;

public record UserDto(string UserName, string Email);

public record UserEntity(string UserName, string Email);

public class UserDetailsDto
{
    public string UserName { get; set; }
    public ContactDetails ContactDetails { get; set; }
}

public class ContactDetails
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class UserDetailsEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
}