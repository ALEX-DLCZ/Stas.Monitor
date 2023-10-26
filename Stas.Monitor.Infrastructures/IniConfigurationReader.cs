using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;

public class IniConfigurationReader : IConfigurationStrategy
{
  private IDictionary<string, string> _currentSection;

  private IDictionary<string, IDictionary<string, string>> _sectionMaps =
    new Dictionary<string, IDictionary<string, string>>();


  public void ReadLine(string line)
  {
    if ( IsCommentOrEmpty(line) )
    {
      return;
    }
    else if ( IsNewSection(line) )
    {
      HandleNewSection(line);
    }
    else if ( IsValuePair(line) )
    {
      ProcessKeyValuePair(line);
    }
  }

  public IDictionary<string, IDictionary<string, string>> GetSectionMaps()
  {
    return _sectionMaps;
  }

  private void ProcessKeyValuePair(string line)
  {
    if ( _currentSection == null ) return;
    var parts = line.Split("=", 2);
    var key = parts[0].Trim();
    var value = parts[1].Trim();
    _currentSection.Add(key, value);
  }

  private bool IsValuePair(string line)
  {
    return line.Contains('=');
  }

  private void HandleNewSection(string line)
  {
    var sectionName = line.Substring(1, line.Length - 2);
    _currentSection = new Dictionary<string, string>();
    _sectionMaps.Add(sectionName, _currentSection);
  }

  private bool IsNewSection(string line)
  {
    return line.StartsWith("[") && line.EndsWith("]");
  }

  private bool IsCommentOrEmpty(string line)
  {
    return line.Equals(null) || line.StartsWith(";") || line.StartsWith("#");
  }
}