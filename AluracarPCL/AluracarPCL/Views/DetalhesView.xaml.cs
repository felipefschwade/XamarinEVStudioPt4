using AluracarPCL.Model;
using AluracarPCL.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AluracarPCL.Views
{
    public partial class DetalhesView : ContentPage
    {
        public Carro Veiculo { get; set; }
        public Usuario Usuario { get; }

        public DetalhesView(Carro veiculo, Usuario usuario)
        {
            Usuario = usuario;
            InitializeComponent(); 
            Veiculo = veiculo;
            BindingContext = new DetalhesViewModel(veiculo);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Carro>(this, "Proximo", (msg) => Navigation.PushAsync(new AgendamentoView(msg, Usuario)));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Carro>(this, "Proximo");
        }

    }
}
