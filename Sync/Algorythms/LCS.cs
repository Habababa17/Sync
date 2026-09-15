public class LCS{
    /// <summary>
    /// Modified version of the Longest Common Subsequence (LCS) algorithm
    /// https://algomaster.io/learn/dsa/longest-common-subsequence
    /// It returns how text1 transformed from text2
    /// </summary>
    /// <param name="text1"> original text </param>
    /// <param name="text2"> out of sync copy </param>
    public List<FileDifference> LongestCommonSubsequence(byte[] text1, byte[] text2) {
        int m = text1.Length, n = text2.Length;
        int[,] dp = new int[m + 1, n + 1];
        //generate dp table
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                if (text1[i - 1] == text2[j - 1]) {
                    // Characters match: extend the LCS
                    dp[i, j] = 1 + dp[i - 1, j - 1];
                } else {
                    // Characters differ: take the best from skipping either
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        //reconstruct the differences
        var differences = new List<FileDifference>();
        //TODO


        return differences;
    }
}