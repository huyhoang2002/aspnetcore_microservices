﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.CQRSBus.Interfaces
{
    public interface IQueryBus
    {
        Task<object> SendAsync(object request);
    }
}
