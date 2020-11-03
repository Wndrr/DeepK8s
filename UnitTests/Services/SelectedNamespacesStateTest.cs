using System;
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

        private static SelectedNamespacesState ConstructSelectedNamespacesStateObject()
        {
            var ss = new Mock<ISessionStorageService>();
            var state = new SelectedNamespacesState();
            return state;
        }

        [Fact]
        public void Add_value_adds()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Add("testnamespace");
            Assert.Contains("testnamespace", state.ToList());
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
        public void Remove_value_removes()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Remove("default");
            Assert.Empty(state.ToList());
        }

        #endregion

        #region CLEAR

        [Fact]
        public void Clear_clears()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Clear();
            Assert.Empty(state.ToList());
        }

        #endregion

        [Fact]
        public void default_value_by_default()
        {
            var state = ConstructSelectedNamespacesStateObject();
            Assert.Contains("default", state.ToList());
        }
    }
}