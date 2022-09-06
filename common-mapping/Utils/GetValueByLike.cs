using common_mapping.Enums;
using common_mapping.Interfaces;
using common_mapping.Utils.MaskedString;

namespace common_mapping.Utils
{
    internal static class GetValueByLike
    {
        /// <summary>
        /// Поиск по списку данных маппинга по совпадению со входящей строкой поиска
        /// </summary>
        /// <param name="items">Список данных маппинга</param>
        /// <param name="searchText">Строка поиска</param>
        /// <param name="fieldForSearch">По какому значению происходит поиск</param>
        /// <returns>Набор подходящих строк с заменой данных по маске, если такая строка будет найдена</returns>
        public static IEnumerable<string> GetLike(IEnumerable<IMapItem> items, string searchText, MapFields fieldForSearch = MapFields.Source)
        {
            List<string> result = new List<string>();
            foreach (var item in items)
            {
                var firstValue = fieldForSearch == MapFields.Source ? item.SourceValue : item.TargetValue;
                var secondValue = fieldForSearch == MapFields.Source ? item.TargetValue : item.SourceValue;
                if (firstValue.Contains("{"))
                {
                    var source = new MaskedData(firstValue);
                    var isLike = source.IsLike(searchText);
                    if (isLike)
                    {
                        var temp = source.Decompose(searchText);
                        var target = new MaskedData(secondValue);
                        result.Add(target.Convert(temp));
                    }
                }
                else
                    if (firstValue == searchText)
                    result.Add(secondValue);
            }
            return result;
        }
    }
}
