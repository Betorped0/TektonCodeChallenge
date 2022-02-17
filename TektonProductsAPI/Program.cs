using Microsoft.EntityFrameworkCore;
using TektonProductsAPI.Data;
using TektonProductsAPI;
using TektonProductsAPI.Repository.IRepository;
using TektonProductsAPI.Repository;
using TektonProductsAPI.ProductsMapper;

var builder = WebApplication.CreateBuilder(args);

    // use context
    builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    {
        //opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        var connString = builder.Configuration.GetConnectionString("DefaultConnection");
        opts.UseSqlServer(connString, options =>
        {
            options.MigrationsAssembly(assemblyName: typeof(ApplicationDbContext).Assembly.FullName.Split(',')[0]);
        });
    });
builder.Services.AddTransient<IProductsRepository, ProductRepository>();

builder.Services.AddTransient<IProductDetailRepository, ProductDetailRepository>();

builder.Services.AddAutoMapper(typeof(ProductsMapper));

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.Run();

