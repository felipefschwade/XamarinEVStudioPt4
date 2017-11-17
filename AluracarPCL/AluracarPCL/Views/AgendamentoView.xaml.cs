using AluracarPCL.Model;
using AluracarPCL.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AluracarPCL.Views
{
    public partial class AgendamentoView : ContentPage
    {
        public AgendamentoViewModel ViewModel { get; set; }
        public AgendamentoView(Carro veiculo, Usuario usuario)
        {
            InitializeComponent();
            ViewModel = new AgendamentoViewModel(veiculo, usuario);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "Agendamento", (msg) => MarcaAgendamento(msg));
            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", $"Seu Agendamento para {msg.Data.ToString("dd/MM/yyyy")} às {msg.Hora.ToString(@"hh\:mm")} foi realizado com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });
            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", $"Não foi possível realizar o agendamento, revise os campos do formulário e se o problema persitir contate o suporte técnico!", "Ok");
                await Navigation.PopToRootAsync();    
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }

        private async void MarcaAgendamento(Agendamento agendamento)
        {
            var result = await DisplayAlert("Realizar Agendamento", $"Deseja mesmo realizar o Agendamento para {agendamento.Data.ToString("dd/MM/yyyy")}?", "Sim", "Não");
            if (result)
            {
                await ViewModel.SalvarAgendamento(agendamento);
            }
        }
    }
}
