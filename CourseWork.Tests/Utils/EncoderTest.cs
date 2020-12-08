using CourseWork.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseWork.Tests.Utils
{
    [TestClass]
    public class EncoderTest
    {
        [TestMethod]
        public void NoExceptionTest1()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder();
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsFalse(exceptionOccured);
        }

        [TestMethod]
        public void NoExceptionTest2()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder("Ключ");
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsFalse(exceptionOccured);
        }

        [TestMethod]
        public void NoExceptionTest3()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder("NewKey", EncodingLanguage.English);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsFalse(exceptionOccured);
        }

        [TestMethod]
        public void ExceptionTest1()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder("NewKey");
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [TestMethod]
        public void ExceptionTest2()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder("Ключ", EncodingLanguage.English);
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [TestMethod]
        public void ExceptionTest3()
        {
            bool exceptionOccured = false;
            try
            {
                var encoder = new Encoder("Плохо12_й=ключ");
            }
            catch
            {
                exceptionOccured = true;
            }

            Assert.IsTrue(exceptionOccured);
        }

        [TestMethod]
        public void CorrectEncodingTest1()
        {
            var encoder = new Encoder("истина");
            var input = "Си шарп - лучший язык программирования!";
            string actual = encoder.Encode(input);
            var expected = "Ъъ киюп - фейбцй зщну эрчфгиъмсвбкннср!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectEncodingTest2()
        {
            var encoder = new Encoder("lie", EncodingLanguage.English);
            var input = "Питон - лучший язык программирования!";
            string actual = encoder.Encode(input);
            var expected = "Питон - лучший язык программирования!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectEncodingTest3()
        {
            var encoder = new Encoder("myKey", EncodingLanguage.English);
            var input = "C# is the best programming language!";
            string actual = encoder.Encode(input);
            var expected = "O# gc xfq zowr bpykpmkwmls jkregyqi!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectDecodingTest1()
        {
            var encoder = new Encoder("ключ");
            var input = "Ьф цчыы - йквджб йущв ъьмъылкдуьмщкщжц!";
            string actual = encoder.Decode(input);
            var expected = "Си шарп - лучший язык программирования!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectDecodingTest2()
        {
            var encoder = new Encoder("myKey", EncodingLanguage.English);
            var input = "Си шарп - лучший язык программирования!";
            string actual = encoder.Decode(input);
            var expected = "Си шарп - лучший язык программирования!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectDecodingTest3()
        {
            var encoder = new Encoder("myKey", EncodingLanguage.English);
            var input = "O# gc xfq zowr bpykpmkwmls jkregyqi!";
            string actual = encoder.Decode(input);
            var expected = "C# is the best programming language!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectEncodeDecode1()
        {
            var encoder = new Encoder("myKey", EncodingLanguage.English);
            var input = "C# is the best programming language!";
            string actual = encoder.Decode(encoder.Encode(input));
            var expected = "C# is the best programming language!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectEncodeDecode2()
        {
            var encoder = new Encoder("Ключ");
            var input = "C# is the best programming language!";
            string actual = encoder.Decode(encoder.Encode(input));
            var expected = "C# is the best programming language!";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CorrectEncodeDecode3()
        {
            var encoder = new Encoder("Истина");
            var input = "C# - мой любимый язык программирования!!!";
            string actual = encoder.Encode(encoder.Decode(input));
            var expected = "C# - мой любимый язык программирования!!!";

            Assert.AreEqual(expected, actual);
        }
    }
}
