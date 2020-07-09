using System;
using AutoMapper;
using NUnit.Framework;
using Tester.Infrastructure.Profiles;

namespace Tester.Tests.Unit
{
    public class MapperTests
    {
        [TestCase(typeof(TesterDtoMappingProfile))]
        public void MapperTest(Type profileType)
        {
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(profileType));
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}