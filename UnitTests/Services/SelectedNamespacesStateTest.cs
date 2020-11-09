using System;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Desktop.Services;
using Moq;
using Xunit;

namespace UnitTests.Services
{
    public class SelectedNamespacesStateTest
    {
        #region ADD

        [Fact]
        public void Add_nullParameter_throws()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Throws<ArgumentNullException>(() => state.Add(null));
        }

        private static FusionSelectedNamespacesState ConstructSelectedNamespacesStateObject()
        {
            var state = new FusionSelectedNamespacesState();
            return state;
        }

        [Fact]
        public async Task Add_value_adds()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Add("testnamespace");
            Assert.Contains("testnamespace", await state.ToList());
        }

        #endregion

        #region REMOVE

        [Fact]
        public void Remove_nullParameter_throws()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Throws<ArgumentNullException>(() => state.Remove(null));
        }

        [Fact]
        public async Task Remove_value_removes()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Remove("default");
            Assert.Empty(await state.ToList());
        }

        #endregion

        #region CLEAR

        [Fact]
        public async Task Clear_clears()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Clear();
            Assert.Empty(await state.ToList());
        }

        #endregion

        [Fact]
        public async Task default_value_by_default()
        {
            var state = ConstructSelectedNamespacesStateObject();
            Assert.Contains("default", await state.ToList());
        }
    }
}