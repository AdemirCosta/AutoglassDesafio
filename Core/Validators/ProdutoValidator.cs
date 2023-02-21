using Core.Entities;
using FluentValidation;

namespace Core.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator() 
        {
            RuleFor(n => n.Descricao).Must(n => !string.IsNullOrWhiteSpace(n)).WithMessage("O campo Descrição é obrigatório.");
            RuleFor(n => n.DataFabricacao).NotEmpty().WithMessage("O campo Data de Fabricação é obrigatório.");
            RuleFor(n => n.DataValidade).NotEmpty().WithMessage("O campo Data de Validade é obrigatório.");
            RuleFor(n => n.DataValidade).GreaterThan(n => n.DataFabricacao).WithMessage("A Data de Validade deve ser maior que a Data de Fabricação.");
            RuleFor(n => n.IdFornecedor).GreaterThan(0).WithMessage("O campo Fornecedor é obrigatório.");
            RuleFor(n => n.Fornecedor).NotNull().WithMessage("Fornecedor inexistente.");
        }
    }
}
