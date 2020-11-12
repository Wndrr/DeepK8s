using System;
using System.Threading.Tasks;
using Desktop.Services;
using Xunit;

namespace UnitTests.Services
{
    public class SelectedNamespacesStateTest
    {
        #region BOILERPLATE

        private static SelectedNamespacesState Construct()
        {
            var state = new SelectedNamespacesState();
            return state;
        }

        #endregion

        #region TOGGLE

        [Fact]
        public void Toggle_nullParameter_throws()
        {
            var state = Construct();

            Assert.Throws<ArgumentNullException>(() => state.Toggle(null));
        }
        
        [Fact]
        public async Task Toggle_default_removes_default()
        {
            var state = Construct();

            Assert.Contains("default", await state.ToList());
            state.Toggle("default");
            Assert.DoesNotContain("default", await state.ToList());
        }
        [Fact]
        public async Task Toggle_missing_adds_new()
        {
            var state = Construct();

            Assert.DoesNotContain("missingnamespace", await state.ToList());
            state.Toggle("missingnamespace");
            Assert.Contains("missingnamespace", await state.ToList());
        }
        

        #endregion

        #region ADD RANGE

        [Fact]
        public void AddRange_nullParameter_throws()
        {
            var state = Construct();

            Assert.Throws<ArgumentNullException>(() => state.AddRange(null));
        }

        [Fact]
        public async Task AddRange_value_adds()
        {
            var state = Construct();
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
            var state = Construct();

            Assert.Throws<ArgumentNullException>(() => state.Add(null));
        }

        [Fact]
        public async Task Add_value_adds()
        {
            var state = Construct();
            state.Add("testnamespace");
            Assert.Contains("testnamespace", await state.ToList());
        }

        #endregion

        #region REMOVE

        [Fact]
        public void Remove_nullParameter_throws()
        {
            var state = Construct();

            Assert.Throws<ArgumentNullException>(() => state.Remove(null));
        }

        [Fact]
        public async Task Remove_value_removes()
        {
            var state = Construct();
            state.Remove("default");
            Assert.Empty(await state.ToList());
        }

        #endregion

        #region CLEAR

        [Fact]
        public async Task Clear_clears()
        {
            var state = Construct();
            state.Clear();
            Assert.Empty(await state.ToList());
        }

        #endregion
        
        #region CLEAR

        [Fact]
        public async Task Reset_to_default_from_default_gives_default()
        {
            var state = Construct();
            state.ResetToDefault();
            Assert.Contains("default", await state.ToList());
        }
        
        [Fact]
        public async Task Reset_to_default_from_non_default_gives_default()
        {
            var state = Construct();
            state.Add("notdefault");
            Assert.Contains("notdefault", await state.ToList());
            state.ResetToDefault();
            Assert.DoesNotContain("notdefault", await state.ToList());
            Assert.Contains("default", await state.ToList());
        }
        
        [Fact]
        public async Task Reset_to_default_from_empty_gives_default()
        {
            var state = Construct();
            state.Clear();
            Assert.Empty(await state.ToList());
            state.ResetToDefault();
            Assert.Contains("default", await state.ToList());
        }

        #endregion

        #region CONTAINS

        [Fact]
        public void Contains_has_default_value()
        {
            var state = Construct();
            Assert.True(state.Contains("default"));
        }

        [Fact]
        public void Contains_has_default_and_custom_after_add()
        {
            var state = Construct();
            state.Add("testnamespace");
            Assert.True(state.Contains("default"));
            Assert.True(state.Contains("testnamespace"));
        }

        [Fact]
        public void Contains_has_custom_but_not_default_after_add_and_remove()
        {
            var state = Construct();
            state.Add("testnamespace");
            state.Remove("default");
            Assert.False(state.Contains("default"));
            Assert.True(state.Contains("testnamespace"));
        }

        [Fact]
        public void Contains_not_has_default_after_remove()
        {
            var state = Construct();
            state.Remove("default");
            Assert.False(state.Contains("default"));
        }

        #endregion

        [Fact]
        public async Task default_value_by_default()
        {
            var state = Construct();
            Assert.Contains("default", await state.ToList());
        }
    }
}