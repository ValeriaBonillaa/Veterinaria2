using Capas.Entidades;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class RegistroUsuario : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public RegistroUsuario()
	{
		InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }
    private async void Registrase_Btn(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtCorreo.Text) || String.IsNullOrEmpty(txtPass.Text) || String.IsNullOrEmpty(txtPersona.Text))
        {
            DisplayAlert("Datos faltantes", "Por favor ingrese un correo,clave y Id", "Cerrar");
        }
        else
        {
            String elCorreo = txtCorreo.Text;
            String elPass = txtPass.Text;
            int elId = int.Parse(txtPersona.Text);

            var url = $"{baseURL}/Usuario";

            var req = new ReqInsertarUsuario();
            req.Usuario = new Usuario();
            req.Usuario.Id_Persona = elId;
            req.Usuario.correo = elCorreo;
            req.Usuario.pass = elPass;


            string json = JsonConvert.SerializeObject(req);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var resAPI = await client.PostAsync(url, content);


            ResInsertarUsuario res = new ResInsertarUsuario();
            var response = await resAPI.Content.ReadAsStringAsync();
            res = System.Text.Json.JsonSerializer.Deserialize<ResInsertarUsuario>(response);

            if (resAPI.IsSuccessStatusCode)
            {
                if (res.result)
                {
                    DisplayAlert("Mensaje enviado", "¡Se registro con exito!", "Cerrar");
                    Navigation.PushAsync(new PantallaPrincipal());
                }
                else
                {
                    DisplayAlert("No se pudieron agregar los datos", res.listaDeErrores.ToList().ToString(), "Cerrar");
                }

            }
            else
            {
                DisplayAlert("Error en el servidor", "Ocurrió un error en el servidor, intente más tarde", "Cerrar");
            }
        }
    }
}