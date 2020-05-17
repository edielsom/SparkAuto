using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SparkAuto.Data;
using SparkAuto.Email;

namespace SparkAuto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // Este lambda determina se o consentimento do usuário para itens não essenciais
            // cookies são necessários para uma determinada solicitação.
            services.Configure<CookiePolicyOptions>(option =>
            {
                option.CheckConsentNeeded = context => true;

                // requer o uso de Microsoft.AspNetCore.Http;
                option.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Adiciona a Conexão de String
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>();

            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Configuração para enviar Email
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailOptions>(Configuration);

            //Configurando para acessar o login através do Facebook
            services.AddAuthentication().AddFacebook(fb =>
            {
                //As informações de Acesso é configurado através da página do facebook developer.
                fb.ClientId = "572381137020363";
                fb.ClientSecret = "c29a84376031ca5ea4196d4552d86375";
            });

            //Configurando para acessar o login através do Google
            //services.AddAuthentication().AddGoogle(go =>
            //{
            //    go.ClientId = "";
            //    go.ClientSecret = "";
            //});

            //Para compilar página Razor
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
             
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


        }
    }
}
