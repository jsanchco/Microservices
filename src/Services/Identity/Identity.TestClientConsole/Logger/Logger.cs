namespace Identity.TestClientConsole.Logger
{
    #region Using

    using System;
    using System.Threading.Tasks;

    #endregion

    public abstract class Logger
    {
        public void Init()
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("*         Test Client with IdentityServer4         *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        protected async Task PrintInConsole(string line)
        {
            await Console.Out.WriteLineAsync(line);
        }
    }
}
