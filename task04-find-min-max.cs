using System;

// Задача: реализовать метод FindMinMax, который должен возвращать минимальное и максимальное значение в массиве вложенных массивов (jagged array).
//   * Метод должен возвращать true, если он содержит хотя бы одно числовое значение.
//   * Метод должен пропускать null значения в массиве.
//   * Метод должен выбрасывать исключение ArgumentNullException в случае, если в метод передали null.
//   * В решении разрешается использовать только конструкции языка. Использовать LINQ запрещено.

class Program
{
    public static bool FindMinMax(int[][] array, out int min, out int max)
    {
        min = int.MinValue;
        max = int.MaxValue;

        if (array == null)
        {
            throw new ArgumentNullException();
        }

        var integerExists = false;
        int[] maxInRows = new int[array.Length];
        int[] minInRows = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    integerExists = true;
                    for (int k = j + 1; k < array[i].Length; k++)
                    {
                        if (array[i][j] < array[i][k])
                        {
                            var temp = array[i][j];
                            array[i][j] = array[i][k];
                            array[i][k] = temp;
                        }
                    }


                    maxInRows[i] = array[i][0];
                    minInRows[i] = array[i][array[i].Length - 1];
                }
            }
        }

        for (int i = 0; i < maxInRows.Length; i++)
        {
            for (int j = i + 1; j < maxInRows.Length; j++)
            {
                if (maxInRows[i] < maxInRows[j])
                {
                    var temp = maxInRows[i];
                    maxInRows[i] = maxInRows[j];
                    maxInRows[j] = temp;
                }
            }
        }

        for (int i = 0; i < minInRows.Length; i++)
        {
            for (int j = i + 1; j < minInRows.Length; j++)
            {
                if (minInRows[i] > minInRows[j])
                {
                    var temp = minInRows[i];
                    minInRows[i] = minInRows[j];
                    minInRows[j] = temp;
                }
            }
        }

        max = maxInRows.Length > 0 ? maxInRows[0] : 0;
        min = minInRows.Length > 0 ? minInRows[0] : 0;

        return integerExists;
    }

    // ДОБАВЬТЕ НОВЫЕ МЕТОДЫ, ЕСЛИ НЕОБХОДИМО

    // ----- ЗАПРЕЩЕНО ИЗМЕНЯТЬ КОД МЕТОДОВ, КОТОРЫЕ НАХОДЯТСЯ НИЖЕ -----

    public static void Main()
    {
        Console.WriteLine("Task is done when all test cases are correct:");

        int testCaseNumber = 1;

        TestReturnedValues(testCaseNumber++, new int[][] { }, false, 0, 0);
        TestReturnedValues(testCaseNumber++, new int[][] { null }, false, 0, 0);
        TestReturnedValues(testCaseNumber++, new int[][] { new int[] { } }, false, 0, 0);
        TestReturnedValues(testCaseNumber++, new int[][] { new int[] { 1 } }, true, 1, 1);
        TestReturnedValues(testCaseNumber++, new int[][] { null, new int[] { } }, false, 0, 0);
        TestReturnedValues(testCaseNumber++, new int[][] { new int[] { }, null }, false, 0, 0);
        TestReturnedValues(testCaseNumber++, new int[][] { new int[] { 2 }, new int[] { 1 } }, true, 1, 2);
        TestReturnedValues(testCaseNumber++, new int[][]
            {
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
            }, true, 1, 14);
        TestReturnedValues(testCaseNumber++, new int[][]
            {
                new int[] { 7, 395, 1, 9, 6, 62, 71, 80, 918 },
                new int[] { 102, 72, 84, 41, 89, 382, 7, 36, 53, 10, 1101, 93, 23, 1401 },
                new int[] { 942, 29, 3, 346, 53, 6, 73, 897, 384, 052, 3, 501 },
                new int[] { 39, 83, 11, 28, 385, 0, 5, 482, 947, 143 }
            }, true, 0, 1401);
        TestException<ArgumentNullException>(testCaseNumber++, null);

        if (correctTestCaseAmount == testCaseNumber - 1)
        {
            Console.WriteLine("Task is done.");
        }
        else
        {
            Console.WriteLine("TASK IS NOT DONE.");
        }
    }

    private static void TestReturnedValues(int testCaseNumber, int[][] array, bool expectedResult, int expectedMin, int expectedMax)
    {
        try
        {
            int min, max;
            var result = FindMinMax(array, out min, out max);

            if (result == expectedResult && expectedMin == min && expectedMax == max)
            {
                Console.WriteLine(correctCaseTemplate, testCaseNumber);
                correctTestCaseAmount++;
            }
            else
            {
                Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
            }
        }
        catch (Exception)
        {
            Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
        }
    }

    private static void TestException<T>(int testCaseNumber, int[][] array) where T : Exception
    {
        try
        {
            int min, max;
            var result = FindMinMax(array, out min, out max);
            Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
        }
        catch (ArgumentException)
        {
            Console.WriteLine(correctCaseTemplate, testCaseNumber);
            correctTestCaseAmount++;
        }
        catch (Exception)
        {
            Console.WriteLine(incorrectCaseTemplate, testCaseNumber);
        }
    }

    private static string correctCaseTemplate = "Case #{0} is correct.";
    private static string incorrectCaseTemplate = "Case #{0} IS NOT CORRECT";
    private static int correctTestCaseAmount = 0;
}
