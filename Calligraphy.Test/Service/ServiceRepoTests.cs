using Calligraphy.Data.Config;
using Calligraphy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace Calligraphy.Test.Service
{
    public class ServiceRepoTests // : IDisposable
    {
        protected DbContextOptions<CalligraphyContext> ContextOptions { get; }

        public ServiceRepoTests(DbContextOptions<CalligraphyContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        //[Fact]
        //[TestBeforeAfter]
        private void Seed()
        {
            using (var context = new CalligraphyContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var service1 = new ServiceEntity { ServiceId = 1, TypeName = Data.Enums.ServiceType.Calligraphy, StartingRate = 20.00f };

                context.AddRange(service1);

                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAllServiceOk()
        {
            // Act
            
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    //public class TestBeforeAfter : BeforeAfterTestAttribute
    //{
    //    readonly Lazy<CultureInfo> culture;
    //    readonly Lazy<CultureInfo> uiCulture;

    //    public CultureInfo Culture { get { return culture.Value; } }
    //    public CultureInfo UICulture { get { return uiCulture.Value; } }

    //    CultureInfo originalCulture;
    //    CultureInfo originalUiCulture;

    //    public TestBeforeAfter(string culture, string uiCulture)
    //    {
    //        this.culture = new Lazy<CultureInfo>(() => new CultureInfo(culture, false));
    //        this.uiCulture = new Lazy<CultureInfo>(() => new CultureInfo(uiCulture, false));
    //    }

    //    public TestBeforeAfter(string culture) : this(culture, culture) { }

    //    public override void Before(MethodInfo methodUnderTest)
    //    {
    //        originalCulture = Thread.CurrentThread.CurrentCulture;
    //        originalUiCulture = Thread.CurrentThread.CurrentUICulture;

    //        Thread.CurrentThread.CurrentCulture = Culture;
    //        Thread.CurrentThread.CurrentUICulture = UICulture;

    //        CultureInfo.CurrentCulture.ClearCachedData();
    //        CultureInfo.CurrentUICulture.ClearCachedData();
    //    }

    //    public override void After(MethodInfo methodUnderTest)
    //    {
    //        Thread.CurrentThread.CurrentCulture = originalCulture;
    //        Thread.CurrentThread.CurrentUICulture = originalUiCulture;

    //        CultureInfo.CurrentCulture.ClearCachedData();
    //        CultureInfo.CurrentUICulture.ClearCachedData();
    //    }
    //}
}
