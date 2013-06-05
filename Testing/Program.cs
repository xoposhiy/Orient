using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Orient;

namespace Testing
{
	class Program
	{
        private static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private static readonly string TestBase = AssemblyPath + @"\..\..\base";
	    private static readonly string Output = AssemblyPath + @"\..\..\Contour\Resources\pattern.csv";

	    static void Main(string[] args)
		{
			Run180Criteria2(Directory.GetDirectories(TestBase).SelectMany(Directory.GetFiles).ToArray());
//			Run180Criteria(Directory.GetFiles(TestBase));
		}

		private static void Run180Criteria(string[] files)
		{
			int ok = 0;
			int wrong = 0;
			try {
                foreach (var file in files)
				{
					Console.Write(file.PadRight(60, ' ') + "  ");
					var state = new MainFormState(file);
					if (state.Criteria180()) ok++;
					else wrong++;
					state = new MainFormState(file);
					state.Rotate(180);
					if (state.Criteria180()) wrong++;
					else ok++;
					Console.WriteLine("OK {0}; WRONG {1}", ok, wrong);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		private static void Run180Criteria2(string[] files)
		{
			try
			{
                var writer = new StreamWriter(Output, false);
                foreach (var file in files)
				{
					var state = new MainFormState(file);
					var orient = new AlgorithmExecutor(state);
					var punctOk = orient.CountPattern(orient.CountPunctuation);
					var upperOk = orient.CountPattern(orient.CountUpperCase);
				    var quatOk = orient.CountPattern(orient.CountQuotations);
					state = new MainFormState(file);
					state.Rotate(180);
					orient = new AlgorithmExecutor(state);
					var punctBad = orient.CountPattern(orient.CountPunctuation);
					var upperBad = orient.CountPattern(orient.CountUpperCase);
                    var quatBad = orient.CountPattern(orient.CountQuotations);

				    var result = string.Format("{0};{1};{2};{3};{4};{5};{6}", punctOk, punctBad, upperOk, upperBad, quatOk, quatBad, file);
				    writer.WriteLine(result);
                    Console.WriteLine(result);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
