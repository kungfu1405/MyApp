using DbData.Entities;
using Mic.Core.Entities;
using Mic.Core.Website;
using Mic.UserDb.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Web.Backend.Models
{
    public class AttractionViewModel : BaseModel
    {
        public AttractionViewModel()
        {
            ActionMode = FormActionMode.Add;
            Attraction = new EAttraction { 
            //DestinationId = new Guid("F14AAF99-E559-4CE4-BD72-929A68DB8ECC"),
            Status = EnumPostStatus.Draft,
            //AttractionTypes = (int)EnumAttractionType.PlaceToEat,
            //TouristClasses = (int)EnumTouristClass.Budget,
            //TouristObjects = (int)EnumTouristObject.Couple,
            //PropertyGroups = (int)EnumPropertyGroup.Beach,
           // TotalComments = 0,
            TotalLikes = 0,
            TotalRates = 0,
            AvgRates = 0,
            FromExperience = true,
            Priority = EnumPriority.Hight
            };
            AttractionTypeViews = new List<AttractionTypeView>()
            {
                new AttractionTypeView() { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToEat },
                new AttractionTypeView() { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToStay },
                new AttractionTypeView() { Checked = false, EnumAttractionType = EnumAttractionType.PlaceToVisit },
            };
            TouristClassViews = new List<TouristClassView>()
            {
                new TouristClassView() { Checked=false , EnumTouristClass = EnumTouristClass.Budget},
                new TouristClassView() { Checked=false , EnumTouristClass = EnumTouristClass.Luxury},
                new TouristClassView() { Checked=false , EnumTouristClass = EnumTouristClass.MidRange},
            };
            PropertyGroupViews = new List<PropertyGroupView>()
            {
                new PropertyGroupView(){ Checked = false , EnumPropertyGroup = EnumPropertyGroup.Beach },
                new PropertyGroupView(){ Checked = false , EnumPropertyGroup = EnumPropertyGroup.Hotel },
                new PropertyGroupView(){ Checked = false , EnumPropertyGroup = EnumPropertyGroup.Restaurant },
            };
            TouristObjectViews = new List<TouristObjectView>()
            {
                new TouristObjectView(){Checked = false , EnumTouristObject = EnumTouristObject.Couple},
                new TouristObjectView(){Checked = false , EnumTouristObject = EnumTouristObject.Family},
                new TouristObjectView(){Checked = false , EnumTouristObject = EnumTouristObject.Friends},
                new TouristObjectView(){Checked = false , EnumTouristObject = EnumTouristObject.Group},
                new TouristObjectView(){Checked = false , EnumTouristObject = EnumTouristObject.Solo},
            };
            StringTags = "";
        }
        public EAttraction Attraction { get; set; }
        [BindProperty]
        public IList<AttractionTypeView> AttractionTypeViews { get; set; }        
        public IList<TouristClassView> TouristClassViews { get; set; }        
        public IList<PropertyGroupView> PropertyGroupViews { get; set; }        
        public IList<TouristObjectView> TouristObjectViews { get; set; }        
        public IList<ELanguage> Languages { get; set; }
        public string StringTags{ get; set; }
    }
    public class AttractionTypeView
    {
        [BindProperty]
        public bool Checked { get; set; }
        public EnumAttractionType EnumAttractionType { get; set; }       
    }
    public class TouristClassView
    {
        [BindProperty]
        public bool Checked { get; set; }
        public EnumTouristClass EnumTouristClass { get; set; }       
    }
    public class PropertyGroupView
    {
        [BindProperty]
        public bool Checked { get; set; }
        public EnumPropertyGroup EnumPropertyGroup { get; set; }       
    }
    public class TouristObjectView
    {
        [BindProperty]
        public bool Checked { get; set; }
        public EnumTouristObject EnumTouristObject { get; set; }       
    }

    public class AttractionListViewModel : BaseModel
    {
        public AttractionListViewModel()
        {
        }      
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; } 
        public string  Name { get; set; }
        public string DestinationId { get; set; }
        public int AttractionTypes { get; set; }
        public int TouristClasses { get; set; }
        public int TouristObjects { get; set; }
        public int PropertyGroups { get; set; }
        public string Tags { get; set; }
        public string LangCode { get; set; }
        public string DefaultLang { get; set; }

      
    }
}
