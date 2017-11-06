using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AluracarPCL.Model
{
    public class Agendamento
    {
        public decimal Preco { get; private set; }
        public string Modelo { get; private set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        DateTime data = DateTime.Today;

        public Agendamento(string modelo, decimal preco, string nome, string email, string telefone, DateTime data, TimeSpan hora)
        {
            Nome = nome;
            Email = email;
            Preco = preco;
            Modelo = modelo;
            Telefone = telefone;
            Data = data;
            Hora = hora;
        }

        public Agendamento(Carro veiculo)
        {
            Veiculo = veiculo;
        }
        [JsonProperty("data")]
        public DateTime Data
        {
            get { return data; }
            set { this.data = value; }
        }
        public TimeSpan Hora { get; set; }
    }
}
