using Microsoft.AspNetCore.Http;
using Moq;

namespace ProjectHub.Tests.Mocks
{
    public static class FormFileMock
    {
        public static FormFile InstanceWithLengthLessThanTwoMegaBytes
        {
            get
            {
                var formfile = new Mock<FormFile>();

                formfile
                    .Setup(f => f.Length)
                    .Returns(0);

                return formfile.Object;
            }
        }
        public static FormFile InstanceWithLengthMoreThanTwoMegaBytes
        {
            get
            {
                var formfile = new Mock<FormFile>();

                formfile
                    .Setup(f => f.Length)
                    .Returns(2100000);

                return formfile.Object;
            }
        }
    }
}
