using Examen1PMUCENM2.Views;

namespace Examen1PMUCENM2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("AgregarEmpleado", typeof(AgregarEmpleado));
            Routing.RegisterRoute("ListaEmpleados", typeof(ListaEmpleados));
        }
    }
}