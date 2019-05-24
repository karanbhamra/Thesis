using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BCrypt.Net;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters;

namespace BenchmarkUtility
{
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class HashVsBCryptHash
    {
        [Benchmark]
        public string DefaultSalt()
        {
            return Guid.NewGuid().ToString();
        }

        [Benchmark]
        public string BSalt11()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
        [Benchmark]
        public string BSalt12()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        [Benchmark]
        public string BSalt13()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(13);
        }

        [Benchmark]
        public string BSalt14()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(14);
        }

        [Benchmark]
        public string BSalt15()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(15);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            
            BenchmarkRunner.Run<HashVsBCryptHash>();
        }
    }

    public class Config : ManualConfig
    {
        public Config()
        {
            Add(CsvMeasurementsExporter.Default);
            Add(RPlotExporter.Default);
        }
    }
}
