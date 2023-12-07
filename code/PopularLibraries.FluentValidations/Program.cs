using System.ComponentModel.DataAnnotations;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<User>, UserValidator>();


var app = builder.Build();

app.MapPost("/user", async (User user, IValidator<User> validator) =>
    {
        var validationResult = await validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return string.Join(',', validationResult.Errors.Select(a => a.ErrorMessage));
        }

        return $"User created (simulated) {user.Name}";
    })
    .WithOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();


public record User(string Name, string Email, int Age, int? AgeOfRetirement);

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(a => a.Age).GreaterThanOrEqualTo(18);
        RuleFor(a => a.Name).NotEmpty();
        RuleFor(a => a.Email).EmailAddress()
            .Must(e => e.EndsWith("@gmail.com"))
            .WithMessage("Debes utilizar un correo de gmail.");

        When(a => a.Age >= 65, () =>
                RuleFor(user => user.AgeOfRetirement).NotEmpty().GreaterThanOrEqualTo(65)
            )
            .Otherwise(() => RuleFor(user => user.AgeOfRetirement).Null());
    }
}