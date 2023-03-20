using System.Text.Json.Serialization;
using DigitalBank.API.Extensions;
using DigitalBank.Data.Context;
using DigitalBank.GraphQL;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using GQLDI = GraphQL.MicrosoftDI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Handlers
builder.Services.AddCustomHandlers();

// Add Repositories
builder.Services.AddCustomRepositories();

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connecting to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Transient);

// Inject DB Contexts according language
builder.Services.AddScoped<DbContext, DataContext>();

builder.Services.AddSingleton<ISchema, GraphQLMainSchema>(services =>
    new GraphQLMainSchema(new GQLDI.SelfActivatingServiceProvider(services)));

GQLDI.GraphQLBuilderExtensions.AddGraphQL(builder.Services)
    .AddServer(true)
    .ConfigureExecution(options =>
        options.EnableMetrics = true)
    .AddSystemTextJson()
    .AddDataLoader()
    .AddGraphTypes(typeof(GraphQLMainSchema).Assembly);

//GQLDI.GraphQLBuilderExtensions.AddGraphQL(builder.Services)
//    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true);

builder.Services.AddGraphQLUpload();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseGraphQLGraphiQL();
app.UseGraphQLPlayground();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphQLUpload<ISchema>().UseGraphQL<ISchema>();

app.Run();
