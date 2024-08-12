using ContrRendaFixa.Data;
using ContrRendaFixa.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ContrRendaFixa.Repository;
using ContrRendaFixa.Services.Interfaces;
using ContrRendaFixa.Services;

namespace ContrRendaFixa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração do PostgreSQL
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Repository
            builder.Services.AddScoped<ITipoProdutoRepository, TipoProdutoRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IContratanteRepository, ContratanteRepository>();
            builder.Services.AddScoped<IContratacaoRepository, ContratacaoRepository>();
            builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            // Services
            builder.Services.AddScoped<ITipoProdutoService, TipoProdutoService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IContratanteService, ContratanteService>();
            builder.Services.AddScoped<IContratacaoService, ContratacaoService>();
            builder.Services.AddScoped<IPagamentoService, PagamentoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<TratamentoErrosMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
