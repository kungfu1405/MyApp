using DbData.Dal;
using DbData.Dal.Interfaces;

namespace DbData.Bll
{
    public class BllDbDataBase
    {
        public BllDbDataBase(IDbDataContext context)
        {
            _context = context;
        }
        private readonly IDbDataContext _context;

        private IContinent _continent;
        protected IContinent ContinentDao
        {
            get { return _continent ?? (_continent = new ContinentDal(_context)); }
        }

        private ICountry _country;
        protected ICountry CountryDao
        {
            get { return _country ?? (_country = new CountryDal(_context)); }
        }

        private IState _state;
        protected IState StateDao
        {
            get { return _state ?? (_state = new StateDal(_context)); }
        }

        private ICity _city;
        protected ICity CityDao
        {
            get { return _city ?? (_city = new CityDal(_context)); }
        }

        // Destination
        private IDestination _destination;
        protected IDestination DestinationDao
        {
            get { return _destination ?? (_destination = new DestinationDal(_context)); }
        }

        private IDestinationLanguage _destinationLanguage;
        protected IDestinationLanguage DestinationLanguageDao
        {
            get { return _destinationLanguage ?? (_destinationLanguage = new DestinationLanguageDal(_context)); }
        }

        private IDestinationOverview _destinationOverview;
        protected IDestinationOverview DestinationOverviewDao
        {
            get { return _destinationOverview ?? (_destinationOverview = new DestinationOverviewDal(_context)); }
        }

        // Attraction
        private IAttraction _attraction;
        protected IAttraction AttractionDao
        {
            get { return _attraction ?? (_attraction = new AttractionDal(_context)); }
        }

        private IAttractionLanguage _attractionLanguage;
        protected IAttractionLanguage AttractionLanguageDao
        {
            get { return _attractionLanguage ?? (_attractionLanguage = new AttractionLanguageDal(_context)); }
        }

        // Experience
        private IExperience _experience;
        protected IExperience ExperienceDao
        {
            get { return _experience ?? (_experience = new ExperienceDal(_context)); }
        }

        private IExperienceLanguage _experienceLanguage;
        protected IExperienceLanguage ExperienceLanguageDao
        {
            get { return _experienceLanguage ?? (_experienceLanguage = new ExperienceLanguageDal(_context)); }
        }

        private IExperienceSession _experienceSession;
        protected IExperienceSession ExperienceSessionDao
        {
            get { return _experienceSession ?? (_experienceSession = new ExperienceSessionDal(_context)); }
        }

        private IExperienceSessionImage _experienceSessionImage;
        protected IExperienceSessionImage ExperienceSessionImageDao
        {
            get { return _experienceSessionImage ?? (_experienceSessionImage = new ExperienceSessionImageDal(_context)); }
        }

        // Comment
        private IComment _comment;
        protected IComment CommentDao
        {
            get { return _comment ?? (_comment = new CommentDal(_context)); }
        }

        private ICommentImage _commentImage;
        protected ICommentImage CommentImageDao
        {
            get { return _commentImage ?? (_commentImage = new CommentImageDal(_context)); }
        }

        private ITag _tag;
        protected ITag TagDao
        {
            get { return _tag ?? (_tag = new TagDal(_context)); }
        }

        private IUserFollow _userFollow;
        protected IUserFollow UserFollowDao
        {
            get { return _userFollow ?? (_userFollow = new UserFollowDal(_context)); }
        }

        private IUserProfile _userProfile;
        protected IUserProfile UserProfileDao
        {
            get { return _userProfile ?? (_userProfile = new UserProfileDal(_context)); }
        }

      
        // AttProperty
        private IAttProperty _attProperty;
        public IAttProperty AttPropertyDao
        {            
            get { return _attProperty ?? (_attProperty = new AttPropertyDal(_context)); }
        }
        // vehicle
        private IVehicle _vehicle;
        public IVehicle VehicleDao
        {            
            get { return _vehicle ?? (_vehicle = new VehicleDal(_context)); }
        }

        // ItemView
        private IItemView _itemView;
        public IItemView ItemViewDao
        {
            get { return _itemView ?? (_itemView = new ItemViewDal(_context)); }
        }
    }
}
