using SistemaAcademico.Domain;

namespace SistemaAcademico.Tests;

public class EntidadesTests
{
    [Fact]
    public void Aluno_DeveNormalizarMatriculaECurso()
    {
        var aluno = FabricasDeTeste.NovoAluno(matricula: "2026ab01", curso: "  Medicina  ");

        Assert.Equal("2026AB01", aluno.Matricula);
        Assert.Equal("2026AB01", aluno.Identificador);
        Assert.Equal("Medicina", aluno.Curso);
    }

    [Fact]
    public void Aluno_DeveRejeitarMatriculaInvalida()
    {
        Assert.Throws<ArgumentException>(() => FabricasDeTeste.NovoAluno(matricula: "MAT-1"));
    }

    [Fact]
    public void Aluno_DescricaoMatricula_DeveConterDadosCadastrados()
    {
        var descricao = FabricasDeTeste.NovoAluno().DescricaoMatricula();

        Assert.Contains("Ana Silva", descricao);
        Assert.Contains("Engenharia de Software", descricao);
        Assert.Contains("turma A", descricao);
        Assert.Contains("2026001", descricao);
    }

    [Fact]
    public void Aluno_CorrespondeAMatricula_DeveIgnorarCaixa()
    {
        Assert.True(FabricasDeTeste.NovoAluno().CorrespondeAMatricula("2026001"));
        Assert.False(FabricasDeTeste.NovoAluno().CorrespondeAMatricula("2026999"));
    }

    [Fact]
    public void Aluno_AlterarTurma_DeveValidarNovoValor()
    {
        var aluno = FabricasDeTeste.NovoAluno();

        Assert.Throws<ArgumentException>(() => aluno.AlterarTurma(" "));
        aluno.AlterarTurma("B");
        Assert.Equal("B", aluno.Turma);
    }

    [Fact]
    public void Pessoa_DevePermitirAtualizarNomeEEmail()
    {
        var aluno = FabricasDeTeste.NovoAluno();

        aluno.AtualizarNome("Ana Maria");
        aluno.AtualizarEmail("ANA.MARIA@escola.br");

        Assert.Equal("Ana Maria", aluno.Nome);
        Assert.Equal("ana.maria@escola.br", aluno.Email);
        Assert.Equal("Pessoa salva com sucesso", aluno.SalvarPessoa());
    }

    [Fact]
    public void Professor_DeveValidarCargaHorariaNoConstrutor()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            FabricasDeTeste.NovoProfessor(cargaHoraria: 100));
    }

    [Fact]
    public void Professor_AlterarCargaHoraria_DeveRespeitarLimites()
    {
        var professor = FabricasDeTeste.NovoProfessor();

        Assert.Throws<ArgumentOutOfRangeException>(() => professor.AlterarCargaHoraria(0));
        professor.AlterarCargaHoraria(20);
        Assert.Equal(20, professor.CargaHoraria);
    }

    [Fact]
    public void Professor_DeveRejeitarRegistroInvalido()
    {
        Assert.Throws<ArgumentException>(() => FabricasDeTeste.NovoProfessor(registro: "P"));
    }

    [Fact]
    public void Professor_DescricaoRegistro_DeveConterDadosCadastrados()
    {
        var descricao = FabricasDeTeste.NovoProfessor().DescricaoRegistro();

        Assert.Contains("Carlos Souza", descricao);
        Assert.Contains("Matemática", descricao);
        Assert.Contains("Doutorado", descricao);
        Assert.Contains("PROF001", descricao);
    }

    [Fact]
    public void Professor_CorrespondeAoRegistro_DeveIgnorarCaixa()
    {
        Assert.True(FabricasDeTeste.NovoProfessor().CorrespondeAoRegistro("prof001"));
        Assert.False(FabricasDeTeste.NovoProfessor().CorrespondeAoRegistro("prof999"));
    }
}
