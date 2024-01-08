using MiliSoftware.Errores;
using MiliSoftware.Inventario.Modelos;
using OpenRestClient;
using OpenRestClient.Attributes;
using OpenRestClient.Enums;
using System.Threading.Tasks;

namespace MiliSoftware.Inventario
{
    [RestController("inventario/grupos")]
    public sealed class InventarioGrupoApi : Api
    {    
        public InventarioGrupoApi(): base(typeof(InventarioGrupoApi)) { }

        [RestMethod(MethodType.GET)]
        public async Task<RestResponse<InventarioGrupo[], AccesoError>> All() =>
             await CallArray<InventarioGrupo, AccesoError>(nameof(All));
        
        [RestMethod(MethodType.GET)]
        public Task<InventarioGrupo> FindById(int id) =>
            Call<InventarioGrupo>(nameof(FindById), id);

        [RestMethod(MethodType.POST)]
        public Task<InventarioGrupo> Save(InventarioGrupo inventoryGroup) =>
            Call<InventarioGrupo>(nameof(Save), inventoryGroup);

        [RestMethod(MethodType.PUT)]
        public Task<int> Update(InventarioGrupo inventoryGroup, int id) =>
            Call<int>(nameof(Update), inventoryGroup, id);

        [RestMethod(MethodType.DELETE)]
        public Task<int> Delete([InJoin(",")] params int[] ids) =>
        Call<int>(nameof(Delete), ids);

    }
}
