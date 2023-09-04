using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Aluno.Models.Request;
using static TRABALHO_1.Controllers.PrincipalController;

namespace Aluno.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : PrincipalAlunoController
    {
        private readonly string _alunoCaminhoArquivo;
        private int codigo_ra;

        public AlunoController()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            _alunoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "aluno.json");
        }

        #region Métodos arquivo
        private List<AlunoViewModel> LerAlunosDoArquivo()
        {
            if (!System.IO.File.Exists(_alunoCaminhoArquivo))
            {
                return new List<AlunoViewModel>();
            }

            string json = System.IO.File.ReadAllText(_alunoCaminhoArquivo);
            return JsonConvert.DeserializeObject<List<AlunoViewModel>>(json);
        }


        private void EscreverAlunosNoArquivo(List<AlunoViewModel> alunos)
        {
            string json = JsonConvert.SerializeObject(alunos);
            System.IO.File.WriteAllText(_alunoCaminhoArquivo, json);
        }
        #endregion

        #region Operações CRUD

        [HttpGet]
        public IActionResult Get()
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            return Ok(alunos);
        }

        [HttpGet("{RA}")]
        public IActionResult Get(string RA)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel aluno = alunos.Find(a => a.RA == RA);
            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AlunoViewModel newAluno)
        {
            if (!ModelState.IsValid)
            {
                return ApiBadRequestResponse(ModelState);
            }

            List<AlunoViewModel> alunos = LerAlunosDoArquivo();

            if (alunos.Any(a => a.RA == newAluno.RA))

                return BadRequest("existe um aluno com esse RA !!");

            AlunoViewModel novoAluno = new AlunoViewModel()
            {

                RA = newAluno.RA,
                Nome = newAluno.Nome,
                Email = newAluno.Email,
                Cpf = newAluno.Cpf,
                Ativo = newAluno.Ativo
            };

            alunos.Add(novoAluno);
            EscreverAlunosNoArquivo(alunos);

            return CreatedAtAction(nameof(Get), new { codigo = novoAluno.RA }, novoAluno);
        }



        [HttpPut("{RA}")]
        public IActionResult Put(string RA, [FromBody] AlunoViewModel aluno)
        {
            if (RA == null)

                return BadRequest();

            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            int index = alunos.FindIndex(a => a.RA == RA);
            if (index == -1)
                return NotFound();

            AlunoViewModel novoAluno = new AlunoViewModel()
            {
                RA = aluno.RA,
                Nome = aluno.Nome,
                Email = aluno.Email,
                Cpf = aluno.Cpf
            };

            alunos[index] = novoAluno;
            EscreverAlunosNoArquivo(alunos);

            return NoContent();
        }

        [HttpDelete("{RA}")]
        public IActionResult Delete(string RA)
        {
            List<AlunoViewModel> alunos = LerAlunosDoArquivo();
            AlunoViewModel aluno = alunos.Find(a => a.RA == RA);
            if (aluno == null)
                return NotFound();

            alunos.Remove(aluno);
            EscreverAlunosNoArquivo(alunos);

            return NoContent();
        }
        #endregion
    }
}