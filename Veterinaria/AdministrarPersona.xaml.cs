using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AdministrarPersona : TabbedPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";
    public AdministrarPersona()
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
        persona();

    }

    private void Agregar_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AgregarPersona());
    }
    private async void persona()
    {

        var url = $"{baseURL}/Persona";

        var req = new ReqObtenerPersona();

        string json = JsonConvert.SerializeObject(req);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var resAPI = await client.GetAsync(url);

        ResObtenerPersona res = new ResObtenerPersona();

        var response = await resAPI.Content.ReadAsStringAsync();

        res = JsonConvert.DeserializeObject<ResObtenerPersona>(response);

        if (resAPI.IsSuccessStatusCode)
        {
            if (res.result)
            {
                List<Persona> personaInicio = new List<Persona>();
                foreach (Persona laPersona in res.listaDePersona)
                {
                    laPersona.Id_Persona.ToString();
                    laPersona.Id_Rol.ToString();
                    laPersona.Nombre_Completo.ToString();
                    laPersona.Num_Contacto.ToString();
                    personaInicio.Add(laPersona);
                }
                listaPersonas.ItemsSource = personaInicio;
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