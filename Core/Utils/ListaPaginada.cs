namespace Core.Utils
{
    public class ListaPaginada<T>
    {
        public int NumeroPagina { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalItens { get; set; }
        public List<T> Itens { get; set; }

        public ListaPaginada(List<T> itens, int totalItens, int numeroPagina, int tamanhoPagina)
        {
            Itens = itens;
            NumeroPagina = numeroPagina;
            TotalItens = totalItens;
            TamanhoPagina = tamanhoPagina;
            TotalPaginas = tamanhoPagina != 0 ? (int)Math.Ceiling(totalItens / (double)tamanhoPagina) : 0;
        }
    }
}
