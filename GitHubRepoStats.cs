using System;
using System.Threading.Tasks;
using Octokit;

namespace GitHubRepoStats
{

    public class GitHubRepoStats

    {

        private readonly GitHubClient _client;
        private readonly string _owner;
        private readonly string _repo;

        public GitHubRepoStats(string owner, string repo, string token)
        {
            _owner = owner;
            _repo = repo;
            _client = new GitHubClient(new ProductHeaderValue("GitHubRepoStats"))
            {
                Credentials = new Credentials(token)
            };
        }

        public async Task DisplayStatsAsync()
        {
            try
            {
                var repo = await _client.Repository.Get(_owner, _repo);
                var contributors = await _client.Repository.GetAllContributors(_owner, _repo);
                var issues = await _client.Issue.GetAllForRepository(_owner, _repo);
                var pullRequests = await _client.PullRequest.GetAllForRepository(_owner, _repo);

                Console.WriteLine($"Repository: {repo.FullName}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"Stars: {repo.StargazersCount}");
                Console.WriteLine($"Forks: {repo.ForksCount}");
                Console.WriteLine($"Open Issues: {issues.Count}");
                Console.WriteLine($"Open Pull Requests: {pullRequests.Count}");
                Console.WriteLine($"Contributors: {contributors.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching repository stats: {ex.Message}");
            }
        }

        public static async Task Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: GitHubRepoStats <owner> <repo> <token>");
                return;
            }

            string owner = args[0];
            string repo = args[1];
            string token = args[2];

            var stats = new GitHubRepoStats(owner, repo, token);
                    await stats.DisplayStatsAsync();
                }
            }
        }
