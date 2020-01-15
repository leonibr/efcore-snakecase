using System;
using Xunit;
using Shouldly;
using Marques.EFCore.SnakeCase;
namespace SnakeCaseTests
{
    public class FormatTests
    {
        [Theory(DisplayName ="Should allow")]
        [InlineData("PersonLogAuditTrail", "person_log_audit_trail")]
        [InlineData("Person_Log", "person_log")]
        [InlineData("_Person_Log", "person_log")]
        [InlineData("Person_1Log", "person_1_log")]
        [InlineData("Person1Log", "person1_log")]
        [InlineData("PersonLOGFile", "person_log_file")]
        [InlineData("PersonCodeId", "person_code_id")]
        [InlineData("PersonAddress_", "person_address_")]
        public void TestFramatNames(string given, string expcted)
        {
            given.ToSnakeCase().ShouldBe(expcted);
        }
    }
}
