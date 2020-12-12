using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsLab1
{
	public static class Refactor
	{
		public static string RenameVariable(string code, string oldVar, string newVar)
		{
			string newCode = "";
			string[] lines = code.Split('\n');
			bool multiLineComment = false;
			bool singleLineComment;
			string[] divLine = null;
			string line;
			for (int i = 0; i < lines.Length; i++)
			{
				line = lines[i];
				if (multiLineComment)
				{
					int endOfCom = line.IndexOf("*/");
					if (endOfCom != -1)
					{
						char[] separator = { '/' };
						divLine = line.Split(separator, 2);
						line = divLine[1];
						multiLineComment = false;
						newCode += divLine[0] + '/';
					}
					else
						newCode += line;
				}

				if (!multiLineComment)
				{
					singleLineComment = false;
					// Відітнути коментар, якщо є
					int indOfS = line.IndexOf("//");
					int indOfM = line.IndexOf("/*");
					if (indOfS != -1 || indOfM != -1)
					{
						char[] separator = { '/' };
						divLine = line.Split(separator, 2);
						line = divLine[0];

						if (indOfS != -1 && indOfM != -1)
						{
							if (indOfS < indOfM)
								singleLineComment = true;
							else
								multiLineComment = true;
						}
						else if (indOfS != -1)
							singleLineComment = true;
						else
							multiLineComment = true;
					}

					int varInd = 0;
					int qmInd;
					do
					{
						varInd = line.IndexOf(oldVar, varInd);
						qmInd = line.IndexOf('"');
						if (qmInd == -1) qmInd = line.Length;

						if (varInd != -1)
						{
							if (varInd < qmInd)
							{// Якщо стара назва не є частиною назви іншої змінної
								char before = (varInd > 0) ? line[varInd - 1] : '\0';
								char after = (varInd + oldVar.Length < line.Length) ? line[varInd + oldVar.Length] : '\0';
								if (!Char.IsLetterOrDigit(before) && !Char.IsLetterOrDigit(after) && before != '_' && after != '_')
								{
									// Видаляємо стару назву
									line = line.Remove(varInd, oldVar.Length);
									// Вставляємо нову назву
									line = line.Insert(varInd, newVar);
								}
								varInd++;
							}
							else
							{
								qmInd = line.IndexOf('"', qmInd + 1);
								varInd = qmInd;
								qmInd = line.IndexOf('"', qmInd + 1);
								if (qmInd == -1) qmInd = line.Length;
							}
						}

					} while (varInd != -1);

					newCode += line;
					if (singleLineComment)
						newCode += "/" + divLine[1];
					if (multiLineComment)
						newCode += "/" + divLine[1];
				}
				if (i != lines.Length - 1)
					newCode += "\n";
			}
			return newCode;
		}
		//======================= Саня ============================
		#region Саня
		public static string AddParameter(string code, string funcName, string newParameter)
		{
			StringBuilder newCode = new StringBuilder();

			string[] lines = code.Split('\n');

			foreach (var line in lines)
			{
				int firstIndexOfCommentary = code.IndexOf("/*");
				int secondIndexOfCommentary = code.IndexOf("*/");
				int lineIndex = code.IndexOf(line);
				bool IsCommented = lineIndex > firstIndexOfCommentary && lineIndex < secondIndexOfCommentary;
				if (line.Contains(funcName) && !line.Contains("//") && !line.Contains(';') && !IsCommented)
				{
					int indexAfterFuncName = line.IndexOf(funcName) + funcName.Length;
					int indexOfLeftBracket = line.IndexOf('(', indexAfterFuncName);
					int indexOfRightBracket = line.IndexOf(')', indexAfterFuncName);
					if (indexOfRightBracket - indexOfLeftBracket > 1)
					{
						newCode.Append(line.Insert(indexOfRightBracket, ',' + newParameter));
						newCode.Append('\n');
					}
					else
					{
						newCode.Append(line.Insert(indexOfLeftBracket + 1, newParameter));
						newCode.Append('\n');
					}
				}
				else
				{
					newCode.Append(line);
					newCode.Append('\n');
				}
			}
			int lastInd = newCode.ToString().LastIndexOf('\n');
			newCode.Remove(lastInd, 1);
			return newCode.ToString();
		}
		#endregion
	}
}
