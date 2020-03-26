using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string file = @"C:\\Docs\Lab\input.htm";
            var factory = SettingsFactoryCreator.CreateFactory(file);
            var settings = factory.CreateSettings();
            
            settings.Print();
        }
    }

    /// <summary>
    /// Класс для создания фабрики настроек в зависимости от файла
    /// </summary>
    static class SettingsFactoryCreator
    {
        /// <summary>
        /// Метод создающий фабрику настроек в зависимости от заданного файла
        /// </summary>
        /// <param name="file">Название или путь файла с расширением</param>
        /// <returns>Фабрику настроек для нужного расширения файла</returns>
        /// <exception cref="InvalidDataException"></exception>
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

    /// <summary>
    /// Конфигурация текстового редактора 
    /// </summary>
    class Settings
    {
        /// <summary>
        /// Общие настройки
        /// </summary>
        public bool SyntaxHighlight;
        public bool AutoCompletion;
        public bool AutoSave;
        public bool LoadProject;
        
        public bool AutoBrackets;
        
        /// <summary>
        /// Настройки табуляций
        /// </summary>
        public bool UseTab;
        public ushort TabSpace;
        
        
        /// <summary>
        /// Настройки для форматирования ключевых слов и имен переменных
        /// </summary>
        public ECase KeywordCase;
        public ECase VariableCase;
        public ECase MethodCase;

        /// <summary>
        /// Вывод информации об конфигурации
        /// </summary>
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

    /// <summary>
    /// Базовый класс для генерации конфигурации редактора
    /// </summary>
    abstract class ASettingsFactory
    {
        /// <summary>
        /// Метод для генерации параметров редактора
        /// </summary>
        /// <returns>Конфигурация редактора</returns>
        public abstract Settings CreateSettings();
    }

    /// <summary>
    /// Генератор настроек редактора для SQL файлов
    /// </summary>
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
    
    /// <summary>
    /// Генератор настроек редактора для HTML файлов
    /// </summary>
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

    
    /// <summary>
    /// Генератор настроек редактора для C# файлов
    /// </summary>
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