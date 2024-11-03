using System.Text;

namespace Library
{
    public class GenerateRandomCode
    {
        public static string RandomString(int size, bool lowerCase = false)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string RandomNumber(int size)
        {
            Random random = new Random();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < size; i++)
                s.Append(random.Next(10).ToString());
            return s.ToString();
        }

        public static string RandomCode(int size)
        {
            string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S",
                        "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rnd = new Random();
            StringBuilder random = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                random.Append(chars[rnd.Next(0, 33)]);
            }
            return random.ToString();
        }

        #region barcode
        public static string GenerateRandomEAN(string contryCode, int length)
        {
            var seri = $"{contryCode}{RandomNumber(length - 4)}";
            var sum13 = 0;
            for (int i = 0; i < length - 1; i++)
            {
                int digit = int.Parse(seri[i].ToString());

                // Vị trí lẻ (0, 2, 4, ...) nhân với 1, vị trí chẵn nhân với 3
                if (i % 2 == 0) sum13 += digit;
                else sum13 += digit * 3;
            }
            var checkDigit13 = (10 - (sum13 % 10)) % 10;
            seri += checkDigit13;
            return seri;
        }

        public static string GenerateRandomCode128(char set, int length)
        {
            char[] CodeSetCharacters = new char[length];
            switch(set)
            {
                case 'A': CodeSetCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray(); break;
                case 'B': CodeSetCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 !\"#$%&'()*+,-./:;<=>?@[\\]^_".ToCharArray(); break;
                case 'C':
                    CodeSetCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
                    if (length % 2 == 1) length += 1;
                    break;
            }    

            if (length <= 0) throw new ArgumentException("Length must be greater than 0.");

            var random = new Random();
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                // Chọn ngẫu nhiên ký tự từ Code Set B
                char randomChar = CodeSetCharacters[random.Next(CodeSetCharacters.Length)];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }

        public static string GenerateRandomUPCA()
        {
            var random = new Random();
            var stringBuilder = new StringBuilder();

            // Tạo 11 chữ số đầu tiên ngẫu nhiên
            for (int i = 0; i < 11; i++)
            {
                stringBuilder.Append(random.Next(0, 10)); // Thêm số từ 0 đến 9
            }

            string upcWithoutCheckDigit = stringBuilder.ToString();

            // Hàm tính toán Check Digit cho mã UPC-A
            int sumOdd = 0;
            int sumEven = 0;

            // Duyệt qua 11 chữ số đầu tiên để tính tổng
            for (int i = 0; i < upcWithoutCheckDigit.Length; i++)
            {
                int digit = int.Parse(upcWithoutCheckDigit[i].ToString());

                if (i % 2 == 0)
                {
                    sumOdd += digit; // Vị trí lẻ (bắt đầu từ 0)
                }
                else
                {
                    sumEven += digit; // Vị trí chẵn
                }
            }

            // Tính tổng theo công thức UPC-A
            int total = (sumOdd * 3) + sumEven;

            // Tính Check Digit sao cho tổng là bội số của 10
            int checkDigit = (10 - (total % 10)) % 10;

            return upcWithoutCheckDigit + checkDigit;
        }
        #endregion
    }
}
