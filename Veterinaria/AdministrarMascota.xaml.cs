using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AdministrarMascota : TabbedPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public AdministrarMascota()
	{
		InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        mascota();

    }

    private void Agregar_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AgregarMascota());
    }

    private async void mascota()
    {

        var url = $"{baseURL}/Mascota";

        var req = new ReqObtenerMascota();

        string json = JsonConvert.SerializeObject(req);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var resAPI = await client.GetAsync(url);

        ResObtenerMascota res = new ResObtenerMascota();

        var response = await resAPI.Content.ReadAsStringAsync();

        res = JsonConvert.DeserializeObject<ResObtenerMascota>(response);

        if (resAPI.IsSuccessStatusCode)
        {
            if (res.result)
            {
                List<Mascota> mascotaInicio = new List<Mascota>();
                foreach (Mascota laMascota in res.listaDeMascota)
                {
                    laMascota.Mascota_ID.ToString();
                    laMascota.Id_Persona.ToString();
                    laMascota.descripcion.ToString();
                    laMascota.Nom_Mascota.ToString();
                    mascotaInicio.Add(laMascota);
                }
                listaMascotas.ItemsSource = mascotaInicio;
            }
            else
            {
                DisplayAlert("No se pudieron mostrar las mascotas", res.listaDeErrores.ToList().ToString(), "Cerrar");
            }

        }
        else
        {
            DisplayAlert("Error en el servidor", "Ocurrió un error en el servidor, intente más tarde", "Cerrar");
        }
    }

}