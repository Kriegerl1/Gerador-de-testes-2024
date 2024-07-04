#region   MDS QUANTO  USING

using GeradorDeTestes.Domain.M�duloTestes;
using GeradorDeTestes.Domain.M�duloMat�rias;
using GeradorDeTestes.Domain.M�duloQuest�es;
using GeradorDeTestes.Domain.M�duloDisciplinas;
using GeradorDeTestes.View.Control.M�duloTestes;
using GeradorDeTestes.Infra.Arquivos.M�duloTestes;
using GeradorDeTestes.View.Control.M�duloMat�rias;
using GeradorDeTestes.Infra.Arquivos.M�duloMat�rias;
using GeradorDeTestes.Domain.M�duloCompartilhado;
using GeradorDeTestes.View.Control.M�duloQuest�es;
using GeradorDeTestes.View.Control.M�duloDisciplinas;
using GeradorDeTestes.Infra.Arquivos.M�duloQuest�es;
using GeradorDeTestes.Infra.Arquivos.M�duloDisciplinas;
using GeradorDeTestes.View.Control.M�duloCompartilhado;
using GeradorDeTestes.Infra.Arquivos.M�duloCompartilhado;

#endregion

namespace GeradorDeTestes.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        ControladorBase controlador;

        ContextoDados contexto;

        IRepositorioTestes repositorioTestes;
        IRepositorioMateria repositorioMaterias;
        IRepositorioQuestoes repositorioQuestoes;
        IRepositorioDisciplina repositorioDisciplina;

        public static TelaPrincipalForm Instancia { get; private set; }

        public TelaPrincipalForm()
        {
            InitializeComponent();
            contexto = new ContextoDados(true);

            lblTipoCadastro.Text = string.Empty;

            Instancia = this;

            repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contexto);
            repositorioMaterias = new RepositorioMateriasEmArquivo(contexto);
            repositorioTestes = new RepositorioTestesEmArquivo(contexto);
            repositorioQuestoes = new RepositorioQuestoesEmArquivo(contexto);

        }

        public void AtualizarRodape(string texto)
        {
            statusLabelPrincipal.Text = texto;
        }

        private void ConfigurarTelaPrincipal(ControladorBase controladorSelecionado)
        {
            lblTipoCadastro.Text = "Cadastro de " + controladorSelecionado.TipoCadastro;

            ConfigurarToolBox(controladorSelecionado);
            ConfigurarListagem(controladorSelecionado);
        }

        private void ConfigurarToolBox(ControladorBase controladorSelecionado)
        {
            btnAdicionar.Enabled = controladorSelecionado is ControladorBase;
            btnEditar.Enabled = controladorSelecionado is ControladorBase;
            btnExcluir.Enabled = controladorSelecionado is ControladorBase;

            btnDuplicar.Enabled = controladorSelecionado is IControladorVisualizar;
            btnVisualizarTeste.Enabled = controladorSelecionado is IControladorVisualizar;
            btnGerarPdf.Enabled = controladorSelecionado is IControladorVisualizar;

            if (controladorSelecionado is IControladorVisualizar)
                btnEditar.Enabled = false;

            ConfigurarToolTips(controladorSelecionado);
        }

        private void ConfigurarToolTips(ControladorBase controladorSelecionado)
        {
            btnAdicionar.ToolTipText = controladorSelecionado.ToolTipAdicionar;
            btnEditar.ToolTipText = controladorSelecionado.ToolTipEditar;
            btnExcluir.ToolTipText = controladorSelecionado.ToolTipExcluir;

            if (controladorSelecionado is IControladorVisualizar controladorVisualizar)
                btnVisualizarTeste.ToolTipText = controladorVisualizar.ToolTipVisualizar;

            if (controladorSelecionado is IControladorGeradorPDF controladorGeradorPDF)
                btnGerarPdf.ToolTipText = controladorGeradorPDF.ToolTipGerarTestePdf;

            if (controladorSelecionado is IControladorDuplicavel controladorDuplicavel)
                btnDuplicar.ToolTipText = controladorDuplicavel.ToolTipDuplicar;

        }

        private void ConfigurarListagem(ControladorBase controladorSelecionado)
        {
            UserControl listagem = controladorSelecionado.ObterListagem();
            listagem.Dock = DockStyle.Fill;

            pnlRegistros.Controls.Clear();
            pnlRegistros.Controls.Add(listagem);
        }

        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorDisciplina(repositorioDisciplina);

            ConfigurarTelaPrincipal(controlador);
        }

        private void testesMenuItem_Click(object sender, EventArgs e)
        {

            controlador = new ControladorTestes(repositorioTestes, repositorioDisciplina, repositorioMaterias, repositorioQuestoes);

            ConfigurarTelaPrincipal(controlador);
        }

        private void materiasMenuItem_Click(object sender, EventArgs e)
        {
            controlador = new ControladorMateria(repositorioMaterias, repositorioDisciplina);

            ConfigurarTelaPrincipal(controlador);
        }
        private void quest�esToolStripMenuItem_Click(object sender, EventArgs e)
        {

            controlador = new ControladorQuestoes(repositorioTestes, repositorioDisciplina, repositorioMaterias, repositorioQuestoes);

            ConfigurarTelaPrincipal(controlador);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            controlador.Adicionar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnVisualizarAlternativa_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorVisualizar controladorVisualizar)
                controladorVisualizar.Visualizar();
        }

        private void btnGerarPdf_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorGeradorPDF controladorGerarTeste)
                controladorGerarTeste.GerarTeste();
        }

        private void btnDuplicar_Click(object sender, EventArgs e)
        {
            if (controlador is IControladorDuplicavel controladorDuplicar)
                controladorDuplicar.Duplicar();

        }
    }
}
