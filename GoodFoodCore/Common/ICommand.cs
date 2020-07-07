using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFoodCore.Common
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }
}
