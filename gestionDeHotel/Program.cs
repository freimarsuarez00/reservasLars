using gestionDeHotel.Data;
using gestionDeHotel.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 27)))); // Cambia la versi�n seg�n sea necesario

// Configura el servicio de correo
builder.Services.AddScoped<IEmailSender>(provider =>
    new EmailSender("smtp.gmail.com", 587, "freimarsuarez45@gmail.com", "byhd ipxa ajom qmml")); // Reemplaza con tus credenciales

builder.Services.AddControllers(); // Agrega los servicios para controladores
builder.Services.AddEndpointsApiExplorer(); // Agregar servicios de Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el middleware de la aplicaci�n
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Usar p�gina de errores en desarrollo
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty; // Establecer la ruta ra�z para acceder a Swagger UI
});

app.UseHttpsRedirection(); // Redirigir a HTTPS
app.UseRouting(); // Habilitar enrutamiento
app.UseAuthorization(); // Habilitar autorizaci�n
app.MapControllers(); // Mapear controladores a las rutas
app.Run(); // Ejecutar la aplicaci�n