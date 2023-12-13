using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AgregarMascota : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";
    public AgregarMascota()
	{
		InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }
    private async void AgregarMascota_Btn(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtIdPersona.Text) || String.IsNullOrEmpty(txtdescripcion.Text) || String.IsNullOrEmpty(txtNom_Mascota.Text))
        {
            DisplayAlert("Datos faltantes", "Por favor ingrese un Id de Persona,descripcion y nombre", "Cerrar");
        }
        else
        {
            String laDescripcion = txtdescripcion.Text;
            String elNombre = txtNom_Mascota.Text;
            int elId = int.Parse(txtIdPersona.Text);

            var url = $"{baseURL}/Mascota";

            var req = new ReqInsertarMascota();
            req.Mascota = new Mascota();
            req.Mascota.Id_Persona = elId;
            req.Mascota.descripcion = laDescripcion;
            req.Mascota.Nom_Mascota = elNombre;


            string json = JsonConvert.SerializeObject(req);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var resAPI = await client.PostAsync(url, content);


            ResInsertarMascota res = new ResInsertarMascota();
            var response = await resAPI.Content.ReadAsStringAsync();
            res = System.Text.Json.JsonSerializer.Deserialize<ResInsertarMascota>(response);

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
