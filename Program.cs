using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;

namespace AzureDevOpsApiSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string organizationUrl = "";
            string personalAccessToken = "";
            string teamProject = "";

            // Create a connection to the organization
            VssConnection connection = new VssConnection(new Uri(organizationUrl), new VssBasicCredential("", personalAccessToken));

            // Get an instance of the WorkHttpClient
            WorkHttpClient workClient = connection.GetClient<WorkHttpClient>();

            // Get the team context for the specified team project
            TeamContext teamContext = new TeamContext(teamProject);

            try
            {
                // Get the list of boards
                var boards = workClient.GetBoardsAsync(teamContext).Result;

                // Output the board names
                foreach (var board in boards)
                {
                    Console.WriteLine(board.Name);
                }
            }
            catch (AggregateException aex)
            {
                VssServiceException vssex = aex.InnerException as VssServiceException;
                if (vssex != null)
                {
                    Console.WriteLine(vssex.Message);
                }
            }
        }
    }
}
