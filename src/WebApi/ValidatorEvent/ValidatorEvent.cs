namespace Webapi.ValidatorListener;

public class ValidatorEvent
{
    public string ErrorMessage { get; }

    public ValidatorEvent(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
