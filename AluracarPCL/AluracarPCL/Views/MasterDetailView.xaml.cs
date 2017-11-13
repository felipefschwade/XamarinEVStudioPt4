using AluracarPCL.Model;
using AluracarPCL.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AluracarPCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario _usuario;
        public MasterDetailView(Usuario usuario)
        {
            InitializeComponent();
            Detail = new NavigationPage(new ListagemView(usuario));
            Master = new MasterView(usuario);
            _usuario = usuario;
        }
        public MasterDetailView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentos", (msg) => 
            {
                Detail = new NavigationPage(new AgendamentosUsuariosView(msg));
                IsPresented = false;
            });
            MessagingCenter.Subscribe<Usuario>(this, "NovoAgendamento", (usuario) => 
            {
                Detail = new NavigationPage(new ListagemView(usuario));
                IsPresented = false;
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "MeusAgendamentos");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovoAgendamento");
            base.OnDisappearing();
        }


    }
}