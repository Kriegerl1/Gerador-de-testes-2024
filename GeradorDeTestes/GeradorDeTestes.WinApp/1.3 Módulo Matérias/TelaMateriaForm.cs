﻿using GeradorDeTestes.Domain.MóduloDisciplinas;
using GeradorDeTestes.Domain.MóduloMatérias;

namespace GeradorDeTestes.View.MóduloMatérias
{
    public partial class TelaMateriaForm : Form
    {
        Materias materia;
        List<Materias> rMaterias;

        public Materias Materia
        {
            set
            {
                txtId.Text = value.Id.ToString();
                txtMateria.Text = value.Nome;
            }
            get => materia;
        }
        public TelaMateriaForm(List<Materias> m)
        {
            InitializeComponent();
            this.rMaterias = m;
        }

        public void CarregarDisciplinas(List<Disciplinas> disciplinas)
        {
            cmbBoxDisciplina.Items.Clear();

            foreach (Disciplinas d in disciplinas)
                cmbBoxDisciplina.Items.Add(d);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nomeMateria = txtMateria.Text;
            Disciplinas disciplina = (Disciplinas)cmbBoxDisciplina.SelectedItem;
            int serie = 0;

            if (rdb1Serie.Checked) serie = 1;
            else if (rdb2Serie.Checked) serie = 2;

            materia = new Materias(nomeMateria, disciplina, serie);

            if (!materia.VerificarRegistros(rMaterias, materia))
            {
                MessageBox.Show(
                     "Não é possível realizar esta ação, já existe um registro com este nome cadastrado.",
                     "Aviso",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning
                     );
                DialogResult = DialogResult.Cancel;
            }



        }
        public void MostrarDisciplinas(List<Disciplinas> disciplinas)
        {
            foreach (Disciplinas d in disciplinas)
                cmbBoxDisciplina.Items.Add(d);
        }

        private void txtMateria_PressKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSalvar.PerformClick();
        }
    }
}