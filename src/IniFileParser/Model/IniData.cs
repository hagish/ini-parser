using System;
using IniParser.Model.Configuration;
using IniParser.Model.Formatting;

namespace IniParser.Model
{
    
    /// <summary>
    /// Represents all data from an INI file
    /// </summary>
    public class IniData : ICloneable
    {
        #region Non-Public Members
        /// <summary>
        /// Represents all sections from an INI file
        /// </summary>
        private SectionDataCollection _sections;
        #endregion

        #region Initialization
        
        /// <summary>
        /// Initializes an empty IniData instance.
        /// </summary>
        public IniData() : this (new SectionDataCollection())
        {
        }

        /// <summary>
        /// Initializes a new IniData instance using a previous
        /// <see cref="SectionDataCollection"/>.
        /// </summary>
        /// <param name="sdc">
        /// <see cref="SectionDataCollection"/> object containing the
        /// data with the sections of the file</param>
        public IniData(SectionDataCollection sdc)
        {
            _sections = (SectionDataCollection)sdc.Clone();
            Global = new KeyDataCollection();
        }

        public IniData(IniData ori): this((SectionDataCollection) ori.Sections)
        {
            Global = (KeyDataCollection) ori.Global.Clone();
            Configuration = ori.Configuration.Clone();
        }
        #endregion

        #region Properties

        /// <summary>
        ///     Configuration used to write an ini file with the proper
        ///     delimiter characters and data.
        /// </summary>
        /// <remarks>
        ///     If the <see cref="IniData"/> instance was created by a parser,
        ///     this instance is a copy of the <see cref="IIniParserConfiguration"/> used
        ///     by the parser (i.e. different objects instances)
        ///     If this instance is created programatically without using a parser, this
        ///     property returns an instance of <see cref=" DefaultIniParserConfiguration"/>
        /// </remarks>
        public IIniParserConfiguration Configuration
        {
            get
            {
                // Lazy initialization
                if (_configuration == null)
                    _configuration = new DefaultIniParserConfiguration();

                return _configuration;
            }

            set { _configuration = value.Clone(); }
        }

        /// <summary>
        /// 	Global sections. Contains key/value pairs which are not
        /// 	enclosed in any section (i.e. they are defined at the beginning 
        /// 	of the file, before any section.
        /// </summary>
        public KeyDataCollection Global { get; private set; }

        /// <summary>
        /// Gets the <see cref="KeyDataCollection"/> instance 
        /// with the specified section name.
        /// </summary>
        public KeyDataCollection this[string sectionName]
        {
            get
            {
                if (_sections.ContainsSection(sectionName))
                    return _sections[sectionName];
                return null;
            }

        }

        /// <summary>
        /// Gets or sets all the <see cref="SectionData"/> 
        /// for this IniData instance.
        /// </summary>
        public SectionDataCollection Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }   
        #endregion

        #region Object Methods
        public override string ToString()
        {
            return ToString(new DefaultIniDataFormatter(Configuration));
        }
        
       
        public virtual string ToString(IIniDataFormatter formatter)
        {
            return formatter.IniDataToString(this);
        }
        

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return new IniData(this);
        }

        #endregion

        #region Fields
        /// <summary>
        ///     See property <see cref="Configuration"/> for more information. 
        /// </summary>
        private IIniParserConfiguration _configuration;
        #endregion
    }
} 