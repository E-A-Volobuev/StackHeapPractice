using BenchmarkDotNet.Attributes;
using System.Diagnostics;

namespace StackHeapPractice;

[MemoryDiagnoser]
public class CounterTest
{
    private const int Operations = 100_000;

    // Структура и класс для сравнения
    public struct CounterStruct { public int Value; }
    public class CounterClass { public int Value; }

    [Benchmark(Baseline = true)]
    public int StackInt()
    {
        int value = 0;
        for (int i = 0; i < Operations; i++) value++;
        return value;
    }

    [Benchmark]
    public int StackStruct()
    {
        CounterStruct counter = new CounterStruct();
        for (int i = 0; i < Operations; i++) counter.Value++;
        return counter.Value;
    }

    [Benchmark]
    public int HeapClass()
    {
        CounterClass counter = new CounterClass();
        for (int i = 0; i < Operations; i++) counter.Value++;
        return counter.Value;
    }

    [Benchmark]
    public int HeapClassReused()
    {
        // Локальная переменная, а не поле класса
        CounterClass counter = new CounterClass();
        counter.Value = 0;
        for (int i = 0; i < Operations; i++) counter.Value++;
        return counter.Value;
    }
}
