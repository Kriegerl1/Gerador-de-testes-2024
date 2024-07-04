﻿using GeradorDeTestes.Domain.MóduloCompartilhado;

namespace GeradorDeTestes.Domain.MóduloDisciplinas
{ 
    public class Disciplinas : EntidadeBase
    {

        public string Nome { get; set; } // Nome da Disciplina (Complicado de achar um nome)

        public Disciplinas() { } //Ctor para Deserializar

        public Disciplinas(string disciplina)
        {
            Nome = disciplina;
        }
        public Disciplinas(int id, string disciplina)
        {
            id = id;
            Nome = disciplina;
        }

        public override void AtualizarRegistro(EntidadeBase novoRegistro)
        {
            Disciplinas Atualizar = (Disciplinas)novoRegistro;

            Nome = Atualizar.Nome;
        }

        public override List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome.Trim()))
                erros.Add("O campo \"disciplina\" precisa ser preenchido");

            return erros;
        }
        public bool VerificarRegistros(List<Disciplinas> repositorio, Disciplinas d)
        {

            foreach (Disciplinas D in repositorio)
                if (D.Nome == d.Nome)
                    return false;

            return true;
        }

        public override string ToString()
        {
            return $"{Nome}";
        }
    }
}
