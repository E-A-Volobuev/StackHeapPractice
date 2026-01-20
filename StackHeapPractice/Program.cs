using BenchmarkDotNet.Running;
using StackHeapPractice;
using System.Diagnostics;

Console.WriteLine("=== ПРОСТОЕ СРАВНЕНИЕ: СТЕК vs КУЧА ===\n");

const int Operations = 1_000_000; // 10 миллионов операций

// ----------------------------------------------------
// ВАРИАНТ 1: Работаем со стеком (int - value type)
// ----------------------------------------------------
Console.WriteLine("1. СТЕК (int):");
int stackNumber = 0; 

var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < Operations; i++)
{
    stackNumber++; 
}
stopwatch.Stop();

Console.WriteLine($"Время: {stopwatch.ElapsedMilliseconds} мс");
Console.WriteLine($"Результат: {stackNumber}\n");

// ----------------------------------------------------
// ВАРИАНТ 2: Работаем с кучей (объект - reference type)
// ----------------------------------------------------
Console.WriteLine("2. КУЧА (класс):");

Counter heapCounter = new Counter(); 

// Дадим GC собрать мусор перед замером
GC.Collect();

stopwatch.Restart();
for (int i = 0; i < Operations; i++)
{
    heapCounter.Value++; // Идем по ссылке в кучу
}
stopwatch.Stop();

Console.WriteLine($"Время: {stopwatch.ElapsedMilliseconds} мс");
Console.WriteLine($"Результат: {heapCounter.Value}\n");

BenchmarkRunner.Run<CounterTest>();
