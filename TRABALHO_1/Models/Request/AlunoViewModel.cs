using TRABALHO_1.Models.Validations;

namespace Aluno.Models.Request

{
    public class AlunoViewModel
    {

        public string Nome { get; set; }

        [RaValidation]
        public string RA { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public bool Ativo { get; set; }

    }
}