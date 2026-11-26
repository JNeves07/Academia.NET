using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Academico
{
    public class Professor:Pessoa
    {
        public Professor() { }

        private string registro;
        private string departamento;
        private string titulacao;
        public string Registro { get => registro; set => registro = value; }
        public string Departamento { get => departamento; set => departamento = value; }
        public string Titulacao { get => titulacao; set => titulacao = value; }

        public string buscarporRegistro( string _registro)
        {
            if (_registro ==  registro)
            {
                return $"O professor {Nome} faz parte do deparamento de {departamento} " +
                    $"e tem a titulação de {titulacao} sobre o registro {registro}";
            }
            else
            {
                return "Professor não encontrado";
            }
        }
    }
}
