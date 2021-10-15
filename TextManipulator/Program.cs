using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;


namespace TextManipulator
{
    class Program
    {
        static string text = Properties.Resources.Last_Wish;
        static string[] separatingStrings = { "\n", " ", ",", ".", "?", "-", "!", ":", "...", "	", "\t", "	—", "\"", "—", "\r" };
        static string[] words = text.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);


        static void WordCount()
        {
            Console.WriteLine("Брой думи: " + words.Length);
        }

        static void ShortestWord()
        {
            string result = words[0];
            foreach (var word in words)
            {
                if (word.Length < 4)
                {
                    continue;
                }
                if (word.Length < result.Length)
                {
                    result = word;
                }
                if (result.Length == 4)
                {
                    break;
                }
            }
            Console.WriteLine("Най-къса дума: " + result); 
        }

        static void LongestWord()
        {
            string result = words[0];
            foreach (var word in words)
            {
                if (word.Length > result.Length)
                {
                    result = word;
                }
            }
            Console.WriteLine("Най-дълга дума: " + result);
        }

        static void AvgWordLength()
        {
            double result = 0;
            foreach (var word in words)
            {
                result += word.Length;
            }
            result /= words.Length;
            result = Math.Round(result, 2);
            Console.WriteLine("Средна дължина на думите: " + result); 
        }
        static void FiveMostCommonWords()
        {
            string text = "";
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (word.Length < 4)
                {
                    continue;
                }
                if (list.ContainsKey(word))
                {
                    list[word]++;
                }
                else
                {
                    list.Add(word, 1);
                }
            }

            text += "\nПетте най-често срещани думи: ";

            foreach (var item in list.OrderByDescending(l => l.Value).Take(5).Select(k => k.Key).ToArray())
            {

                text += "\n" + item;
            }

            Console.WriteLine(text);
        }

        static void FiveLeastCommonWords()
        {
            string text = "";
            Dictionary<string, int> list = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (list.ContainsKey(word))
                {
                    list[word]++;
                }
                else
                {
                    list.Add(word, 1);
                }
            }

            text += "\nПетте най-рядко срещани думи: ";
            
            foreach (var item in list.OrderBy(l => l.Value).Take(5).Select(k => k.Key).ToArray())
            {
                text +="\n" + item;
            }

            Console.WriteLine(text);
        }



        static void Main(string[] args)
        {
            
            Stopwatch time1 = new Stopwatch();
            Stopwatch time2 = new Stopwatch();

            time1.Start();

            WordCount();
            ShortestWord();
            LongestWord();
            AvgWordLength();
            FiveMostCommonWords();
            FiveLeastCommonWords();

            time1.Stop();

            TimeSpan timeSpan = time1.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            

            List<Thread> threads = new List<Thread>();
            time2.Start();
            Thread t1 = new Thread(WordCount);
            threads.Add(t1);
            t1.Start();
            
            Thread t2 = new Thread(ShortestWord);
            threads.Add(t2);
            t2.Start();
            
            Thread t3 = new Thread(LongestWord);
            threads.Add(t3);
            t3.Start();
            
            Thread t4 = new Thread(AvgWordLength);
            threads.Add(t4);
            t4.Start();
            
            Thread t5 = new Thread(FiveMostCommonWords);
            threads.Add(t5);
            t5.Start();
            
            Thread t6 = new Thread(FiveLeastCommonWords);
            threads.Add(t6);
            t6.Start();
            

            foreach (var thread in threads)
            {
                thread.Join();
            }
   
            time2.Stop();
            timeSpan = time2.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
            timeSpan.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

        }
    }
}
