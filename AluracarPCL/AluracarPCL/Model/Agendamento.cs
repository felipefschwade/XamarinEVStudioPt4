using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AluracarPCL.Model
{
    public class Agendamento
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Preco { get; private set; }
        public string Modelo { get; private set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        DateTime data = DateTime.Today;
        public bool Confirmado { get; set; }

        public string DataFormatada 
        {
            get
            {
                return Data.Add(Hora).ToString("dd/MM/yyyy HH:mm");
            }
        }

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

        public Agendamento() { }

        public Agendamento(Carro veiculo)
        {
            Modelo = veiculo.Nome;
            Preco = veiculo.Valor;
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
