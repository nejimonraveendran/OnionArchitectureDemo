using System;

namespace OnionApp.CrossCutting.Logging.Interfaces
{
    public interface IAppLogger
    {
        void Info(string message);
        void Warning(string message);
        void Fatal(string message);

    }
}
