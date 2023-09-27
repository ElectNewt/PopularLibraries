using Riok.Mapperly.Abstractions;

namespace PopularLibraries.Tests.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserEntity ToEntity(UserDto dto);
    public partial UserDto ToDto(UserEntity entity);
}

[Mapper]
public partial class UserDetailsMapper
{   
    [MapProperty(nameof(@UserDetailsDto.ContactDetails.Email), nameof(@UserEntity.Email))]
    public partial UserEntity ToEntity(UserDetailsDto dto);
}

public class TestMapperly
{
    [Fact]
    public void Test_MapDtoToEntity()
    {
        UserMapper mapper = new UserMapper();
        UserDto dto = new UserDto("username", "mail@mail.com");

        UserEntity entity = mapper.ToEntity(dto);

        Assert.Equal(dto.Email, entity.Email);
        Assert.Equal(dto.UserName, entity.UserName);
    }

    
    [Fact]
    public void Test_MapEntityToDto()
    {
        UserMapper mapper = new UserMapper();
        UserEntity entity = new UserEntity("username", "mail@mail.com");

        UserDto dto = mapper.ToDto(entity);

        Assert.Equal(entity.Email, dto.Email);
        Assert.Equal(entity.UserName, dto.UserName);
    }
    
    [Fact]
    public void Test_MapComplexDtoToEntity()
    {
        UserDetailsMapper mapper = new UserDetailsMapper();
        UserDetailsDto dto = new UserDetailsDto()
        {
            ContactDetails = new ContactDetails()
            {
                Email = "email@mail.com",
                PhoneNumber = "1256555"
            },
            UserName = "username"
        };

        UserEntity entity = mapper.ToEntity(dto);

        Assert.Equal(dto.ContactDetails.Email, entity.Email);
        Assert.Equal(dto.UserName, entity.UserName);
    }
}
