using Talabat.Repository.Data.Seed;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;
using API.Talabat.Middleware;

namespace API.Talabat.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            #region Update Datanase And Migrations And Seeding
            using var Scop = app.Services.CreateAsyncScope();
            var Service = Scop.ServiceProvider;
            var DbContext = Service.GetRequiredService<StoreDbContext>();
            var loggerFactor = Service.GetRequiredService<ILoggerFactory>();
            try
            {
                await DbContext.Database.MigrateAsync();
                await StoreDbContextSeed.Seed(DbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactor.CreateLogger<Program>();
                logger.LogError(ex, "Errors Occurred during Migrations ");

            }
            #endregion
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            #region   Configure the HTTP request pipeline

            app.UseMiddleware<ExceptionMiddleware>();
           

            app.UseStatusCodePagesWithReExecute("/errors/{0}");//ال 0 تمثل الكود اللي هيرجع Handle EndPoints Errors(in ErrorsController) دي علشان ال  
            app.UseStaticFiles();// علشان اعرف استقبل البيانات من الجيسون فايل
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
            #endregion
        }

        
    }
}
