namespace prolabcheck.domain.DTOs
{
    public class SearchMedicineDTO
    {
        public string product { get; set; }
        public string description { get; set; }
        public int content { get; set; }
        public string expirationDate { get; set; }
        public string code { get; set; }
        public int numberOfSearches { get; set; }
    }
}
