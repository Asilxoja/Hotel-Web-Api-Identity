#region Usings
using Application.Common.AutoMapperProfiles;
using Application.Common.Constants;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
#endregion

namespace Web;

public static class Startup
{
    private const string CORS_POLICY = "CorsPolicy";

    public static void AddDependencyInjection(this WebApplicationBuilder builder)
    {
        #region Default services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });
        #endregion

        #region DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        #endregion

        #region Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password requirements settings
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
        #endregion

        #region CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(CORS_POLICY,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
        });
        #endregion

        #region Application services

        builder.Services.AddScoped<IIdentityService, IdentityService>(); 
        builder.Services.AddTransient<IStaffInterface, StaffRepository>();
        builder.Services.AddTransient<IGuestInterface, GuestRepository>();
        builder.Services.AddTransient<IOrderInterface, OrderRepository>();
        builder.Services.AddTransient<IOrderStatusInterface, OrderStatusRepository>();
        builder.Services.AddTransient<IRoomInterface, RoomRepository>();
        builder.Services.AddTransient<IRoomStatusInterface, RoomStatusRepository>();
        builder.Services.AddTransient<IRoomTypeInterface, RoomTypeRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        // Add Service
        builder.Services.AddTransient<IStaffService, StaffService>();
        builder.Services.AddTransient<IGuestService, GuestService>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IOrderStatusService, OrderStatusServices>();
        builder.Services.AddTransient<IRoomService, RoomService>();
        builder.Services.AddTransient<IRoomStatusService, RoomStatusServices>();
        builder.Services.AddTransient<IRoomTypeService, RoomTypeService>();


        #endregion

        #region Mapper Profile
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AutoMapperProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);
        #endregion
    }

    public static void AddMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(CORS_POLICY);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        SeedDatabase(app);
    }

    private static void SeedDatabase(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        context.Database.Migrate();

        SeedDataAsync(context, userManager, roleManager).Wait();
    }

    private static async Task SeedDataAsync(ApplicationDbContext context, 
                                            UserManager<ApplicationUser> userManager, 
                                            RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<IdentityRole>
        {
            new IdentityRole { Name = IdentityRoles.SUPER_ADMIN },
            new IdentityRole { Name = IdentityRoles.ADMIN },
            new IdentityRole { Name = IdentityRoles.USER }
        };

        var roleExists = roleManager.Roles.Any();
        if (!roleExists)
        {
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        var superAdmin = new ApplicationUser
        {
            FirstName = "Super",
            LastName = "Admin",
            Email = "superadmin@localhost",
            UserName = "superadmin@localhost"
        };

        var result = await userManager.CreateAsync(superAdmin, "Admin.123$");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(superAdmin, IdentityRoles.SUPER_ADMIN);
        }
    }
}