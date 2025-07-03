using System;
using System.Threading.Tasks;

namespace GitHubRepoStatsTool
{
  class Program
  {
    static async Task Main()
    {
      string token = Prompt("GitHub Token: ");
      string owner = Prompt("Repository Owner: ");
      string repo = Prompt("Repository Name: ");

      if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(owner) || string.IsNullOrWhiteSpace(repo))
      {
        Console.WriteLine("Invalid input. All fields must be filled.");
        return;
      }

      var statsTool = new GitHubRepoStats(token, owner, repo);
      await statsTool.DisplayStatsAsync();
    }

    static string Prompt(string message)
    {
      Console.Write(message);
      return Console.ReadLine()?.Trim();
    }
  }
}
