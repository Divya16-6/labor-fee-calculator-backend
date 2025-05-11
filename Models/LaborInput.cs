namespace labor_fee_calculator.Models
{
    public class LaborInput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? PerHourCharge { get; set; }
        public decimal? OverTimeCharge { get; set; }
    }
}
