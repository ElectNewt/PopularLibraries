using AutoMapper;

namespace PopularLibraries.Mappers;

public class AutomapperExample
{
    private readonly IMapper _mapper;

    public AutomapperExample()
    {
        MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<UserDetailsDto, UserDetailsEntity>()
                .ForMember(destination => destination.Email,
                    options => options
                        .MapFrom(source => source.ContactDetails.Email));

            cfg.CreateMap<AccountInformationDto, AccountInformationDetailsEntity>()
                .ForMember(destination => destination.Users,
                    options => options.MapFrom(a => a.Users));
        });

        _mapper = new Mapper(config);
    }
    
    public AccountInformationDetailsEntity Execute(AccountInformationDto accountInfoDto)
        => _mapper.Map<AccountInformationDetailsEntity>(accountInfoDto);
}