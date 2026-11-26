using SistemaAcademico.Application;
using SistemaAcademico.Cli;

var servico = new ServicoAcademico(
    new RepositorioAlunosEmMemoria(),
    new RepositorioProfessoresEmMemoria());

return MenuConsole.Executar(servico);
