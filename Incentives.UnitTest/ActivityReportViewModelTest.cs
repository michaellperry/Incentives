using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Incentives.Model;
using Incentives.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Memory;

namespace Incentives.UnitTest
{
    [TestClass]
    public class ActivityReportViewModelTest
    {
        private Community _community;
        private Profile _profile;

        private ActivityReportViewModel _viewModel;

        [TestInitialize]
        public async Task Initialize()
        {
            _community = new Community(new MemoryStorageStrategy())
                .Register<CorrespondenceModel>();

            _profile = await _community.AddFactAsync(new Profile());
            _profile.Name = "Michael Perry";

            var company = await _community.AddFactAsync(new Company("improvingenterprises"));
            var quarter = await _community.AddFactAsync(new Quarter(company, new DateTime(2012, 7, 1)));

            var industryContributionLeadership = await _community.AddFactAsync(new Category(company, "Industry Contribution/Leadership"));
            var leadUserGroup = await _community.AddFactAsync(new ActivityDefinition(industryContributionLeadership, "Lead a user group", "mtg"));
            var leadUserGroupReward = await _community.AddFactAsync(new ActivityReward(leadUserGroup, quarter));
            leadUserGroupReward.Points = 3;
            var presentationUserGroup = await _community.AddFactAsync(new ActivityDefinition(industryContributionLeadership, "Presentation - user group", "presentation"));
            var presentationUserGroupReward = await _community.AddFactAsync(new ActivityReward(presentationUserGroup, quarter));
            presentationUserGroupReward.Points = 10;

            var certificationRecognition = await _community.AddFactAsync(new Category(company, "Certification/Recognition"));
            var mvp = await _community.AddFactAsync(new ActivityDefinition(certificationRecognition, "Microsoft MVP", ""));
            var mvpReward = await _community.AddFactAsync(new ActivityReward(mvp, quarter));
            mvpReward.Points = 50;

            var profileQuarter = await _community.AddFactAsync(new ProfileQuarter(_profile, quarter));

            await _community.AddFactAsync(new Activity(profileQuarter, leadUserGroupReward, new DateTime(2012, 9, 4), "Dallas XAML User Group", 1));
            await _community.AddFactAsync(new Activity(profileQuarter, mvpReward, new DateTime(2012, 7, 1), "", 1));
            await _community.AddFactAsync(new Activity(profileQuarter, presentationUserGroupReward, new DateTime(2012, 9, 4), "Dallas XAML User Group", 1));

            _viewModel = new ActivityReportViewModel(_profile);
        }

        [TestMethod]
        public void ActivityReport_HasName()
        {
            string name = _viewModel.Name;

            Assert.AreEqual("Michael Perry", name);
        }

        [TestMethod]
        public void ActivityReport_ListsActivities()
        {
            List<ActivityViewModel> activities = _viewModel.Activities.ToList();

            Assert.AreEqual(3, activities.Count);

            Assert.AreEqual(new DateTime(2012, 07, 01), activities[0].Date);
            Assert.AreEqual("Certification/Recognition", activities[0].Category);
            Assert.AreEqual("Microsoft MVP", activities[0].Activity);
            Assert.AreEqual("50", activities[0].PointsAvailable);
            Assert.AreEqual(50, activities[0].Amount);

            Assert.AreEqual(new DateTime(2012, 09, 04), activities[1].Date);
            Assert.AreEqual("Industry Contribution/Leadership", activities[1].Category);
            Assert.AreEqual("Presentation - user group", activities[1].Activity);
            Assert.AreEqual("Dallas XAML User Group", activities[1].Description);
            Assert.AreEqual("10/presentation", activities[1].PointsAvailable);
            Assert.AreEqual(10, activities[1].Amount);

            Assert.AreEqual(new DateTime(2012, 09, 04), activities[2].Date);
            Assert.AreEqual("Industry Contribution/Leadership", activities[2].Category);
            Assert.AreEqual("Lead a user group", activities[2].Activity);
            Assert.AreEqual("Dallas XAML User Group", activities[2].Description);
            Assert.AreEqual("3/mtg", activities[2].PointsAvailable);
            Assert.AreEqual(3, activities[2].Amount);
        }
	}
}
