using System.Drawing;

namespace lab_4_lection
{
    internal class Program
    {
        static Random rnd = new Random();
        /// <summary>
        /// выводит сообщение message, пока пользователь не ввёл корректное число типа int, если введено не число, то выводится ошибка и ввод идёт заново.
        /// </summary>
        /// <param name="message"> сообщение при вводе </param>
        /// <returns></returns>

        static int IntegerInput(string message)
        {
            bool isCorrect;
            int answer = 0;
            do
            {
                Console.WriteLine(message);
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите целое число");
                    isCorrect = false;
                }
            } while (!isCorrect);

            return answer;
        }

        static int[] CreateArray()
        {
            Console.WriteLine("Введите номер из списка, чтобы выбрать");
            int[] arr = null;
            bool isCorrect;
            int len;
            int answer = IntegerInput("1. ДСЧ\n2. Ручной ввод");

            switch (answer)
            {
                // 1. ДСЧ
                case 1:
                    len = IntegerInput("Сколько элементов?");

                    arr = new int[len];

                    for (int i = 0; i < len; i++)
                    {
                        arr[i] = rnd.Next(-100, 100);
                    }
                    Console.WriteLine("Массив создан");
                    break;
                // 2.Ручной ввод
                case 2:
                    len = IntegerInput("Сколько элементов?");

                    arr = new int[len];
                    for (int i = 0; i < len; i++)
                    {
                        arr[i] = IntegerInput("Введите элемент: ");
                    }
                    Console.WriteLine("Массив создан");
                    break;
            }
            return arr;
        }

        static bool IsEmptyArray(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("Массив пустой");
                return true;
            }
            return false;
        }

        static void PrintArray(int[] arr)
        {
            // массив может быть пустым, это надо проверять
            if (IsEmptyArray(arr)) return; // выходим из функции обязательно

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine(); // это enter, чтобы следующий вывод шёл с новой строки
        }

        static int[] DeleteEvenNumbersFromArray(int[] arr)
        {
            if (IsEmptyArray(arr))
            {
                return arr;
            }
            // удаляются эл-ты с индексами 1, 3, 5, 7..., так как им соответсвуют номера 2, 4, 6, 8...
            int toDelete = (arr.Length + 1) / 2;
            int[] resultArray = new int[toDelete];
            // 0 1 2 3 4
            // 1 2 3 4 5
            int j = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    resultArray[j] = arr[i];
                    j++;
                }
            }

            return resultArray;
        }

        // arr может быть и пустым
        static int[] AddKElementsToTheEnd(int[] arr)
        {
            int k = IntegerInput("Введите кол-во элементов, которое хотите добавить: ");

            // копируем исходный массив в конечный
            int[] resultArr = new int[arr.Length + k];
            if (arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    resultArr[i] = arr[i];
                }
            }


            int j = 0;
            if (arr.Length > 0)
            {
                j = arr.Length;
            }
            while (k > 0)
            {
                resultArr[j] = IntegerInput($"Введите {j + 1}-ый элемент:");
                j++;
                k--;
            }

            return resultArr;
        }

        static int[] ReplaceMinAndMax(int[] arr)
        {
            if (IsEmptyArray(arr))
            {
                return arr;
            }

            int[] resultArray = new int[arr.Length];
            // копируем массив
            for (int i = 0; i < arr.Length; i++)
            {
                resultArray[i] = arr[i];
            }
            // 2 проблемы:
            // может не быть минимума и максимума
            // несколько минимумов и максимумов, их всех нужно поменять местами
            int min = int.MaxValue;
            int max = int.MinValue;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            if (min != max)
            {
                Console.WriteLine($"Минимум: {min}, максимум: {max}");
                // меняем местами мин и макс
                for (int i = 0; i < resultArray.Length; i++)
                {
                    if (resultArray[i] == max)
                    {
                        resultArray[i] = min;
                        continue; // иначе сработает второе условие
                    }
                    if (resultArray[i] == min)
                    {
                        resultArray[i] = max;
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("Последовательность монотонна");
            }

            return resultArray;
        }

        static void FindFirstNegative(int[] arr)
        {
            if (IsEmptyArray(arr))
            {
                return;
            }
            // отрицательного элемента может и не быть
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < 0)
                {
                    Console.WriteLine($"Найдено: {arr[i]}, потребовалось сравнений {i + 1}");
                    return;
                }
            }
            Console.WriteLine($"Не найдено, потребовалось сравнений {arr.Length}");
            return;
        }

        static int[] InsertionSort(int[] arr)
        {
            if (IsEmptyArray(arr))
            {
                return arr;
            }
            int j, el;
            for (int i = 1; i < arr.Length; i++)
            {
                el = arr[i];
                j = i - 1;
                while (j >= 0 && el < arr[j])
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = el;
            }
            return arr;
        }

        class BinarySearchResult
        {
            public int index;
            public int count;
        }
        static BinarySearchResult BinarySearch(int[] array, int searchedValue, int left, int right)
        {
            BinarySearchResult result = new BinarySearchResult();
            result.index = -1;
            result.count = 0;
            // пока не сошлись границы массива
            while (left <= right)
            {
                // индекс среднего элемента
                int middle = (left + right) / 2;
                result.count += 1;

                if (searchedValue == array[middle])
                {
                    result.index = middle;
                    return result;
                }
                else if (searchedValue < array[middle])
                {
                    // сужаем рабочую зону массива с правой стороны
                    right = middle - 1;
                }
                else
                {
                    // сужаем рабочую зону массива с левой стороны
                    left = middle + 1;
                }
            }
            // ничего не нашли
            return result;
        }

        static void BinarySearchFromKeyboard(int[] arr)
        {
            if (IsEmptyArray(arr)) return;

            // сначала отсортировать массив
            arr = InsertionSort(arr);
            int numberToFind = IntegerInput("Какое число нужно найти?");
            BinarySearchResult found = BinarySearch(arr, numberToFind, 0, arr.Length - 1);

            Console.Write("Массив: ");
            PrintArray(arr);
            if (found.index == -1)
            {
                Console.WriteLine("Элемент не найден");
            }
            else
            {
                Console.WriteLine($"Номер элемента {found.index + 1}, потребовалось сравнений {found.count}");
            }
        }

        static void Main(string[] args)
        {
            int answer;
            int[] arr = [];
            do
            {
                answer = IntegerInput("1. Создать массив\n2. Печать массива\n3. Удалить элементы с четными номерами\n4. Добавить k элементов в массив\n5. Поменять местами минимальный и максимальный элементы\n6. Найти первый отрицательный элемент\n7. Сортировка методом простого включения\n8. Найти элемент в отсортированном массиве\n0. Выход\nВведите, чтобы выбрать:");

                switch (answer)
                {
                    case 1:
                        arr = CreateArray();
                        break;
                    case 2:
                        PrintArray(arr);
                        break;
                    case 3:
                        arr = DeleteEvenNumbersFromArray(arr);
                        break;
                    case 4:
                        arr = AddKElementsToTheEnd(arr);
                        break;
                    case 5:
                        arr = ReplaceMinAndMax(arr);
                        break;
                    case 6:
                        FindFirstNegative(arr);
                        break;
                    case 7:
                        arr = InsertionSort(arr);
                        break;
                    case 8:
                        BinarySearchFromKeyboard(arr);
                        break;
                    case 0: // это нужно, чтобы не выполнялся default при выходе из программы
                        break;
                    default:
                        Console.WriteLine("Выберите из списка:");
                        break;
                }
            }
            while (answer != 0);
        }
    }
}
