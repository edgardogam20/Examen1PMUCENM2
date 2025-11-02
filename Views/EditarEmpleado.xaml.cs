using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Examen1PMUCENM2.Controllers;
using Examen1PMUCENM2.Models;

namespace Examen1PMUCENM2.Views
{
    [QueryProperty(nameof(EmpleadoId), "Id")]
    public partial class EditarEmpleado : ContentPage
    {
        private readonly EmpleadosController _controller = new();
        private string _fotoBase64 = string.Empty;
        public int EmpleadoId { get; set; }

        public EditarEmpleado()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var empleado = await _controller.ObtenerEmpleadoPorId(EmpleadoId);
            if (empleado != null)
            {
                nombreEntry.Text = empleado.Nombre;
                puestoEntry.Text = empleado.Puesto;
                correoEntry.Text = empleado.Correo;
                fechaPicker.Date = empleado.FechaIngreso;
                _fotoBase64 = empleado.Foto;

                if (!string.IsNullOrEmpty(_fotoBase64))
                {
                    fotoPreview.Source = ImageSource.FromStream(() =>
                        new MemoryStream(Convert.FromBase64String(_fotoBase64)));
                }
            }
        }

        private async void OnCambiarFoto(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Permiso denegado", "No se puede acceder a la cámara.", "OK");
                    return;
                }

                var foto = await MediaPicker.CapturePhotoAsync();
                if (foto != null)
                {
                    var originalStream = await foto.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await originalStream.CopyToAsync(memoryStream);
                    var imagenBytes = memoryStream.ToArray();

                    _fotoBase64 = Convert.ToBase64String(imagenBytes);
                    fotoPreview.Source = ImageSource.FromStream(() => new MemoryStream(imagenBytes));
                }
            }
            catch (FeatureNotSupportedException)
            {
                var foto = await MediaPicker.PickPhotoAsync();
                if (foto != null)
                {
                    var originalStream = await foto.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await originalStream.CopyToAsync(memoryStream);
                    var imagenBytes = memoryStream.ToArray();

                    _fotoBase64 = Convert.ToBase64String(imagenBytes);
                    fotoPreview.Source = ImageSource.FromStream(() => new MemoryStream(imagenBytes));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un problema: {ex.Message}", "OK");
            }
        }

        private async void OnActualizarEmpleado(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
                string.IsNullOrWhiteSpace(puestoEntry.Text) ||
                string.IsNullOrWhiteSpace(correoEntry.Text))
            {
                await DisplayAlert("Validación", "Todos los campos son obligatorios.", "OK");
                return;
            }

            var empleado = new Empleados
            {
                Id = EmpleadoId,
                Nombre = nombreEntry.Text,
                Puesto = puestoEntry.Text,
                Correo = correoEntry.Text,
                FechaIngreso = fechaPicker.Date,
                Foto = _fotoBase64
            };

            await _controller.ActualizarEmpleado(empleado);
            await DisplayAlert("Éxito", "Empleado actualizado correctamente.", "OK");
            await Shell.Current.GoToAsync("//ListaEmpleados");
        }
    }
}