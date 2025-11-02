using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Examen1PMUCENM2.Controllers;
using Examen1PMUCENM2.Models;

namespace Examen1PMUCENM2.Views
{
    public partial class AgregarEmpleado : ContentPage
    {
        private readonly EmpleadosController _controller = new();
        private FileResult _fotoSeleccionada;
        private string _fotoBase64 = string.Empty;

        public AgregarEmpleado()
        {
            InitializeComponent();
        }

        private async void OnSeleccionarFoto(object sender, EventArgs e)
        {
            try
            {
                _fotoSeleccionada = await MediaPicker.PickPhotoAsync();
                if (_fotoSeleccionada != null)
                {
                    _fotoBase64 = await _controller.ConvertirImagenABase64(_fotoSeleccionada);
                    fotoPreview.Source = ImageSource.FromStream(() => _fotoSeleccionada.OpenReadAsync().Result);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo seleccionar la foto: {ex.Message}", "OK");
            }
        }

        private async void OnGuardarEmpleado(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
                string.IsNullOrWhiteSpace(puestoEntry.Text) ||
                string.IsNullOrWhiteSpace(correoEntry.Text) ||
                string.IsNullOrWhiteSpace(_fotoBase64))
            {
                await DisplayAlert("Validación", "Todos los campos son obligatorios, incluyendo la foto.", "OK");
                return;
            }

            var empleado = new Empleados
            {
                Nombre = nombreEntry.Text,
                FechaIngreso = fechaPicker.Date,
                Puesto = puestoEntry.Text,
                Correo = correoEntry.Text,
                Foto = _fotoBase64
            };

            try
            {
                await _controller.GuardarEmpleado(empleado);
                await DisplayAlert("Éxito", "Empleado guardado correctamente.", "OK");
                await Shell.Current.GoToAsync("//ListaEmpleados");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo guardar el empleado: {ex.Message}", "OK");
            }
        }
    }
}