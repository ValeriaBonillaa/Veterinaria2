using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using Veterinaria.Entidades;
using Veterinaria.Entidades.Request;
using Veterinaria.Entidades.Response;

namespace Veterinaria;

public partial class AgregarCita : ContentPage
{
    HttpClient client;
    JsonSerializerOptions _serializableOptions;
    string baseURL = "https://localhost:44354/api";
    public AgregarCita()
    {
        InitializeComponent();
        client = new HttpClient();
        _serializableOptions = new JsonSerializerOptions
        {
            WriteIndented = true,

        };
    }
    private async void AgregarCita_Btn(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtIdPersona.Text) || string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            DisplayAlert("Datos faltantes", "Por favor ingrese un id y la fecha", "Cerrar");
        }
        else
        { 
            string laFecha = txtFecha.Text;
            int elId = int.Parse(txtIdPersona.Text);

            var url = $"{baseURL}/Cita";

            var req = new ReqInsertarCita();
            req.Cita = new Cita();
            req.Cita.Id_Persona = elId;
            req.Cita.Fecha_Cita = laFecha;



            string json = JsonConvert.SerializeObject(req);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var resAPI = await client.PostAsync(url, content);


            ResInsertarCita res = new ResInsertarCita();
            var response = await resAPI.Content.ReadAsStringAsync();
            res = System.Text.Json.JsonSerializer.Deserialize<ResInsertarCita>(response);

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
