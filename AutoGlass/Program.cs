using Api.Models.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Services.Interfaces;
using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AutoglassContext>(opt => opt.UseInMemoryDatabase("Autoglass"));
builder.Services.AddEntityFrameworkInMemoryDatabase();

#region Dependency Injection

builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<IFornecedorService, FornecedorService>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IFornecedorRepository, FornecedorRepository>();

#endregion

#region Automapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<AddProdutoDto, Produto>();
    cfg.CreateMap<UpdateProdutoDto, Produto>();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AutoglassContext>();
AdicionarDadosIniciais(context);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void AdicionarDadosIniciais(AutoglassContext context)
{
    context.Fornecedores?.Add(new Fornecedor("Marilan", 71533766000180));
    context.Fornecedores?.Add(new Fornecedor("Arcor", 43364274000172));
    context.Fornecedores?.Add(new Fornecedor("Piraquê", 04474305000196));
    context.Fornecedores?.Add(new Fornecedor("Fortaleza", 59165664000153));
    context.Fornecedores?.Add(new Fornecedor("Bela Vista", 04617362000187));

    context.Produtos.Add(new Produto
    {
        Descricao = "Biscoito Recheado de Chocolate",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(8),
        Ativo = true,
        IdFornecedor = 1
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Cream Cracker",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(6),
        Ativo = true,
        IdFornecedor = 2
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Biscoito Recheado de Morango",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(9),
        Ativo = true,
        IdFornecedor = 3
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Biscoito Waffer de Chocolate",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(3),
        Ativo = true,
        IdFornecedor = 4
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Prestígio",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(4),
        Ativo = true,
        IdFornecedor = 5
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Baunilha",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(2),
        Ativo = true,
        IdFornecedor = 1
    });

    context.Produtos.Add(new Produto
    {
        Descricao = "Biscoito Água e Sal",
        DataFabricacao = DateTime.Now,
        DataValidade = DateTime.Now.AddMonths(18),
        Ativo = true,
        IdFornecedor = 2
    });

    context.SaveChanges();
}