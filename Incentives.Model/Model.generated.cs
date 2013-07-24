using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    IndividualProfile -> Individual
    IndividualProfile -> Profile
    Profile__name -> Profile
    Profile__name -> Profile__name [label="  *"]
    Quarter -> Company
    Category -> Company
    Category__description -> Category
    Category__description -> Category__description [label="  *"]
    Category__ordinal -> Category
    Category__ordinal -> Category__ordinal [label="  *"]
    ActivityDefinition -> Category
    ActivityDefinition__description -> ActivityDefinition
    ActivityDefinition__description -> ActivityDefinition__description [label="  *"]
    ActivityDefinition__qualifier -> ActivityDefinition
    ActivityDefinition__qualifier -> ActivityDefinition__qualifier [label="  *"]
    ActivityDefinition__ordinal -> ActivityDefinition
    ActivityDefinition__ordinal -> ActivityDefinition__ordinal [label="  *"]
    ActivityReward -> ActivityDefinition
    ActivityReward -> Quarter
    ActivityReward__points -> ActivityReward
    ActivityReward__points -> ActivityReward__points [label="  *"]
    ProfileQuarter -> Profile
    ProfileQuarter -> Quarter
    Activity -> ProfileQuarter
    Activity -> ActivityReward
    Activity__description -> Activity
    Activity__description -> Activity__description [label="  *"]
    Activity__multiplier -> Activity
    Activity__multiplier -> Activity__multiplier [label="  *"]
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Individual.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Individual.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Individual", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Individual GetUnloadedInstance()
        {
            return new Individual((FactMemento)null) { IsLoaded = false };
        }

        public static Individual GetNullInstance()
        {
            return new Individual((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Individual> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Individual)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryProfiles;

        public static Query GetQueryProfiles()
		{
            if (_cacheQueryProfiles == null)
            {
			    _cacheQueryProfiles = new Query()
		    		.JoinSuccessors(IndividualProfile.GetRoleIndividual())
		    		.JoinPredecessors(IndividualProfile.GetRoleProfile())
                ;
            }
            return _cacheQueryProfiles;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Profile> _profiles;

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
            _profiles = new Result<Profile>(this, GetQueryProfiles(), Profile.GetUnloadedInstance, Profile.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Profile> Profiles
        {
            get { return _profiles; }
        }

        // Mutable property access

    }
    
    public partial class IndividualProfile : CorrespondenceFact
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
				IndividualProfile newFact = new IndividualProfile(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				IndividualProfile fact = (IndividualProfile)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return IndividualProfile.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return IndividualProfile.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.IndividualProfile", 1770677136);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static IndividualProfile GetUnloadedInstance()
        {
            return new IndividualProfile((FactMemento)null) { IsLoaded = false };
        }

        public static IndividualProfile GetNullInstance()
        {
            return new IndividualProfile((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<IndividualProfile> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (IndividualProfile)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleIndividual;
        public static Role GetRoleIndividual()
        {
            if (_cacheRoleIndividual == null)
            {
                _cacheRoleIndividual = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "individual",
			        Individual._correspondenceFactType,
			        false));
            }
            return _cacheRoleIndividual;
        }
        private static Role _cacheRoleProfile;
        public static Role GetRoleProfile()
        {
            if (_cacheRoleProfile == null)
            {
                _cacheRoleProfile = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profile",
			        Profile._correspondenceFactType,
			        false));
            }
            return _cacheRoleProfile;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Individual> _individual;
        private PredecessorObj<Profile> _profile;

        // Fields

        // Results

        // Business constructor
        public IndividualProfile(
            Individual individual
            ,Profile profile
            )
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), individual);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), profile);
        }

        // Hydration constructor
        private IndividualProfile(FactMemento memento)
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), memento, Individual.GetUnloadedInstance, Individual.GetNullInstance);
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), memento, Profile.GetUnloadedInstance, Profile.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Individual Individual
        {
            get { return IsNull ? Individual.GetNullInstance() : _individual.Fact; }
        }
        public Profile Profile
        {
            get { return IsNull ? Profile.GetNullInstance() : _profile.Fact; }
        }

        // Field access

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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Profile.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Profile.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Profile", 2);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Profile GetUnloadedInstance()
        {
            return new Profile((FactMemento)null) { IsLoaded = false };
        }

        public static Profile GetNullInstance()
        {
            return new Profile((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Profile> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Profile)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryName;

        public static Query GetQueryName()
		{
            if (_cacheQueryName == null)
            {
			    _cacheQueryName = new Query()
    				.JoinSuccessors(Profile__name.GetRoleProfile(), Condition.WhereIsEmpty(Profile__name.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryName;
		}
        private static Query _cacheQueryActivities;

        public static Query GetQueryActivities()
		{
            if (_cacheQueryActivities == null)
            {
			    _cacheQueryActivities = new Query()
		    		.JoinSuccessors(ProfileQuarter.GetRoleProfile())
		    		.JoinSuccessors(Activity.GetRoleProfileQuarter())
                ;
            }
            return _cacheQueryActivities;
		}

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
            _name = new Result<Profile__name>(this, GetQueryName(), Profile__name.GetUnloadedInstance, Profile__name.GetNullInstance);
            _activities = new Result<Activity>(this, GetQueryActivities(), Activity.GetUnloadedInstance, Activity.GetNullInstance);
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Profile__name.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Profile__name.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Profile__name", 31410776);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Profile__name GetUnloadedInstance()
        {
            return new Profile__name((FactMemento)null) { IsLoaded = false };
        }

        public static Profile__name GetNullInstance()
        {
            return new Profile__name((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Profile__name> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Profile__name)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleProfile;
        public static Role GetRoleProfile()
        {
            if (_cacheRoleProfile == null)
            {
                _cacheRoleProfile = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profile",
			        Profile._correspondenceFactType,
			        false));
            }
            return _cacheRoleProfile;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Profile__name._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Profile__name.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), profile);
            _prior = new PredecessorList<Profile__name>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Profile__name(FactMemento memento)
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), memento, Profile.GetUnloadedInstance, Profile.GetNullInstance);
            _prior = new PredecessorList<Profile__name>(this, GetRolePrior(), memento, Profile__name.GetUnloadedInstance, Profile__name.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Profile Profile
        {
            get { return IsNull ? Profile.GetNullInstance() : _profile.Fact; }
        }
        public PredecessorList<Profile__name> Prior
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Company.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Company.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Company", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Company GetUnloadedInstance()
        {
            return new Company((FactMemento)null) { IsLoaded = false };
        }

        public static Company GetNullInstance()
        {
            return new Company((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Company> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Company)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryCategories;

        public static Query GetQueryCategories()
		{
            if (_cacheQueryCategories == null)
            {
			    _cacheQueryCategories = new Query()
		    		.JoinSuccessors(Category.GetRoleCompany())
                ;
            }
            return _cacheQueryCategories;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _identifier;

        // Results
        private Result<Category> _categories;

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
            _categories = new Result<Category>(this, GetQueryCategories(), Category.GetUnloadedInstance, Category.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access
        public Result<Category> Categories
        {
            get { return _categories; }
        }

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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Quarter.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Quarter.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Quarter", -1633137520);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Quarter GetUnloadedInstance()
        {
            return new Quarter((FactMemento)null) { IsLoaded = false };
        }

        public static Quarter GetNullInstance()
        {
            return new Quarter((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Quarter> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Quarter)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        false));
            }
            return _cacheRoleCompany;
        }

        // Queries
        private static Query _cacheQueryRewards;

        public static Query GetQueryRewards()
		{
            if (_cacheQueryRewards == null)
            {
			    _cacheQueryRewards = new Query()
		    		.JoinSuccessors(ActivityReward.GetRoleQuarter())
                ;
            }
            return _cacheQueryRewards;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        private DateTime _startDate;

        // Results
        private Result<ActivityReward> _rewards;

        // Business constructor
        public Quarter(
            Company company
            ,DateTime startDate
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
            _startDate = startDate;
        }

        // Hydration constructor
        private Quarter(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _rewards = new Result<ActivityReward>(this, GetQueryRewards(), ActivityReward.GetUnloadedInstance, ActivityReward.GetNullInstance);
        }

        // Predecessor access
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }

        // Field access
        public DateTime StartDate
        {
            get { return _startDate; }
        }

        // Query result access
        public Result<ActivityReward> Rewards
        {
            get { return _rewards; }
        }

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
						newFact._identifier = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Category fact = (Category)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._identifier);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Category.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Category.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Category", -1633137568);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Category GetUnloadedInstance()
        {
            return new Category((FactMemento)null) { IsLoaded = false };
        }

        public static Category GetNullInstance()
        {
            return new Category((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Category> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Category)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCompany;
        public static Role GetRoleCompany()
        {
            if (_cacheRoleCompany == null)
            {
                _cacheRoleCompany = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "company",
			        Company._correspondenceFactType,
			        false));
            }
            return _cacheRoleCompany;
        }

        // Queries
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(Category__description.GetRoleCategory(), Condition.WhereIsEmpty(Category__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryOrdinal;

        public static Query GetQueryOrdinal()
		{
            if (_cacheQueryOrdinal == null)
            {
			    _cacheQueryOrdinal = new Query()
    				.JoinSuccessors(Category__ordinal.GetRoleCategory(), Condition.WhereIsEmpty(Category__ordinal.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryOrdinal;
		}
        private static Query _cacheQueryActivities;

        public static Query GetQueryActivities()
		{
            if (_cacheQueryActivities == null)
            {
			    _cacheQueryActivities = new Query()
		    		.JoinSuccessors(ActivityDefinition.GetRoleCategory())
                ;
            }
            return _cacheQueryActivities;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Company> _company;

        // Fields
        private string _identifier;

        // Results
        private Result<Category__description> _description;
        private Result<Category__ordinal> _ordinal;
        private Result<ActivityDefinition> _activities;

        // Business constructor
        public Category(
            Company company
            ,string identifier
            )
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), company);
            _identifier = identifier;
        }

        // Hydration constructor
        private Category(FactMemento memento)
        {
            InitializeResults();
            _company = new PredecessorObj<Company>(this, GetRoleCompany(), memento, Company.GetUnloadedInstance, Company.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _description = new Result<Category__description>(this, GetQueryDescription(), Category__description.GetUnloadedInstance, Category__description.GetNullInstance);
            _ordinal = new Result<Category__ordinal>(this, GetQueryOrdinal(), Category__ordinal.GetUnloadedInstance, Category__ordinal.GetNullInstance);
            _activities = new Result<ActivityDefinition>(this, GetQueryActivities(), ActivityDefinition.GetUnloadedInstance, ActivityDefinition.GetNullInstance);
        }

        // Predecessor access
        public Company Company
        {
            get { return IsNull ? Company.GetNullInstance() : _company.Fact; }
        }

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access
        public Result<ActivityDefinition> Activities
        {
            get { return _activities; }
        }

        // Mutable property access
        public TransientDisputable<Category__description, string> Description
        {
            get { return _description.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _description.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Category__description(this, _description, value.Value));
                    }
                };
                setter();
			}
        }
        public TransientDisputable<Category__ordinal, int> Ordinal
        {
            get { return _ordinal.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _ordinal.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Category__ordinal(this, _ordinal, value.Value));
                    }
                };
                setter();
			}
        }

    }
    
    public partial class Category__description : CorrespondenceFact
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
				Category__description newFact = new Category__description(memento);

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
				Category__description fact = (Category__description)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Category__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Category__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Category__description", -306270328);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Category__description GetUnloadedInstance()
        {
            return new Category__description((FactMemento)null) { IsLoaded = false };
        }

        public static Category__description GetNullInstance()
        {
            return new Category__description((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Category__description> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Category__description)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCategory;
        public static Role GetRoleCategory()
        {
            if (_cacheRoleCategory == null)
            {
                _cacheRoleCategory = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "category",
			        Category._correspondenceFactType,
			        false));
            }
            return _cacheRoleCategory;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Category__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Category__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Category> _category;
        private PredecessorList<Category__description> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Category__description(
            Category category
            ,IEnumerable<Category__description> prior
            ,string value
            )
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), category);
            _prior = new PredecessorList<Category__description>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Category__description(FactMemento memento)
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), memento, Category.GetUnloadedInstance, Category.GetNullInstance);
            _prior = new PredecessorList<Category__description>(this, GetRolePrior(), memento, Category__description.GetUnloadedInstance, Category__description.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Category Category
        {
            get { return IsNull ? Category.GetNullInstance() : _category.Fact; }
        }
        public PredecessorList<Category__description> Prior
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
    
    public partial class Category__ordinal : CorrespondenceFact
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
				Category__ordinal newFact = new Category__ordinal(memento);

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
				Category__ordinal fact = (Category__ordinal)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Category__ordinal.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Category__ordinal.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Category__ordinal", -306270316);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Category__ordinal GetUnloadedInstance()
        {
            return new Category__ordinal((FactMemento)null) { IsLoaded = false };
        }

        public static Category__ordinal GetNullInstance()
        {
            return new Category__ordinal((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Category__ordinal> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Category__ordinal)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCategory;
        public static Role GetRoleCategory()
        {
            if (_cacheRoleCategory == null)
            {
                _cacheRoleCategory = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "category",
			        Category._correspondenceFactType,
			        false));
            }
            return _cacheRoleCategory;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Category__ordinal._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Category__ordinal.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Category> _category;
        private PredecessorList<Category__ordinal> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public Category__ordinal(
            Category category
            ,IEnumerable<Category__ordinal> prior
            ,int value
            )
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), category);
            _prior = new PredecessorList<Category__ordinal>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Category__ordinal(FactMemento memento)
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), memento, Category.GetUnloadedInstance, Category.GetNullInstance);
            _prior = new PredecessorList<Category__ordinal>(this, GetRolePrior(), memento, Category__ordinal.GetUnloadedInstance, Category__ordinal.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Category Category
        {
            get { return IsNull ? Category.GetNullInstance() : _category.Fact; }
        }
        public PredecessorList<Category__ordinal> Prior
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
						newFact._identifier = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ActivityDefinition fact = (ActivityDefinition)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._identifier);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityDefinition.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityDefinition.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityDefinition", -671524912);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityDefinition GetUnloadedInstance()
        {
            return new ActivityDefinition((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityDefinition GetNullInstance()
        {
            return new ActivityDefinition((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityDefinition> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityDefinition)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCategory;
        public static Role GetRoleCategory()
        {
            if (_cacheRoleCategory == null)
            {
                _cacheRoleCategory = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "category",
			        Category._correspondenceFactType,
			        false));
            }
            return _cacheRoleCategory;
        }

        // Queries
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(ActivityDefinition__description.GetRoleActivityDefinition(), Condition.WhereIsEmpty(ActivityDefinition__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryQualifier;

        public static Query GetQueryQualifier()
		{
            if (_cacheQueryQualifier == null)
            {
			    _cacheQueryQualifier = new Query()
    				.JoinSuccessors(ActivityDefinition__qualifier.GetRoleActivityDefinition(), Condition.WhereIsEmpty(ActivityDefinition__qualifier.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryQualifier;
		}
        private static Query _cacheQueryOrdinal;

        public static Query GetQueryOrdinal()
		{
            if (_cacheQueryOrdinal == null)
            {
			    _cacheQueryOrdinal = new Query()
    				.JoinSuccessors(ActivityDefinition__ordinal.GetRoleActivityDefinition(), Condition.WhereIsEmpty(ActivityDefinition__ordinal.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryOrdinal;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Category> _category;

        // Fields
        private string _identifier;

        // Results
        private Result<ActivityDefinition__description> _description;
        private Result<ActivityDefinition__qualifier> _qualifier;
        private Result<ActivityDefinition__ordinal> _ordinal;

        // Business constructor
        public ActivityDefinition(
            Category category
            ,string identifier
            )
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), category);
            _identifier = identifier;
        }

        // Hydration constructor
        private ActivityDefinition(FactMemento memento)
        {
            InitializeResults();
            _category = new PredecessorObj<Category>(this, GetRoleCategory(), memento, Category.GetUnloadedInstance, Category.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _description = new Result<ActivityDefinition__description>(this, GetQueryDescription(), ActivityDefinition__description.GetUnloadedInstance, ActivityDefinition__description.GetNullInstance);
            _qualifier = new Result<ActivityDefinition__qualifier>(this, GetQueryQualifier(), ActivityDefinition__qualifier.GetUnloadedInstance, ActivityDefinition__qualifier.GetNullInstance);
            _ordinal = new Result<ActivityDefinition__ordinal>(this, GetQueryOrdinal(), ActivityDefinition__ordinal.GetUnloadedInstance, ActivityDefinition__ordinal.GetNullInstance);
        }

        // Predecessor access
        public Category Category
        {
            get { return IsNull ? Category.GetNullInstance() : _category.Fact; }
        }

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access

        // Mutable property access
        public TransientDisputable<ActivityDefinition__description, string> Description
        {
            get { return _description.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _description.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new ActivityDefinition__description(this, _description, value.Value));
                    }
                };
                setter();
			}
        }
        public TransientDisputable<ActivityDefinition__qualifier, string> Qualifier
        {
            get { return _qualifier.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _qualifier.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new ActivityDefinition__qualifier(this, _qualifier, value.Value));
                    }
                };
                setter();
			}
        }
        public TransientDisputable<ActivityDefinition__ordinal, int> Ordinal
        {
            get { return _ordinal.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _ordinal.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new ActivityDefinition__ordinal(this, _ordinal, value.Value));
                    }
                };
                setter();
			}
        }

    }
    
    public partial class ActivityDefinition__description : CorrespondenceFact
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
				ActivityDefinition__description newFact = new ActivityDefinition__description(memento);

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
				ActivityDefinition__description fact = (ActivityDefinition__description)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityDefinition__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityDefinition__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityDefinition__description", 1415118048);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityDefinition__description GetUnloadedInstance()
        {
            return new ActivityDefinition__description((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityDefinition__description GetNullInstance()
        {
            return new ActivityDefinition__description((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityDefinition__description> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityDefinition__description)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivityDefinition;
        public static Role GetRoleActivityDefinition()
        {
            if (_cacheRoleActivityDefinition == null)
            {
                _cacheRoleActivityDefinition = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activityDefinition",
			        ActivityDefinition._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivityDefinition;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ActivityDefinition__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ActivityDefinition__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ActivityDefinition> _activityDefinition;
        private PredecessorList<ActivityDefinition__description> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ActivityDefinition__description(
            ActivityDefinition activityDefinition
            ,IEnumerable<ActivityDefinition__description> prior
            ,string value
            )
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), activityDefinition);
            _prior = new PredecessorList<ActivityDefinition__description>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ActivityDefinition__description(FactMemento memento)
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), memento, ActivityDefinition.GetUnloadedInstance, ActivityDefinition.GetNullInstance);
            _prior = new PredecessorList<ActivityDefinition__description>(this, GetRolePrior(), memento, ActivityDefinition__description.GetUnloadedInstance, ActivityDefinition__description.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ActivityDefinition ActivityDefinition
        {
            get { return IsNull ? ActivityDefinition.GetNullInstance() : _activityDefinition.Fact; }
        }
        public PredecessorList<ActivityDefinition__description> Prior
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
    
    public partial class ActivityDefinition__qualifier : CorrespondenceFact
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
				ActivityDefinition__qualifier newFact = new ActivityDefinition__qualifier(memento);

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
				ActivityDefinition__qualifier fact = (ActivityDefinition__qualifier)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityDefinition__qualifier.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityDefinition__qualifier.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityDefinition__qualifier", 1415118048);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityDefinition__qualifier GetUnloadedInstance()
        {
            return new ActivityDefinition__qualifier((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityDefinition__qualifier GetNullInstance()
        {
            return new ActivityDefinition__qualifier((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityDefinition__qualifier> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityDefinition__qualifier)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivityDefinition;
        public static Role GetRoleActivityDefinition()
        {
            if (_cacheRoleActivityDefinition == null)
            {
                _cacheRoleActivityDefinition = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activityDefinition",
			        ActivityDefinition._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivityDefinition;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ActivityDefinition__qualifier._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ActivityDefinition__qualifier.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ActivityDefinition> _activityDefinition;
        private PredecessorList<ActivityDefinition__qualifier> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public ActivityDefinition__qualifier(
            ActivityDefinition activityDefinition
            ,IEnumerable<ActivityDefinition__qualifier> prior
            ,string value
            )
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), activityDefinition);
            _prior = new PredecessorList<ActivityDefinition__qualifier>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ActivityDefinition__qualifier(FactMemento memento)
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), memento, ActivityDefinition.GetUnloadedInstance, ActivityDefinition.GetNullInstance);
            _prior = new PredecessorList<ActivityDefinition__qualifier>(this, GetRolePrior(), memento, ActivityDefinition__qualifier.GetUnloadedInstance, ActivityDefinition__qualifier.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ActivityDefinition ActivityDefinition
        {
            get { return IsNull ? ActivityDefinition.GetNullInstance() : _activityDefinition.Fact; }
        }
        public PredecessorList<ActivityDefinition__qualifier> Prior
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
    
    public partial class ActivityDefinition__ordinal : CorrespondenceFact
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
				ActivityDefinition__ordinal newFact = new ActivityDefinition__ordinal(memento);

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
				ActivityDefinition__ordinal fact = (ActivityDefinition__ordinal)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityDefinition__ordinal.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityDefinition__ordinal.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityDefinition__ordinal", 1415118060);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityDefinition__ordinal GetUnloadedInstance()
        {
            return new ActivityDefinition__ordinal((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityDefinition__ordinal GetNullInstance()
        {
            return new ActivityDefinition__ordinal((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityDefinition__ordinal> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityDefinition__ordinal)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivityDefinition;
        public static Role GetRoleActivityDefinition()
        {
            if (_cacheRoleActivityDefinition == null)
            {
                _cacheRoleActivityDefinition = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activityDefinition",
			        ActivityDefinition._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivityDefinition;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ActivityDefinition__ordinal._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ActivityDefinition__ordinal.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<ActivityDefinition> _activityDefinition;
        private PredecessorList<ActivityDefinition__ordinal> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public ActivityDefinition__ordinal(
            ActivityDefinition activityDefinition
            ,IEnumerable<ActivityDefinition__ordinal> prior
            ,int value
            )
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), activityDefinition);
            _prior = new PredecessorList<ActivityDefinition__ordinal>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ActivityDefinition__ordinal(FactMemento memento)
        {
            InitializeResults();
            _activityDefinition = new PredecessorObj<ActivityDefinition>(this, GetRoleActivityDefinition(), memento, ActivityDefinition.GetUnloadedInstance, ActivityDefinition.GetNullInstance);
            _prior = new PredecessorList<ActivityDefinition__ordinal>(this, GetRolePrior(), memento, ActivityDefinition__ordinal.GetUnloadedInstance, ActivityDefinition__ordinal.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ActivityDefinition ActivityDefinition
        {
            get { return IsNull ? ActivityDefinition.GetNullInstance() : _activityDefinition.Fact; }
        }
        public PredecessorList<ActivityDefinition__ordinal> Prior
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ActivityReward fact = (ActivityReward)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityReward.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityReward.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityReward", 324210760);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityReward GetUnloadedInstance()
        {
            return new ActivityReward((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityReward GetNullInstance()
        {
            return new ActivityReward((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityReward> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityReward)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleDefinition;
        public static Role GetRoleDefinition()
        {
            if (_cacheRoleDefinition == null)
            {
                _cacheRoleDefinition = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "definition",
			        ActivityDefinition._correspondenceFactType,
			        false));
            }
            return _cacheRoleDefinition;
        }
        private static Role _cacheRoleQuarter;
        public static Role GetRoleQuarter()
        {
            if (_cacheRoleQuarter == null)
            {
                _cacheRoleQuarter = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "quarter",
			        Quarter._correspondenceFactType,
			        false));
            }
            return _cacheRoleQuarter;
        }

        // Queries
        private static Query _cacheQueryPoints;

        public static Query GetQueryPoints()
		{
            if (_cacheQueryPoints == null)
            {
			    _cacheQueryPoints = new Query()
    				.JoinSuccessors(ActivityReward__points.GetRoleActivityReward(), Condition.WhereIsEmpty(ActivityReward__points.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryPoints;
		}

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
            _definition = new PredecessorObj<ActivityDefinition>(this, GetRoleDefinition(), definition);
            _quarter = new PredecessorObj<Quarter>(this, GetRoleQuarter(), quarter);
        }

        // Hydration constructor
        private ActivityReward(FactMemento memento)
        {
            InitializeResults();
            _definition = new PredecessorObj<ActivityDefinition>(this, GetRoleDefinition(), memento, ActivityDefinition.GetUnloadedInstance, ActivityDefinition.GetNullInstance);
            _quarter = new PredecessorObj<Quarter>(this, GetRoleQuarter(), memento, Quarter.GetUnloadedInstance, Quarter.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _points = new Result<ActivityReward__points>(this, GetQueryPoints(), ActivityReward__points.GetUnloadedInstance, ActivityReward__points.GetNullInstance);
        }

        // Predecessor access
        public ActivityDefinition Definition
        {
            get { return IsNull ? ActivityDefinition.GetNullInstance() : _definition.Fact; }
        }
        public Quarter Quarter
        {
            get { return IsNull ? Quarter.GetNullInstance() : _quarter.Fact; }
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

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ActivityReward__points.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ActivityReward__points.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ActivityReward__points", -1644304724);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ActivityReward__points GetUnloadedInstance()
        {
            return new ActivityReward__points((FactMemento)null) { IsLoaded = false };
        }

        public static ActivityReward__points GetNullInstance()
        {
            return new ActivityReward__points((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ActivityReward__points> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ActivityReward__points)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivityReward;
        public static Role GetRoleActivityReward()
        {
            if (_cacheRoleActivityReward == null)
            {
                _cacheRoleActivityReward = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activityReward",
			        ActivityReward._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivityReward;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ActivityReward__points._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ActivityReward__points.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

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
            _activityReward = new PredecessorObj<ActivityReward>(this, GetRoleActivityReward(), activityReward);
            _prior = new PredecessorList<ActivityReward__points>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private ActivityReward__points(FactMemento memento)
        {
            InitializeResults();
            _activityReward = new PredecessorObj<ActivityReward>(this, GetRoleActivityReward(), memento, ActivityReward.GetUnloadedInstance, ActivityReward.GetNullInstance);
            _prior = new PredecessorList<ActivityReward__points>(this, GetRolePrior(), memento, ActivityReward__points.GetUnloadedInstance, ActivityReward__points.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public ActivityReward ActivityReward
        {
            get { return IsNull ? ActivityReward.GetNullInstance() : _activityReward.Fact; }
        }
        public PredecessorList<ActivityReward__points> Prior
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


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ProfileQuarter fact = (ProfileQuarter)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ProfileQuarter.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ProfileQuarter.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.ProfileQuarter", 1413676816);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ProfileQuarter GetUnloadedInstance()
        {
            return new ProfileQuarter((FactMemento)null) { IsLoaded = false };
        }

        public static ProfileQuarter GetNullInstance()
        {
            return new ProfileQuarter((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ProfileQuarter> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ProfileQuarter)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleProfile;
        public static Role GetRoleProfile()
        {
            if (_cacheRoleProfile == null)
            {
                _cacheRoleProfile = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profile",
			        Profile._correspondenceFactType,
			        false));
            }
            return _cacheRoleProfile;
        }
        private static Role _cacheRoleQuarter;
        public static Role GetRoleQuarter()
        {
            if (_cacheRoleQuarter == null)
            {
                _cacheRoleQuarter = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "quarter",
			        Quarter._correspondenceFactType,
			        false));
            }
            return _cacheRoleQuarter;
        }

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
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), profile);
            _quarter = new PredecessorObj<Quarter>(this, GetRoleQuarter(), quarter);
        }

        // Hydration constructor
        private ProfileQuarter(FactMemento memento)
        {
            InitializeResults();
            _profile = new PredecessorObj<Profile>(this, GetRoleProfile(), memento, Profile.GetUnloadedInstance, Profile.GetNullInstance);
            _quarter = new PredecessorObj<Quarter>(this, GetRoleQuarter(), memento, Quarter.GetUnloadedInstance, Quarter.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Profile Profile
        {
            get { return IsNull ? Profile.GetNullInstance() : _profile.Fact; }
        }
        public Quarter Quarter
        {
            get { return IsNull ? Quarter.GetNullInstance() : _quarter.Fact; }
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
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Activity fact = (Activity)obj;
				_fieldSerializerByType[typeof(DateTime)].WriteData(output, fact._activityDate);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Activity.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Activity.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Activity", -105423760);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Activity GetUnloadedInstance()
        {
            return new Activity((FactMemento)null) { IsLoaded = false };
        }

        public static Activity GetNullInstance()
        {
            return new Activity((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Activity> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Activity)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleProfileQuarter;
        public static Role GetRoleProfileQuarter()
        {
            if (_cacheRoleProfileQuarter == null)
            {
                _cacheRoleProfileQuarter = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "profileQuarter",
			        ProfileQuarter._correspondenceFactType,
			        false));
            }
            return _cacheRoleProfileQuarter;
        }
        private static Role _cacheRoleActivityReward;
        public static Role GetRoleActivityReward()
        {
            if (_cacheRoleActivityReward == null)
            {
                _cacheRoleActivityReward = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activityReward",
			        ActivityReward._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivityReward;
        }

        // Queries
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(Activity__description.GetRoleActivity(), Condition.WhereIsEmpty(Activity__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryMultiplier;

        public static Query GetQueryMultiplier()
		{
            if (_cacheQueryMultiplier == null)
            {
			    _cacheQueryMultiplier = new Query()
    				.JoinSuccessors(Activity__multiplier.GetRoleActivity(), Condition.WhereIsEmpty(Activity__multiplier.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryMultiplier;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<ProfileQuarter> _profileQuarter;
        private PredecessorObj<ActivityReward> _activityReward;

        // Fields
        private DateTime _activityDate;

        // Results
        private Result<Activity__description> _description;
        private Result<Activity__multiplier> _multiplier;

        // Business constructor
        public Activity(
            ProfileQuarter profileQuarter
            ,ActivityReward activityReward
            ,DateTime activityDate
            )
        {
            InitializeResults();
            _profileQuarter = new PredecessorObj<ProfileQuarter>(this, GetRoleProfileQuarter(), profileQuarter);
            _activityReward = new PredecessorObj<ActivityReward>(this, GetRoleActivityReward(), activityReward);
            _activityDate = activityDate;
        }

        // Hydration constructor
        private Activity(FactMemento memento)
        {
            InitializeResults();
            _profileQuarter = new PredecessorObj<ProfileQuarter>(this, GetRoleProfileQuarter(), memento, ProfileQuarter.GetUnloadedInstance, ProfileQuarter.GetNullInstance);
            _activityReward = new PredecessorObj<ActivityReward>(this, GetRoleActivityReward(), memento, ActivityReward.GetUnloadedInstance, ActivityReward.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _description = new Result<Activity__description>(this, GetQueryDescription(), Activity__description.GetUnloadedInstance, Activity__description.GetNullInstance);
            _multiplier = new Result<Activity__multiplier>(this, GetQueryMultiplier(), Activity__multiplier.GetUnloadedInstance, Activity__multiplier.GetNullInstance);
        }

        // Predecessor access
        public ProfileQuarter ProfileQuarter
        {
            get { return IsNull ? ProfileQuarter.GetNullInstance() : _profileQuarter.Fact; }
        }
        public ActivityReward ActivityReward
        {
            get { return IsNull ? ActivityReward.GetNullInstance() : _activityReward.Fact; }
        }

        // Field access
        public DateTime ActivityDate
        {
            get { return _activityDate; }
        }

        // Query result access

        // Mutable property access
        public TransientDisputable<Activity__description, string> Description
        {
            get { return _description.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _description.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Activity__description(this, _description, value.Value));
                    }
                };
                setter();
			}
        }
        public TransientDisputable<Activity__multiplier, int> Multiplier
        {
            get { return _multiplier.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Action setter = async delegate()
                {
                    var current = (await _multiplier.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Activity__multiplier(this, _multiplier, value.Value));
                    }
                };
                setter();
			}
        }

    }
    
    public partial class Activity__description : CorrespondenceFact
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
				Activity__description newFact = new Activity__description(memento);

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
				Activity__description fact = (Activity__description)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Activity__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Activity__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Activity__description", 54889504);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Activity__description GetUnloadedInstance()
        {
            return new Activity__description((FactMemento)null) { IsLoaded = false };
        }

        public static Activity__description GetNullInstance()
        {
            return new Activity__description((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Activity__description> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Activity__description)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivity;
        public static Role GetRoleActivity()
        {
            if (_cacheRoleActivity == null)
            {
                _cacheRoleActivity = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activity",
			        Activity._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivity;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Activity__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Activity__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Activity> _activity;
        private PredecessorList<Activity__description> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Activity__description(
            Activity activity
            ,IEnumerable<Activity__description> prior
            ,string value
            )
        {
            InitializeResults();
            _activity = new PredecessorObj<Activity>(this, GetRoleActivity(), activity);
            _prior = new PredecessorList<Activity__description>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Activity__description(FactMemento memento)
        {
            InitializeResults();
            _activity = new PredecessorObj<Activity>(this, GetRoleActivity(), memento, Activity.GetUnloadedInstance, Activity.GetNullInstance);
            _prior = new PredecessorList<Activity__description>(this, GetRolePrior(), memento, Activity__description.GetUnloadedInstance, Activity__description.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Activity Activity
        {
            get { return IsNull ? Activity.GetNullInstance() : _activity.Fact; }
        }
        public PredecessorList<Activity__description> Prior
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
    
    public partial class Activity__multiplier : CorrespondenceFact
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
				Activity__multiplier newFact = new Activity__multiplier(memento);

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
				Activity__multiplier fact = (Activity__multiplier)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Activity__multiplier.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Activity__multiplier.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Incentives.Model.Activity__multiplier", 54889516);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Activity__multiplier GetUnloadedInstance()
        {
            return new Activity__multiplier((FactMemento)null) { IsLoaded = false };
        }

        public static Activity__multiplier GetNullInstance()
        {
            return new Activity__multiplier((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Activity__multiplier> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Activity__multiplier)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleActivity;
        public static Role GetRoleActivity()
        {
            if (_cacheRoleActivity == null)
            {
                _cacheRoleActivity = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "activity",
			        Activity._correspondenceFactType,
			        false));
            }
            return _cacheRoleActivity;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Activity__multiplier._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Activity__multiplier.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Activity> _activity;
        private PredecessorList<Activity__multiplier> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public Activity__multiplier(
            Activity activity
            ,IEnumerable<Activity__multiplier> prior
            ,int value
            )
        {
            InitializeResults();
            _activity = new PredecessorObj<Activity>(this, GetRoleActivity(), activity);
            _prior = new PredecessorList<Activity__multiplier>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Activity__multiplier(FactMemento memento)
        {
            InitializeResults();
            _activity = new PredecessorObj<Activity>(this, GetRoleActivity(), memento, Activity.GetUnloadedInstance, Activity.GetNullInstance);
            _prior = new PredecessorList<Activity__multiplier>(this, GetRolePrior(), memento, Activity__multiplier.GetUnloadedInstance, Activity__multiplier.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Activity Activity
        {
            get { return IsNull ? Activity.GetNullInstance() : _activity.Fact; }
        }
        public PredecessorList<Activity__multiplier> Prior
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
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.GetQueryProfiles().QueryDefinition);
			community.AddType(
				IndividualProfile._correspondenceFactType,
				new IndividualProfile.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { IndividualProfile._correspondenceFactType }));
			community.AddType(
				Profile._correspondenceFactType,
				new Profile.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Profile._correspondenceFactType }));
			community.AddQuery(
				Profile._correspondenceFactType,
				Profile.GetQueryName().QueryDefinition);
			community.AddQuery(
				Profile._correspondenceFactType,
				Profile.GetQueryActivities().QueryDefinition);
			community.AddType(
				Profile__name._correspondenceFactType,
				new Profile__name.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Profile__name._correspondenceFactType }));
			community.AddQuery(
				Profile__name._correspondenceFactType,
				Profile__name.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Company._correspondenceFactType,
				new Company.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Company._correspondenceFactType }));
			community.AddQuery(
				Company._correspondenceFactType,
				Company.GetQueryCategories().QueryDefinition);
			community.AddType(
				Quarter._correspondenceFactType,
				new Quarter.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Quarter._correspondenceFactType }));
			community.AddQuery(
				Quarter._correspondenceFactType,
				Quarter.GetQueryRewards().QueryDefinition);
			community.AddType(
				Category._correspondenceFactType,
				new Category.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Category._correspondenceFactType }));
			community.AddQuery(
				Category._correspondenceFactType,
				Category.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				Category._correspondenceFactType,
				Category.GetQueryOrdinal().QueryDefinition);
			community.AddQuery(
				Category._correspondenceFactType,
				Category.GetQueryActivities().QueryDefinition);
			community.AddType(
				Category__description._correspondenceFactType,
				new Category__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Category__description._correspondenceFactType }));
			community.AddQuery(
				Category__description._correspondenceFactType,
				Category__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Category__ordinal._correspondenceFactType,
				new Category__ordinal.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Category__ordinal._correspondenceFactType }));
			community.AddQuery(
				Category__ordinal._correspondenceFactType,
				Category__ordinal.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ActivityDefinition._correspondenceFactType,
				new ActivityDefinition.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityDefinition._correspondenceFactType }));
			community.AddQuery(
				ActivityDefinition._correspondenceFactType,
				ActivityDefinition.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				ActivityDefinition._correspondenceFactType,
				ActivityDefinition.GetQueryQualifier().QueryDefinition);
			community.AddQuery(
				ActivityDefinition._correspondenceFactType,
				ActivityDefinition.GetQueryOrdinal().QueryDefinition);
			community.AddType(
				ActivityDefinition__description._correspondenceFactType,
				new ActivityDefinition__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityDefinition__description._correspondenceFactType }));
			community.AddQuery(
				ActivityDefinition__description._correspondenceFactType,
				ActivityDefinition__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ActivityDefinition__qualifier._correspondenceFactType,
				new ActivityDefinition__qualifier.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityDefinition__qualifier._correspondenceFactType }));
			community.AddQuery(
				ActivityDefinition__qualifier._correspondenceFactType,
				ActivityDefinition__qualifier.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ActivityDefinition__ordinal._correspondenceFactType,
				new ActivityDefinition__ordinal.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityDefinition__ordinal._correspondenceFactType }));
			community.AddQuery(
				ActivityDefinition__ordinal._correspondenceFactType,
				ActivityDefinition__ordinal.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ActivityReward._correspondenceFactType,
				new ActivityReward.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityReward._correspondenceFactType }));
			community.AddQuery(
				ActivityReward._correspondenceFactType,
				ActivityReward.GetQueryPoints().QueryDefinition);
			community.AddType(
				ActivityReward__points._correspondenceFactType,
				new ActivityReward__points.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ActivityReward__points._correspondenceFactType }));
			community.AddQuery(
				ActivityReward__points._correspondenceFactType,
				ActivityReward__points.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ProfileQuarter._correspondenceFactType,
				new ProfileQuarter.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ProfileQuarter._correspondenceFactType }));
			community.AddType(
				Activity._correspondenceFactType,
				new Activity.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Activity._correspondenceFactType }));
			community.AddQuery(
				Activity._correspondenceFactType,
				Activity.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				Activity._correspondenceFactType,
				Activity.GetQueryMultiplier().QueryDefinition);
			community.AddType(
				Activity__description._correspondenceFactType,
				new Activity__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Activity__description._correspondenceFactType }));
			community.AddQuery(
				Activity__description._correspondenceFactType,
				Activity__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Activity__multiplier._correspondenceFactType,
				new Activity__multiplier.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Activity__multiplier._correspondenceFactType }));
			community.AddQuery(
				Activity__multiplier._correspondenceFactType,
				Activity__multiplier.GetQueryIsCurrent().QueryDefinition);
		}
	}
}
