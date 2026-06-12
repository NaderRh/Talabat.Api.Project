
using API.Talabat.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Talabat.Errors;
using Talabat.Core.Repository.Contract;
using API.Talabat.Helper;
using Talabat.Repository.Repository;
using Talabat.Repository.Data.Seed;
using Talabat.Repository.Data;
using Talabat.Core.Entities.Security_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Service.Contract;
using Talabat.Service;
using StackExchange.Redis;
using Newtonsoft.Json;
using Talabat.Core.Repositoriy.Contract;
using Microsoft.OpenApi.Models;
using API.Talabat.Extension;

namespace API.Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Conifration Of Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           builder.Services.AddInfrastructureService(builder.Configuration);

           builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddApplicationServices();

            #endregion


            var app = builder.Build();

            #region PipeLines
            await app.InitializeDatabaseAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfigurePipeline();

            app.Run(); 
            #endregion
        }
    }
}
