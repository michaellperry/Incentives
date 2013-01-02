using System.Collections.Generic;
using System.Linq;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

/**
/ For use with http://graphviz.org/
digraph "Incentives.Model"
{
    rankdir=BT
    Profile__name -> Profile
    Profile__name -> Profile__name [label="  *"]
    Quarter -> Company
    Category -> Company
    ActivityDefinition -> Category
    ActivityReward -> ActivityDefinition
    ActivityReward -> Quarter
    ActivityReward__points -> ActivityReward
    ActivityReward__points -> ActivityReward__points [label="  *"]
    ProfileQuarter -> Profile
    ProfileQuarter -> Quarter
    Activity -> ProfileQuarter
    Activity -> ActivityReward
}
**/

namespace Incentives.Model
{
    public partial class Individual : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Individual newFact = new Individual(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Individual fact = (Individual)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Individual", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results

        // Business constructor
        public Individual(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Individual(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Profile : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Profile newFact = new Profile(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Profile fact = (Profile)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Profile", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries
        public static Query MakeQueryName()
		{
			return new Query()
				.JoinSuccessors(Profile__name.RoleProfile, Condition.WhereIsEmpty(Profile__name.MakeQueryIsCurrent())
				)
            ;
		}
        public static Query QueryName = MakeQueryName();
        public static Query MakeQueryActivities()
		{
			return new Query()
				.JoinSuccessors(ProfileQuarter.RoleProfile)
				.JoinSuccessors(Activity.RoleProfileQuarter)
            ;
		}
        public static Query QueryActivities = MakeQueryActivities();

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Profile__name> _name;
        private Result<Activity> _activities;

        // Business constructor
        public Profile(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Profile(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _name = new Result<Profile__name>(this, QueryName);
            _activities = new Result<Activity>(this, QueryActivities);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Activity> Activities
        {
            get { return _activities; }
        }

        // Mutable property access
        public TransientDisputable<Profile__name, string> Name
        {
            get { return _name.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _name.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Profile__name(this, _name, value.Value));
                    }
                };
                setter();
			}
        }

    }
    
    public partial class Profile__name : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Profile__name newFact = new Profile__name(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Profile__name fact = (Profile__name)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Profile__name", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleProfile = new Role(new RoleMemento(
			_correspondenceFactType,
			"profile",
			new CorrespondenceFactType("Incentives.Model.Profile", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("Incentives.Model.Profile__name", 1),
			false));

        // Queries
        public static Query MakeQueryIsCurrent()
		{
			return new Query()
				.JoinSuccessors(Profile__name.RolePrior)
            ;
		}
        public static Query QueryIsCurrent = MakeQueryIsCurrent();

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<Profile> _profile;
        private PredecessorList<Profile__name> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Profile__name(
            Profile profile
            ,IEnumerable<Profile__name> prior
            ,string value
            )
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, RoleProfile, profile);
            _prior = new PredecessorList<Profile__name>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private Profile__name(FactMemento memento)
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, RoleProfile, memento);
            _prior = new PredecessorList<Profile__name>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Profile Profile
        {
            get { return _profile.Fact; }
        }
        public IEnumerable<Profile__name> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Company : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Company newFact = new Company(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._identifier = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Company fact = (Company)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._identifier);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Company", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _identifier;

        // Results

        // Business constructor
        public Company(
            string identifier
            )
        {
            InitializeResults();
            _identifier = identifier;
        }

