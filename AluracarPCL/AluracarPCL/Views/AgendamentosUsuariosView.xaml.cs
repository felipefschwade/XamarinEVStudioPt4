using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AluracarPCL.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AluracarPCL.ViewModel;

namespace AluracarPCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentosUsuariosView : ContentPage
    {
        private Usuario Usuario;

        public AgendamentosUsuariosView()
        {
            InitializeComponent();
        }

        public AgendamentosUsuariosView(Usuario user)
        {
            InitializeComponent();
            Model = new AgendamentosUsuarioViewModel(Usuario);
            BindingContext = Model;
            Usuario = user;
        }

        internal AgendamentosUsuarioViewModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.CarregaAgendamentos();
        }
    }
}