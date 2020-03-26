using System;
using AutoMapper;
using NUnit.Framework;
using Tester.Infrastructure.Profiles;
using Tester.Web.Admin.Profiles;

namespace Tester.Tests.Unit
{
    public class MapperTests
    {
        [TestCase(typeof(TesterDtoMappingProfile))]
        [TestCase(typeof(TesterAdminMappingProfile))]
        public void MapperTest(Type profileType)
        {
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(profileType));
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}