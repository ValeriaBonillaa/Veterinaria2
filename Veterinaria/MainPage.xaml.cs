
using Capas.Entidades;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class MainPage : ContentPage
{
    int count = 0;

    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public MainPage()
    {
        InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }

    private async void Login_Btn(object sender, EventArgs e)
    {
        string correo = txtCorreo.Text;
        string password = txtPass.Text;

        if (correo == null || password == null)
        {
            DisplayAlert("Alerta", "Ingrese su correo y clave", "Ok");
            return;
        }
        var url = $"{baseURL}/Usuario";

        var req = new ReqObtenerUsuario();

        string json = JsonConvert.SerializeObject(req);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var resAPI = await client.GetAsync(url);

        ResObtenerUsuario res = new ResObtenerUsuario();

        var response = await resAPI.Content.ReadAsStringAsync();

        res = JsonConvert.DeserializeObject<ResObtenerUsuario>(response);

        if (resAPI.IsSuccessStatusCode)
        {
            if (res.result)
            {
                List<Usuario> usuarioInicio = new List<Usuario>();
                foreach (Usuario elUsuario in res.listaDeUsuario)
                {
                    elUsuario.correo.ToString();
                    elUsuario.pass.ToString();
                    usuarioInicio.Add(elUsuario);
                    DisplayAlert("Datos correctos", "Bienvenido al Sistema", "Ok");
                    Navigation.PushAsync(new PantallaPrincipal());
                }
            }
            else
            {
                DisplayAlert("No se pudo encontrar el usuario", res.listaDeErrores.ToList().ToString(), "Cerrar");
            }

        }
        else
        {
            DisplayAlert("Error en el servidor", "Ocurrió un error en el servidor, intente más tarde", "Cerrar");
        }
    }
    private async void Registrarse_Btn(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegistroUsuario());
    }

 }


