using Infrastructure.Services;
using Infrastructure.Services.Administrator;
using Infrastructure.Services.Category;
using Infrastructure.Services.Class;
using Infrastructure.Services.Client;
using Infrastructure.Services.Equipment;
using Infrastructure.Services.Membership;
using Infrastructure.Services.Room;
using Infrastructure.Services.Service;
using Infrastructure.Services.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"))
        );
        services.AddScoped<IAdministratorService, AdministratorService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IClassService, ClassService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IServiceService, ServiceService>();
    }
}