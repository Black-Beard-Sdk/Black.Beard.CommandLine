using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.CommandLines
{

    public partial class Command<T>
        where T : CommandLineApplication

    {
        
        /// <summary>
        /// Initializes the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public T Initialize(T app)
        {

            //Helper.Load();

            AnsiConsole.GetError(true);

            app.HelpOption(HelpFlag);
            app.VersionOption(VersionFlag, Constants.ShortVersion, Constants.LongVersion);

            app.Name = Constants.Name;
            app.Description = Constants.ProgramHelpDescription;
            app.ExtendedHelpText = Constants.ExtendedHelpText;

            var methods = this.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var method in methods)
                if (method.Name != nameof(Command<T>.Initialize) && typeof(CommandLineApplication).IsAssignableFrom(method.ReturnType))
                {
                    var parameters = method.GetParameters();
                    if (parameters.Length == 1 && typeof(CommandLineApplication).IsAssignableFrom(parameters[0].ParameterType))
                        method.Invoke(this, new object[] { app });
                }

            return app;

        }

        public const string HelpFlag = "-? |-h |--help";
        public const string VersionFlag = "-v |--version";
        public static string _access;

    }

}
