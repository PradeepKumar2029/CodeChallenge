
using CodeChallenge;
using System.Data;

namespace ConsoleApp1
{
    internal class WordFinder
    {
        static char[,]? matrix2DArray;
        private Dictionary<Cordinates, char> setOfCharValuesWithCordinates;

        public WordFinder(IEnumerable<string> matrix, int matrixRow, int matrixColumn)
        {
            if (matrix == null) throw new ArgumentNullException("Matrix cannot be null");

            if (matrix.Count() > Config.maxLength && matrix.First().Length > Config.maxLength)
            {
                throw new OutOfMemoryException("Size cannot exceeds 64");
            }
            setOfCharValuesWithCordinates = new Dictionary<Cordinates, char>();
            matrix2DArray = Create2DArray(matrix, setOfCharValuesWithCordinates, matrixRow, matrixColumn);
        }

        static char[,] Create2DArray(IEnumerable<string> matrix, Dictionary<Cordinates, char> set, int rowMatrix, int columnMatrix)
        {
            char[,] array = new char[rowMatrix, columnMatrix];
            for (int i = 0; i < rowMatrix; i++)
            {
                char[] rowValue = matrix.ElementAt(i).ToCharArray();

                if (rowValue.Length == columnMatrix)
                {
                    for (int j = 0; j < columnMatrix; j++)
                    {
                        array[i, j] = rowValue[j];
                        set.TryAdd(new Cordinates() { XCordinate = i, YCordinate = j }, rowValue[j]);
                    }
                }
                else
                {
                    throw new Exception("Invalid Matrix");
                }
            }
            return array;
        }

        public IEnumerable<string>? Find(IEnumerable<string> wordStreams)
        {
            Dictionary<string, int> foundedWords = new();
            foreach (var wordStream in wordStreams.DistinctBy(m => m))
            {
                if (setOfCharValuesWithCordinates.ContainsValue(wordStream[0]))
                {
                    SearchWordInArray(matrix2DArray, wordStream, setOfCharValuesWithCordinates, foundedWords);
                }
            }
            var top10 = foundedWords?.OrderByDescending(kvp => kvp.Value)
                .Take(10).Select(kvp => kvp.Key).ToList();

            return top10;
        }

        // A method to check if a given word exists in a given 2D word array
        static bool SearchWordInArray(char[,]? array, string word, Dictionary<Cordinates, char> setOfDistinctStorageValue, Dictionary<string, int> foundedWords)
        {
            if (array == null)
            {
                return false;
            }

            //Check for all the cordinates of word which needs to be found.
            var CordinatesValues = setOfDistinctStorageValue.Where(m => m.Value == word[0]).ToList();
            if (CordinatesValues != null)
            {
                foreach (var item in CordinatesValues)
                {
                    int presenceCount = Search2D(array, item.Key.XCordinate, item.Key.YCordinate, word);
                    if (presenceCount > 0)
                    {
                        if (foundedWords != null && foundedWords.ContainsKey(word))
                        {
                            foundedWords[word]++;
                        }
                        else
                            foundedWords?.Add(word, presenceCount);
                    }
                }
            }

            return false;
        }

        static int Search2D(char[,] grid, int rowIndex,
                         int colIndex, String word)
        {
            // If first character of word doesn't match then skip
            if (grid[rowIndex, colIndex] != word[0])
            {
                return 0;
            }

            int len = word.Length;

            int repetationCount = 0;
            // starting from (rowIndex, colIndex)
            for (int dir = 0; dir < Config.xDirection.Length; dir++)
            {
                // Initialize starting point for current direction
                int rowValueInDirection = rowIndex + Config.yDirection[dir];
                int colValueInDirection = colIndex + Config.xDirection[dir];
                int startingPoint;

                // match remaining characters
                for (startingPoint = 1; startingPoint < len; startingPoint++)
                {
                    // If out of bound break
                    if (rowValueInDirection >= matrix2DArray?.GetLength(0) || rowValueInDirection < 0 || colValueInDirection >= matrix2DArray?.GetLength(1) || colValueInDirection < 0)
                    {
                        break;
                    }

                    // If not matched, break
                    if (grid[rowValueInDirection, colValueInDirection] != word[startingPoint])
                    {
                        break;
                    }

                    // Moving in particular direction
                    rowValueInDirection += Config.yDirection[dir];
                    colValueInDirection += Config.xDirection[dir];
                }

                // If all character matched, then value of startingPoint
                // must be equal to length of word
                if (startingPoint == len)
                {
                    repetationCount++;
                }

            }
            return repetationCount;
        }

    }
}
