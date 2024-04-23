using System.Collections.Generic;

namespace Birthday
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int humanscount = 25; // Изменение количества челов

            DateOnly[] human = new DateOnly[humanscount];
            for (int counter = 0; counter < humanscount; counter++)
            {
                human[counter] = DayGenerator(out DateOnly date);
                Console.WriteLine(human[counter]);
            }

            var duplicates = human.GroupBy(x => x)
                     .Where(g => g.Count() > 1)
                     .Select(g => new { Name = g.Key, Count = g.Count() })
                     .OrderByDescending(d => d.Count);

            if (duplicates.Any())
            {
                Console.WriteLine("Совпадения:");
                foreach (var duplicate in duplicates)
                {
                    Console.WriteLine($"Дата: {duplicate.Name}, Количество: {duplicate.Count}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Вероятность: " + CalculateProbability(duplicates.Count(), humanscount));
        }
        static double CalculateProbability(int duplicateCount, int totalCount)
        {
            double probability = 1.0 - Math.Exp(-duplicateCount * (duplicateCount - 1) / (2.0 * totalCount));
            return probability;
        }

        static DateOnly DayGenerator(out DateOnly date)
        {
            Random random = new Random();
            int D = random.Next(1, 32);
            int M = random.Next(1, 13);
            int Y = random.Next(2004, 2006);
            string datestr = $"{D}.{M}.{Y}";
            DateOnly.TryParse(datestr, out date);
            if (Convert.ToString(date) == "01.01.0001")
            {
                DayGenerator(out date);
                return date; 
            }
            else
            { 
                return date; 
            }
            
        }
    }
}