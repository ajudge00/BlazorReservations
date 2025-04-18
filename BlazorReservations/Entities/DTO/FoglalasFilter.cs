namespace BlazorReservations.Entities
{
    public class FoglalasFilter
    {
        public int? Id { get; set; }
        public DateTime? Kezdete { get; set; }
        public DateTime? Vege { get; set; }
        public int? EjszakakSzama { get; set; }
        public string? VendegNev { get; set; }
        public int? SzobaId { get; set; }
        public int? SzemelyekSzama { get; set; }
    }
}
