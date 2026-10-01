var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( policity =>
    {
        policity
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () =>
{
    return "API Sistema de Gestión funcionando";
});

app.MapGet("/api/canchas", () =>
{
    return Results.Ok(new[]
    {
        new
        {
            ID= 1,
            TipoCancha= "Fútbol 7 (Sintética)",
            Fecha= "2026-10-05",
            Hora= "18:00 - 19:00",
            Disponibilidad= "Disponible",
            Precio= 80.00,
            Duracion= "1 hora",
            Servicios= "Iluminación, Vestuarios, Estacionamiento"
        },
        new
        {
            ID= 2,
            TipoCancha= "Fútbol 7 (Sintética)",
            Fecha= "2026-10-05",
            Hora= "19:00 - 20:00",
            Disponibilidad= "Ocupado",
            Precio= 90.00,
            Duracion= "1 hora",
            Servicios= "Iluminación, Vestuarios, Estacionamiento"
        },
        new
        {
            ID= 3,
            TipoCancha= "Fútbol 11 (Césped Natural)",
            Fecha= "2026-10-06",
            Hora= "09:00 - 11:00",
            Disponibilidad= "Disponible",
            Precio= 150.00,
            Duracion= "2 horas",
            Servicios= "Vestuarios, Estacionamiento, Tribuna"
        },
        new
        {
            ID= 4,
            TipoCancha= "Fútbol 5 (Losa)",
            Fecha= "2026-10-06",
            Hora= "20:00 - 21:00",
            Disponibilidad= "Disponible",
            Precio= 50.00,
            Duracion= "1 hora",
            Servicios= "Iluminación"
        },
        new
        {
            ID= 5,
            TipoCancha= "Fútbol 7 (Sintética)",
            Fecha= "2026-10-07",
            Hora= "21:00 - 22:00",
            Disponibilidad= "Mantenimiento",
            Precio= 80.00,
            Duracion= "1 hora",
            Servicios= "Ninguno"
        },
        new
        {
            ID= 6,
            TipoCancha= "Fútbol 11 (Césped Natural)",
            Fecha= "2026-10-08",
            Hora= "15:00 - 17:00",
            Disponibilidad= "Disponible",
            Precio= 120.00,
            Duracion= "2 horas",
            Servicios= "Vestuarios, Estacionamiento, Entrenamientos"
        }
    });
});

var port = Environment.GetEnvironmentVariable("Port") ?? "10000";

app.Run();