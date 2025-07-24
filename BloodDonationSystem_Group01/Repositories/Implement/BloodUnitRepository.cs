using BusinessObject;
using DataAccessLayer;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implement
{
    public class BloodUnitRepository : IBloodUnitRepository
    {
        BloodUnitDAO BloodUnitDAO = new BloodUnitDAO();
        public void AddBloodUnit(BloodUnit unit)
        {
            BloodUnitDAO.AddBloodUnit(unit);
        }

        public void DeleteBloodUnit(string unitId)
        {
            BloodUnitDAO.DeleteBloodUnit(unitId);
        }

        public List<BloodUnit> GetAllBloodUnits()
        {
            return BloodUnitDAO.GetAllBloodUnits();
        }

        public BloodUnit? GetBloodUnitById(string unitId)
        {
            return BloodUnitDAO.GetBloodUnitById(unitId);
        }

        public void UpdateBloodUnit(BloodUnit unit)
        {
            BloodUnitDAO.UpdateBloodUnit(unit);
        }
    }
}
