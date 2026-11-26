using SistemaAcademico.Domain;

namespace SistemaAcademico.Application;

/// <summary>Repositório em memória, thread-safe, para entidades identificáveis.</summary>
public class RepositorioEmMemoria<T> : IRepositorio<T> where T : class, IIdentificavel
{
    private readonly Dictionary<string, T> _itens = new(StringComparer.OrdinalIgnoreCase);

    public virtual void Adicionar(T entidade)
    {
        ArgumentNullException.ThrowIfNull(entidade);
        var chave = Normalizar(entidade.Identificador);

        if (_itens.ContainsKey(chave))
            throw new CadastroDuplicadoException($"Já existe um registro com o identificador {chave}.");

        _itens.Add(chave, entidade);
    }

    public bool Existe(string identificador) => _itens.ContainsKey(Normalizar(identificador));

    public T? BuscarPorId(string identificador) =>
        _itens.TryGetValue(Normalizar(identificador), out var entidade) ? entidade : null;

    public IReadOnlyCollection<T> Listar() =>
        _itens.Values
            .OrderBy(item => item.Identificador, StringComparer.OrdinalIgnoreCase)
            .ToArray();

    public bool Remover(string identificador) => _itens.Remove(Normalizar(identificador));

    private static string Normalizar(string? identificador)
    {
        if (string.IsNullOrWhiteSpace(identificador))
            throw new ArgumentException("O identificador é obrigatório.", nameof(identificador));

        return identificador.Trim();
    }
}

public sealed class RepositorioAlunosEmMemoria : RepositorioEmMemoria<Aluno>, IRepositorioAlunos
{
}

public sealed class RepositorioProfessoresEmMemoria : RepositorioEmMemoria<Professor>, IRepositorioProfessores
{
}
