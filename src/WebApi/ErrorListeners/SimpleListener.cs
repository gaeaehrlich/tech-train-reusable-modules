using Webapi.Contract;
using Webapi.ValidatorListener;

namespace Webapi.ErrorListeners
{
    public class SimpleListener : IErrorListener
    {
        public void Invoke(ValidatorEvent e)
        {
            Console.WriteLine(e.ErrorMessage);
        }
    }
}
