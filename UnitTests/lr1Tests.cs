using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactorMethodLib;

namespace UnitTestProject1
{
    [TestClass]
    public class lr1Tests
    {
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