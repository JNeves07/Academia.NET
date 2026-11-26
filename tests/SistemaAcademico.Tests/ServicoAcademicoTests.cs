using SistemaAcademico.Application;
using SistemaAcademico.Domain;

namespace SistemaAcademico.Tests;

public class ServicoAcademicoTests
{
    private static ServicoAcademico CriarServico() =>
        new(new RepositorioAlunosEmMemoria(), new RepositorioProfessoresEmMemoria());

    [Fact]
    public void CadastrarAluno_DevePersistirAluno()
    {
        var servico = CriarServico();
        var aluno = FabricasDeTeste.NovoAluno();

        var cadastrado = servico.CadastrarAluno(aluno);

        Assert.Same(aluno, cadastrado);
        Assert.Single(servico.ListarAlunos());
        Assert.Same(aluno, servico.BuscarAlunoPorMatricula("2026001"));
    }

    [Fact]
    public void CadastrarAluno_ComMatriculaDuplicada_DeveLancarExcecao()
    {
        var servico = CriarServico();
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno());

        Assert.Throws<CadastroDuplicadoException>(() =>
            servico.CadastrarAluno(FabricasDeTeste.NovoAluno(nome: "Outro Aluno")));
        Assert.Single(servico.ListarAlunos());
    }

    [Fact]
    public void CadastrarAluno_ComMatriculaEmCaixaDiferente_DeveSerDuplicado()
    {
        var servico = CriarServico();
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno(matricula: "2026AB01"));

        Assert.Throws<CadastroDuplicadoException>(() =>
            servico.CadastrarAluno(FabricasDeTeste.NovoAluno(matricula: "2026ab01")));
    }

    [Fact]
    public void ListarAlunos_DeveRetornarOrdenadoPorMatricula()
    {
        var servico = CriarServico();
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno(matricula: "2026003"));
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno(matricula: "2026001"));
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno(matricula: "2026002"));

        var matriculas = servico.ListarAlunos().Select(aluno => aluno.Matricula);

        Assert.Equal(["2026001", "2026002", "2026003"], matriculas);
    }

    [Fact]
    public void BuscarAlunoPorMatricula_DeveIgnorarCaixa()
    {
        var servico = CriarServico();
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno());

        Assert.NotNull(servico.BuscarAlunoPorMatricula("2026001"));
    }

    [Fact]
    public void BuscarAlunoPorMatricula_Inexistente_DeveRetornarNulo()
    {
        Assert.Null(CriarServico().BuscarAlunoPorMatricula("999999"));
    }

    [Fact]
    public void ObterAluno_Inexistente_DeveLancarExcecao()
    {
        Assert.Throws<RegistroNaoEncontradoException>(() =>
            CriarServico().ObterAluno("999999"));
    }

    [Fact]
    public void DescreverAluno_Inexistente_DeveRetornarMensagemAmigavel()
    {
        Assert.Equal("Matricula não encontrada", CriarServico().DescreverAluno("999999"));
    }

    [Fact]
    public void RemoverAluno_DevePermitirNovoCadastroComMesmaMatricula()
    {
        var servico = CriarServico();
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno());

        Assert.True(servico.RemoverAluno("2026001"));
        Assert.Empty(servico.ListarAlunos());
        servico.CadastrarAluno(FabricasDeTeste.NovoAluno(nome: "Ana Nova"));
        Assert.Single(servico.ListarAlunos());
    }

    [Fact]
    public void CadastrarProfessor_DevePersistirProfessor()
    {
        var servico = CriarServico();
        var professor = FabricasDeTeste.NovoProfessor();

        servico.CadastrarProfessor(professor);

        Assert.Single(servico.ListarProfessores());
        Assert.Same(professor, servico.BuscarProfessorPorRegistro("PROF001"));
    }

    [Fact]
    public void CadastrarProfessor_ComRegistroDuplicado_DeveLancarExcecao()
    {
        var servico = CriarServico();
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor());

        Assert.Throws<CadastroDuplicadoException>(() =>
            servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor(nome: "Outro Professor")));
        Assert.Single(servico.ListarProfessores());
    }

    [Fact]
    public void CadastrarProfessor_ComCargaHorariaInvalida_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            FabricasDeTeste.NovoProfessor(cargaHoraria: 0));
    }

    [Fact]
    public void ListarProfessores_DeveRetornarOrdenadoPorRegistro()
    {
        var servico = CriarServico();
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor(registro: "PROF003"));
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor(registro: "PROF001"));
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor(registro: "PROF002"));

        var registros = servico.ListarProfessores().Select(professor => professor.Registro);

        Assert.Equal(["PROF001", "PROF002", "PROF003"], registros);
    }

    [Fact]
    public void BuscarProfessorPorRegistro_DeveRetornarDescricao()
    {
        var servico = CriarServico();
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor());

        Assert.Contains("Carlos Souza", servico.DescreverProfessor("prof001"));
    }

    [Fact]
    public void ObterProfessor_Inexistente_DeveLancarExcecao()
    {
        Assert.Throws<RegistroNaoEncontradoException>(() =>
            CriarServico().ObterProfessor("PROF999"));
    }

    [Fact]
    public void DescreverProfessor_Inexistente_DeveRetornarMensagemAmigavel()
    {
        Assert.Equal("Professor não encontrado", CriarServico().DescreverProfessor("PROF999"));
    }

    [Fact]
    public void RemoverProfessor_DevePermitirNovoCadastroComMesmoRegistro()
    {
        var servico = CriarServico();
        servico.CadastrarProfessor(FabricasDeTeste.NovoProfessor());

        Assert.True(servico.RemoverProfessor("PROF001"));
        Assert.Empty(servico.ListarProfessores());
        Assert.False(servico.RemoverProfessor("PROF001"));
    }
}
