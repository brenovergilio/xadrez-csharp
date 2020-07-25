using System;

namespace tabuleiro
{
    public class TabuleiroException : ApplicationException
    {
        public TabuleiroException (string message) : base(message)
        {}
    }
}