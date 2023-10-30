namespace PopularLibraries.Mappers;


public static class MapperHelper
{
    
    public static AccountInformationDto BuildDefaultDto()
    {
        List<UserDetailsDto> users = new List<UserDetailsDto>();
        for (int i = 0; i < 100000; i++)
        {
            UserDetailsDto contactDetails = new UserDetailsDto()
            {
                ContactDetails = new ContactDetails()
                {
                    Email = $"email+{i}@mail.com",
                    PhoneNumber = $"022222{i}"
                },
                UserName = $"User-{i}",
            };
            users.Add(contactDetails);
        }

        return new AccountInformationDto()
        {
            Users = users,
            AccountId = 1
        };
    }
}


public class AccountInformationDto
{
    public int AccountId { get; set; }
    public List<UserDetailsDto> Users { get; set; }
}

public class UserDetailsDto
{
    public string UserName { get; set; }
    public ContactDetails ContactDetails { get; set; }
}

public class UserDetailsEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class AccountInformationDetailsEntity
{
    public int AccountId { get; set; }
    public List<UserDetailsEntity> Users { get; set; }
}

public class ContactDetails
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

