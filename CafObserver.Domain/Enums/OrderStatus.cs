using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Domain.Enums
{
    public enum OrderStatus
    {
        Pending,
        Confirmed,
        InPreparation,
        ReadyForPickup,
        Completed,
        Cancelled
    }
}
