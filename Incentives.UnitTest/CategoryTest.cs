using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incentives.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;
using Incentives.ViewModel;

namespace Incentives.UnitTest
{
    [TestClass]
    public class CategoryTest
    {
        private Community _community;
        private Company _company;
        private CategoryGenerator _categoryGenerator;

        [TestInitialize]
        public async Task Initialize()
        {
            _community = new Community(new MemoryStorageStrategy())
                .Register<CorrespondenceModel>();

            _company = await _community.AddFactAsync(new Company("improvingEnterprises"));
            _categoryGenerator = new CategoryGenerator(_community, _company);
            await _categoryGenerator.Generate();
        }

        [TestMethod]
        public void GenerateCategories()
        {
            IEnumerable<Category> categories = _company.Categories
                .OrderBy(c => c.Ordinal.Value);

            Assert.AreEqual(12, categories.Count());
            Assert.AreEqual("accountManagement", categories.First().Identifier);
            Assert.AreEqual("Account Management", categories.First().Description.Value);
        }

        [TestMethod]
        public void GenerateActivityDefinitions()
        {
            IEnumerable<ActivityDefinition> definitions = _company.Categories
                .OrderBy(c => c.Ordinal.Value)
                .First()
                .Activities
                .OrderBy(a => a.Ordinal.Value);

            Assert.AreEqual(9, definitions.Count());
            Assert.AreEqual("qualifiedContact", definitions.First().Identifier);
            Assert.AreEqual("Qualified Contact", definitions.First().Description.Value);
            Assert.AreEqual("contact", definitions.First().Qualifier.Value);
        }
    }
}
