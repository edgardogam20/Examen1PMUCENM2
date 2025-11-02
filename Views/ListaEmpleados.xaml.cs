using Microsoft.Maui.Controls;
using Examen1PMUCENM2.Controllers;
using Examen1PMUCENM2.Models;

namespace Examen1PMUCENM2.Views
{
    public partial class ListaEmpleados : ContentPage
    {
        private readonly EmpleadosController _controller = new();

        public ListaEmpleados()
        {
            InitializeComponent();
            CargarEmpleados();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarEmpleados(); 
        }
        private async void CargarEmpleados()
        {
            var empleados = await _controller.ObtenerEmpleados();
            empleadosCollection.ItemsSource = empleados;
        }

        //  Navegar a la pantalla de edición
        private async void OnEditarEmpleado(object sender, EventArgs e)
        {
            var empleado = (sender as Button)?.BindingContext as Empleados;
            if (empleado != null)
            {
                await Shell.Current.GoToAsync($"///EditarEmpleadoPage?Id={empleado.Id}");
            }
        }

        //  Eliminar empleado con confirmación
        private async void OnEliminarEmpleado(object sender, EventArgs e)
        {
            var empleado = (sender as Button)?.BindingContext as Empleados;
            if (empleado != null)
            {
                bool confirmar = await DisplayAlert("Confirmar", $"¿Desea eliminar a {empleado.Nombre}?", "Sí", "No");
                if (confirmar)
                {
                    await _controller.EliminarEmpleado(empleado.Id);
                    await DisplayAlert("Eliminado", "Empleado eliminado correctamente.", "OK");
                    CargarEmpleados(); // Recargar la lista
                }
            }
        }
    }
}