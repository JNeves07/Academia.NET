using SistemaAcademico.Domain;

namespace SistemaAcademico.Tests;

/// <summary>Valores válidos e reutilizáveis para os testes.</summary>
public static class FabricasDeTeste
{
    public static readonly DateTime DataNascimento = new(2000, 1, 15);

    public static Aluno NovoAluno(
        string matricula = "2026001",
        string nome = "Ana Silva",
        string cpf = "123.456.789-09",
        string email = "ana.silva@escola.edu.br",
        string curso = "Engenharia de Software",
        string turma = "A") =>
        new(nome, DataNascimento, cpf, email, matricula, curso, turma);

    public static Professor NovoProfessor(
        string registro = "PROF001",
        string nome = "Carlos Souza",
        string cpf = "987.654.321-00",
        string email = "carlos.souza@escola.edu.br",
        string departamento = "Matemática",
        string titulacao = "Doutorado",
        int cargaHoraria = 40) =>
        new(nome, DataNascimento, cpf, email, registro, departamento, titulacao, cargaHoraria);
}
