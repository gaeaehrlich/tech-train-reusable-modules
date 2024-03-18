using Webapi.Middleware;
using Webapi.ValidatorListener;

namespace Webapi.Contract;

public interface IErrorListener
{
    void Invoke(ValidatorEvent e);
}
