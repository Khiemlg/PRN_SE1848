﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IBloodUnitRepository
    {
        void AddBloodUnit(BloodUnit unit);
        List<BloodUnit> GetAllBloodUnits();
        BloodUnit? GetBloodUnitById(string unitId);
        void UpdateBloodUnit(BloodUnit unit);
        void DeleteBloodUnit(string unitId);
    }
}
