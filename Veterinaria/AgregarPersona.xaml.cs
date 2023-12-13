using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AgregarPersona : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";

    public AgregarPersona()
	{
		InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }

	private async void AgregarPersona_Btn(object sender, EventArgs e)
	{
        if (String.IsNullOrEmpty(txtRol.Text) || String.IsNullOrEmpty(txtNumero.Text) || String.IsNullOrEmpty(txtNombre.Text))
        {
            DisplayAlert("Datos faltantes", "Por favor ingrese un rol,numero y nombre", "Cerrar");
        }
        else
        {
            String elNumero = txtNumero.Text;
            String elNombre = txtNombre.Text;
            int elId = int.Parse(txtRol.Text);

            var url = $"{baseURL}/Persona";

            var req = new ReqInsertarPersona();
            req.Persona = new Persona();
            req.Persona.Id_Rol = elId;
            req.Persona.Nombre_Completo = elNombre;
            req.Persona.Num_Contacto = elNumero;


            string json = JsonConvert.SerializeObject(req);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var resAPI = await client.PostAsync(url, content);


            ResInsertarPersona res = new ResInsertarPersona();
            var response = await resAPI.Content.ReadAsStringAsync();
            res = System.Text.Json.JsonSerializer.Deserialize<ResInsertarPersona>(response);

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
