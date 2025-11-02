using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Examen1PMUCENM2.Controllers;
using Examen1PMUCENM2.Models;

namespace Examen1PMUCENM2.Views
{
    public partial class AgregarEmpleado : ContentPage
    {
        private readonly EmpleadosController _controller = new();
        private FileResult _fotoCapturada;
        private string _fotoBase64 = string.Empty;

        public AgregarEmpleado()
        {
            InitializeComponent();
        }

        private async void OnTomarFoto(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso denegado", "No se puede acceder a la cámara.", "OK");
                    return;
                }

                _fotoCapturada = await MediaPicker.CapturePhotoAsync();

                if (_fotoCapturada != null)
                {
                    _fotoBase64 = await _controller.ConvertirImagenABase64(_fotoCapturada);
                    using var stream = await _fotoCapturada.OpenReadAsync();
                    fotoPreview.Source = ImageSource.FromStream(() => stream);
                    guardarButton.IsEnabled = true; 
                }
                else
                {
                    await DisplayAlert("Cancelado", "No se capturó ninguna imagen.", "OK");
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("No disponible", "Este dispositivo no soporta captura directa. Se abrirá la galería.", "OK");
                _fotoCapturada = await MediaPicker.PickPhotoAsync();
                if (_fotoCapturada != null)
                {
                    _fotoBase64 = await _controller.ConvertirImagenABase64(_fotoCapturada);
                    using var stream = await _fotoCapturada.OpenReadAsync();
                    fotoPreview.Source = ImageSource.FromStream(() => stream);
                    guardarButton.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
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