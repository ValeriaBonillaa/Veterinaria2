using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class ObtenerUsuarios : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api/Usuario/1";

    public ObtenerUsuarios()
	{
		InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }
    private async void  Login_Btn(object sender, EventArgs e)
    {
            var url = $"{baseURL}/Usuario";

            var req = new ReqObtenerUsuario();

            string json = JsonConvert.SerializeObject(req);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


            var resAPI = await client.GetAsync(url);

            ResObtenerUsuario res = new ResObtenerUsuario();


            if (resAPI.IsSuccessStatusCode)
            {
                if (res.result)
                {
                var response = await resAPI.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<ResObtenerUsuario>(response);

                List<ReqObtenerUsuario> Usuario = new List<ReqObtenerUsuario>();
                    foreach (ReqObtenerUsuario elUsuario in Usuario)
                    {
                        elUsuario.Id_Usuario.ToString();
                        elUsuario.Correo.ToString();
                        elUsuario.Pass.ToString();
                        Usuario.Add(elUsuario);

                    DisplayAlert("Funciono", "Ocurrió un error en el servidor, intente más tarde", "Cerrar");
                }
                }
                else
                {
                    DisplayAlert("No se pudieron mostrar los productos", res.listaDeErrores.ToList().ToString(), "Cerrar");
                }
            }
            else
            {
                DisplayAlert("Error en el servidor", "Ocurrió un error en el servidor, intente más tarde", "Cerrar");
            }
    }
}