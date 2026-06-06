using Titan.AniTec.Platform.Financial.Application.CommandServices;
using Titan.AniTec.Platform.Financial.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Financial.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Financial.Domain.Repositories;
using Titan.AniTec.Platform.Financial.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Health.Application.CommandServices;
using Titan.AniTec.Platform.Health.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Health.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Health.Domain.Repositories;
using Titan.AniTec.Platform.Health.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Inventory.Application.CommandServices;
using Titan.AniTec.Platform.Inventory.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Inventory.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Inventory.Domain.Repositories;
using Titan.AniTec.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Iam.Application.Acl;
using Titan.AniTec.Platform.Livestock.Application.CommandServices;
using Titan.AniTec.Platform.Livestock.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Livestock.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Livestock.Domain.Repositories;
using Titan.AniTec.Platform.Livestock.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Iam.Application.CommandServices;
using Titan.AniTec.Platform.Iam.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Iam.Application.Internal.OutboundServices;
using Titan.AniTec.Platform.Iam.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Iam.Application.QueryServices;
using Titan.AniTec.Platform.Iam.Domain.Repositories;
using Titan.AniTec.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Titan.AniTec.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using Titan.AniTec.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Titan.AniTec.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Titan.AniTec.Platform.Iam.Interfaces.Acl;
using Titan.AniTec.Platform.Iam.Resources;
using Titan.AniTec.Platform.Profile.Application.CommandServices;
using Titan.AniTec.Platform.Profile.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Profile.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Profile.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Profile.Resources;
using Titan.AniTec.Platform.Subscription.Application.CommandServices;
using Titan.AniTec.Platform.Subscription.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Subscription.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Subscription.Domain.Repositories;
using Titan.AniTec.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Device.Application.CommandServices;
using Titan.AniTec.Platform.Device.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Device.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Device.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Telemetry.Application.CommandServices;
using Titan.AniTec.Platform.Telemetry.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Telemetry.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;
using Titan.AniTec.Platform.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Appointment.Application.CommandServices;
using Titan.AniTec.Platform.Appointment.Application.Internal.CommandServices;
using Titan.AniTec.Platform.Appointment.Application.Internal.QueryServices;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Appointment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Resources.Errors;
using Titan.AniTec.Platform.Resources.Shared;
using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProblemDetailsFactory = Titan.AniTec.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(
            new Titan.AniTec.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration
                .KebabCaseRouteNamingConvention());
    })
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization();

builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ProfileMessages>, StringLocalizer<ProfileMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new Microsoft.OpenApi.OpenApiInfo
        {
            Title = "Titan.AniTec.Platform",
            Version = "v1",
            Description = "AniTec Platform API",
            TermsOfService = new Uri("https://anitec.com/tos"),
            Contact = new Microsoft.OpenApi.OpenApiContact
            {
                Name = "AniTec Studios",
                Email = "contact@anitec.com"
            },
            License = new Microsoft.OpenApi.OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document =>
        new Microsoft.OpenApi.OpenApiSecurityRequirement
        {
            [new Microsoft.OpenApi.OpenApiSecuritySchemeReference("bearer", null, null)] = []
        });
    options.EnableAnnotations();
});

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Profile Bounded Context
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
builder.Services.AddScoped<IUserPreferencesRepository, UserPreferencesRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// Livestock Bounded Context
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IBreedRepository, BreedRepository>();
builder.Services.AddScoped<IBirthRepository, BirthRepository>();
builder.Services.AddScoped<IMatingRepository, MatingRepository>();
builder.Services.AddScoped<IWeaningRepository, WeaningRepository>();
builder.Services.AddScoped<ILivestockCommandService, LivestockCommandService>();
builder.Services.AddScoped<ILivestockQueryService, LivestockQueryService>();

// Health Bounded Context
builder.Services.AddScoped<IVaccinationRepository, VaccinationRepository>();
builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();
builder.Services.AddScoped<IVeterinaryVisitRepository, VeterinaryVisitRepository>();
builder.Services.AddScoped<IHealthAlertRepository, HealthAlertRepository>();
builder.Services.AddScoped<IHealthCommandService, HealthCommandService>();
builder.Services.AddScoped<IHealthQueryService, HealthQueryService>();

// Inventory Bounded Context
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IInventoryCommandService, InventoryCommandService>();
builder.Services.AddScoped<IInventoryQueryService, InventoryQueryService>();

// Financial Bounded Context
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
builder.Services.AddScoped<IFinancialCommandService, FinancialCommandService>();
builder.Services.AddScoped<IFinancialQueryService, FinancialQueryService>();

// Subscription Bounded Context
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

// Device Bounded Context
builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceAssignmentRepository, DeviceAssignmentRepository>();
builder.Services.AddScoped<IDeviceAlertRepository, DeviceAlertRepository>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();

// Telemetry Bounded Context
builder.Services.AddScoped<ITelemetryReadingRepository, TelemetryReadingRepository>();
builder.Services.AddScoped<ITelemetryThresholdRepository, TelemetryThresholdRepository>();
builder.Services.AddScoped<ITelemetryAlertRepository, TelemetryAlertRepository>();
builder.Services.AddScoped<ITelemetryCommandService, TelemetryCommandService>();
builder.Services.AddScoped<ITelemetryQueryService, TelemetryQueryService>();

// Appointment Bounded Context
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentReminderRepository, AppointmentReminderRepository>();
builder.Services.AddScoped<IAppointmentFollowUpRepository, AppointmentFollowUpRepository>();
builder.Services.AddScoped<IAppointmentNoteRepository, AppointmentNoteRepository>();
builder.Services.AddScoped<IVeterinarianAvailabilityRepository, VeterinarianAvailabilityRepository>();
builder.Services.AddScoped<IAvailabilityBlockRepository, AvailabilityBlockRepository>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseRequestAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
