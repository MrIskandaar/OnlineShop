using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Root.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// migrates databases
        /// </summary>
        /// <param name="app"></param>
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            try
            {
                using var appScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();
                using var context = appScope.ServiceProvider.GetService<ShopContext>();
                context?.Database?.Migrate();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Exception in UpdateDatabase method: " + ex.Message);
            }
        }

        /// <summary>
        /// sets CORS to to web application
        /// </summary>
        /// <param name="app"></param>
        public static void SetCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
               builder.AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
        }
    }
}
