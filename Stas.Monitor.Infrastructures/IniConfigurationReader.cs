namespace Stas.Monitor.Infrastructures;

public class IniConfigurationReader : IConfigurationStrategy
{
    private readonly IDictionary<string, IDictionary<string, string>> _sectionMaps =
        new Dictionary<string, IDictionary<string, string>>();

    private IDictionary<string, string> _currentSection = new Dictionary<string, string>();

    public IDictionary<string, IDictionary<string, string>> ExecuteConfigurationStrategy(string path)
    {
        try
        {
            //Linq qui remplace un foreach
            File.ReadLines(path).ToList().ForEach(ReadLine);
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("monitor: configuration file not found");
        }

        return _sectionMaps;
    }

    //---------- private methods ----------
    private void ReadLine(string line)
    {
        if (IsCommentOrEmpty(line))
        {
        }
        else if (IsNewSection(line))
        {
            HandleNewSection(line);
        }
        else if (IsValuePair(line))
        {
            ProcessKeyValuePair(line);
        }
    }

    private bool IsCommentOrEmpty(string line) => line.Equals(null) || line.StartsWith(";") || line.StartsWith("#");

    private bool IsNewSection(string line) => line.StartsWith("[") && line.EndsWith("]");

    private void HandleNewSection(string line)
    {
        var sectionName = line.Substring(1, line.Length - 2);
        _currentSection = new Dictionary<string, string>();
        _sectionMaps.Add(sectionName, _currentSection);
    }

    private bool IsValuePair(string line) => line.Contains('=');

    private void ProcessKeyValuePair(string line)
    {
        var parts = line.Split("=", 2);
        var key = parts[0].Trim();
        var value = parts[1].Trim();
        _currentSection.Add(key, value);
    }
}
