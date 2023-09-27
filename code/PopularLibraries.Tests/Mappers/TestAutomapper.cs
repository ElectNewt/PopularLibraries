using AutoMapper;

namespace PopularLibraries.Tests.Mappers;

public class TestAutomapper
{
    [Fact]
    public void Test_MapDtoToEntity()
    {
        MapperConfiguration config = new(cfg =>
            cfg.CreateMap<UserDto, UserEntity>());

        IMapper mapper = new Mapper(config);

        UserDto dto = new UserDto("username", "mail@mail.com");

        UserEntity entity = mapper.Map<UserEntity>(dto);

        Assert.Equal(dto.Email, entity.Email);
        Assert.Equal(dto.UserName, entity.UserName);
    }

    [Fact]
    public void Test_MapDtoToEntity_Reverse()
    {
        MapperConfiguration config = new(cfg =>
            cfg.CreateMap<UserDto, UserEntity>().ReverseMap());

        IMapper mapper = new Mapper(config);

        UserEntity entity = new UserEntity("username", "mail@mail.com");

        UserDto dto = mapper.Map<UserDto>(entity);

        Assert.Equal(entity.Email, dto.Email);
        Assert.Equal(entity.UserName, dto.UserName);
    }

    [Fact]
    public void Test_MapComplexObject()
    {
        MapperConfiguration config = new(cfg =>
            cfg.CreateMap<UserDetailsDto, UserDetailsEntity>()
                .ForMember(destination => destination.Email,
                    options => options
                            .MapFrom(source => source.ContactDetails.Email)));

        IMapper mapper = new Mapper(config);

        UserDetailsDto dto = new UserDetailsDto()
        {
            ContactDetails = new ContactDetails()
            {
                Email = "email@mail.com",
                PhoneNumber = "1256555"
            },
            UserName = "username"
        };

        UserDetailsEntity entity = mapper.Map<UserDetailsEntity>(dto);

        Assert.Equal(dto.ContactDetails.Email, entity.Email);
        Assert.Equal(dto.UserName, entity.UserName);
    }
}