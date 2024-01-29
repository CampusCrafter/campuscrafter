using CampusCrafter.Models;
using CampusCrafter.Utils;

namespace CampusCrafterTests;

public class ScoreWeightUtilTests
{
    [Test]
    public void ScoreWeightDeserialize_OneScoreWeight()
    {
        // Arrange
        const string scoreWeightStr = "MaturaBasicPolish=3.0";
        
        // Act
        var scoreWeights = ScoreWeightUtil.Deserialize(scoreWeightStr);
        
        // Assert
        Assert.That(scoreWeights, Is.EquivalentTo(new List<ScoreWeight> { new() { ProgressType = ProgressType.MaturaBasicPolish, Weight = 3m } }));
    }
    
    [Test]
    public void ScoreWeightDeserialize_MultipleScoreWeights()
    {
        // Arrange
        const string scoreWeightStr = "MaturaBasicPolish=3.0;Portfolio=100.56";
        
        // Act
        var scoreWeights = ScoreWeightUtil.Deserialize(scoreWeightStr);
        
        // Assert
        Assert.That(scoreWeights, Is.EquivalentTo(new List<ScoreWeight>
        {
            new() { ProgressType = ProgressType.MaturaBasicPolish, Weight = 3m },
            new() { ProgressType = ProgressType.Portfolio, Weight = 100.56m },
        }));
    }
    
    [Test]
    public void ScoreWeightDeserialize_NoScoreWeights()
    {
        // Arrange
        const string scoreWeightStr = "";
        
        // Act
        var scoreWeights = ScoreWeightUtil.Deserialize(scoreWeightStr);
        
        // Assert
        Assert.That(scoreWeights, Is.Empty);
    }

    [Test]
    public void ScoreWeightSerialize_OneScoreWeight()
    {
        // Arrange
        var scoreWeights = new List<ScoreWeight> { new() { ProgressType = ProgressType.MaturaBasicPolish, Weight = 3m } };
        
        // Act
        var serialized = ScoreWeightUtil.Serialize(scoreWeights);
        
        // Assert
        Assert.That(serialized, Is.EqualTo("MaturaBasicPolish=3"));
    }
    
    [Test]
    public void ScoreWeightSerialize_MultipleScoreWeights()
    {
        // Arrange
        var scoreWeights = new List<ScoreWeight>
        {
            new() { ProgressType = ProgressType.MaturaBasicPolish, Weight = 3m },
            new() { ProgressType = ProgressType.Portfolio, Weight = 100.56m },
        };
        
        // Act
        var serialized = ScoreWeightUtil.Serialize(scoreWeights);
        
        // Assert
        Assert.That(serialized, Is.EqualTo("MaturaBasicPolish=3;Portfolio=100.56"));
    }
    
    [Test]
    public void ScoreWeightSerialize_NoScoreWeights()
    {
        // Arrange
        var scoreWeights = new List<ScoreWeight>();
        
        // Act
        var serialized = ScoreWeightUtil.Serialize(scoreWeights);
        
        // Assert
        Assert.That(serialized, Is.Empty);
    }
}