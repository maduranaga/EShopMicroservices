using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.CQRS
{

    public interface ICommandHandler<in Tcommand>
        : ICommandHandler<Tcommand, Unit>
        where Tcommand : ICommand<Unit>
    { 
    }

    public interface ICommandHandler<in TCommand, TRespnse>
        : IRequestHandler<TCommand, TRespnse> where TCommand : ICommand<TRespnse>
        where TRespnse : notnull
    {
    }
}
