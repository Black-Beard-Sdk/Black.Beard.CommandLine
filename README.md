# Black.Beard.CommandLine


[![Build status](https://ci.appveyor.com/api/projects/status/rdcxoxyrhv13gmb9?svg=true)](https://ci.appveyor.com/project/gaelgael5/black-beard-commandline)


package for manage application starting with command line.

# How to use

First install the last version of the pckage nuget.
```ps

PM> Install-Package Black.Beard.CommandLine -Version 1.0.0

```

Next Copy and past this code in the Program class.

```csharp
public partial class Program
{

    public static object Result { get; private set; }

    public static void Main(string[] args)
    {
        var cmd = Bb.CommandLine.Run<Command, CommandLine>(args);
        Program.Result = cmd.Result;
    }   
}
```

And add a new class CommandLine
```csharp
using Microsoft.Extensions.CommandLineUtils;
public partial class CommandLine : CommandLineApplication
{

    public object Result { get; set; }

}
```

Create a new folder "Commands" at root of the project and add the following code.

```csharp
/// <summary>
/// 
/// </summary>
public partial class Command : Command<CommandLine>
{

    /*
        Syntax
        .exe template execute "<template file>" --source:"value source" --debug
    */

    public CommandLineApplication CommandExecute(CommandLine app)
    {
        
        // The syntax start with template.
        var cmd = app.Command("template", config =>
        {
            config.Description = "template process";
            config.HelpOption(HelpFlag);
        });

        /*
            json template execute -file '' -source
        */
        cmd.Command("execute", config =>
        {

            config.Description = "description of the command";
            config.HelpOption(HelpFlag);

            var validator = new GroupArgument(config);

            // Add argument
            var argTemplatePath = validator.Argument("<template file>", "template path"
                , ValidatorExtension.EvaluateFileExist
                , ValidatorExtension.EvaluateRequired
            );

            // Add argument
            var argSource = validator.Option("--source", "json source path that contains data source"
                , ValidatorExtension.EvaluateFileExist
            );

            // Add option
            var optwithDebug = validator.OptionNoValue("--debug", "activate debug mode");

            config.OnExecute(() =>
            {

                if (!validator.Evaluate(out int errorNum))
                {
                    // add your code here.
                }

                return errorNum;

            });
        });


        return app;

    }

}

```
