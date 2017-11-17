using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AluracarPCL.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AluracarPCL.ViewModel;
using AluracarPCL.Services;

namespace AluracarPCL.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentosUsuariosView : ContentPage
    {
        private Usuario Usuario;
        readonly AgendamentosUsuarioViewModel _model;

        public AgendamentosUsuariosView()
        {
            InitializeComponent();
        }

        public AgendamentosUsuariosView(Usuario user)
        {
            InitializeComponent();
            _model = new AgendamentosUsuarioViewModel(Usuario);
            BindingContext = _model;
            Usuario = user;
        }

        internal AgendamentosUsuarioViewModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (agendamento) => 
            {
                await DisplayAlert("Reenviado com Sucesso!", "Seu agendamento foi reenviado para o servidor com sucesso!", "Ok");
            });
            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento", async (agendamento) => 
            {
                await DisplayAlert(
                    "Falha ao reenviar!", 
                    @"Seu agendamento não foi reenviado para o servidor, por favor tente novamente
                    mais tarde. Se o problema persistir contate o suporte técnico.", "Ok");
            });
            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado", async (agendamento) => 
            {
                var resp = await DisplayAlert("Confirmar Reenvio", "Deseja reenviar os dados para o servidor?", "Sim", "Não");
                if (resp) await AgendamentoService.SalvarAgendamento(agendamento);
                _model.AtualizaLista();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}