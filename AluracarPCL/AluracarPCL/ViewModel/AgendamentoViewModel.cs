using AluracarPCL.Data;
using AluracarPCL.Model;
using AluracarPCL.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AluracarPCL.ViewModel
{
    public class AgendamentoViewModel : ViewModelBase
    {
        private bool isbusy;

        public Usuario Usuario { get; private set; }

        private DateTime data = DateTime.Now;

        public DateTime Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(); }
        }
        private TimeSpan hora;

        public TimeSpan Hora
        {
            get { return hora; }
            set { hora = value; OnPropertyChanged(); }
        }

        public string Nome
        {
            get { return Usuario.Nome; }
        }

        public string Telefone
        {
            get { return Usuario.Nome; }
        }

        public string Modelo { get; set; }
        public decimal Preco { get; set; }

        public string Email
        {
            get { return Usuario.Email; }
        }

        public bool IsBusy
        {
            get { return isbusy; }
            set { isbusy = value; OnPropertyChanged(); }
        }

        public AgendamentoViewModel(Carro veiculo, Usuario usuario)
        {
            Usuario = usuario;
            Modelo = veiculo.Nome;
            Preco = veiculo.Valor;
            AgendarCommand = new Command(() => 
            {
                MessagingCenter.Send(new Agendamento(Modelo, Preco, Nome, Email, Telefone, Data, Hora), "Agendamento");
            }, () => {
                return !String.IsNullOrEmpty(Nome) && !String.IsNullOrEmpty(Telefone) && !String.IsNullOrEmpty(Email);
            });
        }

        public ICommand AgendarCommand { get; set; }

        public async Task SalvarAgendamento(Agendamento agendamento)
        {
            await AgendamentoService.SalvarAgendamento(agendamento);
        }
    }
}
