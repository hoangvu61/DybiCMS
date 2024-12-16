namespace Dybi.Library.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Hàm chuyển chuỗi có dấu thành không dấu
        /// </summary>
        /// <param name="text"> chuỗi cần chuyển</param>
        /// <returns>chuỗi không dấu</returns>
        public static string ConvertToUnSign(string text)
        {
            if (text == null) return string.Empty;

            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            var regex = new System.Text.RegularExpressions.Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
