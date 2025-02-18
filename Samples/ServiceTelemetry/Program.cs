using System.Reactive.Kql;

namespace ServiceTelemetry
{
    internal class Program
    {
        static CollaborationWiki _wiki; 

        static void Main(string[] args)
        {
            _wiki = new CollaborationWiki();
            while (true)
            {
                SimulateUsage(10);
                Thread.Sleep(1000);
            }
        }

        public static void SimulateUsage(int operations)
        {

            for (int i = 0; i < operations; i++)
            {
                var document = Random(_documents);
                var ipAddress = Random(_ipAddresses);
                if (_random.Next(100) > 20)
                {
                    var user = Random(_users);
                    _wiki.ReadDocument(user, document, ipAddress);
                }
                else
                {
                    var grantor = Random(_users);
                    var grantee = Random(_users);
                    _wiki.GrantPermission(grantor, grantee, document, ipAddress);
                }
            }
        }

        #region random_generation
        static string Random(string[] choices)
        {
            int index = _random.Next(choices.Length);
            return choices[index];
        }

        static Random _random = new Random(1);
        static string[] _documents =
        { "Budget.xls", "Vision.docx", "Architecture.pptx", "Plan.pptx" };
        static string[] _users =
        {
            "JohnDoe@contoso.com", "BillSharp@contoso.com", "CindyBlack@contoso.com",
            "JaneShepard@fabrikam.com", "BettyBright@fabrikam.com", "AlexSmith@fabrikam.com"
        };
        static string[] _ipAddresses =
        { "20.112.52.29", "20.103.85.33", "20.84.181.62", "20.81.111.85" };
        #endregion
    }
}