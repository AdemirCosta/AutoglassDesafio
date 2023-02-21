using Core.Entities;

namespace Test.Settings.Seeds
{
    public static class FornecedorSeed
    {
        public static List<Fornecedor> Seeds()
        {
            return new List<Fornecedor>
            {
                new Fornecedor("Marilan", 71533766000180),
                new Fornecedor("Arcor", 43364274000172),
                new Fornecedor("Piraquê", 04474305000196),
                new Fornecedor("Fortaleza", 59165664000153),
                new Fornecedor("Bela Vista", 04617362000187)
            };
        }
    }
}
