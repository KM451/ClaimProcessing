using System.Transactions;

namespace ClaimProcessing.Shared.Claims
{
    public enum ClaimStatus
    {
        Open = 1,
        Assigned,
        WorkInProgress,
        Managed,
        Closed,
        Cancelled,
    }
    public static class ClaimStatusExt
    {
        public static Dictionary<int, string> ToDic()
        {
            var dict = new Dictionary<int, string>();
            foreach (var name in Enum.GetNames(typeof(ClaimStatus)))
            {
                dict.Add((int)Enum.Parse(typeof(ClaimStatus), name), name);
            }
            return dict;
        }
    }
}
