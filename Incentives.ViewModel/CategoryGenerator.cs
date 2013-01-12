using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Incentives.Model;
using UpdateControls.Correspondence;

namespace Incentives.ViewModel
{
    public class CategoryGenerator
    {
        private static readonly Regex Word = new Regex(@"\w+");

        private readonly Community _community;
        private readonly Company _company;

        private List<Category> _categories;

        private static readonly string[] RawCategories =
        {
            "Account Management",
            "Business Development",
            "Certification/Recognition",
            "Direct Revenue",
            "Education/Coaching",
            "Industry Contribution/Leadership",
            "Industry Participation",
            "Networking",
            "Operations Support",
            "Recruiting",
            "Sales/Marketing Support",
            "Other"
        };

        public CategoryGenerator(Community community, Company company)
        {
            _community = community;
            _company = company;
        }

        public async Task Generate()
        {
            _categories = new List<Category>();
            foreach (var c in RawCategories)
            {
                var category = await _community.AddFactAsync(new Category(
                    _company,
                    IdentifierOf(c)));

                _categories.Add(category);
                category.Description = c;
                category.Ordinal = _categories.Count;
            }
        }

        public IEnumerable<Category> Categories
        {
            get { return _categories; }
        }

        private string IdentifierOf(string str)
        {
            var words = Word.Matches(str)
                .OfType<Match>()
                .Select(match => match.Value)
                .ToList();

            var segments = words.Take(1).Select(s => s.ToLower())
                .Concat(words.Skip(1).Select(s => InitialCaps(s)))
                .ToArray();
            return String.Join("", segments);
        }

        private string InitialCaps(string segment)
        {
            return segment.Substring(0, 1).ToUpper() + segment.Substring(1).ToLower();
        }
    }
}
