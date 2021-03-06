﻿using AluracarPCL.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluracarPCL.Data
{
    public class AgendamentoDAO
    {
        private readonly SQLiteConnection _connection;

        public AgendamentoDAO(SQLiteConnection conn)
        {
            _connection = conn;
            this._connection.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (_connection.Find<Agendamento>(agendamento.Id) == null)
            {
                _connection.Insert(agendamento);
            } else _connection.Update(agendamento);
        }

        public IEnumerable<Agendamento> ListaTodos()
        {
            return _connection.Table<Agendamento>();
        }

    }
}
