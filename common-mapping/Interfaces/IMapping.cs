using common_mapping.Enums;

namespace common_mapping.Interfaces
{
    /// <summary>
    /// Main controller of mapping
    /// </summary>
    public interface IMapping
    {
        public IEnumerable<IMapItem> Get(string linkCode);
        /// <summary>
        /// Method for search all mapped data
        /// </summary>
        /// <param name="linkCode"></param>
        /// <param name="searchValue"></param>
        /// <param name="type">Search by field</param>
        /// <returns></returns>
        public IEnumerable<IMapItem> Get(string linkCode, string searchValue, MapFields type = MapFields.Source);
        /// <summary>
        /// Method for search all mapped data with mask
        /// </summary>
        /// <example>Some search text => Some {0} text</example>
        /// <param name="linkId"></param>
        /// <param name="searchValue"></param>
        /// <param name="type">Search by field</param>
        /// <returns></returns>
        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields type = MapFields.Source);
        /// <summary>
        /// Method for get all links
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetLinks();
        public void Insert(IMapItem value);
        public void Insert(string linkCode, string sourceValue, string targetValue);
        public void Delete(IMapItem value);
        public void Delete(string linkCode, string sourceValue);
    }
}
