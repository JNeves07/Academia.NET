using SistemaAcademico.Domain;

namespace SistemaAcademico.Application;

/// <summary>Casos de uso de cadastro, listagem e busca acadêmica.</summary>
public sealed class ServicoAcademico
{
    private readonly IRepositorioAlunos _alunos;
    private readonly IRepositorioProfessores _professores;

    public ServicoAcademico(IRepositorioAlunos alunos, IRepositorioProfessores professores)
    {
        _alunos = alunos ?? throw new ArgumentNullException(nameof(alunos));
        _professores = professores ?? throw new ArgumentNullException(nameof(professores));
    }

    public Aluno CadastrarAluno(Aluno aluno)
    {
        ArgumentNullException.ThrowIfNull(aluno);

        if (_alunos.Existe(aluno.Matricula))
            throw new CadastroDuplicadoException($"Já existe aluno com a matrícula {aluno.Matricula}.");

        _alunos.Adicionar(aluno);
        return aluno;
    }

    public IReadOnlyCollection<Aluno> ListarAlunos() => _alunos.Listar();

    public Aluno? BuscarAlunoPorMatricula(string matricula) => _alunos.BuscarPorId(matricula);

    public Aluno ObterAluno(string matricula) =>
        BuscarAlunoPorMatricula(matricula)
        ?? throw new RegistroNaoEncontradoException($"Aluno com matrícula {matricula} não encontrado.");

    public string DescreverAluno(string matricula) =>
        BuscarAlunoPorMatricula(matricula)?.DescricaoMatricula() ?? "Matricula não encontrada";

    public bool RemoverAluno(string matricula) => _alunos.Remover(matricula);

    public Professor CadastrarProfessor(Professor professor)
    {
        ArgumentNullException.ThrowIfNull(professor);

        if (_professores.Existe(professor.Registro))
            throw new CadastroDuplicadoException($"Já existe professor com o registro {professor.Registro}.");

        _professores.Adicionar(professor);
        return professor;
    }

    public IReadOnlyCollection<Professor> ListarProfessores() => _professores.Listar();

    public Professor? BuscarProfessorPorRegistro(string registro) => _professores.BuscarPorId(registro);

    public Professor ObterProfessor(string registro) =>
        BuscarProfessorPorRegistro(registro)
        ?? throw new RegistroNaoEncontradoException($"Professor com registro {registro} não encontrado.");

    public string DescreverProfessor(string registro) =>
        BuscarProfessorPorRegistro(registro)?.DescricaoRegistro() ?? "Professor não encontrado";

    public bool RemoverProfessor(string registro) => _professores.Remover(registro);
}
