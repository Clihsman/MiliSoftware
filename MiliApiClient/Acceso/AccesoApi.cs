using MiliSoftware.Acceso.Modelos;
using OpenRestClient.Attributes;
using OpenRestClient.Enums;
using System.Threading.Tasks;

namespace MiliSoftware.Acceso
{
    [RestController("auth")]
    public class AccesoApi : Api
    {
        public AccesoApi() : base(typeof(AccesoApi)) { }

        [RestMethod("signin", MethodType.POST)]
        public Task<AccesoToken> Acceder(Credenciales credenciales) =>
            Call<AccesoToken>(nameof(Acceder), credenciales);
    }
}
