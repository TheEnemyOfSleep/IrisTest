﻿using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SignalConflictValidator
{
    public interface ISignalConflictValidator
    {
        Task<Signal> GetConflictingSignal(Signal signal);

        Task<Signal> GetConflictingSignal(int id);
    }
}
