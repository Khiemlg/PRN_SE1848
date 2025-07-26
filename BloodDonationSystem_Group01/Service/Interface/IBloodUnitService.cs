using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBloodUnitService
    {
        void AddBloodUnit(BloodUnit unit);
        List<BloodUnit> GetAllBloodUnits();
        Task<List<BloodUnit>> GetAllBloodUnitsAsync();
        BloodUnit? GetBloodUnitById(string unitId);
        void UpdateBloodUnit(BloodUnit unit);
        void DeleteBloodUnit(string unitId);
        void MarkUnitAsUsed(string unitId);
        void DiscardUnit(string unitId, string reason);
    }
}
