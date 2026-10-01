var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policity =>
    {
        policity
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();

app.MapGet("/", () =>
{
    return Results.Redirect("/index.html");
});

// Endpoint para el Usuario 1 (Consulta de horarios y disponibilidad)
app.MapGet("/api/horarios", () =>
{
    return Results.Ok(new[]
    {
        new
        {
            ID= 1,
            Fecha= "2026-11-01",
            Hora= "19:00",
            Estado= "Disponible",
            TipoCancha= "Fútbol 11 (Césped Natural)",
            Precio= 150.00,
            Duracion= "1 hora"
        },
        new
        {
            ID= 2,
            Fecha= "2026-11-01",
            Hora= "20:00",
            Estado= "Ocupado",
            TipoCancha= "Fútbol 11 (Césped Natural)",
            Precio= 180.00,
            Duracion= "1 hora"
        },
        new
        {
            ID= 3,
            Fecha= "2026-11-02",
            Hora= "18:00",
            Estado= "Mantenimiento",
            TipoCancha= "Fútbol 7 (Sintética)",
            Precio= 90.00,
            Duracion= "1 hora"
        },
        new
        {
            ID= 4,
            Fecha= "2026-11-02",
            Hora= "21:00",
            Estado= "Disponible",
            TipoCancha= "Fútbol 5 (Losa)",
            Precio= 60.00,
            Duracion= "1 hora"
        }
    });
});

// Endpoint para el Usuario 2 (Consulta de servicios)
app.MapGet("/api/servicios", () =>
{
    return Results.Ok(new[]
    {
        new
        {
            ID= 1,
            Nombre= "Arbitraje profesional",
            Descripcion= "Árbitros certificados para dirigir tus partidos oficiales o amistosos."
        },
        new
        {
            ID= 2,
            Nombre= "Grabación de partidos",
            Descripcion= "Cámaras HD para grabar el encuentro y revivir las mejores jugadas."
        },
        new
        {
            ID= 3,
            Nombre= "Alquiler de implementos",
            Descripcion= "Balones oficiales, chalecos de colores y guantes de arquero disponibles."
        },
        new
        {
            ID= 4,
            Nombre= "Cafetería y Bar",
            Descripcion= "Bebidas rehidratantes, snacks y zona de parrillas post-partido."
        }
    });
});

// Endpoint extra para el Usuario 2 (Promociones)
app.MapGet("/api/promociones", () =>
{
    return Results.Ok(new[]
    {
        new 
        { 
            ID = 1, 
            Titulo = "Cumpleañero no paga", 
            Descripcion = "Si reservas el día de tu cumpleaños, te descontamos el 100% de una hora de alquiler." 
        },
        new 
        { 
            ID = 2, 
            Titulo = "Ligas Nocturnas", 
            Descripcion = "Reserva 4 fechas seguidas a partir de las 22:00 hrs y obtén un 30% de descuento total." 
        }
    });
});

var port = Environment.GetEnvironmentVariable("Port") ?? "10000";

app.Run($"http://0.0.0.0:{port}");