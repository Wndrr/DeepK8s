using System;
using System.Threading.Tasks;
using Desktop.Services;
using Xunit;

namespace UnitTests.Services
{
    public class SelectedNamespacesStateTest
    {
        #region BOILERPLATE

        private static SelectedNamespacesState ConstructSelectedNamespacesStateObject()
        {
            var state = new SelectedNamespacesState();
            return state;
        }

        #endregion

        #region TOGGLE

        [Fact]
        public void Toggle_nullParameter_throws()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Throws<ArgumentNullException>(() => state.Toggle(null));
        }
        
        [Fact]
        public async Task Toggle_default_removes_default()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Contains("default", await state.ToList());
            state.Toggle("default");
            Assert.DoesNotContain("default", await state.ToList());
        }
        [Fact]
        public async Task Toggle_missing_adds_new()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.DoesNotContain("missingnamespace", await state.ToList());
            state.Toggle("missingnamespace");
            Assert.Contains("missingnamespace", await state.ToList());
        }
        

        #endregion

        #region ADD RANGE

        [Fact]
        public void AddRange_nullParameter_throws()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Throws<ArgumentNullException>(() => state.AddRange(null));
        }

        [Fact]
        public async Task AddRange_value_adds()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.AddRange(new[] {"testnamespace", "testnamespace2"});
            var listed = await state.ToList();
            Assert.Contains("testnamespace", listed);
            Assert.Contains("testnamespace2", listed);
        }

        #endregion

        #region ADD

        [Fact]
        public void Add_nullParameter_throws()
        {
            var state = ConstructSelectedNamespacesStateObject();

            Assert.Throws<ArgumentNullException>(() => state.Add(null));
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

        #region CONTAINS

        [Fact]
        public void Contains_has_default_value()
        {
            var state = ConstructSelectedNamespacesStateObject();
            Assert.True(state.Contains("default"));
        }

        [Fact]
        public void Contains_has_default_and_custom_after_add()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Add("testnamespace");
            Assert.True(state.Contains("default"));
            Assert.True(state.Contains("testnamespace"));
        }

        [Fact]
        public void Contains_has_custom_but_not_default_after_add_and_remove()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Add("testnamespace");
            state.Remove("default");
            Assert.False(state.Contains("default"));
            Assert.True(state.Contains("testnamespace"));
        }

        [Fact]
        public void Contains_not_has_default_after_remove()
        {
            var state = ConstructSelectedNamespacesStateObject();
            state.Remove("default");
            Assert.False(state.Contains("default"));
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