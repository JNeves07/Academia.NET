namespace SistemaAcademico.Domain;

/// <summary>Aluno matriculado em um curso e turma.</summary>
public sealed class Aluno : Pessoa, IIdentificavel
{
    public Aluno(
        string nome,
        DateTime dataNascimento,
        string cpf,
        string email,
        string matricula,
        string curso,
        string turma)
        : base(nome, dataNascimento, cpf, email)
    {
        Matricula = Validacao.ValidarMatricula(matricula);
        Curso = Validacao.TextoObrigatorio(curso, nameof(Curso));
        Turma = Validacao.TextoObrigatorio(turma, nameof(Turma));
    }

    public string Identificador => Matricula;

    public string Matricula { get; }

    public string Curso { get; private set; }

    public string Turma { get; private set; }

    public void AlterarCurso(string curso) => Curso = Validacao.TextoObrigatorio(curso, nameof(curso));

    public void AlterarTurma(string turma) => Turma = Validacao.TextoObrigatorio(turma, nameof(turma));

    public bool CorrespondeAMatricula(string? matricula) =>
        string.Equals(Matricula, matricula?.Trim(), StringComparison.OrdinalIgnoreCase);

    public string DescricaoMatricula() =>
        $"O aluno {Nome} está matriculado no curso {Curso} e na turma {Turma} com matrícula {Matricula}";
}
