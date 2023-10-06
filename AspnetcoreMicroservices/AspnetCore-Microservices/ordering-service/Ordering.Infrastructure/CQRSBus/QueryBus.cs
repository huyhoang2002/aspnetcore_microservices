using MediatR;
using Ordering.Infrastructure.CQRSBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.CQRSBus
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator _meditor;

        public QueryBus(IMediator meditor)
        {
            _meditor = meditor;
        }

        public async Task<object> SendAsync(object request)
        {
            return await _meditor.Send(request);
        }
    }
}
