using Microsoft.Maui.Controls;
using Examen1PMUCENM2.Controllers;

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

        private async void CargarEmpleados()
        {
            var empleados = await _controller.ObtenerEmpleados();
            empleadosCollection.ItemsSource = empleados;
        }
    }
}