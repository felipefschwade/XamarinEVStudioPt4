using AluracarPCL.Model;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using AluracarPCL.Data;

namespace AluracarPCL.ViewModel
{
    class AgendamentosUsuarioViewModel : ViewModelBase
    {
        public Usuario Usuario { get; private set; }
        public ObservableCollection<Agendamento> Agendamentos { get; set; }

        private bool isbusy;

        public bool IsBusy
        {
            get { return isbusy; }
            set { isbusy = value; OnPropertyChanged(); }
        }

        public AgendamentosUsuarioViewModel(Usuario usuario)
        {
            Usuario = usuario;
            Agendamentos = new ObservableCollection<Agendamento>();
        }

        public void CarregaAgendamentos()
        {
            this.IsBusy = true;
            using (var conn = DependencyService.Get<ISQlite>().GetConnection())
            {
                var dao = new AgendamentoDAO(conn);
                foreach (var agendamento in dao.ListaTodos())
                {
                    Agendamentos.Add(agendamento);
                }
            }
            this.IsBusy = false;
        }

    }
}
