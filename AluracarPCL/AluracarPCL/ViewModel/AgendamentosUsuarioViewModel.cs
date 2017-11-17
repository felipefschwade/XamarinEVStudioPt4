using AluracarPCL.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using AluracarPCL.Data;
using System.Linq;
using System.Windows.Input;

namespace AluracarPCL.ViewModel
{
    class AgendamentosUsuarioViewModel : ViewModelBase
    {
        public Usuario Usuario { get; private set; }
        public ObservableCollection<Agendamento> Agendamentos { get; set; }

        private Agendamento agendamentoSelecionado;

        public Agendamento AgendamentoSelecionado
        {
            get { return agendamentoSelecionado; }
            set
            {
                agendamentoSelecionado = value;
                if (value != null)
                {
                    if (!agendamentoSelecionado.Confirmado) MessagingCenter.Send<Agendamento>(AgendamentoSelecionado, "AgendamentoSelecionado");
                }
            }
        }


        private bool isbusy;

        public bool IsBusy
        {
            get { return isbusy; }
            set { isbusy = value; OnPropertyChanged(); }
        }

        public AgendamentosUsuarioViewModel(Usuario usuario)
        {
            Agendamentos = new ObservableCollection<Agendamento>();
            Usuario = usuario;
            AtualizaLista();
        }

        public void AtualizaLista()
        {
            using (var conn = DependencyService.Get<ISQlite>().GetConnection())
            {
                var dao = new AgendamentoDAO(conn);
                Agendamentos.Clear();
                foreach (var agendamento in dao.ListaTodos().OrderBy(a => a.DataFormatada))
                {
                    Agendamentos.Add(agendamento);
                }
            }
        }
    }
}
