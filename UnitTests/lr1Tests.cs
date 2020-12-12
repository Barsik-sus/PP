using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorMethodLib;
using WinFormsLab1;

namespace UnitTestProject1
{
    [TestClass]
    public class lr1Tests
    {
		// ============ Тести для додавання параметра ===========
		#region Саня
		[TestMethod]
		public void TestAddingParameter1()
		{
			string result = Refactor.AddParameter(
				"void Method(){}"
				, "Method", "string a");
			Assert.AreEqual("void Method(string a){}", result);
		}

		[TestMethod]
		public void TestAddingParameter2()
		{
			string result = Refactor.AddParameter(
				"void Method(){}" +
				"void AnotherMethod(){}"
				, "Method", "string a");
			Assert.AreEqual(
				"void Method(string a){}" +
				"void AnotherMethod(){}"
				, result);
		}

		[TestMethod]
		public void TestAddingParameter3()
		{
			string result = Refactor.AddParameter(
				"void Method(int a){}"
				,
				"Method", "string b");
			Assert.AreEqual(
				"void Method(int a,string b){}"
				, result);
		}

		[TestMethod]
		public void TestAddingParameter4()
		{
			string cppCode = @"
			// void method() {}
			";
			string result = Refactor.AddParameter(cppCode, "method", "double num");
			Assert.AreEqual(cppCode, result);
		}

		[TestMethod]
		public void TestAddingParameter5()
		{
			string cppCode = @"
			// void method() {}
			void method() {}
			";

			string newCppCode = @"
			// void method() {}
			void method(string a) {}
			";

			string result = Refactor.AddParameter(cppCode, "method", "string a");
			Assert.AreEqual(newCppCode, result);

		}

		[TestMethod]
		public void TestAddingParameter6()
		{
			string cppCode = @"
			string method1() {}
			void method() {
				string name = method1();
			}
			";

			string newCppCode = @"
			string method1(int num=0) {}
			void method() {
				string name = method1();
			}
			";

			string result = Refactor.AddParameter(cppCode, "method1", "int num=0");
			Assert.AreEqual(newCppCode, result);
		}

		[TestMethod]
		public void TestAddingParameter7()
		{
			string cppCode = @"
			if(a == b){
			 someMethod();
			}
			";

			string newCppCode = @"
			if(a == b){
			 someMethod();
			}
			";

			string result = Refactor.AddParameter(cppCode, "someMethod", "string a");
			Assert.AreEqual(newCppCode, result);
		}

		[TestMethod]
		public void TestAddingParameter8()
		{
			string cppCode = @"
			/* 
			int ReturnInt() {}
			*/

			double ReturnDouble() {}

			";

			string newCppCode = @"
			/* 
			int ReturnInt() {}
			*/

			double ReturnDouble() {}

			";

			string result = Refactor.AddParameter(cppCode, "ReturnInt", "int a");
			Assert.AreEqual(newCppCode, result);
		}

		[TestMethod]
		public void TestAddingParameter9()
		{
			string cppCode = @"
			
			class Human
			{
				public string SomeHumanMethod()
					{
					}
				private int SomeMethod()
					{
					}
			}

			";

			string newCppCode = @"
			
			class Human
			{
				public string SomeHumanMethod(string parameter)
					{
					}
				private int SomeMethod()
					{
					}
			}

			";
			string result = Refactor.AddParameter(cppCode, "SomeHumanMethod", "string parameter");
			Assert.AreEqual(newCppCode, result);
		}

		[TestMethod]
		public void TestAddingParameter10()
		{
			string cppCode = @"
			#include <iostream>
			#include <string>

			using namespace std;

			void check_pass ()
			{
				string valid_pass = ""qwerty123"";

				if (password == valid_pass)
				{
					cout << ""Доступ разрешен."" << endl;
				}
				else
				{
					cout << ""Неверный пароль!"" << endl;
				}
			}

			int main()
			{
			string user_pass;
			cout << ""Введите пароль: "";
			getline(cin, user_pass);
			check_pass(user_pass);
			return 0;
			}
			";
			string newCppCode = @"
			#include <iostream>
			#include <string>

			using namespace std;

			void check_pass (string password)
			{
				string valid_pass = ""qwerty123"";

				if (password == valid_pass)
				{
					cout << ""Доступ разрешен."" << endl;
				}
				else
				{
					cout << ""Неверный пароль!"" << endl;
				}
			}

			int main()
			{
			string user_pass;
			cout << ""Введите пароль: "";
			getline(cin, user_pass);
			check_pass(user_pass);
			return 0;
			}
			";
			string result = Refactor.AddParameter(cppCode, "check_pass", "string password");
			Assert.AreEqual(newCppCode, result);
		}
		#endregion
		// ============ Тести для перейменування змінної ===========
		#region Данил
		[TestMethod]
		public void TestRenameVariable1()
		{
			string res = Refactor.RenameVariable("int a=1;", "a", "Number");
			Assert.AreEqual("int Number=1;", res);
		}

		[TestMethod]
		public void TestRenameVariable2()
		{
			string res = Refactor.RenameVariable("int a=1; int b=a*a;", "a", "Number");
			Assert.AreEqual("int Number=1; int b=Number*Number;", res);
		}

