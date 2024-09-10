// See https://aka.ms/new-console-template for more information

using Spectre.Console;
using Spectre.Console.Cli;


UserInfo user = LoginUser();

Environment environment = AnsiConsole.Prompt(
    new SelectionPrompt<Environment>()
        .Title("in which environment?")
        .AddChoices(
            Environment.CodeSpaces,
            Environment.Development,
            Environment.Staging,
            Environment.Production));

AnsiConsole.MarkupLine($"Selected Environment: [blue]>[/] {environment}");

string application = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Which application?")
        .AddChoices(
            "Test app 1",
            "test app 2",
            "test app 3"));

AnsiConsole.MarkupLine($"[blue]Application: >[/] {application}");



string shouldDeployString = AnsiConsole.Prompt(new TextPrompt<string>($"deploy {application} in {environment} [[y/n]] (y):")
    .DefaultValue("y")
    .AddChoice("y")
    .AddChoice("n")
    .AllowEmpty());

if (shouldDeployString.Equals("y", StringComparison.OrdinalIgnoreCase))
{
 
    AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .Start("[yellow]Deploying... [/]", ctx => {
            //This simulates the deployment process
            Thread.Sleep(5000);
            
            AnsiConsole.MarkupLine("[green]deployment completed![/]");
        });

    Table table = new Table();
    table.Title = new TableTitle("Deployment details");
    table.AddColumn(new TableColumn("User"));
    table.AddColumn(new TableColumn("Environment"));
    table.AddColumn(new TableColumn("Application"));
    table.AddRow([
        user.Username,
        environment.ToString(),
        application
    ]);
    
    AnsiConsole.Write(table);
}


UserInfo LoginUser()
{
    string username = AnsiConsole.Prompt(new TextPrompt<string>("Username:")
        .Validate(username =>
        {
            if (!username.Equals("netmentor", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Error("[red]user not found[/]");
            }

            return ValidationResult.Success();
        }));

    string role = string.Empty;
    AnsiConsole.Prompt(new TextPrompt<string>("Password:")
        .Secret()
        .Validate(password =>
        {
            if (!password.Equals("pass", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Error("[orange1]Wrong password[/], try again");
            }

            // logic to validate user and password
            role = "invented-role-1";
            return ValidationResult.Success();
        }));

    return new UserInfo(username, role);
}


public enum Environment
{
    CodeSpaces,
    Development,
    Staging,
    Production
}


public record UserInfo(string Username, string Role);