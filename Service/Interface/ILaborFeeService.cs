using labor_fee_calculator.Models;

namespace labor_fee_calculator.Service.Interface
{
    public interface ILaborFeeService
    {
        LaborResponse GetLaborFeeCalculation(LaborInput input);
    }
}
