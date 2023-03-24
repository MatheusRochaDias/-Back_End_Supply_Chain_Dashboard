namespace ApiSupplyChain.Data
{
    public class Estoque
    {
        public int Id { get; set; }
        public int ? ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public string Local { get; set; }

    }
}
