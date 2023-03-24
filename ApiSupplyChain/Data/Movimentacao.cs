namespace ApiSupplyChain.Data
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public Produto ? Produto  { get; set; } 
        public DateTime DataEvento { get; set; }
        public string TipoMovimentacao { get; set; }
        public string Local { get; set; }
        public int QuantidadeMovimentada { get; set; }
    }
}
