namespace MyApp.BLL.Infostructure
{
    using System;
    public class Validation : Exception
    {
        public string Property { get; protected set; }

        public Validation(string message, string property) : base(message)
        {
            Property = property;
        }
    }
}
