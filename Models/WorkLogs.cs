namespace labor_fee_calculator.Models
{
    public class WorkLogs
    {
        public DateTime Date { get; set; }

        public decimal HoursWorked { get; set; }
        public decimal NormalHours { get; set; }

        public decimal OvertimeHours { get; set; }

        public decimal Pay { get; set; }
    }
}
