using MediatR;
using Ordering.Infrastructure.CQRSBus.Interfaces;
using Ordering.Infrastructure.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.CQRSBus
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _meditor;
        private readonly IUnitOfwork _unitOfWork;

        public CommandBus(IMediator meditor, IUnitOfwork unitOfWork)
        {
            _meditor = meditor;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> SendAsync(object request)
        {
            return await _unitOfWork.SaveChangeAsync<object>(() => _meditor.Send(request));
        }
    }
}
