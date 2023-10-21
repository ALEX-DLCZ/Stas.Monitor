namespace Stas.Monitor.Infrastructures;

public interface IInfoStrategy 
{
  
  void ReadLine(string line);
  
  Queue<List<string>> GetInfo();

}