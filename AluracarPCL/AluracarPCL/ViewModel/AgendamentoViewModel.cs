using AluracarPCL.Data;
using AluracarPCL.Model;
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
        const string URL_POST_AGENDAMENTO = "http://aluracar.herokuapp.com/salvaragendamento";
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


        public async Task SalvaAgendamento()
        {
            IsBusy = true;
            var client = new HttpClient();
            var dataEHora = new DateTime(Data.Year, Data.Month, Data.Day, Hora.Hours, Hora.Minutes, 0);
            var json = JsonConvert.SerializeObject(new {
                nome = Nome,
                fone = Telefone,
                email = Email,
                carro = Modelo,
                preco = Preco,
                dataAgendamento = dataEHora
            });
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            IsBusy = false;
            if (result.IsSuccessStatusCode)
            {
                var agendamento = new Agendamento(Modelo, Preco, Nome, Email, Telefone, Data, Hora);
                MessagingCenter.Send(agendamento, "SucessoAgendamento");
                using (var conn = DependencyService.Get<ISQlite>().GetConnection())
                {
                    var dao = new AgendamentoDAO(conn);
                    dao.Salvar(agendamento);
                }
            }
            else
            {
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
            }
        }

    }
}
