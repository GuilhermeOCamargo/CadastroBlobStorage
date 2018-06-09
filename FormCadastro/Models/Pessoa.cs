using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormCadastro.Models
{
    public class Pessoa
    {
        private int id;
        private String nome;
        private String email;

        public Pessoa(int id, string nome, string email)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
    }
}