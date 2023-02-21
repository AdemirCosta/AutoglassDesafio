using Api.Models.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Parametros;
using Core.Services.Interfaces;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        public IProdutoService _produtoService;
        public IMapper _mapper;

        public ProdutoController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoService.GetById(id);
            if (produto != null)
                return Ok(produto);
            else
                return NoContent();
        }

        [HttpGet]
        public ActionResult<ListaPaginada<Produto>> GetPaged(int numeroPagina, int tamanhoPagina, string? descricao, DateTime? dataFabricacao, DateTime? dataValidade, int? idFornecedor)
        {
            ProdutoParametros produtoParametros = new()
            {
                NumeroPagina = numeroPagina,
                TamanhoPagina = tamanhoPagina,
                Descricao = descricao,
                DataFabricacao = dataFabricacao,
                DataValidade = dataValidade,
                IdFornecedor = idFornecedor
            };

            var produtos = _produtoService.GetPaged(produtoParametros);

            return Ok(produtos);
        }

        [HttpPost("add")]
        public ActionResult<Produto> Add([FromBody] AddProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            var erros = _produtoService.CanAdd(produto);
            if (erros.Any())
            {
                return BadRequest(erros.ToList());
            }

            return Ok(_produtoService.Add(produto));
        }

        [HttpPost("update")]
        public ActionResult<Produto> Update([FromBody] UpdateProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);

            var erros = _produtoService.CanUpdate(produto);
            if (erros.Any())
            {
                return BadRequest(erros.ToList());
            }

            return Ok(_produtoService.Update(produto));
        }

        [HttpPost("delete/{id}")]
        public ActionResult Delete(int id)
        {
            _produtoService.Delete(id);
            return Ok();
        }

    }
}
