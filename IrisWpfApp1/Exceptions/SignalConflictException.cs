using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Exceptions
{
    class SignalConflictException : Exception
    {
        public Signal ExistingSignal { get; }
        public Signal IncomingSignal { get; }

        public SignalConflictException(Signal existingSignal = null, Signal incomingSignal = null)
        {
            ExistingSignal = existingSignal;
            IncomingSignal = incomingSignal;
        }

        public SignalConflictException(string message, Signal existingSignal = null, Signal incomingSignal = null) : base(message)
        {
            ExistingSignal = existingSignal;
            IncomingSignal = incomingSignal;
        }

        public SignalConflictException(string message, Exception innerException, Signal existingSignal = null, Signal incomingSensor = null) : base(message, innerException)
        {
            ExistingSignal = existingSignal;
            IncomingSignal = incomingSensor;
        }
    }
}