        // Hydration constructor
        private Company(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Quarter : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Quarter newFact = new Quarter(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._startDate = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Quarter fact = (Quarter)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._startDate);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Quarter", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("Incentives.Model.Company", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        private DateTime _startDate;

        // Results

        // Business constructor
        public Quarter(
            Company company
            ,DateTime startDate
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _startDate = startDate;
        }

        // Hydration constructor
        private Quarter(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public DateTime StartDate
        {
            get { return _startDate; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Category : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Category newFact = new Category(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._description = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Category fact = (Category)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._description);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Category", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCompany = new Role(new RoleMemento(
			_correspondenceFactType,
			"company",
			new CorrespondenceFactType("Incentives.Model.Company", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        private string _description;

        // Results

        // Business constructor
        public Category(
            Company company
            ,string description
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, company);
            _description = description;
        }

        // Hydration constructor
        private Category(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, RoleCompany, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Company Company
        {
            get { return _company.Fact; }
        }

        // Field access
        public string Description
        {
            get { return _description; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ActivityDefinition : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ActivityDefinition newFact = new ActivityDefinition(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._description = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
						newFact._qualifier = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ActivityDefinition fact = (ActivityDefinition)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._description);
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._qualifier);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityDefinition", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleCategory = new Role(new RoleMemento(
			_correspondenceFactType,
			"category",
			new CorrespondenceFactType("Incentives.Model.Category", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Category> _category;

        // Fields
        private string _description;
        private string _qualifier;

        // Results

        // Business constructor
        public ActivityDefinition(
            Category category
            ,string description
            ,string qualifier
            )
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, RoleCategory, category);
            _description = description;
            _qualifier = qualifier;
        }

        // Hydration constructor
        private ActivityDefinition(FactMemento memento)
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, RoleCategory, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Category Category
        {
            get { return _category.Fact; }
        }

        // Field access
        public string Description
        {
            get { return _description; }
        }
        public string Qualifier
        {
            get { return _qualifier; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ActivityReward : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ActivityReward newFact = new ActivityReward(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ActivityReward fact = (ActivityReward)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityReward", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleDefinition = new Role(new RoleMemento(
			_correspondenceFactType,
			"definition",
			new CorrespondenceFactType("Incentives.Model.ActivityDefinition", 1),
			false));
        public static Role RoleQuarter = new Role(new RoleMemento(
			_correspondenceFactType,
			"quarter",
			new CorrespondenceFactType("Incentives.Model.Quarter", 1),
			false));

        // Queries
        public static Query MakeQueryPoints()
		{
			return new Query()
				.JoinSuccessors(ActivityReward__points.RoleActivityReward, Condition.WhereIsEmpty(ActivityReward__points.MakeQueryIsCurrent())
				)
            ;
		}
        public static Query QueryPoints = MakeQueryPoints();

        // Predicates

        // Predecessors
        private PredecessorObj<ActivityDefinition> _definition;
        private PredecessorObj<Quarter> _quarter;

        // Fields

        // Results
        private Result<ActivityReward__points> _points;

        // Business constructor
        public ActivityReward(
            ActivityDefinition definition
            ,Quarter quarter
            )
        {
            InitializeResults();
            _definition = new PredecessorObj<ActivityDefinition>(this, RoleDefinition, definition);
            _quarter = new PredecessorObj<Quarter>(this, RoleQuarter, quarter);
        }

        // Hydration constructor
        private ActivityReward(FactMemento memento)
        {
            InitializeResults();
            _definition = new PredecessorObj<ActivityDefinition>(this, RoleDefinition, memento);
            _quarter = new PredecessorObj<Quarter>(this, RoleQuarter, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
            _points = new Result<ActivityReward__points>(this, QueryPoints);
        }

        // Predecessor access
        public ActivityDefinition Definition
        {
            get { return _definition.Fact; }
        }
        public Quarter Quarter
        {
            get { return _quarter.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access
        public TransientDisputable<ActivityReward__points, int> Points
        {
            get { return _points.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _points.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new ActivityReward__points(this, _points, value.Value));
                    }
                };
                setter();
			}
        }

    }
    
    public partial class ActivityReward__points : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ActivityReward__points newFact = new ActivityReward__points(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ActivityReward__points fact = (ActivityReward__points)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityReward__points", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleActivityReward = new Role(new RoleMemento(
			_correspondenceFactType,
			"activityReward",
			new CorrespondenceFactType("Incentives.Model.ActivityReward", 1),
			false));
        public static Role RolePrior = new Role(new RoleMemento(
			_correspondenceFactType,
			"prior",
			new CorrespondenceFactType("Incentives.Model.ActivityReward__points", 1),
			false));

        // Queries
        public static Query MakeQueryIsCurrent()
		{
			return new Query()
				.JoinSuccessors(ActivityReward__points.RolePrior)
            ;
		}
        public static Query QueryIsCurrent = MakeQueryIsCurrent();

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(QueryIsCurrent);

        // Predecessors
        private PredecessorObj<ActivityReward> _activityReward;
        private PredecessorList<ActivityReward__points> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public ActivityReward__points(
            ActivityReward activityReward
            ,IEnumerable<ActivityReward__points> prior
            ,int value
            )
        {
            InitializeResults();
            _activityReward = new PredecessorObj<ActivityReward>(this, RoleActivityReward, activityReward);
            _prior = new PredecessorList<ActivityReward__points>(this, RolePrior, prior);
            _value = value;
        }

        // Hydration constructor
        private ActivityReward__points(FactMemento memento)
        {
            InitializeResults();
            _activityReward = new PredecessorObj<ActivityReward>(this, RoleActivityReward, memento);
            _prior = new PredecessorList<ActivityReward__points>(this, RolePrior, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ActivityReward ActivityReward
        {
            get { return _activityReward.Fact; }
        }
        public IEnumerable<ActivityReward__points> Prior
        {
            get { return _prior; }
        }
     
        // Field access
        public int Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ProfileQuarter : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ProfileQuarter newFact = new ProfileQuarter(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ProfileQuarter fact = (ProfileQuarter)obj;
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ProfileQuarter", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleProfile = new Role(new RoleMemento(
			_correspondenceFactType,
			"profile",
			new CorrespondenceFactType("Incentives.Model.Profile", 1),
			false));
        public static Role RoleQuarter = new Role(new RoleMemento(
			_correspondenceFactType,
			"quarter",
			new CorrespondenceFactType("Incentives.Model.Quarter", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Profile> _profile;
        private PredecessorObj<Quarter> _quarter;

        // Fields

        // Results

        // Business constructor
        public ProfileQuarter(
            Profile profile
            ,Quarter quarter
            )
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, RoleProfile, profile);
            _quarter = new PredecessorObj<Quarter>(this, RoleQuarter, quarter);
        }

        // Hydration constructor
        private ProfileQuarter(FactMemento memento)
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, RoleProfile, memento);
            _quarter = new PredecessorObj<Quarter>(this, RoleQuarter, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Profile Profile
        {
            get { return _profile.Fact; }
        }
        public Quarter Quarter
        {
            get { return _quarter.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Activity : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Activity newFact = new Activity(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._activityDate = (DateTime)_fieldSerializerByType[typeof(DateTime)].ReadData(output);
						newFact._description = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
						newFact._multiplier = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Activity fact = (Activity)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._activityDate);
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._description);
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._multiplier);
			}
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Activity", 1);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Roles
        public static Role RoleProfileQuarter = new Role(new RoleMemento(
			_correspondenceFactType,
			"profileQuarter",
			new CorrespondenceFactType("Incentives.Model.ProfileQuarter", 1),
			false));
        public static Role RoleActivityReward = new Role(new RoleMemento(
			_correspondenceFactType,
			"activityReward",
			new CorrespondenceFactType("Incentives.Model.ActivityReward", 1),
			false));

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<ProfileQuarter> _profileQuarter;
        private PredecessorObj<ActivityReward> _activityReward;

        // Fields
        private DateTime _activityDate;
        private string _description;
        private int _multiplier;

        // Results

        // Business constructor
        public Activity(
            ProfileQuarter profileQuarter
            ,ActivityReward activityReward
            ,DateTime activityDate
            ,string description
            ,int multiplier
            )
        {
            InitializeResults();
            _profileQuarter = new PredecessorObj<ProfileQuarter>(this, RoleProfileQuarter, profileQuarter);
            _activityReward = new PredecessorObj<ActivityReward>(this, RoleActivityReward, activityReward);
            _activityDate = activityDate;
            _description = description;
            _multiplier = multiplier;
        }

        // Hydration constructor
        private Activity(FactMemento memento)
        {
            InitializeResults();
            _profileQuarter = new PredecessorObj<ProfileQuarter>(this, RoleProfileQuarter, memento);
            _activityReward = new PredecessorObj<ActivityReward>(this, RoleActivityReward, memento);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ProfileQuarter ProfileQuarter
        {
            get { return _profileQuarter.Fact; }
        }
        public ActivityReward ActivityReward
        {
            get { return _activityReward.Fact; }
        }

        // Field access
        public DateTime ActivityDate
        {
            get { return _activityDate; }
        }
        public string Description
        {
            get { return _description; }
        }
        public int Multiplier
        {
            get { return _multiplier; }
        }

        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddType(
				Profile._correspondenceFactType,
				new Profile.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Profile._correspondenceFactType }));
			community.AddQuery(
				Profile._correspondenceFactType,
				Profile.QueryName.QueryDefinition);
			community.AddQuery(
				Profile._correspondenceFactType,
				Profile.QueryActivities.QueryDefinition);
			community.AddType(
				Profile__name._correspondenceFactType,
				new Profile__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Profile__name._correspondenceFactType }));
			community.AddQuery(
				Profile__name._correspondenceFactType,
				Profile__name.QueryIsCurrent.QueryDefinition);
			community.AddType(
				Company._correspondenceFactType,
				new Company.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company._correspondenceFactType }));
			community.AddType(
				Quarter._correspondenceFactType,
				new Quarter.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Quarter._correspondenceFactType }));
			community.AddType(
				Category._correspondenceFactType,
				new Category.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Category._correspondenceFactType }));
			community.AddType(
				ActivityDefinition._correspondenceFactType,
				new ActivityDefinition.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityDefinition._correspondenceFactType }));
			community.AddType(
				ActivityReward._correspondenceFactType,
				new ActivityReward.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityReward._correspondenceFactType }));
			community.AddQuery(
				ActivityReward._correspondenceFactType,
				ActivityReward.QueryPoints.QueryDefinition);
			community.AddType(
				ActivityReward__points._correspondenceFactType,
				new ActivityReward__points.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityReward__points._correspondenceFactType }));
			community.AddQuery(
				ActivityReward__points._correspondenceFactType,
				ActivityReward__points.QueryIsCurrent.QueryDefinition);
			community.AddType(
				ProfileQuarter._correspondenceFactType,
				new ProfileQuarter.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ProfileQuarter._correspondenceFactType }));
			community.AddType(
				Activity._correspondenceFactType,
				new Activity.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Activity._correspondenceFactType }));
		}
	}
}
