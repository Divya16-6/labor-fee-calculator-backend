namespace labor_fee_calculator.Models
{
    public class LaborFeeOutput
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal PerHourCharge { get; set; }
        public decimal OverTimeCharge { get; set; }

        public List<WorkLogs> WorkLogs { get; set; }
        public double TotalNormalHours { get; set; }
        public double TotalOvertimeHours { get; set; }

        public decimal TotalPay { get; set; }
    }
}
