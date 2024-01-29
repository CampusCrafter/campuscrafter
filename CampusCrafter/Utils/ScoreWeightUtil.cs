using System.Text;
using CampusCrafter.Models;

namespace CampusCrafter.Utils;

public static class ScoreWeightUtil
{
    public static List<ScoreWeight> Deserialize(string str)
    {
        List<ScoreWeight> scoreWeights = [];
        foreach (var scoreWeightStr in str.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var data = scoreWeightStr.Split("=");

            var scoreWeight = new ScoreWeight
            {
                ProgressType = Enum.Parse<ProgressType>(data[0]),
                Weight = decimal.Parse(data[1])
            };
            scoreWeights.Add(scoreWeight);
        }

        return scoreWeights;
    }

    public static string Serialize(IEnumerable<ScoreWeight> scoreWeights)
    {
        StringBuilder builder = new();
        builder.AppendJoin(";",
            scoreWeights.Select(scoreWeight => $"{scoreWeight.ProgressType.ToString()}={scoreWeight.Weight}"));
        return builder.ToString();
    }
}