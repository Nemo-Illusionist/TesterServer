using System;
using AutoMapper;
using NUnit.Framework;
using Tester.Dto.Profiles;
using Tester.Web.Admin.Profiles;

namespace Tester.Tests.Unit
{
    public class MapperTests
    {
        [TestCase(typeof(TesterMappingProfile))]
        [TestCase(typeof(TesterAdminMappingProfile))]
        public void MapperTest(Type profileType)
        {
            var mapperConfiguration = new MapperConfiguration(c => c.AddProfile(profileType));
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}