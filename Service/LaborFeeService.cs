using labor_fee_calculator.Models;
using labor_fee_calculator.Service.Interface;

namespace labor_fee_calculator.Service
{
    public class LaborFeeService : ILaborFeeService
    {
        public LaborResponse GetLaborFeeCalculation(LaborInput input)
        {
            // Validating the StartDate and EndDate

            if (input.StartDate > DateTime.Now)
            {
                return new LaborResponse { Data = null, ErrorMessage = "Start date cannot be in the future." };
            }

            if (input.EndDate < input.StartDate)
            {
                return new LaborResponse { Data = null, ErrorMessage = "End date cannot be before start date." };
            }

            // Use default values if PerHourCharge and OverTimeCharge are not provided or are 0

            decimal normalRate = (input.PerHourCharge.HasValue && input.PerHourCharge.Value > 0) ? input.PerHourCharge.Value : 10;
            decimal overtimeRate = (input.OverTimeCharge.HasValue && input.OverTimeCharge.Value > 0) ? input.OverTimeCharge.Value : 15;

            var workLogs = new List<(DateTime Date, int HoursWorked)>();
            var random = new Random();

            for (DateTime date = input.StartDate.Date; date <= input.EndDate.Date; date = date.AddDays(1))
            {
                int hoursWorked = 0;
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday) // Skip weekends (Saturday and Sunday)
                {
                    hoursWorked = random.Next(6, 11);   // Weekday: generate random hours between 6 and 10 hours
                }

                workLogs.Add((date, hoursWorked)); // Adding the work log entry
            }

            double totalNormal = 0, totalOvertime = 0;

            // Calculating total normal and overtime hours, and calculate pay for each day
            var detailedLogs = workLogs.Select(w =>
            {
                var normal = Math.Min(w.HoursWorked, 8); // Max 8 hours as normal hours
                var overtime = Math.Max(0, w.HoursWorked - 8); // Overtime is any hours above 8

                totalNormal += normal;
                totalOvertime += overtime;

                // Calculate the daily pay for each entry (normal + overtime)
                decimal dailyPay = (decimal)normal * normalRate + (decimal)overtime * overtimeRate;

                return new WorkLogs()
                {
                    Date = w.Date,
                    HoursWorked = w.HoursWorked,
                    NormalHours = normal,
                    OvertimeHours = overtime,
                    Pay = dailyPay
                };
            }).ToList();

            // Calculating the total pay for the entire date range
            var totalPay = (decimal)totalNormal * normalRate + (decimal)totalOvertime * overtimeRate;

            return new LaborResponse()
            {
                Data = new LaborFeeOutput()
                {
                    StartDate = input.StartDate,
                    EndDate = input.EndDate,
                    PerHourCharge = normalRate,
                    OverTimeCharge = overtimeRate,
                    WorkLogs = detailedLogs,
                    TotalNormalHours = totalNormal,
                    TotalOvertimeHours = totalOvertime,
                    TotalPay = totalPay
                },
                ErrorMessage = string.Empty
            };
        }
    }
}
