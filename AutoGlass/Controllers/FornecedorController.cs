using AutoMapper;
using Core.Entities;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedorController : ControllerBase
    {
        public IFornecedorService _fornecedorService;
        public IMapper _mapper;

        public FornecedorController(IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Fornecedor>> GetAll()
        {
            var fornecedores = _fornecedorService.GetAll();

            if (fornecedores.Count > 0)
                return Ok(fornecedores);
            else
                return NoContent();
        }
    }
}
