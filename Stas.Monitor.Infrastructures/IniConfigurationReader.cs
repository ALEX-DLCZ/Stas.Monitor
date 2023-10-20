using Stas.Monitor.Domains;

namespace Stas.Monitor.Infrastructures;


public class IniConfigurationReader : IConfigurationStrategy
{
  
  private IDictionary<string, string> _currentSection ;
  
  private IDictionary<string,IDictionary<string,string>> _sectionMaps = new Dictionary<string, IDictionary<string, string>>();


  public FileInfo GetFileInfo(string pathArg) 
  {
    var fileInfo = new FileInfo(pathArg);
      if ( !fileInfo.Exists )
      {
        throw new FileNotFoundException();
      }
    return fileInfo;
    
  }
  
  public void ReadLine(string line)
  {
    if ( IsCommentOrEmpty(line) ){
      return;
    }
    else if ( IsNewSection(line) ){
      HandleNewSection(line);
    } else if ( IsValuePair(line) )
    {
      ProcessKeyValuePair(line);
    }
  }
  
  public IDictionary<string,IDictionary<string,string>> GetSectionMaps()
  {
    return _sectionMaps;
  }

  private void ProcessKeyValuePair(string line)
  {
    /*
     java code:
    if (_currentSection != null) {
      String[] parts = line.split("=", 2);
      String key = parts[0].trim();
      String value = parts[1].trim();
      _currentSection.put(key, value);
    }
    */
    if ( _currentSection != null ){
      var parts = line.Split("=", 2);
      var key = parts[0].Trim();
      var value = parts[1].Trim();
      _currentSection.Add(key, value);
    }


    //throw new NotImplementedException();
  }
  private bool IsValuePair(string line)
  {
    /*
     java code:
        return line.contains("=");
    */
    return line.Contains('=');
    
    //throw new NotImplementedException();
  }

  private void HandleNewSection(string line)
  {
    /*
     java code:
        String sectionName = line.substring(1, line.length() - 1);
        currentSection = new HashMap<>();
        sectionMaps.put(sectionName, currentSection);
    */
    var sectionName = line.Substring(1, line.Length - 2);
    _currentSection = new Dictionary<string, string>();
    _sectionMaps.Add(sectionName, _currentSection);
    //ughu
    
    
    //throw new NotImplementedException();
  }

  private bool IsNewSection(string line)
  {
    /*
     java code:
        return line.startsWith("[") && line.endsWith("]");
    */
    return line.StartsWith("[") && line.EndsWith("]");
    
    //throw new NotImplementedException();
  }

  private bool IsCommentOrEmpty(string line)
  {
    /*
     java code:
        return line.isEmpty() || line.startsWith(";") || line.startsWith("#");
    */
    
    return line.Equals(null) || line.StartsWith(";") || line.StartsWith("#");
    
    
    //throw new NotImplementedException();
  }
  
  
  
}