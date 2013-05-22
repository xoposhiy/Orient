using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Orient;

namespace Testing
{
	class Program
	{
		private static readonly string TestBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
										  @"\..\..\base\more";

		static void Main(string[] args)
		{
//			Run180Criteria(Directory.GetDirectories(TestBase).SelectMany(Directory.GetFiles).ToArray());
			Run180Criteria(Directory.GetFiles(TestBase));
		}

		private static void Run180Criteria(string[] files)
		{
			int ok = 0;
			int wrong = 0;
			try
			{
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
				foreach (var file in files)
				{
					var state = new MainFormState(file);
					var orient = new Orient.Orient(state);
					var punctOk = orient.CountPattern(orient.CountPunctuation);
					var upperOk = orient.CountPattern(orient.CountUpperCase);
					state = new MainFormState(file);
					state.Rotate(180);
					orient = new Orient.Orient(state);
					var punctBad = orient.CountPattern(orient.CountPunctuation);
					var upperBad = orient.CountPattern(orient.CountUpperCase);

					Console.WriteLine("{0};{1};{2};{3};{4}", punctOk, punctBad, upperOk, upperBad, file);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
