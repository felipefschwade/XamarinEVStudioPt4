using AluracarPCL.Model;
using AluracarPCL.ViewModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AluracarPCL.Views
{
    public partial class ListagemView : ContentPage
    {
        ListagemViewModel Model { get; set; }
        public Usuario Usuario { get; private set; }

        public ListagemView(Usuario usuario)
        {
            Usuario = usuario;
            Model = new ListagemViewModel();
            BindingContext = Model;
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            await Model.GetVeiculosAsync();
            MessagingCenter.Subscribe<Carro>(this, "VeiculoSelecionado",
                (carro) => Navigation.PushAsync(new DetalhesView(carro, Usuario)));
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Carro>(this, "VeiculoSelecionado");
            base.OnDisappearing();
        }

    }
}
