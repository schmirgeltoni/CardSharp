using CardSharp;

namespace CardSharpTest;

public class RankTest
{
    [Fact]
    public void ToCharMethod_ReturnsCorrectCharacters()
    {
        char[] charRepresentations = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
        TestUtils.CheckAllValues<Rank, char>(RankMethods.ToChar, charRepresentations);
    }

    [Fact]
    public void BlackJackValues_ReturnCorrect()
    {
        int[] values = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11];
        TestUtils.CheckAllValues<Rank, int>(RankMethods.BlackJackValue, values);
    }
}