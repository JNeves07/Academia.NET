namespace SistemaAcademico.Domain;

/// <summary>Entidade que possui um identificador único usado pelo repositório.</summary>
public interface IIdentificavel
{
    string Identificador { get; }
}

/// <summary>Porta de persistência definida pelo domínio e implementada pela aplicação.</summary>
public interface IRepositorio<T> where T : class, IIdentificavel
{
    void Adicionar(T entidade);

    bool Existe(string identificador);

    T? BuscarPorId(string identificador);

    IReadOnlyCollection<T> Listar();

    bool Remover(string identificador);
}

public interface IRepositorioAlunos : IRepositorio<Aluno>
{
}

public interface IRepositorioProfessores : IRepositorio<Professor>
{
}
