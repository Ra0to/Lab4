using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string file = @"C:\\Docs\Lab\input.HTML";
            var factory = SettingsFactoryCreator.CreateFactory(file);
            var settings = factory.CreateSettings();
            
            settings.Print();
        }
    }

    static class SettingsFactoryCreator
    {
        public static ASettingsFactory CreateFactory(string file)
        {
            if (Regex.IsMatch(file, "^(.)+\\.cs$", RegexOptions.IgnoreCase))
                return new CsharpSettingsFactory();
            
            if (Regex.IsMatch(file, "^(.)+\\.sql$", RegexOptions.IgnoreCase))
                return new SqlSettingsFactory();
            
            if (Regex.IsMatch(file, "^(.)+\\.html$", RegexOptions.IgnoreCase))
                return new HtmlSettingsFactory();
            
            throw new InvalidDataException("Unrecognized file type");
        }
    }

    class Settings
    {
        public bool SyntaxHighlight;
        public bool AutoCompletion;
        public bool AutoSave;
        public bool LoadProject;
        
        public bool AutoBrackets;
        
        public bool UseTab;
        public ushort TabSpace;
        
        public ECase KeywordCase;
        public ECase VariableCase;
        public ECase MethodCase;

        public void Print()
        {
            Console.WriteLine($"SyntaxHighlight: {SyntaxHighlight}");
            Console.WriteLine($"AutoCompletion: {AutoCompletion}");
            Console.WriteLine($"AutoSave: {AutoSave}");
            Console.WriteLine($"LoadProject: {LoadProject}");
            Console.WriteLine($"AutoBrackets: {AutoBrackets}");
            Console.WriteLine($"UseTab: {UseTab}");
            Console.WriteLine($"TabSpace: {TabSpace}");
            Console.WriteLine($"KeywordCase: {KeywordCase}");
            Console.WriteLine($"VariableCase: {VariableCase}");
            Console.WriteLine($"MethodCase: {MethodCase}");
        }
    }

    enum ECase
    {
        Camel,
        Upper,
        Lower,
        Initcap,
        Shake,
    }

    abstract class ASettingsFactory
    {
        public abstract Settings CreateSettings();
    }

    class SqlSettingsFactory : ASettingsFactory
    {
        public override Settings CreateSettings()
        {
            var settings = new Settings
            {
                SyntaxHighlight = true,
                AutoCompletion = true,
                AutoSave = false,
                LoadProject = false,
                AutoBrackets = true,
                UseTab = false,
                TabSpace = 8,
                KeywordCase = ECase.Upper,
                VariableCase = ECase.Shake,
                MethodCase = ECase.Shake
            };

            return settings;
        }
    }
    
    class HtmlSettingsFactory : ASettingsFactory
    {
        public override Settings CreateSettings()
        {
            var settings = new Settings
            {
                SyntaxHighlight = true,
                AutoCompletion = true,
                AutoSave = true,
                LoadProject = false,
                AutoBrackets = true,
                UseTab = true,
                TabSpace = 8,
                KeywordCase = ECase.Lower,
                VariableCase = ECase.Lower,
                MethodCase = ECase.Lower
            };

            return settings;
        }
    }

    class CsharpSettingsFactory : ASettingsFactory
    {
        public override Settings CreateSettings()
        {
            var settings = new Settings
            {
                SyntaxHighlight = true,
                AutoCompletion = true,
                AutoSave = true,
                LoadProject = true,
                AutoBrackets = true,
                UseTab = true,
                TabSpace = 4,
                KeywordCase = ECase.Lower,
                VariableCase = ECase.Camel,
                MethodCase = ECase.Initcap
            };

            return settings;
        }
    }
}