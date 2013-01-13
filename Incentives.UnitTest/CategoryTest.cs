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
        private Quarter _quarter;
        private CategoryGenerator _categoryGenerator;

        [TestInitialize]
        public async Task Initialize()
        {
            _community = new Community(new MemoryStorageStrategy())
                .Register<CorrespondenceModel>();

            _company = await _community.AddFactAsync(new Company("improvingEnterprises"));
            _quarter = await _community.AddFactAsync(new Quarter(_company, new DateTime(2013, 1, 1)));
            _categoryGenerator = new CategoryGenerator(_community, _company, _quarter);
            await _categoryGenerator.GenerateAsync();
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
        public async Task GenerateActivityDefinitions()
        {
            IEnumerable<ActivityDefinition> definitions = _company.Categories
                .OrderBy(c => c.Ordinal.Value)
                .First()
                .Activities
                .OrderBy(a => a.Ordinal.Value);

            Assert.AreEqual(9, definitions.Count());

            ActivityDefinition activityDefinition = definitions.First();
            Assert.AreEqual("qualifiedContact", activityDefinition.Identifier);
            Assert.AreEqual("Qualified Contact", activityDefinition.Description.Value);
            Assert.AreEqual("contact", activityDefinition.Qualifier.Value);

            var activityReward = await _community.FindFactAsync(new ActivityReward(activityDefinition, _quarter));
            Assert.AreEqual(1, activityReward.Points.Value);
        }

        [TestMethod]
        public async Task IdentifyStarActivities()
        {
            var category = await _community.FindFactAsync(new Category(_company, "industryContributionLeadership"));
            var activity = await _community.FindFactAsync(new ActivityDefinition(category, "openSourceDevelopment"));

            Assert.AreEqual("point", activity.Qualifier.Value);

            var activityReward = await _community.FindFactAsync(new ActivityReward(activity, _quarter));
            Assert.AreEqual(1, activityReward.Points.Value);
        }

        [TestMethod]
        public async Task IdentifyUnqualifiedActivities()
        {
            var category = await _community.FindFactAsync(new Category(_company, "certificationRecognition"));
            var activity = await _community.FindFactAsync(new ActivityDefinition(category, "scmAgile"));

            Assert.IsNull(activity.Qualifier.Value);

            var activityReward = await _community.FindFactAsync(new ActivityReward(activity, _quarter));
            Assert.AreEqual(10, activityReward.Points.Value);
        }
    }
}