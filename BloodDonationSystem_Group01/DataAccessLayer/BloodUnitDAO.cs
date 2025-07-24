using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class BloodUnitDAO
    {
        private readonly BloodDsystemContext _context;

        public BloodUnitDAO()
        {
            _context = new BloodDsystemContext();
        }

        public void AddBloodUnit(BloodUnit unit)
        {
            _context.BloodUnits.Add(unit);
            _context.SaveChanges();
        }

        public List<BloodUnit> GetAllBloodUnits()
        {
            return _context.BloodUnits
                           .Include(b => b.Donation)
                           .ToList();
        }

        public BloodUnit? GetBloodUnitById(string unitId)
        {
            return _context.BloodUnits
                           .Include(b => b.Donation)
                           .FirstOrDefault(b => b.UnitId == unitId);
        }

        public void UpdateBloodUnit(BloodUnit updatedUnit)
        {
            var existingUnit = _context.BloodUnits.Find(updatedUnit.UnitId);
            if (existingUnit != null)
            {
                _context.Entry(existingUnit).CurrentValues.SetValues(updatedUnit);
                _context.SaveChanges();
            }
        }

        public void DeleteBloodUnit(string unitId)
        {
            var unit = _context.BloodUnits.Find(unitId);
            if (unit != null)
            {
                _context.BloodUnits.Remove(unit);
                _context.SaveChanges();
            }
        }
    }
}
