using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaGerenciamento.Startup))]
namespace SistemaGerenciamento
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
