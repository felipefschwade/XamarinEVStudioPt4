using AluracarPCL.Data;
using AluracarPCL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AluracarPCL.Services
{
    public class AgendamentoService
    {
        const string URL_POST_AGENDAMENTO = "http://aluracar.herokuapp.com/salvaragendamento";
        public static async Task SalvarAgendamento(Agendamento agendamento)
        {
            var client = new HttpClient();
            var dataEHora = new DateTime(agendamento.Data.Year, agendamento.Data.Month, agendamento.Data.Day, agendamento.Hora.Hours, agendamento.Hora.Minutes, 0);
            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Telefone,
                email = agendamento.Email,
                carro = agendamento.Modelo,
                preco = agendamento.Preco,
                dataAgendamento = dataEHora
            });
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            if (result.IsSuccessStatusCode)
            {
                agendamento.Confirmado = true;
                MessagingCenter.Send(agendamento, "SucessoAgendamento");
            }
            else
            {
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
                agendamento.Confirmado = false;
            }
            SalvarAgendamentoDB(agendamento);
        }

        public static void SalvarAgendamentoDB(Agendamento agendamento)
        {
            using (var conn = DependencyService.Get<ISQlite>().GetConnection())
            {
                var dao = new AgendamentoDAO(conn);
                dao.Salvar(agendamento);
            }
        }
    }
}
