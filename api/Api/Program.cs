using Data;
using Domain.Models;
using Domain.Requests;
using Domain.Validators;
using FluentValidation;
using Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCfgDatabase(builder.Configuration);
builder.Services.AddEventDispatchers(builder.Configuration);
builder.Services.AddBlobStorage(builder.Configuration);
builder.Services.AddCfgData();


//Validators
builder.Services.AddScoped<IValidator<ArchiveIdRequest>, ArchiveIdRequestValidator>();
builder.Services.AddScoped<IValidator<List<ArchiveUploadRequest>>, ArchiveUploadRequestValidator>();
builder.Services.AddScoped<IValidator<DepartmentPayment>, DepartmentPaymentValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MigrationInitialisation();

app.MapControllers();

app.Run();
