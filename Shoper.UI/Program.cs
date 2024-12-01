using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Shoper.BusinessLogic.Interface;
using Shoper.BusinessLogic.Service;
using Shoper.BusinessLogic.Utility;
using Shoper.Data;
using Shoper.Data.Interface;
using Shoper.Data.Repository;
using Shoper.Data.Validator;
using Shoper.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IMailSender, MailHelper>();

builder.Services.AddScoped<IManifactureRepository, ManifactureRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
builder.Services.AddScoped<IProductItemValueRepository, ProductItemValueRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();


builder.Services.AddScoped<IManifactureService, ManifactureService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductDiscountService, ProductDiscountService>();
builder.Services.AddScoped<IProductCommentService, ProductCommentService>();
builder.Services.AddScoped<IProductItemService, ProductItemService>();
builder.Services.AddScoped<IProductItemValueService, ProductItemValueService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<ICouponService, CouponService>();

builder.Services.AddDbContext<ShoperContext>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcçdefgğhiıjklmnoöpqrsştuüvwxyzABCÇDEFGĞHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-@.";
    options.User.RequireUniqueEmail = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    options.SignIn.RequireConfirmedAccount = true;
})

                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ShoperContext>()
                .AddPasswordValidator<PasswordValidator>()
                .AddUserValidator<UserValidator>()
                .AddErrorDescriber<ErrorDescriber>()
                .AddDefaultTokenProviders();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
});
builder.Services.ConfigureApplicationCookie(_ =>
{
    _.LoginPath = new PathString("/Home/Login");
    _.LogoutPath = new PathString("/Home/LogOut");
    _.Cookie = new CookieBuilder
    {
        Name = "ShoperCookie", //Oluþturulacak Cookie'yi isimlendiriyoruz.
        HttpOnly = false, //Kötü niyetli insanlarýn client-side tarafýndan Cookie'ye eriþmesini engelliyoruz.
                          //Expiration = TimeSpan.FromMinutes(2), //Oluþturulacak Cookie'nin vadesini belirliyoruz.
        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
        SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden eriþilebilir yapýyoruz.
    };
    _.SlidingExpiration = true; //Expiration süresinin yarýsý kadar süre zarfýnda istekte bulunulursa eðer geri kalan yarýsýný tekrar sýfýrlayarak ilk ayarlanan süreyi tazeleyecektir.
    _.ExpireTimeSpan = TimeSpan.FromMinutes(10); //CookieBuilder nesnesinde tanýmlanan Expiration deðerinin varsayýlan deðerlerle ezilme ihtimaline karþýn tekrardan Cookie vadesi burada da belirtiliyor.
    _.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddAuthentication();

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
});
builder.Services.AddDataProtection().PersistKeysToDbContext<ShoperContext>().UseCryptographicAlgorithms(
        new Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorConfiguration
        {
            EncryptionAlgorithm=Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.EncryptionAlgorithm.AES_192_CBC,
            ValidationAlgorithm=Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ValidationAlgorithm.HMACSHA256
        });
builder.Services.AddAntiforgery(options =>
{     // Set Cookie properties using CookieBuilder properties†.
    
    options.Cookie.Expiration = TimeSpan.Zero;
    
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// https için
app.UseHttpsRedirection();  

app.UseStaticFiles();
/*
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
               Path.Combine(Directory.GetCurrentDirectory(), @"/Shoper.Management/wwwroot/images")),
    RequestPath = new PathString("/AdminImages")
});
*/
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
//app.MapRazorComponents<shoperUI.>().DisableAntiforgery().AddInteractiveServerRenderMode();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

  
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShoperContext>();
    
   
    db.Database.Migrate();
}


app.Run();
