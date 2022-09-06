namespace common_mapping.Utils.MaskedString
{
    /// <summary>
    /// Класс для декомпозиции строки с маской, проверкой другой строки на совпадение с маской, конвертирование параметров в строчку
    /// </summary>
    internal class MaskedData
    {
        public IEnumerable<MaskedItem> Pieces { get; }
        /// <summary>
        /// Инициализация строки с маской
        /// </summary>
        /// <param name="value"></param>
        /// <example>Let's make some {0}!!!</example>
        /// <exception cref="Exception"></exception>
        public MaskedData(string value)
        {
            List<MaskedItem> pieces = new();
            var piecesSource = value.Split("{");
            foreach (var item in piecesSource)
            {
                if (item.Contains("}"))
                {
                    var temp = item.Split("}");
                    if (temp.Length == 2)
                    {
                        if (temp[0].Length > 0)
                        {
                            if (pieces.Last()?.Type == MaskedItemType.Param)
                            {
                                var exception = "{" + pieces.Last().Value + "}{" + temp[0] + "}";
                                throw new Exception($"Two parameters one after the other: {exception}");
                            }
                            pieces.Add(new MaskedItem() { Value = temp[0], Type = MaskedItemType.Param });
                        }
                        if (temp[1].Length > 0)
                            pieces.Add(new MaskedItem() { Value = temp[1], Type = MaskedItemType.Str });
                    }
                    else
                        if (temp.Length > 0)
                        pieces.Add(new MaskedItem() { Value = temp[0], Type = MaskedItemType.Str });
                }
                else
                    pieces.Add(new MaskedItem() { Value = item, Type = MaskedItemType.Str });
            }
            Pieces = pieces;
        }

        /// <summary>
        /// Метод проверки на совпадение входящей строки со строкой с маской
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsLike(string value)
        {
            if (!Pieces.Where(x => x.Type == MaskedItemType.Str).Any())
                throw new ArgumentException("Decomposition pieces is empty");

            int oldIndex = 0;
            foreach (var piece in Pieces.Where(x => x.Type == MaskedItemType.Str))
            {
                if (piece.Value.Length == 0)
                    continue;
                if (!value.Contains(piece.Value))
                    return false;

                var index = value.IndexOf(piece.Value);
                if (index < oldIndex)
                    return false;

                oldIndex = index;
            }
            return true;
        }

        /// <summary>
        /// Декомпозирует входящую строчку по параметрам в основной строки с маской
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Справочник параметра строчки с маской на значение из входящей строчки</returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, string> Decompose(string value)
        {
            if (!Pieces.Any())
                throw new ArgumentException("Decomposition pieces is empty");

            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> param = new List<string>();
            var pieces = Pieces.ToList();

            // работаем не через foreach, т.к. требуется работа со "следующим" элементом массива
            for (int i = 0; i < pieces.Count(); i++)
            {
                var piece = pieces[i];
                // если вдруг пустая строчка, то ее разбирать смысла нет
                if (piece.Value.Length == 0)
                    continue;

                // если "кусок" текстовый, то его нужно отделить от текста
                if (piece.Type == MaskedItemType.Str)
                {
                    var index = value.IndexOf(piece.Value);

                    if (index < 0)
                        continue;
                    value = value.Remove(0, index);                        

                    if (value.Length >= piece.Value.Length)
                        value = value.Remove(0, piece.Value.Length);
                    else
                        value = value.Remove(0, value.Length);
                }
                // если "кусок" параметр, то отделяем значение для параметра на основе следующего элемента списка
                else
                {
                    int index;
                    if (i + 1 < pieces.Count())
                        index = value.IndexOf(pieces[i + 1].Value);
                    else
                        index = value.Length;

                    if (index > 0)
                    {
                        param.Add(value.Substring(0, index));
                        value = value.Remove(0, index);
                    }
                }
            }

            // собираем готовую текстовку с заполненными параметрами
            var j = 0;
            foreach (var item in Pieces.Where(x => x.Type == MaskedItemType.Param))
            {
                if (j < param.Count)
                {
                    result.Add(item.Value, param[j]);
                    j++;
                }
                else
                    result.Add(item.Value, "");
            }

            return result;
        }

        public override string ToString()
        {
            return String.Join("", Pieces.Select(x => x.Value));
        }

        /// <summary>
        /// Конвертирование основной строки с маской данными по параметрам
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string Convert(Dictionary<string, string> param = null)
        {
            string result = "";
            foreach (var piece in Pieces)
            {
                if (piece.Type == MaskedItemType.Str)
                    result += piece.Value;
                else
                {
                    var temp = param.FirstOrDefault(x => x.Key == piece.Value);
                    if (temp.Key == "")
                        continue;
                    result += temp.Value;
                }
            }
            return result;
        }
    }
}
