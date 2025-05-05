using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Materials;
using Domain.Prosthetics;
using Domain.Request;
using Domain.Reviews;
using Domain.Roles;
using Domain.Statuses;
using Domain.Users;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuild = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<RequestRepository>();
        services.AddScoped<IRequestRepository>(provider => provider.GetRequiredService<RequestRepository>());
        services.AddScoped<IRequestQueries>(provider => provider.GetRequiredService<RequestRepository>());

        services.AddScoped<ProstheticRepository>();
        services.AddScoped<IProstheticRepository>(provider => provider.GetRequiredService<ProstheticRepository>());
        services.AddScoped<IProstheticQueries>(provider => provider.GetRequiredService<ProstheticRepository>());

        services.AddScoped<MaterialRepository>();
        services.AddScoped<IMaterialRepository>(provider => provider.GetRequiredService<MaterialRepository>());
        services.AddScoped<IMaterialQueries>(provider => provider.GetRequiredService<MaterialRepository>());

        services.AddScoped<FunctionalityRepository>();
        services.AddScoped<IFunctionalityRepository>(provider => provider.GetRequiredService<FunctionalityRepository>());
        services.AddScoped<IFunctionalityQueries>(provider => provider.GetRequiredService<FunctionalityRepository>());

        services.AddScoped<TypeRepository>();
        services.AddScoped<ITypeRepository>(provider => provider.GetRequiredService<TypeRepository>());
        services.AddScoped<ITypeQueries>(provider => provider.GetRequiredService<TypeRepository>());

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());

        services.AddScoped<RoleRepository>();
        services.AddScoped<IRoleRepository>(provider => provider.GetRequiredService<RoleRepository>());
        services.AddScoped<IRoleQueries>(provider => provider.GetRequiredService<RoleRepository>());
        
        services.AddScoped<StatusRepository>();
        services.AddScoped<IStatusRepository>(provider => provider.GetRequiredService<StatusRepository>());
        services.AddScoped<IStatusQueries>(provider => provider.GetRequiredService<StatusRepository>());

        services.AddScoped<ReviewRepository>();
        services.AddScoped<IReviewRepository>(provider => provider.GetRequiredService<ReviewRepository>());
        services.AddScoped<IReviewQueries>(provider => provider.GetRequiredService<ReviewRepository>());
    }
}