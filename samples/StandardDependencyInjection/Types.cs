using System;

namespace StandardDependencyInjection
{
    public interface IMessenger
    {
        void WriteMessage(string message);
    }

    public class ConsoleMessenger : IMessenger
    {
        private readonly ConsoleMessengerOptions _options;

        public ConsoleMessenger() : this(new ConsoleMessengerOptions()) { }

        public ConsoleMessenger(ConsoleMessengerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ConsoleMessengerOptions
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.Cyan;
    }
}