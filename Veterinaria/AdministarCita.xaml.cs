using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AdministarCita : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public AdministarCita()
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
        cita();
    }
    private void Agregar_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AgregarCita());
    }
    private async void cita()
    {

        var url = $"{baseURL}/Cita";

        var req = new ReqObtenerCita();

        string json = JsonConvert.SerializeObject(req);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var resAPI = await client.GetAsync(url);

        ResObtenerCita res = new ResObtenerCita();

        var response = await resAPI.Content.ReadAsStringAsync();

        res = JsonConvert.DeserializeObject<ResObtenerCita>(response);

        if (resAPI.IsSuccessStatusCode)
        {
            if (res.result)
            {
                List<Cita> mascotaInicio = new List<Cita>();
                foreach (Cita laCita in res.listaDeCita)
                {
                    laCita.Id_Persona.ToString();
                    laCita.Fecha_Cita.ToString();
                    mascotaInicio.Add(laCita);
                }
                listaCitas.ItemsSource = mascotaInicio;
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