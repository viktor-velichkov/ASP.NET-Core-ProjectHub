using AutoMapper;
using ProjectHub.AutoMapper;

namespace ProjectHub.Tests.Mocks
{
    public static class AutoMapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });

                return mapperConfig.CreateMapper();
            }
        }
    }
}
