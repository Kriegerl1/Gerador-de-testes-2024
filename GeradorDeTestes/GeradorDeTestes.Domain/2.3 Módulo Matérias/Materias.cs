﻿using GeradorDeTestes.Domain.MóduloCompartilhado;
using GeradorDeTestes.Domain.MóduloDisciplinas;

namespace GeradorDeTestes.Domain.MóduloMatérias
{
    public class Materias : EntidadeBase
    {

        public string Nome { get; set; }
        public Disciplinas Disciplina { get; set; }
        public int Serie { get; set; }
        public Materias() { }
        public Materias(string nome, Disciplinas disciplina, int serie)
        {
            Nome = nome;
            Disciplina = disciplina;
            Serie = serie;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");
            if (Serie == 0)
                erros.Add("O campo \"série\" é obrigatório");

            return erros;
        }

        public bool VerificarRegistros(List<Materias> repositorio, Materias m)
        {

            foreach (Materias M in repositorio)
                if (M.Nome == m.Nome)
                    return false;

            return true;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Materias atualizado = (Materias)novoRegistro;

            Nome = atualizado.Nome;
            Disciplina = atualizado.Disciplina;
            Serie = atualizado.Serie;
        }



        public override string ToString()
        {
            return $"{Nome}, {Disciplina.Nome}";
        }
    }
}
