using BusinessObject;
using Repositories.Implement;
using Repositories.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BloodUnitService : IBloodUnitService
    {
        IBloodUnitRepository _bloodUnitRepository;
        public BloodUnitService()
        {
            _bloodUnitRepository = new BloodUnitRepository();
        }
        public void AddBloodUnit(BloodUnit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit), "Blood unit cannot be null");
            }
            _bloodUnitRepository.AddBloodUnit(unit);
        }

        public void DeleteBloodUnit(string unitId)
        {
            if (string.IsNullOrEmpty(unitId))
            {
                throw new ArgumentException("Unit ID cannot be null or empty", nameof(unitId));
            }
            _bloodUnitRepository.DeleteBloodUnit(unitId);
        }

        public List<BloodUnit> GetAllBloodUnits()
        {
            return _bloodUnitRepository.GetAllBloodUnits();
        }

        public async Task<List<BloodUnit>> GetAllBloodUnitsAsync()
        {
            return await Task.FromResult(_bloodUnitRepository.GetAllBloodUnits());
        }

        public BloodUnit? GetBloodUnitById(string unitId)
        {
            if (string.IsNullOrEmpty(unitId))
            {
                throw new ArgumentException("Unit ID cannot be null or empty", nameof(unitId));
            }
            var unit = _bloodUnitRepository.GetBloodUnitById(unitId);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Blood unit with ID {unitId} not found");
            }
            return unit;
        }

        public void UpdateBloodUnit(BloodUnit unit)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit), "Blood unit cannot be null");
            }
            if (string.IsNullOrEmpty(unit.UnitId))
            {
                throw new ArgumentException("Unit ID cannot be null or empty", nameof(unit.UnitId));
            }
            _bloodUnitRepository.UpdateBloodUnit(unit);
        }

        public void MarkUnitAsUsed(string unitId)
        {
            if (string.IsNullOrEmpty(unitId))
            {
                throw new ArgumentException("Unit ID cannot be null or empty", nameof(unitId));
            }
            
            var unit = _bloodUnitRepository.GetBloodUnitById(unitId);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Blood unit with ID {unitId} not found");
            }
            
            unit.Status = "Used";
            _bloodUnitRepository.UpdateBloodUnit(unit);
        }

        public void DiscardUnit(string unitId, string reason)
        {
            if (string.IsNullOrEmpty(unitId))
            {
                throw new ArgumentException("Unit ID cannot be null or empty", nameof(unitId));
            }
            
            var unit = _bloodUnitRepository.GetBloodUnitById(unitId);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Blood unit with ID {unitId} not found");
            }
            
            unit.Status = "Discarded";
            unit.DiscardReason = reason;
            _bloodUnitRepository.UpdateBloodUnit(unit);
        }
    }
}
