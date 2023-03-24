namespace ApiSupplyChain.Data
{
    public class Produto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Register_Number { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string ? Description { get; set; }
    }
}
