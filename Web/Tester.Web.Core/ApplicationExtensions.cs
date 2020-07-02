using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Tester.Web.Core
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                foreach (var versionDescription in provider.ApiVersionDescriptions)
                {
                    o.SwaggerEndpoint($"/swagger/{(object) versionDescription.GroupName}/swagger.json",
                        versionDescription.GroupName.ToUpperInvariant());
                }

                o.EnableDeepLinking();
            });

            return app;
        }
    }
}