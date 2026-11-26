using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Academico
{
    public class Aluno:Pessoa
    {
        private string matricula;
        private string curso;
        private string turma;

        public string Matricula { get => matricula; set => matricula = value; }
        public string Curso { get => curso; set => curso = value; }
        public string Turma { get => turma; set => turma = value; }

        public string buscarporMatricula(string _matricula)
        {
            if (_matricula == matricula)
            {
                return $"O aluno {Nome} está matriculado no curso {curso}" +
                    $" e na turma {turma} com matricula {matricula}";
            }
            else
            {
                return "Matricula não encontrada";
            }
        }
    }
}