		[TestMethod]
		public void TestRenameVariable3()
		{
			string res = Refactor.RenameVariable("int i=1;", "i", "Number");
			Assert.AreEqual("int Number=1;", res);
		}

		[TestMethod]
		public void TestRenameVariable4()
		{
			string res = Refactor.RenameVariable("int a=1,a1;", "a", "Number");
			Assert.AreEqual("int Number=1,a1;", res);
		}

		[TestMethod]
		public void TestRenameVariable5()
		{
			string res = Refactor.RenameVariable("int a=1; int mod = abs(a);", "a", "Number");
			Assert.AreEqual("int Number=1; int mod = abs(Number);", res);
		}

		[TestMethod]
		public void TestRenameVariable6()
		{
			string res = Refactor.RenameVariable("int a1 = 1; bool a = False;", "a", "Number");
			Assert.AreEqual("int a1 = 1; bool Number = False;", res);
		}

		[TestMethod]
		public void TestRenameVariable7()
		{
			string res = Refactor.RenameVariable("int b = 1; double b1 = 1.0;", "b", "Number");
			Assert.AreEqual("int Number = 1; double b1 = 1.0;", res);
		}

		[TestMethod]
		public void TestRenameVariable8()
		{
			string res = Refactor.RenameVariable("int s = 5; int mass[s] = {0};", "s", "Size");
			Assert.AreEqual("int Size = 5; int mass[Size] = {0};", res);
		}

		[TestMethod]
		public void TestRenameVariable9()
		{
			string res = Refactor.RenameVariable("bool b = True; //b=123", "b", "Var");
			Assert.AreEqual("bool Var = True; //b=123", res);
		}

		[TestMethod]
		public void TestRenameVariable10()
		{
			string res = Refactor.RenameVariable("int a1 = 1; string a = \"a is\";", "a", "Number");
			Assert.AreEqual("int a1 = 1; string Number = \"a is\";", res);
		}
		#endregion

		[TestMethod]
        public void DelParam_ParamOne_RemovedParam()
        {
            string str = "void Test(int param)";
            string method = "Test";
            string param = "param";
            string expected = "void Test()";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamsMultiple_RemovedCorrectParametr()
        {
            string str = "void Test(int desiredParam, double x)";
            string method = "Test";
            string param = "desiredParam";
            string expected = "void Test( double x)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamWithDefaultValue_RemovedParametrAndValue()
        {
            string str = "void Test(int param = 12, double x)";
            string method = "Test";
            string param = "param";
            string expected = "void Test( double x)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamWithCustomType_AllSymbolOfTypeRemoved()
        {
            string str = "void Test(std::string&&* param, double x)";
            string method = "Test";
            string param = "param";
            string expected = "void Test( double x)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamWithConst_ConstRemovedToo()
        {
            string str = "void Test(const char* param, double x)";
            string method = "Test";
            string param = "param";
            string expected = "void Test( double x)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamBetween_OnlyDesiredParamRemoved()
        {
            string str = "void Test(Obj obj, int desiredParam, double x)";
            string method = "Test";
            string param = "desiredParam";
            string expected = "void Test(Obj obj, double x)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_ParamWrong_UnchangedString()
        {
            string str = "void Test(int param)";
            string method = "Test";
            string param = "wrong";
            string expected = "void Test(int param)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_FuncWrong_UnchangedString()
        {
            string str = "void Test(int param)";
            string method = "Wrong";
            string param = "param";
            string expected = "void Test(int param)";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DelParam_Spacing_IgnoredSpaces()
        {
            string str = "void Test  ( int param , char x )";
            string method = "Test";
            string param = "param";
            string expected = "void Test  ( char x )";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.DelParam(str, method, param);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_MethodOne_RenamedMethod()
        {
            string str = "f(int a,int b);";
            string method = "f";
            string new_name = "function";
            string expected = "function(int a,int b);";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_MethodsMultiple_RenamedOnlyDesiredMethod()
        {
            string str = "add_hp(30); remove_hp(15);";
            string method = "add_hp";
            string new_name = "add_cash";
            string expected = "add_cash(30); remove_hp(15);";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_NotFullMethodName_UnchangedString()
        {
            string str = "add_hp(30); remove_hp(15);";
            string method = "hp";
            string new_name = "add_cash";
            string expected = "add_hp(30); remove_hp(15);";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_VariableWithMethodName_RenamedOnlyMethod()
        {
            string str = "void add_hp(){int add_hp;}";
            string method = "add_hp";
            string new_name = "remove_hp";
            string expected = "void remove_hp(){int add_hp;}";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_MethodInConstString_RenamedOnlyMethod()
        {
            string str = "add_hp(30){cout<<\"add_hp()\"}; add_hp_bar();";
            string method = "add_hp";
            string new_name = "heal";
            string expected = "heal(30){cout<<\"add_hp()\"}; add_hp_bar();";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Rename_MethodWithSimilarName_RenamedOnlyCorrectMethod()
        {
            string str = "add_hp(30); add_hp_bar();";
            string method = "add_hp";
            string new_name = "heal";
            string expected = "heal(30); add_hp_bar();";

            RefactorMethod obj = new RefactorMethod();
            string actual = obj.Rename(str, method, new_name);

            Assert.AreEqual(expected, actual);
        }
    }
}