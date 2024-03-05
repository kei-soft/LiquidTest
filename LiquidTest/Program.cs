using System.Text.RegularExpressions;

using Fluid;

namespace LiquidTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new FluidParser();

            var model = new { Firstname = "Bill", Lastname = "Gates", Age = 30, Address = "USA" };

            var source = @"
SELECT * 
FROM Customer
WHERE 1=1
AND Name = '{{Firstname}} {{Lastname}}'
{% if Age > 18 %}
AND AgeType ='adult'
{% elsif Age > 12 %}
AND AgeType ='teenager'
{% else %}
AND AgeType ='child'
{% endif %}
{% if Address != ''%}
AND Address = '{{Address}}'
{% endif %}
";

            if (parser.TryParse(source, out var template, out var error))
            {
                var context = new TemplateContext(model);

                string result = template.Render(context);

                // 빈줄 제거
                result = Regex.Replace(result, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Error: {error}");
            }

            Console.Read();
        }
    }
}
