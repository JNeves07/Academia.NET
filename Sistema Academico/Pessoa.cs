using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Academico
{
    public abstract class Pessoa
    {
        public Pessoa() { 
        }
        private string nome;
        private DateTime dataN;
        private string cpf;
        private string email;

        public string Nome { get => nome; set => nome =  value; }
        public DateTime DataN { get => dataN; set => dataN = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }

        public string salvarPessoa()
        {
            return "Pessoa salva com sucesso";
        }
    }
}
