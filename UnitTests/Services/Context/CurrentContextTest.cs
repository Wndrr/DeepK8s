using System.Threading.Tasks;
using Desktop.Services.Context;
using Xunit;

namespace UnitTests.Services.Context
{
    public class CurrentContextTest
    {
        #region BOILERPLATE

        private static CurrentContext Construct()
        {
            var state = new CurrentContext();
            return state;
        }

        #endregion

        #region SET

        [Fact]
        public async Task Set_changes_get()
        {
            var state = Construct();
            state.Set("current");
            Assert.Equal("current",await state.Get());
        }

        #endregion
    }
}