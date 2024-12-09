using System.Collections.Concurrent;

class Program
{
    // Функция для разложения числа на простые множители
    static List<int> PrimeFactors(int n)
    {
        var factors = new List<int>();
        int divisor = 2;
        while (n > 1)
        {
            while (n % divisor == 0)
            {
                factors.Add(divisor);
                n /= divisor;
            }
            divisor++;
        }
        return factors;
    }

    static void Main(string[] args)
    {
        Console.Write("Введите значение N: ");
        int N = int.Parse(Console.ReadLine());

        // Конкурентная очередь для хранения чисел
        var tasksQueue = new ConcurrentQueue<int>(Enumerable.Range(1, N));
        var results = new ConcurrentDictionary<int, List<int>>();

        // Определяем количество потоков
        int numThreads = Environment.ProcessorCount;

        // Создаем и запускаем задачи
        var tasks = new Task[numThreads];
        for (int i = 0; i < numThreads; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                while (tasksQueue.TryDequeue(out int number))
                {
                    var factors = PrimeFactors(number);
                    results[number] = factors;
                }
            });
        }

        // Ждем завершения всех задач
        Task.WaitAll(tasks);

        // Выводим результаты
        foreach (var number in Enumerable.Range(1, N))
        {
            Console.WriteLine($"{number}: {string.Join(" ", results[number])}");
        }
    }
}