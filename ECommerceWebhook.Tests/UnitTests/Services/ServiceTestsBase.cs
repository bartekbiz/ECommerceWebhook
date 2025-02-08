using AutoMapper;
using ECommerceWebhook.Application.MappingProfiles;
using Xunit.Abstractions;

namespace ECommerceWebhook.Tests.UnitTests.Services;

public abstract class ServiceTestsBase
{
    protected readonly ITestOutputHelper Output;
    protected readonly IMapper Mapper;
    
    protected ServiceTestsBase(ITestOutputHelper output)
    {
        Output = output;
        Mapper = CreateMapper();
    }
    
    private IMapper CreateMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile(new MappingProfile());
        });

        return new Mapper(mapperConfig);
    }
}