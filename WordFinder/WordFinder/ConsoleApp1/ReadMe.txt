Title: WORD FINDER

Description: This project is a C# .NET Framework 7.0 application that can find the top 10 most frequent words 
			in a 2D grid of characters. The project has the following features:

•  It takes an IEnumerable<string> as a constructor parameter to create the 2D grid of characters 
	with the specified rows and columns. It throws a custom exception if the size of the grid is invalid.

•  It also takes another IEnumerable<string> as an input parameter for the Find method,
	which returns the top 10 most frequent words in the grid that match the input words.

•  It uses a Dictionary<Coordinates, char> to store the coordinates and values of each character in the grid.
	This helps to reduce the time complexity by avoiding unnecessary traversal and searching only in the expected areas.

•  It uses another Dictionary<string, int> to store the frequency of each word found in the grid. 
	This helps to sort and filter the results by frequency.

•  It uses a recursive SearchWordInArray method to search for a word in the grid in two directions:
	left to right and top to bottom. 
	It uses another recursive Search2D method to search for a word in all eight directions from a given cell.

•  It uses code analysis tools to measure the maintainability, cyclomatic complexity,
	and depth of inheritance of the solution. The results are 75%, 38%, and 1% respectively.

	The significance of using a dictionary to store the coordinates and values of each character in the grid
	is to improve the performance and efficiency of the solution.
	It also helps to handle the case when a word is not present in the grid at all.