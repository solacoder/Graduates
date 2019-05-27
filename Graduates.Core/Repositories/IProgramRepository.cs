﻿using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Core.Repositories
{
    public interface IProgramRepository : IRepository<Program>
    {
        bool IsExists(Program obj);
    }
}
