using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class PantallaPrincipal : TabbedPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public PantallaPrincipal()
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
        persona();
        cita();
    }

    private async void persona(){

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
                //listaPersonas.ItemsSource = personaInicio;
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

    private async void mascota() {

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
                    //listaMascotas.ItemsSource = mascotaInicio;
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
               //listaCitas.ItemsSource = mascotaInicio;
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
    private  void AdministarMascota_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdministrarMascota());
    }
    private void AdministarPersona_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdministrarPersona());
    }
    private void AdministarCita_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AdministarCita());
    }

}